using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emulator
{
    public partial class AddUser : Form
    {
        String[] users;

        /// <summary>
        /// Данные о пользователе
        /// Item1: UserName
        /// Item2: PasswordHash (SHA256)
        /// </summary>
        public (String, String) User { get; private set; }

        public AddUser(String[] users)
        {
            InitializeComponent();
            this.users = users;
            this.ActiveControl = textBox1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
                MessageBox.Show("Логин не должен содержать \"$\""); ;
                return;
            }

            if (login.Contains(':'))
            {
                MessageBox.Show("Логин не должен содержать \":\""); ;
                return;
            }

            if (users.Contains(login))
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
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(textBox2.Text));
                // Get the hashed string.  
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower(); // 64 символа

                this.User = new(login, hash);
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
