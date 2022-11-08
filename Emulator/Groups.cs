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
        (UInt16, String, UInt16[])[] groups;
        (UInt16, String, UInt16, String)[] users;
        public UInt16 group_id { get; private set; }

        public Groups(ref (UInt16, String, UInt16, String)[] users,
            ref (UInt16, String, UInt16[])[] groups, bool getOne = false)
        {
            InitializeComponent();

            this.groups = groups;
            this.users = users;

            if (getOne)
            {
                this.dataGridView1.CellDoubleClick +=
                    new DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            }
        }

        public void ChangeData(ref (UInt16, String, UInt16, String)[] users,
            ref (UInt16, String, UInt16[])[] groups)
        {
            this.groups = groups;
            this.users = users;
        }

        private void Groups_Activated(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            dataGridView1.Rows.Clear();

            foreach (var row in groups)
            {
                var users = String.Join(", ", 
                    from user in this.users 
                    where row.Item3.Contains(user.Item1) 
                    select user.Item2);
                dataGridView1.Rows.Add(row.Item1, row.Item2, users);
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
