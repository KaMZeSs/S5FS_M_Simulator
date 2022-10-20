using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    internal class Program
    {
        public static void Main()
        {
            File.Delete("qwe");
            var s5fs = S5FS.format("qwe", 2048, 5242880);        
        }
    }
}
