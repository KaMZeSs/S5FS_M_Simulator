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
            var users = new (int, String)[]
            {
                new (1, "qwe"),
                new (2, "asd"),
                new (3, "zxc")
            };

            Tester1(ref users);
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