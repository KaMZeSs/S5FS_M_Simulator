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
        (UInt16, String)[] groups;

        public Accounts((UInt16, String, UInt16, String)[] users, (UInt16, String)[] groups)
        {
            InitializeComponent();
            this.users = users;
            this.groups = groups;
        }

        private void Accounts_Load(object sender, EventArgs e)
        {

        }

        
    }
}
