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
        FileStream fs;

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

        private void WriteBitMap(BitMap map)
        {
            var slicer = Helper.Slicer(map.map, this.sb.s_blen).GetEnumerator();
            for (UInt64 i = map.start_block; slicer.MoveNext(); i++)
            {
                this.WriteToDataBlock(slicer.Current, i);
            }
        }

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

        private byte[] ReadFromDataBlock(UInt64 num)
        {
            byte[] bytes = new byte[(int)this.sb.s_blen];

            UInt64 currBlock_pos = (UInt64)this.blocks_offset + num * this.sb.s_blen;
            this.Seek(currBlock_pos, SeekOrigin.Begin);
            fs.Read(bytes, 0, bytes.Length);

            return bytes;
        }
    }
}
