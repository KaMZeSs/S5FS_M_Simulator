using System;
using System.Collections;
using System.Collections.Specialized;
using System.Numerics;

namespace Test
{
    class Program
    {
        enum A { val1, val2, val3}

        public static void Main()
        {
            var a = A.val3;

            var b = (int)a;

            UInt16 val = 0b01_111_101_100_100_00; //тип|rwx|rwx|rwx|isSystem|isReadOnly|isHidden|2reserved

            var arr = new BitArray(BitConverter.GetBytes(val));

            var attr = GetAttributes(val);
            

        }

        public static Attributes GetAttributes(UInt16 val)
        {
            var arr = new BitArray(BitConverter.GetBytes(val));
            var arr2 = new BitVector32(val);

            byte[] bytes2 = new byte[4];

            var sect0 = BitVector32.CreateSection(3);
            var sect1 = BitVector32.CreateSection(7, sect0);
            var sect2 = BitVector32.CreateSection(7, sect1);
            var sect3 = BitVector32.CreateSection(7, sect2);
            var sect4 = BitVector32.CreateSection(7, sect3);

            var o = arr2[sect0];
            var a = arr2[sect1];
            var b = arr2[sect2];
            var c = arr2[sect3];
            var d = arr2[sect4];

            arr2[sect4] = 3;

            //if (arr2[29])
            //    bytes2[0] += 1;
            //if (arr2[28])
            //    bytes2[0] += 2;
            //if (arr2[27])
            //    bytes2[0] += 4;

            //if (arr2[5])
            //    bytes2[1] += 1;
            //if (arr2[6])
            //    bytes2[1] += 2;
            //if (arr2[7])
            //    bytes2[1] += 4;

            //if (arr2[8])
            //    bytes2[2] += 1;
            //if (arr2[9])
            //    bytes2[2] += 2;
            //if (arr2[10])
            //    bytes2[2] += 4;

            //if (arr2[11])
            //    bytes2[3] += 1;
            //if (arr2[12])
            //    bytes2[3] += 2;
            //if (arr2[13])
            //    bytes2[3] += 4;




            bool[] bools = new bool[3];

            bools[0] = arr[2];
            bools[1] = arr[3];
            bools[2] = arr[4];

            byte[] bytes = new byte[4];

            new BitArray(bools).CopyTo(bytes, 0);

            bools[0] = arr[5];
            bools[1] = arr[6];
            bools[2] = arr[7];

            new BitArray(bools).CopyTo(bytes, 1);

            bools[0] = arr[8];
            bools[1] = arr[9];
            bools[2] = arr[10];

            new BitArray(bools).CopyTo(bytes, 2);

            bools[0] = arr[11];
            bools[1] = arr[12];
            bools[2] = arr[13];

            new BitArray(bools).CopyTo(bytes, 3);

            var attr = new Attributes()
            {
                Flags = bytes[0],
                OtherPerm = bytes[1],
                GroupPerm = bytes[2],
                OwnerPerm = bytes[3]
            };
            return attr;
        }

        public struct Attributes
        {
            public byte OwnerPerm;
            public byte GroupPerm;
            public byte OtherPerm;
            public byte Flags;
        }

        public static bool[] ToBoolArray(UInt32 num)
        {
            //BitArray bitArray = new BitArray(BitConverter.GetBytes(num)).;
            return Convert.ToString(num, 2).Select(s => s.Equals('1')).ToArray();
        }

        public static bool[] ToBoolArray(UInt16 num)
        {
            return Convert.ToString(num, 2).Select(s => s.Equals('1')).ToArray();
        }
        public static bool[] ToBoolArray(Byte num)
        {
            return Convert.ToString(num, 2).Select(s => s.Equals('1')).ToArray();
        }

        public static UInt32 BoolArrayToUInt32(bool[] bits)
        {
            if (bits.Length is not 32) 
                throw new ArgumentException("Can only fit 32 bits in a uint");

            UInt32 r = 0;
            for (int i = 0; i < bits.Length; i++) 
                if (bits[i]) r |= (uint)(1 << (bits.Length - i));
            return r;
        }
        public static UInt16 BoolArrayToUInt16(bool[] bits)
        {
            if (bits.Length is not 16) 
                throw new ArgumentException("Can only fit 16 bits");

            UInt16 r = 0;
            for (int i = 0; i < bits.Length; i++) 
                if (bits[i]) r |= (UInt16)(1 << (bits.Length - i));
            return r;
        }

        public static Byte BoolArrayToByte(bool[] bits)
        {
            if (bits.Length is not 8)
                throw new ArgumentException("Can only fit 8 bits");

            Byte r = 0;
            for (int i = 0; i < bits.Length; i++)
                if (bits[i]) r |= (Byte)(1 << (bits.Length - i));
            return r;
        }

    }
}