namespace Emulator
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файловаяСистемаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вывестиРазмерФСToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вывестиСвободноеМестоToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.папкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.переименоватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.свойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пользовательToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сменитьПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вывестиСписокПользователейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FileID_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileType_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileSize_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileCreation_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileModification_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileRead_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileOwner_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileUPerm_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileGPerm_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileOPerm_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSystem_Column = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsReadOnly_Column = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsVisible_Column = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 34);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(35, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(765, 34);
            this.panel3.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(765, 33);
            this.textBox1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(35, 34);
            this.panel2.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файловаяСистемаToolStripMenuItem,
            this.пользовательToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файловаяСистемаToolStripMenuItem
            // 
            this.файловаяСистемаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вывестиРазмерФСToolStripMenuItem,
            this.вывестиСвободноеМестоToolStripMenuItem,
            this.создатьToolStripMenuItem,
            this.переименоватьToolStripMenuItem,
            this.удалитьToolStripMenuItem,
            this.свойстваToolStripMenuItem,
            this.открытьToolStripMenuItem});
            this.файловаяСистемаToolStripMenuItem.Name = "файловаяСистемаToolStripMenuItem";
            this.файловаяСистемаToolStripMenuItem.Size = new System.Drawing.Size(121, 20);
            this.файловаяСистемаToolStripMenuItem.Text = "Файловая система";
            // 
            // вывестиРазмерФСToolStripMenuItem
            // 
            this.вывестиРазмерФСToolStripMenuItem.Name = "вывестиРазмерФСToolStripMenuItem";
            this.вывестиРазмерФСToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.вывестиРазмерФСToolStripMenuItem.Text = "Вывести размер ФС";
            // 
            // вывестиСвободноеМестоToolStripMenuItem
            // 
            this.вывестиСвободноеМестоToolStripMenuItem.Name = "вывестиСвободноеМестоToolStripMenuItem";
            this.вывестиСвободноеМестоToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.вывестиСвободноеМестоToolStripMenuItem.Text = "Вывести свободное место";
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.папкуToolStripMenuItem});
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.создатьToolStripMenuItem.Text = "Создать";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.файлToolStripMenuItem.Text = "Файл";
            this.файлToolStripMenuItem.Click += new System.EventHandler(this.CreateFileToolStripMenuItem_Click);
            // 
            // папкуToolStripMenuItem
            // 
            this.папкуToolStripMenuItem.Name = "папкуToolStripMenuItem";
            this.папкуToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.папкуToolStripMenuItem.Text = "Папку";
            this.папкуToolStripMenuItem.Click += new System.EventHandler(this.CreateFileToolStripMenuItem_Click);
            // 
            // переименоватьToolStripMenuItem
            // 
            this.переименоватьToolStripMenuItem.Name = "переименоватьToolStripMenuItem";
            this.переименоватьToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.переименоватьToolStripMenuItem.Text = "Переименовать";
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            // 
            // свойстваToolStripMenuItem
            // 
            this.свойстваToolStripMenuItem.Name = "свойстваToolStripMenuItem";
            this.свойстваToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.свойстваToolStripMenuItem.Text = "Свойства";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            // 
            // пользовательToolStripMenuItem
            // 
            this.пользовательToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сменитьПользователяToolStripMenuItem,
            this.удалитьПользователяToolStripMenuItem,
            this.вывестиСписокПользователейToolStripMenuItem});
            this.пользовательToolStripMenuItem.Name = "пользовательToolStripMenuItem";
            this.пользовательToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.пользовательToolStripMenuItem.Text = "Пользователь";
            // 
            // сменитьПользователяToolStripMenuItem
            // 
            this.сменитьПользователяToolStripMenuItem.Name = "сменитьПользователяToolStripMenuItem";
            this.сменитьПользователяToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.сменитьПользователяToolStripMenuItem.Text = "Сменить пользователя";
            // 
            // удалитьПользователяToolStripMenuItem
            // 
            this.удалитьПользователяToolStripMenuItem.Name = "удалитьПользователяToolStripMenuItem";
            this.удалитьПользователяToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.удалитьПользователяToolStripMenuItem.Text = "Удалить пользователя";
            // 
            // вывестиСписокПользователейToolStripMenuItem
            // 
            this.вывестиСписокПользователейToolStripMenuItem.Name = "вывестиСписокПользователейToolStripMenuItem";
            this.вывестиСписокПользователейToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.вывестиСписокПользователейToolStripMenuItem.Text = "Вывести список пользователей";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileID_Column,
            this.FileName_Column,
            this.FileType_Column,
            this.FileSize_Column,
            this.FileCreation_Column,
            this.FileModification_Column,
            this.FileRead_Column,
            this.FileOwner_Column,
            this.FileUPerm_Column,
            this.FileGPerm_Column,
            this.FileOPerm_Column,
            this.IsSystem_Column,
            this.IsReadOnly_Column,
            this.IsVisible_Column});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 58);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(800, 392);
            this.dataGridView1.TabIndex = 2;
            // 
            // FileID_Column
            // 
            this.FileID_Column.HeaderText = "ID";
            this.FileID_Column.Name = "FileID_Column";
            this.FileID_Column.ReadOnly = true;
            this.FileID_Column.Visible = false;
            // 
            // FileName_Column
            // 
            this.FileName_Column.HeaderText = "Имя";
            this.FileName_Column.Name = "FileName_Column";
            this.FileName_Column.ReadOnly = true;
            // 
            // FileType_Column
            // 
            this.FileType_Column.HeaderText = "Тип";
            this.FileType_Column.Name = "FileType_Column";
            this.FileType_Column.ReadOnly = true;
            // 
            // FileSize_Column
            // 
            this.FileSize_Column.HeaderText = "Размер";
            this.FileSize_Column.Name = "FileSize_Column";
            this.FileSize_Column.ReadOnly = true;
            // 
            // FileCreation_Column
            // 
            this.FileCreation_Column.HeaderText = "Дата создания";
            this.FileCreation_Column.Name = "FileCreation_Column";
            this.FileCreation_Column.ReadOnly = true;
            // 
            // FileModification_Column
            // 
            this.FileModification_Column.HeaderText = "Дата последнего изменения";
            this.FileModification_Column.Name = "FileModification_Column";
            // 
            // FileRead_Column
            // 
            this.FileRead_Column.HeaderText = "Дата последнего чтения";
            this.FileRead_Column.Name = "FileRead_Column";
            this.FileRead_Column.ReadOnly = true;
            // 
            // FileOwner_Column
            // 
            this.FileOwner_Column.HeaderText = "Владелец файла";
            this.FileOwner_Column.Name = "FileOwner_Column";
            this.FileOwner_Column.ReadOnly = true;
            // 
            // FileUPerm_Column
            // 
            this.FileUPerm_Column.HeaderText = "Разрешения владельца";
            this.FileUPerm_Column.Name = "FileUPerm_Column";
            this.FileUPerm_Column.ReadOnly = true;
            // 
            // FileGPerm_Column
            // 
            this.FileGPerm_Column.HeaderText = "Разрешения группы";
            this.FileGPerm_Column.Name = "FileGPerm_Column";
            this.FileGPerm_Column.ReadOnly = true;
            // 
            // FileOPerm_Column
            // 
            this.FileOPerm_Column.HeaderText = "Разрешения остальнох";
            this.FileOPerm_Column.Name = "FileOPerm_Column";
            this.FileOPerm_Column.ReadOnly = true;
            // 
            // IsSystem_Column
            // 
            this.IsSystem_Column.HeaderText = "Системный";
            this.IsSystem_Column.Name = "IsSystem_Column";
            this.IsSystem_Column.ReadOnly = true;
            this.IsSystem_Column.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsSystem_Column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.IsSystem_Column.Visible = false;
            // 
            // IsReadOnly_Column
            // 
            this.IsReadOnly_Column.HeaderText = "Только для чтения";
            this.IsReadOnly_Column.Name = "IsReadOnly_Column";
            this.IsReadOnly_Column.ReadOnly = true;
            // 
            // IsVisible_Column
            // 
            this.IsVisible_Column.HeaderText = "Видимый";
            this.IsVisible_Column.Name = "IsVisible_Column";
            this.IsVisible_Column.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private TextBox textBox1;
        private Panel panel2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файловаяСистемаToolStripMenuItem;
        private ToolStripMenuItem вывестиРазмерФСToolStripMenuItem;
        private ToolStripMenuItem вывестиСвободноеМестоToolStripMenuItem;
        private ToolStripMenuItem пользовательToolStripMenuItem;
        private ToolStripMenuItem сменитьПользователяToolStripMenuItem;
        private ToolStripMenuItem удалитьПользователяToolStripMenuItem;
        private ToolStripMenuItem вывестиСписокПользователейToolStripMenuItem;
        private DataGridView dataGridView1;
        private ToolStripMenuItem создатьToolStripMenuItem;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem папкуToolStripMenuItem;
        private ToolStripMenuItem переименоватьToolStripMenuItem;
        private ToolStripMenuItem удалитьToolStripMenuItem;
        private ToolStripMenuItem свойстваToolStripMenuItem;
        private ToolStripMenuItem открытьToolStripMenuItem;
        private DataGridViewTextBoxColumn FileID_Column;
        private DataGridViewTextBoxColumn FileName_Column;
        private DataGridViewTextBoxColumn FileType_Column;
        private DataGridViewTextBoxColumn FileSize_Column;
        private DataGridViewTextBoxColumn FileCreation_Column;
        private DataGridViewTextBoxColumn FileModification_Column;
        private DataGridViewTextBoxColumn FileRead_Column;
        private DataGridViewTextBoxColumn FileOwner_Column;
        private DataGridViewTextBoxColumn FileUPerm_Column;
        private DataGridViewTextBoxColumn FileGPerm_Column;
        private DataGridViewTextBoxColumn FileOPerm_Column;
        private DataGridViewCheckBoxColumn IsSystem_Column;
        private DataGridViewCheckBoxColumn IsReadOnly_Column;
        private DataGridViewCheckBoxColumn IsVisible_Column;
    }
}