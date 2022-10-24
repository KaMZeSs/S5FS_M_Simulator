using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS.HighLevel
{
    internal class Obj
    {

        private bool isFolder 
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public string Name { get; set; }
        public string Path { get; }
        public DateTime ChangeDateTime { get; }
        public DateTime ReadDateTime { get; }
        public DateTime CreationTime { get; }
        public UInt64 GetSize { get; }

        protected byte[] data;
        protected Inode inode;
        protected Inode parent_inode;
    }
}
