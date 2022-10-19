using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    /// <summary>
    /// Массив байт, который содержит о свободных/занятых блоках/кластерах. Для определения, что iй блок пуст/заполнен, необходимо проанализировать iй бит.
    /// Содержится в n-ом к-ве первых кластеров. Данные кластеры входят в карту.
    /// </summary>
    internal class BitMap 
    {
        /// <summary>
        /// Битовая карта
        /// </summary>
        public byte[] map;
        public UInt64 length;

        public BitMap(byte[] map, ulong length)
        {
            this.map = map;
            this.length = length;
        }

        /// <summary>
        /// Проверка на занятость блока.
        /// </summary>
        /// <param name="block_number">Номер блока</param>
        /// <returns>Булевое значение занятости блока. True - блок свободен, False - блок занят.</returns>
        public bool isBlockEmpty(UInt64 block_number)
        {
            if (block_number >= this.length || block_number < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Byte bit_num = (byte)(block_number % 8);
            UInt64 byte_num = block_number / 8;
            System.Collections.BitArray bitArray = new System.Collections.BitArray(new byte[] { map[byte_num] });
            return bitArray.Get(bit_num);
        }

        /// <summary>
        /// Изменить состояние блока.
        /// </summary>
        /// <param name="block_number">Номер блока.</param>
        /// <param name="value">Новое состояние.</param>
        public void ChangeBlockState(UInt64 block_number, bool value)
        {
            if (block_number >= this.length || block_number < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            Byte bit_num = (byte)(block_number % 8);
            UInt64 byte_num = block_number / 8;
            System.Collections.BitArray bitArray = new System.Collections.BitArray(new byte[] { map[byte_num] });
            bitArray.Set(bit_num, value);
            map[byte_num] = ConvertToByte(bitArray);
        }

        /// <summary>
        /// Конвертация массива из 8 битов в один байт
        /// </summary>
        /// <param name="bits">Массив битов.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
