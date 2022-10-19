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
        SuperBlock sb;
        BitMap bm_inode;
        BitMap bm_block;
        BinaryWriter bw;

        UInt64 ilist_bytes_len;
        Int32 blocks_offset;

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
            s5FS.bw = new(File.OpenWrite(file));
            
            //Суперблок
            s5FS.sb = new(disk_size: disk_size, s_blen: s_blen);
            byte[] buffer = SuperBlock.SaveToByteArray(s5FS.sb);
            s5FS.bw.Write(buffer);
            s5FS.bw.Flush();
            s5FS.ilist_bytes_len = (ulong)buffer.LongLength;
            s5FS.blocks_offset = (int)(s5FS.ilist_bytes_len + SuperBlock.superblock_size);

            //Выделить место под индексные
            buffer = new byte[s5FS.sb.s_isize * 144];
            s5FS.bw.Write(buffer);
            s5FS.bw.Flush();

            //Тут неправильно
            //Надо метод для записи в блок, чтобы отдельно
            //И надо сначала выделить место под блоки данных

            ////Выделить место под битовую карту инодов
            //UInt64 byteLen = s5FS.sb.s_isize / 8;
            //if (s5FS.sb.s_isize % 8 != 0)
            //{
            //    byteLen++;
            //}
            //s5FS.bm_inode = new(new byte[byteLen], s5FS.sb.s_isize);
            //UInt64 inodeMap_bytes = s5FS.bm_inode.length / 8;
            //if (s5FS.bm_inode.length % 8 != 0)
            //{
            //    inodeMap_bytes++;
            //}
            //UInt64 inodeMap_blocks = inodeMap_bytes / s5FS.sb.s_blen;
            //if (inodeMap_bytes % s5FS.sb.s_blen != 0)
            //{
            //    inodeMap_blocks++;
            //}
            
            ////Выделить место под битовую карту блоков
            //byteLen = (s5FS.sb.s_fsize - 2) / 8;
            //if ((s5FS.sb.s_fsize - 2) % 8 != 0)
            //{
            //    byteLen++;
            //}
            //s5FS.bm_block = new(new byte[byteLen], s5FS.sb.s_fsize);
            //UInt64 blockMap_bytes = s5FS.bm_block.length / 8;
            //if (s5FS.bm_block.length % 8 != 0)
            //{
            //    blockMap_bytes++;
            //}
            //UInt64 blockMap_blocks = blockMap_bytes / s5FS.sb.s_blen;
            //if (blockMap_bytes % s5FS.sb.s_blen != 0)
            //{
            //    blockMap_blocks++;
            //}

            //for (UInt64 i = 0; i < inodeMap_blocks + blockMap_blocks; i++)
            //{
            //    s5FS.bm_block.ChangeBlockState(i, false);
            //}

            //buffer = s5FS.bm_inode.map;
            //s5FS.bw.Write(buffer);
            //s5FS.bw.Flush();

            //buffer = s5FS.bm_block.map;
            //s5FS.bw.Write(buffer);
            //s5FS.bw.Flush();

            // Выделить место под блоки данных
            var block_bytes = new byte[s5FS.sb.s_blen * (s5FS.sb.s_fsize-2)];
            s5FS.bw.Write(block_bytes);

            return s5FS;
        }

        /// <summary>
        /// Перезаписывает битовую карту инодов
        /// </summary>
        /// <returns>Количество занимаемых блоков</returns>
        private int WriteInodeBitMap()
        {
            int size = 0;
            //Считаем к-во занимаемых блоков
            if (this.bm_inode.map.LongLength % this.sb.s_blen == 0)
            {
                size = (int)(this.bm_inode.map.Length / this.sb.s_blen);
            }
            else
            {
                size = (int)(this.bm_inode.map.LongLength / this.sb.s_blen + 1);
            }
            //Записываем битовую карту инодов начиная с первого блока
            this.bw.Seek(blocks_offset, SeekOrigin.Begin);
            this.bw.Write(this.bm_inode.map);
            this.bw.Flush();

            return size;
        }

        /// <summary>
        /// Перезаписывает битовую карту блоков
        /// </summary>
        /// <returns>Количество занимаемых блоков</returns>
        private int WriteBlockBitMap(Int32 offset)
        {
            int size = 0;
            //Считаем к-во занимаемых блоков
            if (this.bm_block.map.LongLength % this.sb.s_blen == 0)
            {
                size = (int)(this.bm_block.map.LongLength / this.sb.s_blen);
            }
            else
            {
                size = (int)(this.bm_block.map.LongLength / this.sb.s_blen + 1);
            }
            //Записываем битовую карту инодов начиная со следующего за битовой картой инодов блока
            this.bw.Seek(blocks_offset + offset * (int)this.sb.s_blen, SeekOrigin.Begin);
            this.bw.Write(this.bm_block.map);
            this.bw.Flush();

            return size;
        }

        private void WriteToDataBlock(byte[] bytes)
        {

        }


    }
}
