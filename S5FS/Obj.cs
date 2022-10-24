using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
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
                switch (Inode.GetInodeType(inode))
                {
                    case InodeTypeEnum.Folder: return true;
                    case InodeTypeEnum.File: return false;
                }
                throw new Exception();
            }
        }
        /// <summary>
        /// Имя файла.
        /// </summary>
        public string Name { get; set; }
        ///// <summary>
        ///// Путь к файлу.
        ///// </summary>
        //public string Path { get; } // Установить в конструкторе Или зачем его устанавливать?
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
        public ulong GetSize
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

        /// <summary>
        /// Массив байт содержимого файла.
        /// </summary>
        protected byte[] data;
        /// <summary>
        /// Инод файла.
        /// Из него получены все свойства файла, кроме пути и имени.
        /// </summary>
        protected Inode inode;
        /// <summary>
        /// Родительский инод.
        /// Нужен для удобного переименования файла.
        /// </summary>
        protected Inode parent_inode;
    }
}
