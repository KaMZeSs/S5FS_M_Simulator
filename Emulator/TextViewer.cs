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
        bool isChanged;

        public TextViewer(String text)
        {
            InitializeComponent();
            richTextBox1.Text = text;

            isChanged = false;
        }

        private void TextViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isChanged)
            {
                TextView = richTextBox1.Text;
                this.DialogResult = DialogResult.Cancel;
                return;
            }
                
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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            isChanged = true;
        }
    }
}
