﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    static class Helper
    {
        /// <summary>
        /// "Нарезает" входной массив на блоки одинаковой длины. Если последний блок - меньше, остаток заполняется нулями.
        /// </summary>
        /// <param name="main_array"></param>
        /// <param name="block_size"></param>
        /// <returns></returns>
        public static IEnumerable<byte[]> Slicer(byte[] main_array, UInt32 block_size)
        {
            byte[] result;

            if (main_array.Length is 0)
            {
                result = new byte[block_size];
                yield return result;
            }

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

        public static String StringExtender(String str, int prefer_size)
        {
            if (str.Length != prefer_size)
            {
                var list = str.ToList();
                while (list.Count != prefer_size)
                {
                    list.Add(' ');
                }
                str = new string(list.ToArray());
            }
            return str;
        }
    }
}
