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
            ushort val = 0b01_00_00_1_000_000_000;
            var bytes = BitConverter.GetBytes(val);
            BitArray bitArray = new BitArray(bytes);
            var a = bitArray[bitArray.Length - 1];
            var b = bitArray[bitArray.Length - 2];
            //var s5fs = S5FS.load_from_file("qwe");
            //File.Delete("qwe");
            //var s5fs = S5FS.format("qwe", 2048, 5242880);
        }
    }
}
