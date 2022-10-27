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
    public partial class TextViewer : Form
    {
        public String TextView { get; private set; }

        public TextViewer(String text)
        {
            InitializeComponent();
            richTextBox1.Text = text;
            
        }

        private void TextViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            var res = MessageBox.Show("Сохранить изменения?", "Сохранить изменения?", MessageBoxButtons.YesNoCancel);
            if (res is DialogResult.Yes)
            {
                TextView = richTextBox1.Text;
                this.DialogResult = DialogResult.OK;
            }
            else if (res is DialogResult.No)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                e.Cancel = true;
            }

        }
    }
}
