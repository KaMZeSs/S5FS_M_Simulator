using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    /// <summary>
    /// Основной клас для взаимодействия с файловой системой.
    /// Файловая система: Суперблок (512); ilist (хз); бит карта инодов (хз); бит карта блоков (хз)
    /// </summary>
    internal class S5FS
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

        /// <summary>
        /// Открытый в данный момент inode
        /// </summary>
        Inode inode;

        /// <summary>
        /// Позиция самого первого блока относительно начала файла
        /// </summary>
        UInt64 blocks_offset;

        private S5FS() { }

        /// <summary>
        /// Создание новой файловой системы.
        /// </summary>
        /// <param name="file">Путь к файлу.</param>
        /// <param name="s_blen">Размер блока.</param>
        /// <param name="disk_size">Размер диска.</param>
        /// <returns>Один экземпляр файловой системы.</returns>
        public static S5FS format(String file, UInt32 s_blen, UInt64 disk_size)
        {
            S5FS s5FS = new();
            s5FS.fs = new(file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            
            //Суперблок
            s5FS.sb = new(disk_size: disk_size, s_blen: s_blen);
            byte[] buffer = SuperBlock.SaveToByteArray(s5FS.sb);
            s5FS.fs.Write(buffer);
            s5FS.fs.Flush();

            //Выделить место под индексные
            buffer = new byte[s5FS.sb.s_isize * 144];
            s5FS.fs.Write(buffer);
            s5FS.fs.Flush();

            s5FS.blocks_offset = (ulong)buffer.LongLength + SuperBlock.superblock_size;

            // Выделить место под блоки данных
            // Потом, когда все будет работать, выделить больше до полного размера
            var block_bytes = new byte[s5FS.sb.s_blen * (s5FS.sb.s_fsize-2)];
            s5FS.fs.Write(block_bytes);
            s5FS.fs.Flush();

            //Создание битмапов
            UInt64 inode_bm_len = s5FS.sb.s_isize / 8;
            if (s5FS.sb.s_isize % 8 != 0)
            {
                inode_bm_len++;
            }
            s5FS.bm_inode = new(inode_bm_len, s5FS.sb.s_isize, 0);
            s5FS.WriteBitMap(s5FS.bm_inode);

            UInt64 blocks_bm_len = (s5FS.sb.s_fsize - 2) / 8;
            if (((s5FS.sb.s_fsize - 2) % 8) != 0)
            {
                blocks_bm_len++;
            }
            UInt64 blocks_bm_start = (UInt64)s5FS.bm_inode.map.LongLength / s5FS.sb.s_fsize;
            if ((UInt64)s5FS.bm_inode.map.LongLength % s5FS.sb.s_fsize != 0)
            {
                blocks_bm_start++;
            }
            s5FS.bm_block = new(blocks_bm_len, s5FS.sb.s_fsize - 2, blocks_bm_start);
            //Зная, где начинается и заканчиваются битмапы - заполняю битмап
            var temp = blocks_bm_len % s5FS.sb.s_blen == 0 ? blocks_bm_len / s5FS.sb.s_blen : (blocks_bm_len / s5FS.sb.s_blen + 1);
            UInt64 len = blocks_bm_start + temp;
            for (UInt64 i = 0; i < len; i++)
            {
                s5FS.bm_block.ChangeBlockState(i, false);
            }

            s5FS.WriteBitMap(s5FS.bm_block);

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

            var bytes = new byte[512];
            
            //Суперблок
            s5fs.fs.Read(bytes, 0, bytes.Length);
            s5fs.sb = SuperBlock.LoadFromByteArray(bytes);

            s5fs.blocks_offset = s5fs.sb.s_isize * 144 + SuperBlock.superblock_size;

            //Подгрузить битмапы
            //Иноды
            UInt64 inode_bm_len = s5fs.sb.s_isize / 8;
            if (s5fs.sb.s_isize % 8 != 0)
            {
                inode_bm_len++;
            }
            UInt64 blocks_to_read = inode_bm_len / s5fs.sb.s_blen;
            if (inode_bm_len % s5fs.sb.s_blen != 0) 
            {
                blocks_to_read++;
            }
            List<byte> readed_bytes = new();

            for (UInt64 i = 0; i < blocks_to_read; i++)
            {
                readed_bytes.AddRange(s5fs.ReadFromDataBlock(i));
            }

            bytes = new byte[inode_bm_len];
            Array.Copy(readed_bytes.ToArray(), bytes, bytes.LongLength);

            s5fs.bm_inode = new(bytes, inode_bm_len, 0);

            //Битовая карта блоков
            UInt64 bm_start = blocks_to_read;
            UInt64 blocks_bm_len = (s5fs.sb.s_fsize - 2) / 8;
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

            for (UInt64 i = bm_start; i < bm_start + blocks_to_read; i++)
            {
                readed_bytes.AddRange(s5fs.ReadFromDataBlock(i));
            }
            bytes = new byte[blocks_bm_len];
            Array.Copy(readed_bytes.ToArray(), bytes, bytes.LongLength);

            s5fs.bm_block = new(bytes, blocks_bm_len, bm_start);

            return s5fs;
        }

        /// <summary>
        /// Стандартный метод записи/обновления битовой карты.
        /// </summary>
        /// <param name="map"></param>
        private void WriteBitMap(BitMap map)
        {
            var slicer = Helper.Slicer(map.map, this.sb.s_blen).GetEnumerator();
            for (UInt64 i = map.start_block; slicer.MoveNext(); i++)
            {
                this.WriteToDataBlock(slicer.Current, i);
            }
        }

        /// <summary>
        /// Стандартный метод записи информации в блок данных. Нумерация с 0.
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="num"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void WriteToDataBlock(byte[] bytes, UInt64 num)
        {
            if (bytes.LongLength != this.sb.s_blen)
            {
                throw new ArgumentOutOfRangeException();
            }
            UInt64 currBlock_pos = (UInt64)this.blocks_offset + num * this.sb.s_blen;
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
            while (num > Int32.MaxValue)
            {
                num -= Int32.MaxValue;
                fs.Seek(Int32.MaxValue, SeekOrigin.Current);
            }
            fs.Seek((Int32)num, SeekOrigin.Current);
        }

        /// <summary>
        /// Стандартный метод считывания информации из блока данных. Нумерация с 0.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private byte[] ReadFromDataBlock(UInt64 num)
        {
            byte[] bytes = new byte[(int)this.sb.s_blen];

            UInt64 currBlock_pos = (UInt64)this.blocks_offset + num * this.sb.s_blen;
            this.Seek(currBlock_pos, SeekOrigin.Begin);
            fs.Read(bytes, 0, bytes.Length);

            return bytes;
        }

        /// <summary>
        /// Считать i-й инод
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private Inode ReadInode(UInt64 num)
        {
            UInt64 position = SuperBlock.superblock_size + num * 144;
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
        private void WriteInode(Inode inode)
        {
            UInt64 position = SuperBlock.superblock_size + inode.index * 144;
            byte[] bytes = Inode.SaveToByteArray(inode);
            this.Seek(position, SeekOrigin.Begin);
            fs.Write(bytes, 0, bytes.Length);
        }

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
                di_ctime = 0, // Текущее
                di_addr = new ulong[0] // Получить первый свободный блок
            };
        }


    }
}
