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
            byte[] array = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var slc = Helper.Slicer(array, 4).GetEnumerator();
            while (slc.MoveNext())
            {
                Console.WriteLine(String.Join(" ", slc.Current));
            }            
        }
    }
}
