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

        public (String, String) User { get; private set; }

        public AddUser(String[] users)
        {
            InitializeComponent();
            this.users = users;
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
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                this.User = new(login, hash);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
