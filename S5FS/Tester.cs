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
            SuperBlock block = new();
            var arr = block.SaveToByteArray();
            SuperBlock bl2 = SuperBlock.LoadFromByteArray(arr);
        }
    }
}
