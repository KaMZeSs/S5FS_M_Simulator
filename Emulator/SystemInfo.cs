using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emulator
{
    public partial class SystemInfo : Form
    {
        public SystemInfo(S5FS.S5FS fS)
        {
            InitializeComponent();

            var date = DateTime.FromBinary(fS.ReadInode(0).di_ctime).ToString("F");

            var sb = fS.SuperBlock;

            UsedData_Label.Text = $"Использовано: {(sb.s_fsize - 2 - sb.s_tfree) * sb.s_blen} байт";
            FreeData_Label.Text = $"Свободно: {sb.s_tfree * sb.s_blen} байт";
            FullSize_Label.Text = $"Общий размер: {(sb.s_fsize - 2) * sb.s_blen} байт";
            CreationDate_Label.Text = $"Дата создания диска: {date}";
            BlockSize_Label.Text = $"Размер блока: {sb.s_blen} байт";
            FreeBlocks_Label.Text = $"Количество свободных блоков: {sb.s_tfree}";
            FreeInodes_Label.Text = $"Количество свободных инодов: {sb.s_tinode}";
        }
    }
}
