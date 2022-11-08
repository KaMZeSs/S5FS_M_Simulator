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
            var users = new (int, string, int)[]
            {
                new (0, "root", 0),
                new (1, "qwe", 0),
                new (2, "asd", 1),
                new (3, "qxz", 2)
            };

            var a = users.GetType();

            var groups = new (int, string)[]
            {
                new (0, "root_group"),
                new (1, "2nd group"),
                new (2, "3rd group"),
                new (3, "4th group")
            };

            var vs = (from user in users
                      join gr in groups on user.Item3 equals gr.Item1
                      select new { id = user.Item1, name = user.Item2, group_name = gr.Item2 })
                     .ToArray();
        }

        

    }
}