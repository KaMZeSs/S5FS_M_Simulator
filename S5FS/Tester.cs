using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    internal class Program
    {
        public static void Main()
        {
            BinaryWriter bw = new BinaryWriter(System.IO.File.OpenWrite("qweqw"));
            UInt64 b = 0x1023456789ABCDEF;
            bw.Write(b);
            bw.Flush();
            
        }
    }
}
