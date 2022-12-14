using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    /// <summary>
    /// Вес: 80 байт
    /// </summary>
    public class Inode : ICloneable
    {
        public const int inode_size = 84;

        /// <summary>
        /// Тип файла, права доступа тип|rwx|rwx|rwx|isSystem|isReadOnly|isHidden|2reserved. 2 байта.
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
        /// Размер файла в байтах. 4 байта.
        /// </summary>
        public UInt32 di_size;
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
        /// Массив адресов дисковых блоков хранения данных. 12 элементов. 4*12 =  48 байт.
        /// </summary>
        public UInt32[] di_addr;
        /// <summary>
        /// Позиция в массиве инодов
        /// </summary>
        public UInt32 index;

        public Inode(UInt32 num)
        {
            this.di_addr = new UInt32[12];
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
            var bytes = new byte[Inode.inode_size];

            Array.Copy(BitConverter.GetBytes(inode.di_mode), 0, bytes, 0, 2);
            Array.Copy(BitConverter.GetBytes(inode.di_nlinks), 0, bytes, 2, 2);
            Array.Copy(BitConverter.GetBytes(inode.di_uid), 0, bytes, 4, 2);
            Array.Copy(BitConverter.GetBytes(inode.di_gid), 0, bytes, 6, 2);
            Array.Copy(BitConverter.GetBytes(inode.di_size), 0, bytes, 8, 4);
            Array.Copy(BitConverter.GetBytes(inode.di_atime), 0, bytes, 12, 8);
            Array.Copy(BitConverter.GetBytes(inode.di_mtime), 0, bytes, 20, 8);
            Array.Copy(BitConverter.GetBytes(inode.di_ctime), 0, bytes, 28, 8);

            long curr = 36;
            foreach (var i in inode.di_addr)
            {
                Array.Copy(BitConverter.GetBytes(i), 0, bytes, curr, 4);
                curr += 4;
            }

            return bytes;
        }

        /// <summary>
        /// Преобразует массив байт в один инод.
        /// </summary>
        /// <param name="array">Обрабатываемый массив.</param>
        /// <returns>Один инод.</returns>
        /// <exception cref="Exception"></exception>
        public static Inode LoadFromByteArray(byte[] array, UInt32 num)
        {
            if (array.Length != inode_size)
            {
                throw new Exception($"Inode size must be {Inode.inode_size} bytes");
            }

            Inode inode = new(num);

            inode.di_mode = BitConverter.ToUInt16(array, 0);
            inode.di_nlinks = BitConverter.ToUInt16(array, 2);
            inode.di_uid = BitConverter.ToUInt16(array, 4);
            inode.di_gid = BitConverter.ToUInt16(array, 6);
            inode.di_size = BitConverter.ToUInt32(array, 8);
            inode.di_atime = BitConverter.ToInt64(array, 12);
            inode.di_mtime = BitConverter.ToInt64(array, 20);
            inode.di_ctime = BitConverter.ToInt64(array, 28);


            for (int i = 0, curr = 36; i < inode.di_addr.Length; i++, curr += 4)
            {
                inode.di_addr[i] = BitConverter.ToUInt32(array, curr);
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

        public class Attributes
        {
            public byte OwnerPerm;
            public byte GroupPerm;
            public byte OtherPerm;
            public byte Flags;

            private readonly static BitVector32.Section r_sect = BitVector32.CreateSection(3);
            private readonly static BitVector32.Section f_sect = BitVector32.CreateSection(7, r_sect);
            private readonly static BitVector32.Section o_sect = BitVector32.CreateSection(7, f_sect);
            private readonly static BitVector32.Section g_sect = BitVector32.CreateSection(7, o_sect);
            private readonly static BitVector32.Section u_sect = BitVector32.CreateSection(7, g_sect);
            private readonly static BitVector32.Section t_sect = BitVector32.CreateSection(3, u_sect);

            private Attributes() { }

            public static Attributes ChangeAttributes(Attributes attributes, byte OwnerPerm = 8, 
                byte GroupPerm = 8, byte OtherPerm = 8, byte Flags = 8)
            {
                if (OwnerPerm < 8)
                {
                    attributes.OwnerPerm = OwnerPerm;
                }
                if (GroupPerm < 8)
                {
                    attributes.GroupPerm = GroupPerm;
                }
                if (OtherPerm < 8)
                {
                    attributes.OtherPerm = OtherPerm;
                }
                if (Flags < 8)
                {
                    attributes.Flags = Flags;
                }

                return attributes;
            }

            public static Attributes UInt16ToAttributes(UInt16 val)
            {
                var vect = new BitVector32(val);
                return new Attributes()
                {
                    Flags = (byte)vect[Attributes.f_sect],
                    OtherPerm = (byte)vect[Attributes.o_sect],
                    GroupPerm = (byte)vect[Attributes.g_sect],
                    OwnerPerm = (byte)vect[Attributes.u_sect],
                };
            }

            public static UInt16 AttributesToUInt16(Attributes attr, InodeTypeEnum type)
            {
                var vect = new BitVector32(0);

                vect[Attributes.f_sect] = attr.Flags;
                vect[Attributes.o_sect] = attr.OtherPerm;
                vect[Attributes.g_sect] = attr.GroupPerm;
                vect[Attributes.u_sect] = attr.OwnerPerm;
                var a = (int)type;
                vect[Attributes.t_sect] = a;

                return (UInt16)vect.Data;
            }
        }
    }
}
