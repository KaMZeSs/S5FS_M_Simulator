using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    internal class S5FS // для инодов можно юзать (размер диска - сб) / 16 байт т.к. примерно по 2 кб (остается немного лишних, но не суть)
    {
        SuperBlock sb;
        BitMap bm;

        public static S5FS format(Char disk_name, UInt32 s_blen, UInt64 disk_size)
        {

        }
    }
}
