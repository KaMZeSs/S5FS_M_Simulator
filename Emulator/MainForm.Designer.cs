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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.copied_label = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файловаяСистемаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вывестиРазмерФСToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.папкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.переименоватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.свойстваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вырезатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьСсылкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вставитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пользовательToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вывестиСписокПользователейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сменитьПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.группаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вывестиСписокГруппToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьГруппуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьГруппуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.FileID_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileType_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileSize_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Permissions_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileCreation_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileModification_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileRead_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileOwner_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileGroup_Column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSystem_Column = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsReadOnly_Column = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsHidden_Column = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.EmptyColumns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.File_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.открытьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.переименоватьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.копироватьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.вырезатьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьСсылкуToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.свойстваToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.System_ContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.вставитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.создатьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.файлToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.папкуToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.вывестиИнформациюОФСToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьБуферОбменаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.удалитьГруппуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.праваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьВладельцаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изменитьГруппуToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.File_ContextMenu.SuspendLayout();
            this.System_ContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(922, 34);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.textBox1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(35, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(887, 34);
            this.panel3.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(669, 33);
            this.textBox1.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.copied_label);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(669, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(218, 34);
            this.panel4.TabIndex = 1;
            // 
            // copied_label
            // 
            this.copied_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.copied_label.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.copied_label.Location = new System.Drawing.Point(0, 0);
            this.copied_label.Margin = new System.Windows.Forms.Padding(0);
            this.copied_label.Name = "copied_label";
            this.copied_label.Size = new System.Drawing.Size(218, 34);
            this.copied_label.TabIndex = 0;
            this.copied_label.Text = "Нет скопированных элементов";
            this.copied_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(35, 34);
            this.panel2.TabIndex = 0;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файловаяСистемаToolStripMenuItem,
            this.пользовательToolStripMenuItem,
            this.группаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(922, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файловаяСистемаToolStripMenuItem
            // 
            this.файловаяСистемаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вывестиРазмерФСToolStripMenuItem,
            this.создатьToolStripMenuItem,
            this.переименоватьToolStripMenuItem,
            this.удалитьToolStripMenuItem,
            this.свойстваToolStripMenuItem,
            this.открытьToolStripMenuItem,
            this.копироватьToolStripMenuItem,
            this.вырезатьToolStripMenuItem,
            this.создатьСсылкуToolStripMenuItem,
            this.вставитьToolStripMenuItem});
            this.файловаяСистемаToolStripMenuItem.Name = "файловаяСистемаToolStripMenuItem";
            this.файловаяСистемаToolStripMenuItem.Size = new System.Drawing.Size(121, 20);
            this.файловаяСистемаToolStripMenuItem.Text = "Файловая система";
            // 
            // вывестиРазмерФСToolStripMenuItem
            // 
            this.вывестиРазмерФСToolStripMenuItem.Name = "вывестиРазмерФСToolStripMenuItem";
            this.вывестиРазмерФСToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.вывестиРазмерФСToolStripMenuItem.Text = "Вывести информацию о ФС";
            this.вывестиРазмерФСToolStripMenuItem.Click += new System.EventHandler(this.вывестиРазмерФСToolStripMenuItem_Click);
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.папкуToolStripMenuItem});
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
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
            this.переименоватьToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.переименоватьToolStripMenuItem.Text = "Переименовать";
            this.переименоватьToolStripMenuItem.Click += new System.EventHandler(this.переименоватьToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // свойстваToolStripMenuItem
            // 
            this.свойстваToolStripMenuItem.Name = "свойстваToolStripMenuItem";
            this.свойстваToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.свойстваToolStripMenuItem.Text = "Свойства";
            this.свойстваToolStripMenuItem.Click += new System.EventHandler(this.свойстваToolStripMenuItem1_Click);
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // копироватьToolStripMenuItem
            // 
            this.копироватьToolStripMenuItem.Name = "копироватьToolStripMenuItem";
            this.копироватьToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.копироватьToolStripMenuItem.Text = "Копировать";
            this.копироватьToolStripMenuItem.Click += new System.EventHandler(this.копироватьToolStripMenuItem_Click);
            // 
            // вырезатьToolStripMenuItem
            // 
            this.вырезатьToolStripMenuItem.Name = "вырезатьToolStripMenuItem";
            this.вырезатьToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.вырезатьToolStripMenuItem.Text = "Вырезать";
            this.вырезатьToolStripMenuItem.Click += new System.EventHandler(this.вырезатьToolStripMenuItem_Click);
            // 
            // создатьСсылкуToolStripMenuItem
            // 
            this.создатьСсылкуToolStripMenuItem.Name = "создатьСсылкуToolStripMenuItem";
            this.создатьСсылкуToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.создатьСсылкуToolStripMenuItem.Text = "Создать ссылку";
            this.создатьСсылкуToolStripMenuItem.Click += new System.EventHandler(this.создатьСсылкуToolStripMenuItem_Click);
            // 
            // вставитьToolStripMenuItem
            // 
            this.вставитьToolStripMenuItem.Name = "вставитьToolStripMenuItem";
            this.вставитьToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.вставитьToolStripMenuItem.Text = "Вставить";
            this.вставитьToolStripMenuItem.Click += new System.EventHandler(this.вставитьToolStripMenuItem_Click);
            // 
            // пользовательToolStripMenuItem
            // 
            this.пользовательToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вывестиСписокПользователейToolStripMenuItem,
            this.сменитьПользователяToolStripMenuItem,
            this.удалитьПользователяToolStripMenuItem,
            this.создатьПользователяToolStripMenuItem});
            this.пользовательToolStripMenuItem.Name = "пользовательToolStripMenuItem";
            this.пользовательToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.пользовательToolStripMenuItem.Text = "Пользователь";
            // 
            // вывестиСписокПользователейToolStripMenuItem
            // 
            this.вывестиСписокПользователейToolStripMenuItem.Name = "вывестиСписокПользователейToolStripMenuItem";
            this.вывестиСписокПользователейToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.вывестиСписокПользователейToolStripMenuItem.Text = "Вывести список пользователей";
            this.вывестиСписокПользователейToolStripMenuItem.Click += new System.EventHandler(this.вывестиСписокПользователейToolStripMenuItem_Click);
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
            this.удалитьПользователяToolStripMenuItem.Click += new System.EventHandler(this.удалитьПользователяToolStripMenuItem_Click);
            // 
            // создатьПользователяToolStripMenuItem
            // 
            this.создатьПользователяToolStripMenuItem.Name = "создатьПользователяToolStripMenuItem";
            this.создатьПользователяToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.создатьПользователяToolStripMenuItem.Text = "Создать пользователя";
            this.создатьПользователяToolStripMenuItem.Click += new System.EventHandler(this.создатьПользователяToolStripMenuItem_Click);
            // 
            // группаToolStripMenuItem
            // 
            this.группаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вывестиСписокГруппToolStripMenuItem,
            this.создатьГруппуToolStripMenuItem,
            this.изменитьГруппуToolStripMenuItem,
            this.удалитьГруппуToolStripMenuItem});
            this.группаToolStripMenuItem.Name = "группаToolStripMenuItem";
            this.группаToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.группаToolStripMenuItem.Text = "Группы";
            // 
            // вывестиСписокГруппToolStripMenuItem
            // 
            this.вывестиСписокГруппToolStripMenuItem.Name = "вывестиСписокГруппToolStripMenuItem";
            this.вывестиСписокГруппToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.вывестиСписокГруппToolStripMenuItem.Text = "Вывести список групп";
            this.вывестиСписокГруппToolStripMenuItem.Click += new System.EventHandler(this.вывестиСписокГруппToolStripMenuItem_Click);
            // 
            // создатьГруппуToolStripMenuItem
            // 
            this.создатьГруппуToolStripMenuItem.Name = "создатьГруппуToolStripMenuItem";
            this.создатьГруппуToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.создатьГруппуToolStripMenuItem.Text = "Создать группу";
            this.создатьГруппуToolStripMenuItem.Click += new System.EventHandler(this.создатьГруппуToolStripMenuItem_Click);
            // 
            // изменитьГруппуToolStripMenuItem
            // 
            this.изменитьГруппуToolStripMenuItem.Name = "изменитьГруппуToolStripMenuItem";
            this.изменитьГруппуToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.изменитьГруппуToolStripMenuItem.Text = "Изменить группу";
            this.изменитьГруппуToolStripMenuItem.Click += new System.EventHandler(this.изменитьГруппуToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileID_Column,
            this.FileName_Column,
            this.FileType_Column,
            this.FileSize_Column,
            this.Permissions_Column,
            this.FileCreation_Column,
            this.FileModification_Column,
            this.FileRead_Column,
            this.FileOwner_Column,
            this.FileGroup_Column,
            this.IsSystem_Column,
            this.IsReadOnly_Column,
            this.IsHidden_Column,
            this.EmptyColumns});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 58);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 15;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(922, 423);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView1_SortCompare);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
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
            // Permissions_Column
            // 
            this.Permissions_Column.HeaderText = "Разрешения";
            this.Permissions_Column.Name = "Permissions_Column";
            this.Permissions_Column.ReadOnly = true;
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
            this.FileOwner_Column.HeaderText = "Владелец";
            this.FileOwner_Column.Name = "FileOwner_Column";
            this.FileOwner_Column.ReadOnly = true;
            // 
            // FileGroup_Column
            // 
            this.FileGroup_Column.HeaderText = "Группа владелец";
            this.FileGroup_Column.Name = "FileGroup_Column";
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
            this.IsReadOnly_Column.Visible = false;
            // 
            // IsHidden_Column
            // 
            this.IsHidden_Column.HeaderText = "Скрытый";
            this.IsHidden_Column.Name = "IsHidden_Column";
            this.IsHidden_Column.ReadOnly = true;
            this.IsHidden_Column.Visible = false;
            // 
            // EmptyColumns
            // 
            this.EmptyColumns.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmptyColumns.HeaderText = "";
            this.EmptyColumns.Name = "EmptyColumns";
            this.EmptyColumns.ReadOnly = true;
            // 
            // File_ContextMenu
            // 
            this.File_ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem1,
            this.переименоватьToolStripMenuItem1,
            this.удалитьToolStripMenuItem1,
            this.копироватьToolStripMenuItem1,
            this.вырезатьToolStripMenuItem1,
            this.создатьСсылкуToolStripMenuItem1,
            this.свойстваToolStripMenuItem1,
            this.праваToolStripMenuItem});
            this.File_ContextMenu.Name = "File_ContextMenu";
            this.File_ContextMenu.Size = new System.Drawing.Size(162, 180);
            // 
            // открытьToolStripMenuItem1
            // 
            this.открытьToolStripMenuItem1.Name = "открытьToolStripMenuItem1";
            this.открытьToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.открытьToolStripMenuItem1.Text = "Открыть";
            this.открытьToolStripMenuItem1.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // переименоватьToolStripMenuItem1
            // 
            this.переименоватьToolStripMenuItem1.Name = "переименоватьToolStripMenuItem1";
            this.переименоватьToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.переименоватьToolStripMenuItem1.Text = "Переименовать";
            this.переименоватьToolStripMenuItem1.Click += new System.EventHandler(this.переименоватьToolStripMenuItem_Click);
            // 
            // удалитьToolStripMenuItem1
            // 
            this.удалитьToolStripMenuItem1.Name = "удалитьToolStripMenuItem1";
            this.удалитьToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.удалитьToolStripMenuItem1.Text = "Удалить";
            this.удалитьToolStripMenuItem1.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // копироватьToolStripMenuItem1
            // 
            this.копироватьToolStripMenuItem1.Name = "копироватьToolStripMenuItem1";
            this.копироватьToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.копироватьToolStripMenuItem1.Text = "Копировать";
            this.копироватьToolStripMenuItem1.Click += new System.EventHandler(this.копироватьToolStripMenuItem_Click);
            // 
            // вырезатьToolStripMenuItem1
            // 
            this.вырезатьToolStripMenuItem1.Name = "вырезатьToolStripMenuItem1";
            this.вырезатьToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.вырезатьToolStripMenuItem1.Text = "Вырезать";
            this.вырезатьToolStripMenuItem1.Click += new System.EventHandler(this.вырезатьToolStripMenuItem_Click);
            // 
            // создатьСсылкуToolStripMenuItem1
            // 
            this.создатьСсылкуToolStripMenuItem1.Name = "создатьСсылкуToolStripMenuItem1";
            this.создатьСсылкуToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.создатьСсылкуToolStripMenuItem1.Text = "Создать ссылку";
            this.создатьСсылкуToolStripMenuItem1.Click += new System.EventHandler(this.создатьСсылкуToolStripMenuItem_Click);
            // 
            // свойстваToolStripMenuItem1
            // 
            this.свойстваToolStripMenuItem1.Name = "свойстваToolStripMenuItem1";
            this.свойстваToolStripMenuItem1.Size = new System.Drawing.Size(161, 22);
            this.свойстваToolStripMenuItem1.Text = "Свойства";
            this.свойстваToolStripMenuItem1.Click += new System.EventHandler(this.свойстваToolStripMenuItem1_Click);
            // 
            // System_ContextMenu
            // 
            this.System_ContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вставитьToolStripMenuItem1,
            this.создатьToolStripMenuItem1,
            this.вывестиИнформациюОФСToolStripMenuItem,
            this.очиститьБуферОбменаToolStripMenuItem,
            this.обновитьToolStripMenuItem});
            this.System_ContextMenu.Name = "System_ContextMenu";
            this.System_ContextMenu.Size = new System.Drawing.Size(230, 114);
            // 
            // вставитьToolStripMenuItem1
            // 
            this.вставитьToolStripMenuItem1.Name = "вставитьToolStripMenuItem1";
            this.вставитьToolStripMenuItem1.Size = new System.Drawing.Size(229, 22);
            this.вставитьToolStripMenuItem1.Text = "Вставить";
            this.вставитьToolStripMenuItem1.Click += new System.EventHandler(this.вставитьToolStripMenuItem_Click);
            // 
            // создатьToolStripMenuItem1
            // 
            this.создатьToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem1,
            this.папкуToolStripMenuItem1});
            this.создатьToolStripMenuItem1.Name = "создатьToolStripMenuItem1";
            this.создатьToolStripMenuItem1.Size = new System.Drawing.Size(229, 22);
            this.создатьToolStripMenuItem1.Text = "Создать";
            // 
            // файлToolStripMenuItem1
            // 
            this.файлToolStripMenuItem1.Name = "файлToolStripMenuItem1";
            this.файлToolStripMenuItem1.Size = new System.Drawing.Size(108, 22);
            this.файлToolStripMenuItem1.Text = "Файл";
            this.файлToolStripMenuItem1.Click += new System.EventHandler(this.CreateFileToolStripMenuItem_Click);
            // 
            // папкуToolStripMenuItem1
            // 
            this.папкуToolStripMenuItem1.Name = "папкуToolStripMenuItem1";
            this.папкуToolStripMenuItem1.Size = new System.Drawing.Size(108, 22);
            this.папкуToolStripMenuItem1.Text = "Папку";
            this.папкуToolStripMenuItem1.Click += new System.EventHandler(this.CreateFileToolStripMenuItem_Click);
            // 
            // вывестиИнформациюОФСToolStripMenuItem
            // 
            this.вывестиИнформациюОФСToolStripMenuItem.Name = "вывестиИнформациюОФСToolStripMenuItem";
            this.вывестиИнформациюОФСToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.вывестиИнформациюОФСToolStripMenuItem.Text = "Вывести информацию о ФС";
            this.вывестиИнформациюОФСToolStripMenuItem.Click += new System.EventHandler(this.вывестиРазмерФСToolStripMenuItem_Click);
            // 
            // очиститьБуферОбменаToolStripMenuItem
            // 
            this.очиститьБуферОбменаToolStripMenuItem.Name = "очиститьБуферОбменаToolStripMenuItem";
            this.очиститьБуферОбменаToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.очиститьБуферОбменаToolStripMenuItem.Text = "Очистить буфер обмена";
            this.очиститьБуферОбменаToolStripMenuItem.Click += new System.EventHandler(this.очиститьБуферОбменаToolStripMenuItem_Click);
            // 
            // обновитьToolStripMenuItem
            // 
            this.обновитьToolStripMenuItem.Name = "обновитьToolStripMenuItem";
            this.обновитьToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.обновитьToolStripMenuItem.Text = "Обновить";
            this.обновитьToolStripMenuItem.Click += new System.EventHandler(this.обновитьToolStripMenuItem_Click);
            // 
            // удалитьГруппуToolStripMenuItem
            // 
            this.удалитьГруппуToolStripMenuItem.Name = "удалитьГруппуToolStripMenuItem";
            this.удалитьГруппуToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.удалитьГруппуToolStripMenuItem.Text = "Удалить группу";
            // 
            // праваToolStripMenuItem
            // 
            this.праваToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.изменитьВладельцаToolStripMenuItem,
            this.изменитьГруппуToolStripMenuItem1});
            this.праваToolStripMenuItem.Name = "праваToolStripMenuItem";
            this.праваToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.праваToolStripMenuItem.Text = "Права";
            // 
            // изменитьВладельцаToolStripMenuItem
            // 
            this.изменитьВладельцаToolStripMenuItem.Name = "изменитьВладельцаToolStripMenuItem";
            this.изменитьВладельцаToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.изменитьВладельцаToolStripMenuItem.Text = "Изменить владельца";
            // 
            // изменитьГруппуToolStripMenuItem1
            // 
            this.изменитьГруппуToolStripMenuItem1.Name = "изменитьГруппуToolStripMenuItem1";
            this.изменитьГруппуToolStripMenuItem1.Size = new System.Drawing.Size(188, 22);
            this.изменитьГруппуToolStripMenuItem1.Text = "Изменить группу";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 481);
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
            this.panel4.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.File_ContextMenu.ResumeLayout(false);
            this.System_ContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файловаяСистемаToolStripMenuItem;
        private ToolStripMenuItem вывестиРазмерФСToolStripMenuItem;
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
        private TextBox textBox1;
        private Panel panel4;
        private ToolStripMenuItem копироватьToolStripMenuItem;
        private ToolStripMenuItem вырезатьToolStripMenuItem;
        private ToolStripMenuItem вставитьToolStripMenuItem;
        private Label copied_label;
        private ToolStripMenuItem создатьСсылкуToolStripMenuItem;
        private ContextMenuStrip File_ContextMenu;
        private ToolStripMenuItem открытьToolStripMenuItem1;
        private ToolStripMenuItem переименоватьToolStripMenuItem1;
        private ToolStripMenuItem удалитьToolStripMenuItem1;
        private ToolStripMenuItem копироватьToolStripMenuItem1;
        private ToolStripMenuItem вырезатьToolStripMenuItem1;
        private ToolStripMenuItem создатьСсылкуToolStripMenuItem1;
        private ContextMenuStrip System_ContextMenu;
        private ToolStripMenuItem вставитьToolStripMenuItem1;
        private ToolStripMenuItem создатьToolStripMenuItem1;
        private ToolStripMenuItem файлToolStripMenuItem1;
        private ToolStripMenuItem папкуToolStripMenuItem1;
        private ToolStripMenuItem вывестиИнформациюОФСToolStripMenuItem;
        private ToolStripMenuItem очиститьБуферОбменаToolStripMenuItem;
        private ToolStripMenuItem свойстваToolStripMenuItem1;
        private ToolStripMenuItem создатьПользователяToolStripMenuItem;
        private ToolStripMenuItem группаToolStripMenuItem;
        private ToolStripMenuItem вывестиСписокГруппToolStripMenuItem;
        private ToolStripMenuItem создатьГруппуToolStripMenuItem;
        private ToolStripMenuItem изменитьГруппуToolStripMenuItem;
        private ToolStripMenuItem обновитьToolStripMenuItem;
        private DataGridViewTextBoxColumn FileID_Column;
        private DataGridViewTextBoxColumn FileName_Column;
        private DataGridViewTextBoxColumn FileType_Column;
        private DataGridViewTextBoxColumn FileSize_Column;
        private DataGridViewTextBoxColumn Permissions_Column;
        private DataGridViewTextBoxColumn FileCreation_Column;
        private DataGridViewTextBoxColumn FileModification_Column;
        private DataGridViewTextBoxColumn FileRead_Column;
        private DataGridViewTextBoxColumn FileOwner_Column;
        private DataGridViewTextBoxColumn FileGroup_Column;
        private DataGridViewCheckBoxColumn IsSystem_Column;
        private DataGridViewCheckBoxColumn IsReadOnly_Column;
        private DataGridViewCheckBoxColumn IsHidden_Column;
        private DataGridViewTextBoxColumn EmptyColumns;
        private ToolStripMenuItem удалитьГруппуToolStripMenuItem;
        private ToolStripMenuItem праваToolStripMenuItem;
        private ToolStripMenuItem изменитьВладельцаToolStripMenuItem;
        private ToolStripMenuItem изменитьГруппуToolStripMenuItem1;
    }
}