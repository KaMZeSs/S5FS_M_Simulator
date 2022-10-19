using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    public enum InodeTypeEnum: UInt16
    {
        Empty,
        Folder,
        File
    }
}
