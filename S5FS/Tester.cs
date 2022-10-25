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
            //var fs = S5FS.format("qwe", 2048, 20971520);
            //var file = fs.CreateFile("", "Hello");
            //fs.WriteDataByInode(file.daughter, Obj.StringToByteArr("Hello World!!!"));
            //var file2 = fs.OpenFile("", "Hello");
            //Console.WriteLine(Obj.ByteArrToString(file2.data));

            //var fs = S5FS.load_from_file("qwe");
            //var file2 = fs.OpenFile("", "Hello");
            //Console.WriteLine(Obj.ByteArrToString(file2.data));

            var fs = S5FS.format("qwe", 2048, 20971520);
            var file1 = fs.CreateFile("", "File");
            fs.WriteDataByInode(file1.daughter, Obj.StringToByteArr("Test!"));
            var folder = fs.CreateFile("", "Papaka", true);
            //var file = fs.CreateFile("Papaka", "file1");
            //fs.WriteDataByInode(file.daughter, Obj.StringToByteArr("Proverka!"));
            //var file2 = fs.OpenFile("Papaka", "file1");
            //Console.WriteLine(Obj.ByteArrToString(file2.data));

            var arr = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var slcd = Helper.Slicer(arr, 3).GetEnumerator();
            while (slcd.MoveNext())
            {
                Console.WriteLine(String.Join(' ', slcd.Current));
            }

        }
    }
}
