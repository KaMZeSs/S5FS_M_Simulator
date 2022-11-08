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

namespace Emulator
{
    public partial class Groups : Form
    {
        (UInt16, String)[] groups;
        public UInt16 group_id { get; private set; }

        public Groups(ref (UInt16, String)[] groups, bool getOne = false)
        {
            InitializeComponent();

            this.groups = groups;

            if (getOne)
            {
                this.dataGridView1.CellDoubleClick +=
                    new DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            }
        }

        public void ChangeData(ref (UInt16, String)[] groups)
        {
            this.groups = groups;
        }

        private void Groups_Activated(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            dataGridView1.Rows.Clear();

            foreach (var row in groups)
            {
                dataGridView1.Rows.Add(row.Item1, row.Item2);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.group_id = (UInt16)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
