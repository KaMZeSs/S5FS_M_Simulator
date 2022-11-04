using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using S5FS;

namespace Emulator
{
    /// <summary>
    /// Класс объекта файловой системы.
    /// Все файлы и папки - объекты и имеют те же атрибуты.
    /// </summary>
    internal class Obj
    {
        /// <summary>
        /// Является ли файл папкой
        /// </summary>
        public bool isFolder
        {
            get
            {
                switch (S5FS.Inode.GetInodeType(inode))
                {
                    case S5FS.InodeTypeEnum.Folder: return true;
                    case S5FS.InodeTypeEnum.File: return false;
                }
                throw new Exception();
            }
        }
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string Name { get; }
        /////// <summary>
        /////// Путь к файлу.
        /////// </summary>
        //public string Path { get; }
        /// <summary>
        /// Дата и время последнего изменения файла.
        /// </summary>
        public DateTime ChangeDateTime
        {
            get
            {
                return DateTime.FromBinary(inode.di_mtime);
            }
        }
        /// <summary>
        /// Дата и время последнего чтения файла.
        /// </summary>
        public DateTime ReadDateTime
        {
            get
            {
                return DateTime.FromBinary(inode.di_atime);
            }
        }
        /// <summary>
        /// Дата и время создания файла.
        /// </summary>
        public DateTime CreationTime
        {
            get
            {
                return DateTime.FromBinary(inode.di_ctime);
            }
        }
        /// <summary>
        /// Размер файла.
        /// </summary>
        public UInt32 GetSize
        {
            get
            {
                return inode.di_size;
            }
        }
        /// <summary>
        /// ID владельца файла.
        /// </summary>
        public ushort UserID
        {
            get
            {
                return inode.di_uid;
            }
        }
        /// <summary>
        /// ID группы владельца файла.
        /// </summary>
        public ushort GroupID
        {
            get
            {
                return inode.di_gid;
            }
        }

        public byte OwnerPermissions
        {
            get
            {
                return 0;
            }
        }
        public byte GroupPermissions
        {
            get
            {
                return 0;
            }
        }
        public byte OtherPermissions
        {
            get
            {
                return 0;
            }
        }

        public bool IsSystem
        {
            get
            {
                return false;
            }
        }
        public bool IsVisible
        {
            get
            {
                return true;
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        ///// <summary>
        ///// Массив байт содержимого файла.
        ///// </summary>
        //protected byte[] data;
        /// <summary>
        /// Инод файла.
        /// Из него получены все свойства файла, кроме пути и имени.
        /// </summary>
        public S5FS.Inode inode;
        /// <summary>
        /// Родительский инод.
        /// Нужен для удобного переименования файла.
        /// </summary>
        public S5FS.Inode parent_inode;

        public static byte[] StringToByteArr(String str)
        {
            return Encoding.UTF8.GetBytes(str);
        }
        public static String ByteArrToString(byte[] arr)
        {
            return Encoding.UTF8.GetString(arr);
        }

        public Obj(string name, Inode inode, Inode parent_inode)
        {
            Name = name.Trim();
            this.inode = inode;
            this.parent_inode = parent_inode;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
