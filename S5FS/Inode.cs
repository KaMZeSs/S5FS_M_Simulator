using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    internal class Inode //                                                                         144 байта
    {
        const int inode_size = 144;

        public UInt16 di_mode; // тип файла, права доступа тип|suid|sgid|stickyBit|rwx|rwx|rwx           2 байта
        public UInt16 di_nlinks; // число ссылок на файл                                                   2 байта
        public UInt16 di_uid; // идентефикатор владельца-пользователя                                      2 байта
        public UInt16 di_gid; // идентефикатор владельца-группы                                            2 байта
        public UInt64 di_size; // размер файла в байтах                                                    8 байт
        public UInt64 di_atime; // время последнего доступа к файлу                                        8 байт
        public UInt64 di_mtime; // время последней модификации                                             8 байт
        public UInt64 di_ctime; // время последней модификации inode                                       8 байт
        public UInt64[] di_addr; //массив адресов дисковых блоков хранения данных. 13 элементов    8*13 =  104 байт

        public Inode()
        {
            this.di_addr = new UInt64[13];
        }

        public static byte[] SaveToByteArray(Inode inode)
        {
            var bytes = new byte[144];

            Array.Copy(BitConverter.GetBytes(inode.di_mode), 0, bytes, 0, 2);
            Array.Copy(BitConverter.GetBytes(inode.di_nlinks), 0, bytes, 2, 2);
            Array.Copy(BitConverter.GetBytes(inode.di_uid), 0, bytes, 4, 2);
            Array.Copy(BitConverter.GetBytes(inode.di_gid), 0, bytes, 6, 2);
            Array.Copy(BitConverter.GetBytes(inode.di_size), 0, bytes, 8, 8);
            Array.Copy(BitConverter.GetBytes(inode.di_atime), 0, bytes, 16, 8);
            Array.Copy(BitConverter.GetBytes(inode.di_mtime), 0, bytes, 24, 8);
            Array.Copy(BitConverter.GetBytes(inode.di_ctime), 0, bytes, 32, 8);

            long curr = 40;
            foreach (var i in inode.di_addr)
            {
                Array.Copy(BitConverter.GetBytes(i), 0, bytes, curr, 8);
                curr += 8;
            }

            return bytes;
        }

        public static Inode LoadFromByteArray(byte[] array)
        {
            if (array.Length != inode_size)
            {
                throw new Exception("Inode size must be 144 bytes");
            }

            Inode inode = new();

            inode.di_mode = BitConverter.ToUInt16(array, 0);
            inode.di_nlinks = BitConverter.ToUInt16(array, 2);
            inode.di_uid = BitConverter.ToUInt16(array, 4);
            inode.di_gid = BitConverter.ToUInt16(array, 6);
            inode.di_size = BitConverter.ToUInt64(array, 8);
            inode.di_atime = BitConverter.ToUInt64(array, 16);
            inode.di_mtime = BitConverter.ToUInt64(array, 24);
            inode.di_ctime = BitConverter.ToUInt64(array, 32);


            for (int i = 0, curr = 40; i < inode.di_addr.Length; i++, curr += 8)
            {
                inode.di_addr[i] = BitConverter.ToUInt64(array, curr);
            }

            return inode;
        }
    }
}
