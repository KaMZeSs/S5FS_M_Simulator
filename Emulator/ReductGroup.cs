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
    public partial class ReductGroup : Form
    {
        (UInt16, String, UInt16[])[] groups;
        (UInt16, String, UInt16, String)[] users;
        UInt16 group_id_to_change;
        UInt16 index;

        public ref (UInt16, String, UInt16[]) ChangedGroup
        {
            get
            {
                return ref groups[index];
            }
        }

        List<UInt16> users_in_group;

        public ReductGroup(ref (UInt16, String, UInt16[])[] groups,
        ref (UInt16, String, UInt16, String)[] users, UInt16 group_id_to_change)
        {
            InitializeComponent();
            this.groups = groups;
            this.users = users;
            this.group_id_to_change = group_id_to_change;
            

            for (UInt16 i = 0; i < groups.Length; i++)
            {
                if (groups[i].Item1 == group_id_to_change)
                {
                    index = i;
                    break;
                }
            }

            this.users_in_group = groups[index].Item3.ToList();
        }

        private void DeleteUser_button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is null)
                return;

            var selected_user = users.First(x => x.Item2.Equals(listBox1.SelectedItem.ToString()));

            if (selected_user.Item3 == group_id_to_change)
            {
                MessageBox.Show("Нельзя удалить пользователя из его основной группы");
                return;
            }

            users_in_group.Remove(selected_user.Item1);

            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void ReductGroup_Activated(object sender, EventArgs e)
        {
            this.textBox1.Text = this.ChangedGroup.Item2;
            this.listBox1.Items.Clear();

            foreach (var user_id in users_in_group)
            {
                try
                {
                    listBox1.Items.Add(users.First(x => x.Item1 == user_id).Item2);
                }
                catch { }
            }
        }

        private void AddUser_button_Click(object sender, EventArgs e)
        {
            Accounts accounts = new(ref users, ref groups, true);

            var dialog_result = accounts.ShowDialog(this);
            if (dialog_result is not DialogResult.OK)
                return;

            var user_id = accounts.user_id;

            if (listBox1.Items.Contains(users.First(x => x.Item1 == user_id).Item2))
            {
                MessageBox.Show("Данный пользователь уже состоит в этой группе");
                return;
            }

            users_in_group.Add(user_id);

            listBox1.Items.Add(users.First(x => x.Item1 == user_id).Item2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var group_name = this.textBox1.Text.Trim();
            if (group_name.Length is 0)
            {
                MessageBox.Show("Название группы не должно быть пустым");
                return;
            }

            var isNameChanged = !groups[index].Item2.Equals(group_name);

            if (isNameChanged && groups.Count(x => x.Item2.Equals(group_name)) is not 0)
            {
                MessageBox.Show("Группа с таким названием уже существует");
                return;
            }

            groups[index] = new(group_id_to_change,
                group_name, users_in_group.ToArray());

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
