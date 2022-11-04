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
    public partial class NameGetForm : Form
    {
        public String Result { get; private set; }
        private String[] exist;

        public NameGetForm(String[] exist)
        {
            InitializeComponent();
            this.exist = exist;
            this.DialogResult = DialogResult.Cancel;
        }

        public NameGetForm(String[] exist, String text)
        {
            InitializeComponent();
            this.exist = exist;
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = textBox1.Text.Trim();
            if (text.Length is 0)
            {
                MessageBox.Show("Имя не может быть пустым");
                return;
            }
            if (exist.Contains(text))
            {
                MessageBox.Show("Имя уже существует");
                return;
            }
            if (Encoding.UTF8.GetByteCount(text.ToArray()) > 60)
            {
                MessageBox.Show("Имя слишком большое");
                return;
            }
            Result = text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
