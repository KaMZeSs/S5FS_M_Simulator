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
            var array = new byte[] { 1, 2, 3, 4, 5, 6 };
            var array2 = new byte[4];
            array.CopyTo(array2, 0);
            
            
            
            //var s5fs = S5FS.load_from_file("qwe");
            //File.Delete("qwe");
            //var s5fs = S5FS.format("qwe", 2048, 5242880);
        }
    }
}
