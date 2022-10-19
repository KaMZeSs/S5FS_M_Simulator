using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S5FS
{
    /// <summary>
    /// Основной клас для взаимодействия с файловой системой.
    /// </summary>
    internal class S5FS
    {
        SuperBlock sb;
        BitMap bm;
        BinaryWriter bw;

        /// <summary>
        /// Создание новой файловой системы.
        /// </summary>
        /// <param name="file">Путь к файлу.</param>
        /// <param name="s_blen">Размер блока.</param>
        /// <param name="disk_size">Размер диска.</param>
        /// <returns>Один экземпляр файловой системы.</returns>
        public static S5FS format(String file, UInt32 s_blen, UInt64 disk_size)
        {
            S5FS s5FS = new();
            s5FS.bw = new(File.OpenWrite(file));
            
            //Суперблок
            s5FS.sb = new(disk_size: disk_size, s_blen: s_blen);
            byte[] buffer = SuperBlock.SaveToByteArray(s5FS.sb);
            s5FS.bw.Write(buffer);
            s5FS.bw.Flush();

            //Записать место под индексные
            buffer = new byte[s5FS.sb.s_isize * 144];
            s5FS.bw.Write(buffer);
            s5FS.bw.Flush();


            return s5FS;
        }

        public void UpdateInodes()
        {

        }
    }
}
