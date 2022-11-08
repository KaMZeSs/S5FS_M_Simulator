using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emulator
{
    // Я хз, что придумать
    // Можно из главной формы вызывать эту для просмотра, или для выбора нужного пользователя из списка при его изменении, или удалении
    // Когда для изменения: 1) открыл эту 2) выбрал нужного 3) Скрыл эту 4) Открыл вторую 5) Изменил 6) Закрыл вторую
    // 7) Открыл первую
    // И так пока не отмена, либо без 7 го шага
    // Аналогично группы
    // Менять, удалять, добавлять пользователей может ток суперюзер
    // У пользователя можно менять имя, группу, пароль
    // У группы можно менять название
    // Группу нельзя удалить, пока в ней кто-то состоит
    // Добавлять пользователей в группу рута можно, тк она не дает прав суперпользователя


    public partial class Accounts : Form 
    {
        (UInt16, String, UInt16, String)[] users;
        (UInt16, String, UInt16[])[] groups;

        public UInt16 user_id { get; private set; }

        public Accounts(ref (UInt16, String, UInt16, String)[] users,
            ref (UInt16, String, UInt16[])[] groups, bool getOne = false)
        {
            InitializeComponent();
            this.users = users;
            this.groups = groups;

            if (getOne)
            {
                this.dataGridView1.CellDoubleClick += 
                    new DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            }
        }

        public void ChangeData(ref (UInt16, String, UInt16, String)[] users,
            ref (UInt16, String, UInt16[])[] groups)
        {
            this.users = users;
            this.groups = groups;
        }

        private void Accounts_Activated(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.None;

            var vs = (from user in users
                      join gr in groups on user.Item3 equals gr.Item1
                      select new { id = user.Item1, name = user.Item2, group_name = gr.Item2 })
                      .ToArray();

            dataGridView1.Rows.Clear();

            foreach (var row in vs)
            {
                dataGridView1.Rows.Add(row.id, row.name, row.group_name);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.user_id = (UInt16)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
