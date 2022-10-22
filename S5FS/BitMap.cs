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
        public UInt64 start_block;

        public BitMap(ulong array_len, ulong length, ulong start_block)
        {
            this.map = new byte[array_len];
            Array.Fill<byte>(this.map, 0b_1111_1111);
            this.length = length;
            this.start_block = start_block;
        }

        public BitMap(byte[] array, ulong length, ulong start_block)
        {
            this.map = array;
            this.length = length;
            this.start_block = start_block;
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
        /// True - блок свободен, False - блок занят.
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


        /// <summary>
        /// Найти первый пустой блок/инод.
        /// </summary>
        /// <returns>Номер пустого блока/инода. 0 означает, что пустых блоков/инодов больше нет (так как нулевой блок/инод всегда использован системой)</returns>
        public UInt64 FirstEmpty()
        {
            for (UInt64 i = 1; i < this.length; i++)
            {
                if (this.isBlockEmpty(i))
                {
                    return i;
                }
            }
            return 0;
        }

        public UInt64[] GetNEmpty(UInt64 num)
        {
            var adresses = new UInt64[num];
            for (UInt64 i = 1, counter = 0; i < this.length && counter < num; i++)
            {
                if (this.isBlockEmpty(i))
                {
                    adresses[counter++] = i;
                }
            }
            
            return adresses;
        }
    }
}
