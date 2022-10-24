using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    internal class Program
    {
        public static void Main()
        {
            var s5fs = S5FS.format("qwe", 2048, 5242880);
            s5fs.CreateFile("", "qwe", false);
            s5fs.CreateFile("", "qwe2", false);
            
        }
    }
}
