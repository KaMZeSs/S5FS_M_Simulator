using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    /// <summary>
    /// Вес: 144 байта
    /// </summary>
    internal class Inode : ICloneable
    {
        public const int inode_size = 144;

        /// <summary>
        /// Тип файла, права доступа тип|rwx|rwx|rwx|reserv5bit. 2 байта.
        /// </summary>
        public UInt16 di_mode;
        /// <summary>
        /// Число ссылок на файл. 2 байта.
        /// </summary>
        public UInt16 di_nlinks;
        /// <summary>
        /// Идентификатор владельца-пользователя. 2 байта.
        /// </summary>
        public UInt16 di_uid;
        /// <summary>
        /// Идентификатор владельца-группы. 2 байта.
        /// </summary>
        public UInt16 di_gid;
        /// <summary>
        /// Размер файла в байтах. 8 байт.
        /// </summary>
        public UInt64 di_size;
        /// <summary>
        /// Время последнего доступа к файлу. 8 байт.
        /// </summary>
        public Int64 di_atime;
        /// <summary>
        /// Время последней модификации. 8 байт.
        /// </summary>
        public Int64 di_mtime;
        /// <summary>
        /// Теперь это время создания.
        /// Время последней модификации inode. 8 байт.
        /// </summary>
        public Int64 di_ctime;
        /// <summary>
        /// Массив адресов дисковых блоков хранения данных. 13 элементов. 8*13 =  104 байт.
        /// </summary>
        public UInt64[] di_addr;
        /// <summary>
        /// Позиция в массиве инодов
        /// </summary>
        public UInt64 index;

        public Inode(UInt64 num)
        {
            this.di_addr = new UInt64[13];
            this.index = num;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// Преобразует один инод в массив байт.
        /// </summary>
        /// <param name="inode">Преобразуемый инод.</param>
        /// <returns>Массив байт.</returns>
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

        /// <summary>
        /// Преобразует массив байт в один инод.
        /// </summary>
        /// <param name="array">Обрабатываемый массив.</param>
        /// <returns>Один инод.</returns>
        /// <exception cref="Exception"></exception>
        public static Inode LoadFromByteArray(byte[] array, UInt64 num)
        {
            if (array.Length != inode_size)
            {
                throw new Exception("Inode size must be 144 bytes");
            }

            Inode inode = new(num);

            inode.di_mode = BitConverter.ToUInt16(array, 0);
            inode.di_nlinks = BitConverter.ToUInt16(array, 2);
            inode.di_uid = BitConverter.ToUInt16(array, 4);
            inode.di_gid = BitConverter.ToUInt16(array, 6);
            inode.di_size = BitConverter.ToUInt64(array, 8);
            inode.di_atime = BitConverter.ToInt64(array, 16);
            inode.di_mtime = BitConverter.ToInt64(array, 24);
            inode.di_ctime = BitConverter.ToInt64(array, 32);


            for (int i = 0, curr = 40; i < inode.di_addr.Length; i++, curr += 8)
            {
                inode.di_addr[i] = BitConverter.ToUInt64(array, curr);
            }

            return inode;
        }

        public static InodeTypeEnum GetInodeType(Inode inode)
        {
            var bytes = BitConverter.GetBytes(inode.di_mode);
            BitArray bitArray = new BitArray(bytes);
            var a = bitArray[bitArray.Length - 1];
            var b = bitArray[bitArray.Length - 2];

            if (a is false && b is false)
            {
                return InodeTypeEnum.Empty;
            }
            if (a is false && b is true)
            {
                return InodeTypeEnum.Folder;
            }
            if (a is true && b is false)
            {
                return InodeTypeEnum.File;
            }
            return InodeTypeEnum.Empty;
        }
    }
}
