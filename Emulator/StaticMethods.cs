using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emulator
{
    static class StaticMethods
    {
        //UID,Name,MainGID,HashPassw
        public static (UInt16, String, UInt16, String)[] GetUsersFromString(String users_text)
        {
            var lines = users_text.Trim().Split(Environment.NewLine)
                .Where(x => !String.IsNullOrEmpty(x)).ToArray();

            var result = new List<(UInt16, String, UInt16, String)>();

            foreach (var line in lines)
            {
                var parts = line.Split(':');
                var uid = UInt16.Parse(parts[0]);
                var name = parts[1];
                var gid = UInt16.Parse(parts[2]);
                var psw = parts[3];

                result.Add(new(uid, name, gid, psw));
            }

            return result.OrderBy(x => x.Item1).ToArray();
        }
        public static String GetStringFromUsers((UInt16, String, UInt16, String)[] users)
        {
            StringBuilder sb = new();

            foreach (var user in users)
            {
                sb.Append($"{user.Item1}:{user.Item2}:{user.Item3}:{user.Item4}{Environment.NewLine}");
            }

            return sb.ToString();
        }
        //GID,Name,User_ids
        public static (UInt16, String, UInt16[])[] GetGroupsFromString(String groups_text)
        {
            var lines = groups_text.Trim().Split(Environment.NewLine)
                .Where(x => !String.IsNullOrEmpty(x)).ToArray();

            var result = new List<(UInt16, String, UInt16[])>();

            foreach (var line in lines)
            {
                var parts = line.Split(':');
                var uid = UInt16.Parse(parts[0]);
                var name = parts[1];

                var list = new List<UInt16>();
                for (int i = 2; i < parts.Length; i++)
                {
                    var item = UInt16.Parse(parts[i]);
                    list.Add(item);
                }

                result.Add(new(uid, name, list.ToArray()));
            }

            return result.OrderBy(x => x.Item1).ToArray();
        }
        public static String GetStringFromGroups((UInt16, String, UInt16[])[] groups)
        {
            StringBuilder sb = new();

            foreach (var user in groups)
            {
                sb.Append($"{user.Item1}:{user.Item2}:{String.Join(':', user.Item3)}{Environment.NewLine}");
            }

            return sb.ToString();
        }

        public static UInt16 GetNextId(UInt16[] array)
        {
            if (array.Length is 0)
                return 0;

            if (array.Last() is not UInt16.MaxValue)
            {
                return (ushort)(array.Last() + 1);
            }

            for (UInt16 i = 0; i < array.Length - 1; i++)
            {
                if (array[i + 1] - array[i] > 1)
                {
                    return (ushort)(array[i] + 1);
                }
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}
