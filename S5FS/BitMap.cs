using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    internal class BitMap
    {
        public byte[] map;

        public bool isBlockEmpty(UInt64 block_number)
        {
            
            Byte bit_num = (byte)(block_number % 8);
            UInt64 byte_num = block_number / 8;
            System.Collections.BitArray bitArray = new System.Collections.BitArray(new byte[] { map[byte_num] });
            return bitArray.Get(bit_num);
        }

        public void ChangeBlockState(UInt64 block_number, bool value)
        {

            Byte bit_num = (byte)(block_number % 8);
            UInt64 byte_num = block_number / 8;
            System.Collections.BitArray bitArray = new System.Collections.BitArray(new byte[] { map[byte_num] });
            bitArray.Set(bit_num, value);
            map[byte_num] = ConvertToByte(bitArray);
        }

        byte ConvertToByte(System.Collections.BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }
    }
}
