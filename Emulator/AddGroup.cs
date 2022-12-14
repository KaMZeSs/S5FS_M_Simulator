using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.Logging;

namespace Emulator
{
    public partial class AddGroup : Form
    {
        String[] groups;
        public String GroupName { get; private set; }

        public AddGroup(String[] groups)
        {
            InitializeComponent();
            this.groups = groups;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var group = this.textBox1.Text.Trim();
            if (group.Length is 0)
            {
                MessageBox.Show("Название группы не должно быть пустым");
                return;
            }

            if (group.Contains('$'))
            {
                MessageBox.Show("Название группы не должно содержать \"$\"");
                return;
            }

            if (groups.Contains(group))
            {
                MessageBox.Show("Группа с таким названием уже существует");
                return;
            }

            this.GroupName = group;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Contains('$'))
            {
                var vs = this.textBox1.SelectionStart;
                this.textBox1.Text = this.textBox1.Text.Replace("$", String.Empty);
                this.textBox1.SelectionStart = vs - 1;
            }
        }
    }
}
