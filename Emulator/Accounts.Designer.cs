namespace Emulator
{
    partial class Accounts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.UserID_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserGroup_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Empty_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 414);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 36);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserID_Column,
            this.UserName_Column,
            this.UserGroup_Column,
            this.Empty_Column});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(800, 414);
            this.dataGridView1.TabIndex = 1;
            // 
            // UserID_Column
            // 
            this.UserID_Column.HeaderText = "ID";
            this.UserID_Column.Name = "UserID_Column";
            this.UserID_Column.ReadOnly = true;
            // 
            // UserName_Column
            // 
            this.UserName_Column.HeaderText = "Логин";
            this.UserName_Column.Name = "UserName_Column";
            this.UserName_Column.ReadOnly = true;
            // 
            // UserGroup_Column
            // 
            this.UserGroup_Column.HeaderText = "Группа";
            this.UserGroup_Column.Name = "UserGroup_Column";
            this.UserGroup_Column.ReadOnly = true;
            // 
            // Empty_Column
            // 
            this.Empty_Column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Empty_Column.HeaderText = "";
            this.Empty_Column.Name = "Empty_Column";
            this.Empty_Column.ReadOnly = true;
            // 
            // Accounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "Accounts";
            this.Text = "Accounts";
            this.Load += new System.EventHandler(this.Accounts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn UserID_Column;
        private DataGridViewTextBoxColumn UserName_Column;
        private DataGridViewTextBoxColumn UserGroup_Column;
        private DataGridViewTextBoxColumn Empty_Column;
    }
}