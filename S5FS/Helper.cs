using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    static class Helper
    {
        public static IEnumerable<byte[]> Slicer(byte[] main_array, UInt32 block_size)
        {
            byte[] result;
            for (Int32 i = 0; ; i += (int)block_size)
            {
                result = main_array.Skip(i).Take((int)block_size).ToArray();
                if (result.Length == 0)
                {
                    yield break;
                }
                else if (result.Length != (int)block_size)
                {
                    var temp_arr = result;
                    result = new byte[block_size];
                    temp_arr.CopyTo(result, 0);
                    yield return result;
                }
                else
                {
                    yield return result;
                }
            }
        }
    }
}
