using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    /// <summary>
    /// Вес: 512(38) байт (резерв)
    /// </summary>
    internal class SuperBlock
    {
        public const int superblock_size = 512;

        /// <summary>
        /// Тип ФС. 1 байт.
        /// </summary>
        public Byte s_type;
        /// <summary>
        /// Размер ФС в логических блоках (включ СуперБлок, ilist и блоки данных). 8 байт.
        /// </summary>
        public UInt64 s_fsize;
        /// <summary>
        /// Размер массива индексных дескрипторов. 8 байт.
        /// </summary>
        public UInt64 s_isize;
        /// <summary>
        /// Число свободных блоков, доступных для размещения. 8 байт.
        /// </summary>
        public UInt64 s_tfree;
        /// <summary>
        /// Число свободных inode, доступных для размещения. 8 байт.
        /// </summary>
        public UInt64 s_tinode;
        /// <summary>
        /// Флаг модификации. 1 байт.
        /// </summary>
        public Byte s_fmod;
        /// <summary>
        /// Размер логического блока. 4 байт.
        /// </summary>
        public UInt32 s_blen;
        

        //Зочем, если размер массива inode есть, размер фс (1сб + 1ilist + к-во блоков, т.е. к-во блоков, значит есть к-во бит),
        //размер блока есть. Расчитывать это при открытии и сохранять в битмапе, или s5fs
        ///// <summary>
        ///// Номер последнего блока, занятого битовой картой блоков. 8 байт.
        ///// </summary>
        //public UInt64 s_lbit;
        ///// <summary>
        ///// Номер последнего блока, занятого битовой картой инодов. 8 байт.
        ///// </summary>
        //public UInt64 s_ibit;
        

        public SuperBlock() { }    

        /// <summary>
        /// Создание нового суперблока при форматировании.
        /// </summary>
        /// <param name="disk_size">Размер выделенного пространства на диске в байтах.</param>
        /// <param name="s_blen">Размер одного блока данных.</param>
        public SuperBlock(UInt64 disk_size, UInt32 s_blen)
        {
            this.s_type = 0xFA;
            this.s_blen = s_blen;
            this.s_fmod = 0xFF;

            //Получаем к-во inode
            this.s_isize = this.s_tinode = (disk_size - 2048) / 16 / 144;

            UInt64 left_disk_size = disk_size - (s_isize * 144);
            
            this.s_tfree = left_disk_size / this.s_blen;
            this.s_fsize = this.s_tfree + 2;

            //this.s_lbit = this.s_tfree / this.s_blen;
            //if (this.s_tfree % this.s_blen == 0)
            //{
            //    this.s_lbit--;
            //}

            //this.s_ibit = this.s_isize / this.s_blen;
            //if (this.s_isize % this.s_blen == 0)
            //{
            //    this.s_ibit--;
            //}
        }

        /// <summary>
        /// Преобразует суперблок в массив байт.
        /// </summary>
        /// <param name="sb">Преобразуемый суперблок.</param>
        /// <returns>Массив байт размером 2048.</returns>
        public static byte[] SaveToByteArray(SuperBlock sb)
        {
            var superBlock = new byte[SuperBlock.superblock_size];

            superBlock[0] = sb.s_type; //Array.Copy(BitConverter.GetBytes(sb.s_type), 0, superBlock, 0, 1);
            Array.Copy(BitConverter.GetBytes(sb.s_fsize), 0, superBlock, 1, 8);
            Array.Copy(BitConverter.GetBytes(sb.s_isize), 0, superBlock, 9, 8);
            Array.Copy(BitConverter.GetBytes(sb.s_tfree), 0, superBlock, 17, 8);
            Array.Copy(BitConverter.GetBytes(sb.s_tinode), 0, superBlock, 25, 8);
            superBlock[33] = sb.s_fmod; //Array.Copy(BitConverter.GetBytes(sb.s_fmod), 0, superBlock, 33, 1);
            Array.Copy(BitConverter.GetBytes(sb.s_blen), 0, superBlock, 34, 4);
            //Array.Copy(BitConverter.GetBytes(sb.s_lbit), 0, superBlock, 38, 8);
            //long curr = 46;
            //foreach (var i in sb.s_f_inodes)
            //{
            //    Array.Copy(BitConverter.GetBytes(i), 0, superBlock, curr, 8);
            //    curr += 8;
            //}

            /*Array.Copy(BitConverter.GetBytes(0xFFFF), 0, superBlock, 2046, 2);*/ //Резервыные 2 байта

            return superBlock;
        }

        /// <summary>
        /// Преобразует массив байт длиной 2048 в суперблок.
        /// </summary>
        /// <param name="array">Обрабатываемый массив.</param>
        /// <returns>Один экземпляр суперблока.</returns>
        /// <exception cref="Exception"></exception>
        public static SuperBlock LoadFromByteArray(byte[] array)
        {
            var sb = new SuperBlock();
            if (array.Length != superblock_size)
            {
                throw new Exception("SuperBlock size must be 2048 bytes");
            }

            sb.s_type = array[0];
            sb.s_fsize = BitConverter.ToUInt64(array, 1);
            sb.s_isize = BitConverter.ToUInt64(array, 9);
            sb.s_tfree = BitConverter.ToUInt64(array, 17);
            sb.s_tinode = BitConverter.ToUInt64(array, 25);
            sb.s_fmod = array[33];
            sb.s_blen = BitConverter.ToUInt32(array, 34);
            //sb.s_lbit = BitConverter.ToUInt64(array, 38);

            //for (int i = 0, curr = 46; i < sb.s_f_inodes.Length; i++, curr += 8)
            //{
            //    sb.s_f_inodes[i] = BitConverter.ToUInt64(array, curr);
            //}

            return sb;
        }
    }
}
