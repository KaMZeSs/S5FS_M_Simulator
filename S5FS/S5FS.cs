using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace S5FS
{
    /// <summary>
    /// Основной клас для взаимодействия с файловой системой.
    /// Файловая система: Суперблок (512); ilist (хз); бит карта инодов (хз); бит карта блоков (хз)
    /// Максимальный размер системы зависит от кластера
    /// </summary>
    public class S5FS
    {
        /// <summary>
        /// Суперблок
        /// </summary>
        SuperBlock sb;
        /// <summary>
        /// Битовая карта инодов
        /// </summary>
        BitMap bm_inode;
        /// <summary>
        /// Битовая карта блоков данных
        /// </summary>
        BitMap bm_block;
        /// <summary>
        /// Файловый поток для записи/считывания данных
        /// </summary>
        FileStream fs;

        BitMap bm_block_temp;
        BitMap bm_inode_temp;

        SuperBlock sb_temp;


        public UInt32 max_file_size { get; private set; }

        /// <summary>
        /// Позиция самого первого блока относительно начала файла
        /// </summary>
        UInt32 blocks_offset;

        UInt16 curr_user_id;
        UInt16 curr_group_id;

        /// <summary>
        /// Количество сохраняемых в блоке адресов
        /// </summary>
        UInt32 addr_in_block;

        private S5FS() { curr_user_id = curr_group_id = 0; }

        /// <summary>
        /// Стандартный метод записи/обновления битовой карты.
        /// </summary>
        /// <param name="map"></param>
        private void WriteBitMap(BitMap map)
        {
            var slicer = Helper.Slicer(map.map, this.sb.s_blen).GetEnumerator();
            for (UInt32 i = map.start_block; slicer.MoveNext(); i++)
            {
                this.WriteToDataBlock(slicer.Current, i);
            }
            if (map.start_block is 0)
            {
                bm_inode_temp = (BitMap)map.Clone();
            }
            else
            {
                bm_block_temp = (BitMap)map.Clone();
            }
        }

        /// <summary>
        /// Стандартный метод записи информации в блок данных. Нумерация с 0.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="num"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void WriteToDataBlock(byte[] bytes, UInt32 num)
        {
            if (bytes.LongLength != this.sb.s_blen)
            {
                throw new ArgumentOutOfRangeException();
            }
            UInt64 currBlock_pos = this.blocks_offset + (UInt64)num * this.sb.s_blen;
            //Записываем в блок
            this.Seek(currBlock_pos, SeekOrigin.Begin);
            fs.Write(bytes);
            fs.Flush();
        }

        /// <summary>
        /// Перемещение по файловому потоку. Нужен, тк стандартный метод принимат максимум long.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="seekOrigin"></param>
        /// <exception cref="Exception"></exception>
        private void Seek(UInt64 num, SeekOrigin seekOrigin)
        {
            if (seekOrigin is SeekOrigin.End)
            {
                throw new Exception("Мы так не работаем");
            }
            fs.Seek(0, seekOrigin);
            while (num > Int64.MaxValue)
            {
                num -= Int64.MaxValue;
                fs.Seek(Int64.MaxValue, SeekOrigin.Current);
            }
            fs.Seek((Int64)num, SeekOrigin.Current);
        }

        /// <summary>
        /// Стандартный метод считывания информации из блока данных. Нумерация с 0.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private byte[] ReadFromDataBlock(UInt32 num)
        {
            byte[] bytes = new byte[(int)this.sb.s_blen];

            UInt32 currBlock_pos = (UInt32)this.blocks_offset + num * this.sb.s_blen;
            this.Seek(currBlock_pos, SeekOrigin.Begin);
            fs.Read(bytes, 0, bytes.Length);

            return bytes;
        }

        /// <summary>
        /// Считать i-й инод
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Inode ReadInode(UInt32 num)
        {
            UInt32 position = SuperBlock.superblock_size + num * Inode.inode_size;
            this.Seek(position, SeekOrigin.Begin);
            var bytes = new byte[Inode.inode_size];
            fs.Read(bytes, 0, Inode.inode_size);
            return Inode.LoadFromByteArray(bytes, num);
        }

        /// <summary>
        /// Перезаписать i-й инод
        /// </summary>
        /// <param name="inode"></param>
        /// <param name="num"></param>
        public void WriteInode(Inode inode)
        {
            UInt32 position = SuperBlock.superblock_size + inode.index * Inode.inode_size;
            byte[] bytes = Inode.SaveToByteArray(inode);
            this.Seek(position, SeekOrigin.Begin);
            fs.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Создание корневого каталога при форматировании
        /// </summary>
        private void CreateRootFolder()
        {
            var inode = new Inode(0)
            {
                di_mode = 0b_01_00_00_0_000_000_000, // Папка, остальное по нулям
                di_nlinks = 1,
                di_uid = 0,
                di_gid = 0,
                di_size = 0, // Нет, но пока да
                di_atime = 0, // Текущее
                di_mtime = 0, // Текущее
                di_ctime = 0 // Текущее
            };
            UInt32 block_num = this.bm_block.FirstEmpty();
            inode.di_addr[0] = block_num;
            inode.di_atime = inode.di_mtime = inode.di_ctime = DateTime.Now.Ticks;
            WriteInode(inode);
            //Блок не очищаю, тк в нашем случае все пусто
            
            //Изменим карты
            this.bm_inode.ChangeBlockState(0, false);
            this.WriteBitMap(bm_inode);
            this.bm_block.ChangeBlockState(block_num, false);
            this.WriteBitMap(bm_block);
        }
        
        /// <summary>
        /// Прочитать все данные с помощью инода
        /// </summary>
        /// <param name="inode"></param>
        /// <returns></returns>
        public byte[] ReadDataByInode(Inode inode)
        {
            UInt32 block_num = inode.di_size % this.sb.s_blen == 0 ? 
                inode.di_size / this.sb.s_blen :
                (inode.di_size / this.sb.s_blen + 1);
            if (block_num is 0) block_num = 1;

            var last_len = (int)(inode.di_size % this.sb.s_blen); // Скок байт в ласт блоке. Можно в int, тк размер блока макс 64кб
            if (inode.di_size is 0) last_len = 0;

            var result = new byte[inode.di_size];
            byte[] block = new byte[0];

            UInt32 block_counter = 0; // Общий счетчик блоков
            try
            {
                for (UInt32 i = 0; i < 10 && block_counter < block_num; i++, block_counter++) // Первые 10 адресов
                {
                    var addr = inode.di_addr[i]; //Номер i-го блока
                    block = this.ReadFromDataBlock(addr);
                    block.CopyTo(result, block_counter * this.sb.s_blen);
                }

                //Получить некст адреса
                block = this.ReadFromDataBlock(inode.di_addr[10]);
                var addresses_1 = this.GetAddressesFromBlock(block);

                for (long i = 0; i < addresses_1.LongLength && block_counter < block_num; i++, block_counter++) // Все адреса из блока 10
                {
                    var addr = addresses_1[i];
                    block = this.ReadFromDataBlock(addr);
                    block.CopyTo(result, block_counter * this.sb.s_blen);
                }
            }
            catch (Exception) // Если Exception - значит ласт блок не влез
            {
                Array.Resize(ref block, last_len);
                block.CopyTo(result, (long)(block_counter * this.sb.s_blen));
            }

            inode.di_atime = DateTime.Now.Ticks;
            this.WriteInode(inode);

            return result;
        }

        /// <summary>
        /// Получить список адресов из массива байтов - блока
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private UInt32[] GetAddressesFromBlock(byte[] bytes)
        {
            var result = new UInt32[bytes.Length / 4];
            var sliced = Helper.Slicer(bytes, 4).GetEnumerator();
            for (int i = 0; sliced.MoveNext(); i++)
            {
                result[i] = BitConverter.ToUInt32(sliced.Current, 0);
            }
            return result;
        }

        /// <summary>
        /// Получить массив байт для записи из массива адресов
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        private byte[] AddressesToBlock(UInt32[] addr)
        {
            byte[] result = new byte[this.sb.s_blen];
            for (int i = 0; i < addr.Length; i++)
            {
                var toWrite = BitConverter.GetBytes(addr[i]);
                Array.Copy(toWrite, 0, result, i * 4, 4);
            }
            return result;
        }

        /// <summary>
        /// Преобразовать массив байт - данные папки в массив данных типа id инода - имя файла
        /// Папка будет 64 байта - 8 на адрес инода, остальное - имя
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public KeyValuePair<UInt32, String>[] GetFilesFromFolderData(byte[] data)
        {
            var files = new List<KeyValuePair<UInt32, String>>();
            var splitted = Helper.Slicer(data, 64).GetEnumerator();
            while (splitted.MoveNext())
            {
                var id = BitConverter.ToUInt32(splitted.Current, 0);
                
                var name = Encoding.Unicode.GetString(splitted.Current, 4, 64 - 4);
                files.Add(new(id, name));
            }
            var existing_files = from file in files where file.Key != 0 select file; // онли существующие
            return existing_files.ToArray();
        }

        /// <summary>
        /// Преобразовать данные папки в массив байт
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private byte[] DictToFolderData(KeyValuePair<UInt32, String>[] files)
        {
            var bytes = new List<byte>();

            for (long i = 0; i < files.Count(); i++)
            {
                var id = BitConverter.GetBytes(files[i].Key);

                var str = Helper.StringExtender(files[i].Value, 30);

                var value = Encoding.Unicode.GetBytes(str);
                bytes.AddRange(id);
                bytes.AddRange(value);
            }

            return bytes.ToArray();
        }

        /// <summary>
        /// Метод добавления ссылки на файл в родительский каталог
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="newFile"></param>
        /// <param name="file_name"></param>
        public void AddFileLinkToDirectory(Inode folder, Inode newFile, String file_name)
        {
            var parent_inode_bytes = this.ReadDataByInode(folder);
            var files_in_parent_inode = GetFilesFromFolderData(parent_inode_bytes).ToList();
            files_in_parent_inode.Add(new(newFile.index, file_name));
            //Теперь надо перезаписать на диск
            parent_inode_bytes = this.DictToFolderData(files_in_parent_inode.ToArray());
            //Размер мог стать больше предыдущего к-ва кластеров
            this.WriteDataByInode(folder, parent_inode_bytes);
            
            newFile.di_nlinks++;
            this.WriteInode(newFile);
        }

        /// <summary>
        /// Найти последний инод в цепочке
        /// </summary>
        /// <param name="path"></param>
        public Inode GetInodeByPath(String[] path)
        {
            var last_inode = ReadInode(0);
            var inode_bytes = this.ReadDataByInode(last_inode);
            var files_in_last_inode = GetFilesFromFolderData(inode_bytes);

            foreach (var part in path)
            {
                var vs = part.Trim();
                var items = from fold in files_in_last_inode
                            where fold.Value.Trim() == part
                            select fold; // Может быть только одно имя, либо ничего
                if (items.Count() is 0)
                {
                    throw new Exception($"Папка @{part}@ не существует");
                }

                var item = items.First();
                // Проверка, что item - не файл
                last_inode = this.ReadInode(item.Key);
                var type = Inode.GetInodeType(last_inode);
                if (type is InodeTypeEnum.File)
                {
                    throw new Exception($"Папка @{part}@ не существует");
                }
                // Если дошли сюда, значит проверка на папку пройдена
                // Выполняем те же действия, что и перед циклом
                inode_bytes = this.ReadDataByInode(last_inode);
                files_in_last_inode = GetFilesFromFolderData(inode_bytes);
            }

            return last_inode;
        }

        private void WriteSuperBlock()
        {
            byte[] buffer = SuperBlock.SaveToByteArray(this.sb);
            this.fs.Seek(0, SeekOrigin.Begin);
            this.fs.Write(buffer);
            this.fs.Flush();
            sb_temp = (SuperBlock)this.sb.Clone();
        }

        /// <summary>
        /// Мб работающий метод удаления файла по иноду
        /// </summary>
        /// <param name="inode"></param>
        public void ReleaseBlocksByInode(Inode inode)
        {
            UInt32 block_num = inode.di_size % this.sb.s_blen == 0 ?
                inode.di_size / this.sb.s_blen :
                (inode.di_size / this.sb.s_blen + 1);

            for (UInt32 i = 0; i < 12; i++)
            {
                if (block_num is 0)
                {
                    break;
                }
                if (i < 10)
                {
                    this.bm_block.ChangeBlockState(inode.di_addr[i], true);
                    this.sb.s_tfree++;
                    inode.di_addr[i] = 0;
                    block_num--;
                }
                if (i is 10)
                {
                    if (block_num is 0)
                    {
                        break;
                    }
                    this.bm_block.ChangeBlockState(inode.di_addr[i], true);
                    this.sb.s_tfree++;
                    inode.di_addr[i] = 0;
                    var existing_addresses = this.GetAddressesFromBlock(this.ReadFromDataBlock(inode.di_addr[i]));
                    for (int j = 0; j < existing_addresses.Length; j++)
                    {
                        if (block_num is 0)
                        {
                            break;
                        }
                        this.bm_block.ChangeBlockState(existing_addresses[j], true);
                        this.sb.s_tfree++;
                        block_num--;
                    }
                }
                if (i is 11)
                {
                    if (block_num is 0)
                    {
                        break;
                    }
                    this.bm_block.ChangeBlockState(inode.di_addr[i], true);
                    this.sb.s_tfree++;
                    inode.di_addr[i] = 0;
                    var existing_addresses_1 = this.GetAddressesFromBlock(this.ReadFromDataBlock(inode.di_addr[i]));
                    for (int j = 0; j < existing_addresses_1.Length; j++)
                    {
                        if (block_num is 0)
                        {
                            break;
                        }
                        this.bm_block.ChangeBlockState(inode.di_addr[j], true);
                        this.sb.s_tfree++;
                        var existing_addresses_2 = this.GetAddressesFromBlock(this.ReadFromDataBlock(existing_addresses_1[j]));
                        for (int k = 0; k < existing_addresses_2.Length; k++)
                        {
                            if (block_num is 0)
                            {
                                break;
                            }
                            this.bm_block.ChangeBlockState(existing_addresses_2[k], true);
                            this.sb.s_tfree++;
                            block_num--;
                        }
                    }
                }
            }

            inode.di_addr = new UInt32[12];

            inode.di_mode = 0;
            this.WriteInode(inode);
            this.WriteBitMap(this.bm_inode);
            this.WriteBitMap(this.bm_block);
            this.WriteSuperBlock();
        }

        private void DeleteFileLinkFromDirectory(Inode parentFolder, Inode inode)
        {
            var parent_inode_bytes = this.ReadDataByInode(parentFolder);
            var files_in_parent_inode = GetFilesFromFolderData(parent_inode_bytes).ToList();
            
            for (int i = 0; i < files_in_parent_inode.Count; i++)
            {
                if (files_in_parent_inode[i].Key == inode.index)
                {
                    files_in_parent_inode.RemoveAt(i);
                    break;
                }
            }

            //Теперь надо перезаписать на диск
            parent_inode_bytes = this.DictToFolderData(files_in_parent_inode.ToArray());
            //Размер мог стать меньше предыдущего к-ва кластеров
            this.WriteDataByInode(parentFolder, parent_inode_bytes);

            inode.di_nlinks--;

            if (inode.di_nlinks is 0) // Если больше нет ссылок - очистить блоки
            {
                this.ReleaseBlocksByInode(inode);
            }

            this.WriteInode(inode);
        }

        /// <summary>
        /// Создание новой файловой системы.
        /// </summary>
        /// <param name="file">Путь к файлу.</param>
        /// <param name="s_blen">Размер блока.</param>
        /// <param name="disk_size">Размер диска.</param>
        /// <returns>Один экземпляр файловой системы.</returns>
        public static S5FS format(String file, UInt32 s_blen, UInt32 disk_size)
        {
            S5FS s5FS = new();
            s5FS.fs = new(file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            s5FS.fs.Seek(disk_size, SeekOrigin.Begin);
            s5FS.fs.Seek(0, SeekOrigin.Begin);

            //Суперблок
            s5FS.sb = new(disk_size: disk_size, s_blen: s_blen);
            byte[] buffer = SuperBlock.SaveToByteArray(s5FS.sb);
            s5FS.fs.Write(buffer);
            s5FS.fs.Flush();

            s5FS.max_file_size = SuperBlock.max_blocks_per_file * s5FS.sb.s_blen;

            //Скок в блок адресов влезет
            s5FS.addr_in_block = s5FS.sb.s_blen / 4;

            //Выделить место под индексные
            buffer = new byte[s5FS.sb.s_isize * Inode.inode_size];
            s5FS.fs.Write(buffer);
            s5FS.fs.Flush();

            s5FS.blocks_offset = (UInt32)buffer.LongLength + SuperBlock.superblock_size;

            //Создание битмапов
            UInt32 inode_bm_len = s5FS.sb.s_isize / 8;
            if (s5FS.sb.s_isize % 8 != 0)
            {
                inode_bm_len++;
            }
            s5FS.bm_inode = new(inode_bm_len, s5FS.sb.s_isize, 0);
            s5FS.WriteBitMap(s5FS.bm_inode);

            UInt32 blocks_bm_len = (s5FS.sb.s_fsize - 2) / 8;
            if (((s5FS.sb.s_fsize - 2) % 8) != 0)
            {
                blocks_bm_len++;
            }
            UInt32 blocks_bm_start = (UInt32)s5FS.bm_inode.map.LongLength / s5FS.sb.s_fsize;
            if ((UInt32)s5FS.bm_inode.map.LongLength % s5FS.sb.s_fsize != 0)
            {
                blocks_bm_start++;
            }
            s5FS.bm_block = new(blocks_bm_len, s5FS.sb.s_fsize - 2, blocks_bm_start);
            //Зная, где начинается и заканчиваются битмапы - заполняю битмап
            var temp = blocks_bm_len % s5FS.sb.s_blen == 0 ? blocks_bm_len / s5FS.sb.s_blen : (blocks_bm_len / s5FS.sb.s_blen + 1);
            UInt32 len = blocks_bm_start + temp;
            for (UInt32 i = 0; i < len; i++)
            {
                s5FS.bm_block.ChangeBlockState(i, false);
            }
            //Изменить в суперблоке к-во свободных блоков
            s5FS.sb.s_tfree -= len;

            var check_free_inodes = s5FS.bm_inode.FreeBlocks;
            var check_free_blocks = s5FS.bm_block.FreeBlocks;
            s5FS.sb.s_tfree = check_free_blocks;
            s5FS.sb.s_tinode = check_free_inodes;

            s5FS.WriteSuperBlock();
            s5FS.WriteBitMap(s5FS.bm_block);
            s5FS.CreateRootFolder();
            //На корневой каталог - 1 инод и блок
            s5FS.sb.s_tfree--;
            s5FS.sb.s_tinode--;

            return s5FS;
        }

        /// <summary>
        /// Считывание файловой системы из файла
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Объект файловой системы</returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static S5FS load_from_file(String file)
        {
            S5FS s5fs = new();
            if (!File.Exists(file))
            {
                throw new FileNotFoundException();
            }
            s5fs.fs = new(file, FileMode.Open, FileAccess.ReadWrite);

            var bytes = new byte[SuperBlock.superblock_size];

            //Суперблок
            s5fs.fs.Read(bytes, 0, bytes.Length);
            s5fs.sb = SuperBlock.LoadFromByteArray(bytes);

            s5fs.blocks_offset = s5fs.sb.s_isize * Inode.inode_size + SuperBlock.superblock_size;

            //Скок в блок адресов влезет
            s5fs.addr_in_block = s5fs.sb.s_blen / 4;

            //Подгрузить битмапы
            //Иноды
            UInt32 inode_bm_len = s5fs.sb.s_isize / 8;
            if (s5fs.sb.s_isize % 8 != 0)
            {
                inode_bm_len++;
            }
            UInt32 blocks_to_read = inode_bm_len / s5fs.sb.s_blen;
            if (inode_bm_len % s5fs.sb.s_blen != 0)
            {
                blocks_to_read++;
            }
            List<byte> readed_bytes = new();

            for (UInt32 i = 0; i < blocks_to_read; i++)
            {
                readed_bytes.AddRange(s5fs.ReadFromDataBlock(i));
            }

            bytes = new byte[inode_bm_len];
            Array.Copy(readed_bytes.ToArray(), bytes, bytes.LongLength);

            s5fs.bm_inode = new(bytes, inode_bm_len, 0);

            //Битовая карта блоков
            UInt32 bm_start = blocks_to_read;
            UInt32 blocks_bm_len = (s5fs.sb.s_fsize - 2) / 8;
            if ((s5fs.sb.s_fsize - 2) % 8 != 0)
            {
                blocks_bm_len++;
            }
            blocks_to_read = blocks_bm_len / s5fs.sb.s_blen;
            if (blocks_bm_len % s5fs.sb.s_blen != 0)
            {
                blocks_to_read++;
            }
            readed_bytes.Clear();

            for (UInt32 i = bm_start; i < bm_start + blocks_to_read; i++)
            {
                readed_bytes.AddRange(s5fs.ReadFromDataBlock(i));
            }
            bytes = new byte[blocks_bm_len];
            Array.Copy(readed_bytes.ToArray(), bytes, bytes.LongLength);

            s5fs.bm_block = new(bytes, blocks_bm_len, bm_start);

            return s5fs;
        }

        public struct InodeInfo
        {
            public Inode parent;
            public Inode daughter;
            public byte[]? data; 

            public InodeInfo(Inode parent, Inode daughter, byte[] data = null)
            {
                this.parent = parent;
                this.daughter = daughter;
                this.data = data;
            }
        }

        public InodeInfo CreateFile(Inode root, String name, bool isFolder = false)
        {
            name = Helper.StringExtender(name, 30);
            var root_data = this.ReadDataByInode(root);
            var files_in_root = this.GetFilesFromFolderData(root_data);

            var check = from fold in files_in_root
                        where fold.Value == name
                        select fold;
            if (check.Count() is not 0)
            {
                throw new Exception($"В данной папке уже имеется файл/папка с именем {name.Trim()}");
            }
            //Получение номера свободного Inode
            if (this.sb.s_tinode == 0)
            {
                throw new Exception();
            }
            var inode_num = this.bm_inode.FirstEmpty();
            this.sb.s_tinode--;
            //Получение номера свободного блока
            if (this.sb.s_tfree == 0)
            {
                throw new Exception();
            }
            var block_num = this.bm_block.FirstEmpty();
            this.sb.s_tfree--;

            //Если дошли сюда, значит есть свободные блоки/инод
            long time = DateTime.Now.Ticks;

            ushort di_mode = (ushort)(isFolder ? 0b01_111_101_100_00000 : 0b10_111_101_100_00000); // Надо бы еще и пользователей/группы прикрутить

            var inode = new Inode(inode_num)
            {
                di_mode = di_mode,
                di_nlinks = 0,
                di_uid = curr_user_id,
                di_gid = curr_group_id,
                di_size = 0,
                di_atime = time,
                di_mtime = time,
                di_ctime = time,
            };
            inode.di_addr[0] = block_num;

            WriteInode(inode);

            //Изменим карты
            this.bm_inode.ChangeBlockState(inode.index, false);
            this.WriteBitMap(bm_inode);
            this.bm_block.ChangeBlockState(block_num, false);
            this.WriteBitMap(bm_block);

            //Запишем новый суперблок
            this.WriteSuperBlock();

            //Нужно записать фул пустой блок, чтобы мусора не было
            this.WriteToDataBlock(new byte[this.sb.s_blen], block_num);

            //Запишем инфу о папке в родительскую папку
            this.AddFileLinkToDirectory(root, inode, name);

            return new(root, inode);
        }

        /// <summary>
        /// Метод открытия файла.
        /// По существу, передавая массив байт, можно открывать файлы и папки, обрабатывая в соответствующих класах
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public InodeInfo OpenFile(String path, String name)
        {
            Inode inode;
            if (path.Length is 0)
            {
                inode = ReadInode(0);
            }
            else
            {
                inode = this.GetInodeByPath(path.Split("\\"));
            }

            var str = Helper.StringExtender(name, 30);
                
            var inode_bytes = this.ReadDataByInode(inode);
            var files_in_last_inode = GetFilesFromFolderData(inode_bytes);

            var check = from fold in files_in_last_inode
                        where fold.Value == str
                        select fold;
            if (check.Count() is 0)
            {
                throw new Exception($"В данной папке нет файла с именем {name}");
            }
            // если тут, то в check - inode файла
            var file_to_open = check.First();
            var inode_to_open = this.ReadInode(file_to_open.Key);
            var data = ReadDataByInode(inode_to_open);
            return new(inode, inode_to_open, data);
        }

        public KeyValuePair<String, InodeInfo>[] OpenFolder(InodeInfo info)
        {
            List<KeyValuePair<String, InodeInfo>> res = new();

            var files = this.GetFilesFromFolderData(info.data);

            foreach (var file in files)
            {
                var inode = this.ReadInode(file.Key);
                res.Add(new(file.Value, new InodeInfo(info.daughter, inode)));
            }

            return res.ToArray();
        }

        public KeyValuePair<String, InodeInfo>[] OpenRootFolder()
        {
            List<KeyValuePair<String, InodeInfo>> res = new();

            var root = this.ReadInode(0);
            var data = this.ReadDataByInode(root);

            var files = this.GetFilesFromFolderData(data);

            foreach (var file in files)
            {
                var inode = this.ReadInode(file.Key);
                res.Add(new(file.Value, new InodeInfo(root, inode)));
            }

            return res.ToArray();
        }

        public InodeInfo OpenFile(Inode parent, String name)
        {
            var str = Helper.StringExtender(name, 30);

            var inode_bytes = this.ReadDataByInode(parent);
            var files_in_last_inode = GetFilesFromFolderData(inode_bytes);

            var check = from fold in files_in_last_inode
                        where fold.Value == str
                        select fold;
            if (check.Count() is 0)
            {
                throw new Exception($"В данной папке нет файла с именем {name}");
            }
            // если тут, то в check - inode файла
            var file_to_open = check.First();
            var inode_to_open = this.ReadInode(file_to_open.Key);
            var data = ReadDataByInode(inode_to_open);
            return new(parent, inode_to_open, data);
        }

        public UInt32 GetAddressesBlocksNeed(UInt32 block_num)
        {
            UInt32 result = 0;
            if (block_num <= 10)
            {
                return 0;
            }
            else if (block_num <= 10 + this.addr_in_block)
            {
                return 1;
            }
            else
            {
                var temp = block_num - 10 - this.addr_in_block;
                var temp_res = temp / this.addr_in_block;
                if (temp % this.addr_in_block is not 0)
                {
                    temp_res++;
                }

                return 1 + 1 + temp_res;
            }
        }

        /// <summary>
        /// Возможно рабочий метод записи/перезаписи/добавления инфы по иноду
        /// </summary>
        /// <param name="inode"></param>
        /// <param name="newData"></param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="OutOfMemoryException"></exception>
        public void WriteDataByInode(Inode inode, byte[] newData)
        {
            UInt32 block_num = inode.di_size % this.sb.s_blen == 0 ?
                inode.di_size / this.sb.s_blen :
                (inode.di_size / this.sb.s_blen + 1);
            if (block_num is 0) block_num = 1;
            UInt32 new_block_num = (UInt32)(newData.LongLength % this.sb.s_blen == 0 ?
                newData.LongLength / this.sb.s_blen :
                (newData.LongLength / this.sb.s_blen + 1));

            if (new_block_num > SuperBlock.max_blocks_per_file)
            {
                throw new OutOfMemoryException();
            }

            UInt32 prev_addresses_for_blocks = GetAddressesBlocksNeed(block_num);
            UInt32 new_addresses_for_blocks = GetAddressesBlocksNeed(new_block_num);

            //После этого можно считать, что памяти хватит
            if (new_block_num > block_num)
            {
                var temp = new_block_num - block_num + new_addresses_for_blocks - prev_addresses_for_blocks;
                if (this.sb.s_tfree < temp)
                {
                    throw new OutOfMemoryException();
                }
            } //Проверка на то, чтобы хватило

            var data_to_write = Helper.Slicer(newData, this.sb.s_blen).GetEnumerator();
            data_to_write.MoveNext();
            if (block_num == new_block_num) 
            {
                var blocks_to_write = new_block_num;
                // Иду по номерам в массиве адресов в иноде
                for (UInt64 i = 0; ; i++)
                {
                    if (blocks_to_write is 0)
                    {
                        break;
                    }

                    if (i < 10)
                    {
                        if (blocks_to_write is 0)
                        {
                            break;
                        }

                        this.WriteToDataBlock(data_to_write.Current, inode.di_addr[i]);

                        data_to_write.MoveNext();
                        blocks_to_write--;
                    }
                    else if (i is 10) // 1й уровень. Блоки те же.
                    {
                        var existing_addresses = this.GetAddressesFromBlock(this.ReadFromDataBlock(inode.di_addr[i]))
                            .Where(x => x is not 0).GetEnumerator();

                        while (existing_addresses.MoveNext())
                        {
                            if (blocks_to_write is 0)
                            {
                                break;
                            }

                            this.WriteToDataBlock(data_to_write.Current, existing_addresses.Current);

                            data_to_write.MoveNext();
                            blocks_to_write--;
                        } // Просто считал адресы. Записал существующие. Если закончились блоки (я хз как, но а вдруг) - вышел раньше.
                    }
                    else if (i is 11) // 2й уровень. Блоки те же
                    {
                        var existing_addresses_1st = this.GetAddressesFromBlock(this.ReadFromDataBlock(inode.di_addr[i]))
                            .Where(x => x is not 0).GetEnumerator();

                        while (existing_addresses_1st.MoveNext())
                        {
                            if (blocks_to_write is 0)
                            {
                                break;
                            }
                            var existing_addresses_2nd = this.GetAddressesFromBlock(this.ReadFromDataBlock(existing_addresses_1st.Current))
                                .Where(x => x is not 0).GetEnumerator();

                            while (existing_addresses_2nd.MoveNext())
                            {
                                if (blocks_to_write is 0)
                                {
                                    break;
                                }
                                
                                this.WriteToDataBlock(data_to_write.Current, existing_addresses_2nd.Current);
                                
                                data_to_write.MoveNext();
                                blocks_to_write--;
                            }
                        }
                    }
                    else
                    {
                        bm_block = (BitMap)bm_block_temp.Clone();
                        bm_inode = (BitMap)bm_inode_temp.Clone();
                        sb = (SuperBlock)sb_temp.Clone();
                        throw new OutOfMemoryException("Карамба");
                    }
                }
            } // К-во занимаемых блоков не изменилось
            else if (block_num < new_block_num) // Здесь все аналогично, но создать булеву, чтобы когда нужно еще - выделяли
            {
                var blocks_to_write = new_block_num;
                var existing_blocks = block_num;

                // Иду по номерам в массиве адресов в иноде
                for (UInt64 i = 0; ; i++)
                {
                    if (blocks_to_write <= 0)
                    {
                        break;
                    }

                    if (i < 10)
                    {
                        if (blocks_to_write is 0)
                        {
                            break;
                        }
                        if (existing_blocks is 0) // Больше некуда
                        {
                            var newBlock = this.bm_block.FirstEmpty();
                            inode.di_addr[i] = newBlock;
                            this.WriteToDataBlock(data_to_write.Current, newBlock);
                            this.sb.s_tfree--;
                        }
                        else // Еще есть куда записывать
                        {
                            this.WriteToDataBlock(data_to_write.Current, inode.di_addr[i]);
                            existing_blocks--;
                        }

                        data_to_write.MoveNext();
                        blocks_to_write--;
                    }
                    else if (i is 10) // 1й уровень. Блоки те же.
                    {
                        UInt32[] existing_addresses;
                        if (existing_blocks is 0) // Создаем блок для 1го уровня
                        {
                            inode.di_addr[i] = this.bm_block.FirstEmpty();
                            existing_addresses = new UInt32[addr_in_block];
                            this.sb.s_tfree--;
                        }
                        else
                        {
                            existing_addresses = this.GetAddressesFromBlock(this.ReadFromDataBlock(inode.di_addr[i]));
                        }

                        for (int j = 0; j < existing_addresses.Length; j++)
                        {
                            if (blocks_to_write is 0)
                            {
                                break;
                            }
                            if (existing_blocks is 0) // Больше некуда
                            {
                                var newBlock = this.bm_block.FirstEmpty();
                                existing_addresses[j] = newBlock;
                                this.WriteToDataBlock(data_to_write.Current, newBlock);
                                this.sb.s_tfree--;
                            }
                            else // Еще есть куда записывать
                            {
                                this.WriteToDataBlock(data_to_write.Current, existing_addresses[j]);
                                existing_blocks--;
                            }

                            data_to_write.MoveNext();
                            blocks_to_write--;
                        }
                        this.WriteToDataBlock(this.AddressesToBlock(existing_addresses), inode.di_addr[i]);
                    }
                    else if (i is 11)
                    {
                        UInt32[] existing_addresses_1st;
                        if (existing_blocks is 0) // Создаем блок для 1го уровня
                        {
                            inode.di_addr[i] = this.bm_block.FirstEmpty();
                            existing_addresses_1st = new UInt32[addr_in_block];
                            this.sb.s_tfree--;
                        }
                        else
                        {
                            existing_addresses_1st = this.GetAddressesFromBlock(this.ReadFromDataBlock(inode.di_addr[i]));
                        }
                        

                        for (int j = 0; j < existing_addresses_1st.Length; j++)
                        {
                            if (blocks_to_write is 0)
                            {
                                break;
                            }
                            UInt32[] existing_addresses_2nd;
                            if (existing_blocks is 0) // Добавляем блок 2го уровня
                            {
                                existing_addresses_1st[j] = this.bm_block.FirstEmpty();
                                existing_addresses_2nd = new UInt32[addr_in_block];
                                this.sb.s_tfree--;
                            }
                            else
                            {
                                existing_addresses_2nd = this.GetAddressesFromBlock(this.ReadFromDataBlock(existing_addresses_1st[j]));
                            }                            

                            for (int k = 0; k < existing_addresses_2nd.Length; k++)
                            {
                                if (blocks_to_write is 0)
                                {
                                    break;
                                }
                                if (existing_blocks is 0) // Больше некуда
                                {
                                    var newBlock = this.bm_block.FirstEmpty();
                                    existing_addresses_2nd[k] = newBlock;
                                    this.WriteToDataBlock(data_to_write.Current, newBlock);
                                    this.sb.s_tfree--;
                                }
                                else // Еще есть куда записывать
                                {
                                    this.WriteToDataBlock(data_to_write.Current, existing_addresses_2nd[k]);
                                    existing_blocks--;
                                }

                                data_to_write.MoveNext();
                                blocks_to_write--;
                            }
                            this.WriteToDataBlock(this.AddressesToBlock(existing_addresses_2nd), existing_addresses_1st[j]);
                        }
                        this.WriteToDataBlock(this.AddressesToBlock(existing_addresses_1st), inode.di_addr[i]);
                    }
                    else
                    {
                        bm_block = (BitMap)bm_block_temp.Clone();
                        bm_inode = (BitMap)bm_inode_temp.Clone();
                        sb = (SuperBlock)sb_temp.Clone();
                        throw new OutOfMemoryException("Карамба");
                    }
                }
            } // К-во блоков стало больше
            else 
            {
                var blocks_to_write = new_block_num;
                // Иду по номерам в массиве адресов в иноде
                for (UInt64 i = 0; ; i++)
                {
                    if (i < 10)
                    {
                        if (blocks_to_write is not 0)
                        {
                            this.WriteToDataBlock(data_to_write.Current, inode.di_addr[i]);
                            blocks_to_write--;
                        }
                        else
                        {
                            if (inode.di_addr[i] is 0) // Дальше уже не будет
                            {
                                break;
                            }
                            this.bm_block.ChangeBlockState(inode.di_addr[i], true);
                            inode.di_addr[i] = 0;
                            this.sb.s_tfree++;
                        }

                        data_to_write.MoveNext();
                    }
                    else if (i is 10) // 1й уровень. Блоки те же.
                    {
                        if (inode.di_addr[i] is 0) // Дальше уже не будет
                        {
                            break;
                        }
                        var addresses = this.GetAddressesFromBlock(this.ReadFromDataBlock(inode.di_addr[i]));
                        
                        for (int j = 0; j < addresses.Length; j++)
                        {
                            if (blocks_to_write is not 0)
                            {
                                this.WriteToDataBlock(data_to_write.Current, addresses[j]);
                                blocks_to_write--;
                            }
                            else
                            {
                                if (addresses[j] is 0) // Дальше уже не будет
                                {
                                    break;
                                }
                                this.bm_block.ChangeBlockState(addresses[j], true);
                                addresses[j] = 0;
                                this.sb.s_tfree++;
                            }

                            data_to_write.MoveNext();
                        }

                        this.WriteToDataBlock(this.AddressesToBlock(addresses), inode.di_addr[i]);

                        if (addresses.Count(x => x is not 0) is 0) // Если уровень удален полностью (только 0)
                        {
                            this.bm_block.ChangeBlockState(inode.di_addr[i], true);
                            inode.di_addr[i] = 0;
                            this.sb.s_tfree++;
                        }
                    }
                    else if (i is 11)
                    {
                        if (inode.di_addr[i] is 0) // Дальше уже не будет
                        {
                            break;
                        }

                        var addresses_1st = this.GetAddressesFromBlock(this.ReadFromDataBlock(inode.di_addr[i]));

                        for (int j = 0; j < addresses_1st.Length; j++) // Берем адреса из 1го уровня
                        {
                            if (addresses_1st[j] is 0) // Дальше уже не будет
                            {
                                break;
                            }

                            var addresses_2nd = this.GetAddressesFromBlock(this.ReadFromDataBlock(addresses_1st[j])); // Блок адресов 2го уровня

                            for (int k = 0; k < addresses_2nd.Length; k++)
                            {
                                if (blocks_to_write is not 0)
                                {
                                    this.WriteToDataBlock(data_to_write.Current, addresses_2nd[k]);
                                    blocks_to_write--;
                                }
                                else
                                {
                                    if (addresses_2nd[k] is 0) // Дальше уже не будет
                                    {
                                        break;
                                    }
                                    this.bm_block.ChangeBlockState(addresses_2nd[k], true);
                                    addresses_2nd[k] = 0;
                                    this.sb.s_tfree++;
                                }

                                data_to_write.MoveNext();
                            }

                            this.WriteToDataBlock(this.AddressesToBlock(addresses_2nd), addresses_1st[j]);

                            if (addresses_2nd.Count(x => x is not 0) is 0) // Если уровень удален полностью (только 0)
                            {
                                this.bm_block.ChangeBlockState(addresses_1st[j], true);
                                addresses_1st[j] = 0;
                                this.sb.s_tfree++;
                            }
                        }

                        this.WriteToDataBlock(this.AddressesToBlock(addresses_1st), inode.di_addr[i]);

                        if (addresses_1st.Count(x => x is not 0) is 0) // Если уровень удален полностью (только 0)
                        {
                            this.bm_block.ChangeBlockState(inode.di_addr[i], true);
                            inode.di_addr[i] = 0;
                            this.sb.s_tfree++;
                        }
                    }
                    else if(blocks_to_write is not 0)
                    {
                        
                        bm_block = (BitMap)bm_block_temp.Clone();
                        bm_inode = (BitMap)bm_inode_temp.Clone();
                        sb = (SuperBlock)sb_temp.Clone();
                        throw new OutOfMemoryException("Карамба");
                    }
                    else
                    {
                        break;
                    }
                }
            } // К-во блоков стало меньше

            //Надо записать на диск карты и инод

            inode.di_size = (uint)newData.Length;
            inode.di_atime = DateTime.Now.Ticks;
            inode.di_mtime = DateTime.Now.Ticks;

            this.WriteInode(inode);
            this.WriteBitMap(this.bm_inode);
            this.WriteBitMap(this.bm_block);
            this.WriteSuperBlock();
        }
    }
}
