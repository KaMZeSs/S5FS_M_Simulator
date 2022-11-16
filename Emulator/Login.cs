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
    public partial class Login : Form
    {
        (UInt16, String, UInt16, String)[] users;

        public (UInt16?, String, UInt16?, String) LoggedUser { get; private set; }

        public Login(ref (UInt16, String, UInt16, String)[] users)
        {
            InitializeComponent();
            this.users = users;
            this.ActiveControl = textBox1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text.Trim();
            if (login.Length is 0)
            {
                MessageBox.Show("Логин не должен быть пустым");
                return;
            }

            if (login.Contains('$'))
            {
                MessageBox.Show("Пользователь с таким именем не существует");
                return;
            }
            var user = this.users.FirstOrDefault(x => x.Item2.Equals(login));
            if (user.Item2 is null)
            {
                MessageBox.Show("Пользователь с таким именем не существует");
                return;
            }
            if (textBox2.Text.Length < 4)
            {
                MessageBox.Show("Пароль должен быть минимум 4 символа");
                return;
            }

            String hash;
            // GetHash
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(textBox2.Text));
                // Get the hashed string.  
                hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower(); // 64 символа
            }

            if (!user.Item4.Equals(hash))
            {
                MessageBox.Show("Неверный пароль");
                return;
            }

            this.LoggedUser = new(user.Item1, user.Item2, user.Item3, user.Item4);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            this.users = users;
            this.ActiveControl = textBox1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var login = textBox1.Text.Trim();
            if (login.Length is 0)
            {
                MessageBox.Show("Логин не должен быть пустым");
                return;
            }

            if (login.Contains('$'))
            {
                MessageBox.Show("Логин не должен содержать \"$\""); ;
                return;
            }

            if (users.Any(x => x.Item2.Equals(login)))
            {
                MessageBox.Show("Пользователь с таким именем уже существует");
                return;
            }

            if (textBox2.Text.Length < 4)
            {
                MessageBox.Show("Пароль должен быть минимум 4 символа");
                return;
            }

            // GetHash
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(textBox2.Text));
                // Get the hashed string.  
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower(); // 64 символа

                this.LoggedUser = new(null, login, null, hash);
            }

            this.DialogResult = DialogResult.OK;
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
