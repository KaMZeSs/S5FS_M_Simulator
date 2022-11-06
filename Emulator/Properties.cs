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
    public partial class Properties : Form
    {
        public Obj obj;
        ushort curr_user_id;
        KeyValuePair<int, String>[] users;

        public Properties(Obj obj, ushort curr_user_id, KeyValuePair<int, String>[] users)
        {
            InitializeComponent();
            this.obj = obj;
            this.users = users;
            this.curr_user_id = curr_user_id;
        }

        private void Properties_Load(object sender, EventArgs e)
        {
            this.Name_Label.Text += obj.Name;
            this.Size_Label.Text += obj.GetSize;
            this.Creator_Label.Text += users.First(x => x.Key == obj.UserID).Value;
            this.CreationDate_Label.Text += obj.CreationTime.ToString("f");
            this.ModificationDate_Label.Text += obj.ChangeDateTime.ToString("f");
            this.ReadDate_Label.Text += obj.ReadDateTime.ToString("f");
            
            this.isHidden_Check.Checked = obj.IsHidden;
            this.isReadOnly_Check.Checked = obj.IsReadOnly;
            this.isSystem_Check.Checked = obj.IsSystem;

            this.UserRead_Check.Checked = obj.OwnerPermissions.CanRead;
            this.UserWrite_Check.Checked = obj.OwnerPermissions.CanWrite;
            this.UserExecute_Check.Checked = obj.OwnerPermissions.CanExecute;

            this.GroupRead_Check.Checked = obj.GroupPermissions.CanRead;
            this.GroupWrite_Check.Checked = obj.GroupPermissions.CanWrite;
            this.GroupExecute_Check.Checked = obj.GroupPermissions.CanExecute;

            this.OtherRead_Check.Checked = obj.OtherPermissions.CanRead;
            this.OtherWrite_Check.Checked = obj.OtherPermissions.CanWrite;
            this.OtherExecute_Check.Checked = obj.OtherPermissions.CanExecute;


            // Если зашел не создатель, или рут - онли смотреть
            if (curr_user_id is not 0 || curr_user_id != obj.UserID)
            {
                this.isHidden_Check.Click += isSystem_Check_Click;
                this.isReadOnly_Check.Click += isSystem_Check_Click;

                this.UserRead_Check.Click += isSystem_Check_Click;
                this.UserWrite_Check.Click += isSystem_Check_Click;
                this.UserExecute_Check.Click += isSystem_Check_Click;

                this.GroupRead_Check.Click += isSystem_Check_Click;
                this.GroupWrite_Check.Click += isSystem_Check_Click;
                this.GroupExecute_Check.Click += isSystem_Check_Click;

                this.OtherRead_Check.Click += isSystem_Check_Click;
                this.OtherWrite_Check.Click += isSystem_Check_Click;
                this.OtherExecute_Check.Click += isSystem_Check_Click;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var flags = (byte)((this.isHidden_Check.Checked ? 1 : 0) +
                    (this.isReadOnly_Check.Checked ? 2 : 0) +
                    (this.isSystem_Check.Checked ? 4 : 0));

            var owner = (byte)((this.UserExecute_Check.Checked ? 1 : 0) +
                (this.UserWrite_Check.Checked ? 2 : 0) +
                (this.UserRead_Check.Checked ? 4 : 0));

            var group = (byte)((this.GroupExecute_Check.Checked ? 1 : 0) +
                (this.GroupWrite_Check.Checked ? 2 : 0) +
                (this.GroupRead_Check.Checked ? 4 : 0));

            var other = (byte)((this.OtherExecute_Check.Checked ? 1 : 0) +
                (this.OtherWrite_Check.Checked ? 2 : 0) +
                (this.OtherRead_Check.Checked ? 4 : 0));

            obj.attr = S5FS.Inode.Attributes.ChangeAttributes(obj.attr,
                OwnerPerm: owner, GroupPerm: group, OtherPerm: other, Flags: flags);

            obj.inode.di_mode = S5FS.Inode.Attributes.
                AttributesToUInt16(obj.attr, S5FS.Inode.GetInodeType(obj.inode));

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void isSystem_Check_Click(object sender, EventArgs e)
        {
            if (sender is null)
                return;
            if (sender is not CheckBox)
                return;
            (sender as CheckBox).Checked = !(sender as CheckBox).Checked;

        }
    }
}
