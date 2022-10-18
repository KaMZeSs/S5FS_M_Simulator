using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    internal class SuperBlock
    {
        const int superblock_size = 2048;
        const int max_inode_list_size = 251;

        public Byte s_type; // Тип ФС                                                                                  1 байт
        public UInt64 s_fsize; // Размер ФС в логических блоках (включ СуперБлок, ilist и блоки данных)                8 байт
        public UInt64 s_isize; // Размер массива индексных дескрипторов                                                8 байт
        public UInt64 s_tfree; // Число свободных блоков, доступных для размещения                                     8 байт
        public UInt64 s_tinode; // Число свободных inode, доступных для размещения                                     8 байт
        public Byte s_fmod; // флаг модификации                                                                        1 байт
        public UInt32 s_blen; // размер логического блока                                                              4 байт
        public UInt64 s_lbit; // Номер последнего блока, занятого битовой картой                                       8 байт
        public UInt64[] s_f_inodes; // список номеров свободных inode PS: постоянного размера                      2000 байт (250 адресов) нужно в конце зарезервировать 2 байта чтобы до 2048

        public SuperBlock()
        {
            s_type = 0xFA; 
            s_fsize = 1024;
            s_isize = 256;
            s_tfree = 512;
            s_tinode = 128;
            s_fmod = 0xFF;
            s_blen = 2048;
            s_f_inodes = new UInt64[max_inode_list_size];
        }

        public SuperBlock(bool a)
        {
            s_f_inodes = new UInt64[max_inode_list_size];
        }

        public byte[] SaveToByteArray()
        {
            var superBlock = new byte[2048];

            superBlock[0] = s_type; //Array.Copy(BitConverter.GetBytes(s_type), 0, superBlock, 0, 1);
            Array.Copy(BitConverter.GetBytes(s_fsize), 0, superBlock, 1, 8);
            Array.Copy(BitConverter.GetBytes(s_isize), 0, superBlock, 9, 8);
            Array.Copy(BitConverter.GetBytes(s_tfree), 0, superBlock, 17, 8);
            Array.Copy(BitConverter.GetBytes(s_tinode), 0, superBlock, 25, 8);
            superBlock[33] = s_fmod; //Array.Copy(BitConverter.GetBytes(s_fmod), 0, superBlock, 33, 1);
            Array.Copy(BitConverter.GetBytes(s_blen), 0, superBlock, 34, 4);
            Array.Copy(BitConverter.GetBytes(s_lbit), 0, superBlock, 38, 8);
            long curr = 46;
            foreach (var i in s_f_inodes)
            {
                Array.Copy(BitConverter.GetBytes(i), 0, superBlock, curr, 8);
                curr += 8;
            }

            return superBlock;
        }

        public static SuperBlock LoadFromByteArray(byte[] array)
        {
            var sb = new SuperBlock(true);
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
            sb.s_lbit = BitConverter.ToUInt64(array, 38);

            for (int i = 0, curr = 46; i < sb.s_f_inodes.Length; i++, curr += 8)
            {
                sb.s_f_inodes[i] = BitConverter.ToUInt64(array, curr);
            }

            return sb;
        }
    }
}
