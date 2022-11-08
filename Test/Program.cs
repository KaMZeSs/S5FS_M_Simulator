using System;
using System.Collections;
using System.Collections.Specialized;
using System.Numerics;

namespace Test
{
    class Program
    {


        public static void Main()
        {
            var array = new UInt16[] { 0, 1, 2, 4, 5, 7, 8 };

            UInt16 index = 0;

            for (UInt16 i = 0; i < array.Length - 1; i++)
            {
                if (array[i + 1] - array[i] > 1)
                {
                    index = i;
                    break;
                }
            }
        }

        public static void Tester1(ref (int, String)[] users)
        {
            users[1] = new(4, "asf2rqweq");
            Tester2(ref users[2]);
        }

        public static void Tester2(ref (int, String) user)
        { 
            user = new(5, "zxvdsfqweq");
        }

    }
}