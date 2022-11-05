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
    public class Obj
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

        public Permissions OwnerPermissions
        {
            get
            {
                return new (attr.OwnerPerm);
            }
        }
        public Permissions GroupPermissions
        {
            get
            {
                return new(attr.GroupPerm);
            }
        }
        public Permissions OtherPermissions
        {
            get
            {
                return new(attr.OtherPerm);
            }
        }

        public bool IsSystem
        {
            get
            {
                return (attr.Flags is 7) || (attr.Flags is 6) || (attr.Flags is 5) || (attr.Flags is 4);
            }
        }
        public bool IsHidden
        {
            get
            {
                return (attr.Flags is 7) || (attr.Flags is 5) || (attr.Flags is 3) || (attr.Flags is 1);
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return (attr.Flags is 7) || (attr.Flags is 6) || (attr.Flags is 3) || (attr.Flags is 2);
            }
        }

        public int NLinks 
        { 
            get 
            {
                return inode.di_nlinks;
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

        public S5FS.Inode.Attributes attr;

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
            this.attr = S5FS.Inode.Attributes.UInt16ToAttributes(this.inode.di_mode);
        }

        public override string ToString()
        {
            return this.Name;
        }

        public static String PermToString(byte perm) => perm switch
        {
            0 => "---",
            1 => "--x",
            2 => "-w-",
            3 => "-wx",
            4 => "r--",
            5 => "r-x",
            6 => "rw-",
            7 => "rwx",
            _ => throw new ArgumentOutOfRangeException(),
        };

        public class Permissions
        {
            public byte Data { get { return data; } }
            private byte data;

            public bool CanRead
            {
                get
                {
                    return data switch
                    {
                        4 => true,
                        5 => true,
                        6 => true,
                        7 => true,
                        _ => false,
                    };
                }
            }
            public bool CanWrite
            {
                get
                {
                    return data switch
                    {
                        2 => true,
                        3 => true,
                        6 => true,
                        7 => true,
                        _ => false,
                    };
                }
            }
            public bool CanExecute
            {
                get
                {
                    return data switch
                    {
                        1 => true,
                        3 => true,
                        5 => true,
                        7 => true,
                        _ => false,
                    };
                }
            }

            public Permissions(byte num)
            {
                data = num;
            }
        }
    }
}
