namespace Emulator
{
    partial class Properties
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
            this.Name_Label = new System.Windows.Forms.Label();
            this.Size_Label = new System.Windows.Forms.Label();
            this.Creator_Label = new System.Windows.Forms.Label();
            this.CreationDate_Label = new System.Windows.Forms.Label();
            this.ModificationDate_Label = new System.Windows.Forms.Label();
            this.ReadDate_Label = new System.Windows.Forms.Label();
            this.isHidden_Check = new System.Windows.Forms.CheckBox();
            this.isReadOnly_Check = new System.Windows.Forms.CheckBox();
            this.isSystem_Check = new System.Windows.Forms.CheckBox();
            this.OwnerPerm_Label = new System.Windows.Forms.Label();
            this.GroupPerm_Label = new System.Windows.Forms.Label();
            this.OtherPerm_Label = new System.Windows.Forms.Label();
            this.UserRead_Check = new System.Windows.Forms.CheckBox();
            this.GroupRead_Check = new System.Windows.Forms.CheckBox();
            this.OtherRead_Check = new System.Windows.Forms.CheckBox();
            this.OtherWrite_Check = new System.Windows.Forms.CheckBox();
            this.GroupWrite_Check = new System.Windows.Forms.CheckBox();
            this.UserWrite_Check = new System.Windows.Forms.CheckBox();
            this.OtherExecute_Check = new System.Windows.Forms.CheckBox();
            this.GroupExecute_Check = new System.Windows.Forms.CheckBox();
            this.UserExecute_Check = new System.Windows.Forms.CheckBox();
            this.ReadPerm_Label = new System.Windows.Forms.Label();
            this.WritePerm_Label = new System.Windows.Forms.Label();
            this.ExecutionPerm_Label = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Group_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Name_Label
            // 
            this.Name_Label.AutoSize = true;
            this.Name_Label.Location = new System.Drawing.Point(12, 18);
            this.Name_Label.Name = "Name_Label";
            this.Name_Label.Size = new System.Drawing.Size(75, 15);
            this.Name_Label.TabIndex = 0;
            this.Name_Label.Text = "Имя файла: ";
            // 
            // Size_Label
            // 
            this.Size_Label.AutoSize = true;
            this.Size_Label.Location = new System.Drawing.Point(12, 48);
            this.Size_Label.Name = "Size_Label";
            this.Size_Label.Size = new System.Drawing.Size(91, 15);
            this.Size_Label.TabIndex = 0;
            this.Size_Label.Text = "Размер файла: ";
            // 
            // Creator_Label
            // 
            this.Creator_Label.AutoSize = true;
            this.Creator_Label.Location = new System.Drawing.Point(12, 79);
            this.Creator_Label.Name = "Creator_Label";
            this.Creator_Label.Size = new System.Drawing.Size(103, 15);
            this.Creator_Label.TabIndex = 0;
            this.Creator_Label.Text = "Владелец файла: ";
            // 
            // CreationDate_Label
            // 
            this.CreationDate_Label.AutoSize = true;
            this.CreationDate_Label.Location = new System.Drawing.Point(12, 146);
            this.CreationDate_Label.Name = "CreationDate_Label";
            this.CreationDate_Label.Size = new System.Drawing.Size(91, 15);
            this.CreationDate_Label.TabIndex = 0;
            this.CreationDate_Label.Text = "Дата создания: ";
            // 
            // ModificationDate_Label
            // 
            this.ModificationDate_Label.AutoSize = true;
            this.ModificationDate_Label.Location = new System.Drawing.Point(12, 177);
            this.ModificationDate_Label.Name = "ModificationDate_Label";
            this.ModificationDate_Label.Size = new System.Drawing.Size(101, 15);
            this.ModificationDate_Label.TabIndex = 0;
            this.ModificationDate_Label.Text = "Дата изменения: ";
            // 
            // ReadDate_Label
            // 
            this.ReadDate_Label.AutoSize = true;
            this.ReadDate_Label.Location = new System.Drawing.Point(12, 213);
            this.ReadDate_Label.Name = "ReadDate_Label";
            this.ReadDate_Label.Size = new System.Drawing.Size(79, 15);
            this.ReadDate_Label.TabIndex = 0;
            this.ReadDate_Label.Text = "Дата чтения: ";
            // 
            // isHidden_Check
            // 
            this.isHidden_Check.AutoSize = true;
            this.isHidden_Check.Location = new System.Drawing.Point(12, 246);
            this.isHidden_Check.Name = "isHidden_Check";
            this.isHidden_Check.Size = new System.Drawing.Size(77, 19);
            this.isHidden_Check.TabIndex = 1;
            this.isHidden_Check.Text = "Скрытый";
            this.isHidden_Check.UseVisualStyleBackColor = true;
            // 
            // isReadOnly_Check
            // 
            this.isReadOnly_Check.AutoSize = true;
            this.isReadOnly_Check.Location = new System.Drawing.Point(12, 271);
            this.isReadOnly_Check.Name = "isReadOnly_Check";
            this.isReadOnly_Check.Size = new System.Drawing.Size(128, 19);
            this.isReadOnly_Check.TabIndex = 1;
            this.isReadOnly_Check.Text = "Только для чтения";
            this.isReadOnly_Check.UseVisualStyleBackColor = true;
            // 
            // isSystem_Check
            // 
            this.isSystem_Check.AutoSize = true;
            this.isSystem_Check.Location = new System.Drawing.Point(12, 296);
            this.isSystem_Check.Name = "isSystem_Check";
            this.isSystem_Check.Size = new System.Drawing.Size(90, 19);
            this.isSystem_Check.TabIndex = 1;
            this.isSystem_Check.Text = "Системный";
            this.isSystem_Check.UseVisualStyleBackColor = true;
            this.isSystem_Check.Click += new System.EventHandler(this.isSystem_Check_Click);
            // 
            // OwnerPerm_Label
            // 
            this.OwnerPerm_Label.AutoSize = true;
            this.OwnerPerm_Label.Location = new System.Drawing.Point(11, 347);
            this.OwnerPerm_Label.Name = "OwnerPerm_Label";
            this.OwnerPerm_Label.Size = new System.Drawing.Size(59, 15);
            this.OwnerPerm_Label.TabIndex = 2;
            this.OwnerPerm_Label.Text = "Владелец";
            // 
            // GroupPerm_Label
            // 
            this.GroupPerm_Label.AutoSize = true;
            this.GroupPerm_Label.Location = new System.Drawing.Point(12, 378);
            this.GroupPerm_Label.Name = "GroupPerm_Label";
            this.GroupPerm_Label.Size = new System.Drawing.Size(46, 15);
            this.GroupPerm_Label.TabIndex = 2;
            this.GroupPerm_Label.Text = "Группа";
            // 
            // OtherPerm_Label
            // 
            this.OtherPerm_Label.AutoSize = true;
            this.OtherPerm_Label.Location = new System.Drawing.Point(11, 409);
            this.OtherPerm_Label.Name = "OtherPerm_Label";
            this.OtherPerm_Label.Size = new System.Drawing.Size(46, 15);
            this.OtherPerm_Label.TabIndex = 2;
            this.OtherPerm_Label.Text = "Другие";
            // 
            // UserRead_Check
            // 
            this.UserRead_Check.AutoSize = true;
            this.UserRead_Check.Location = new System.Drawing.Point(88, 348);
            this.UserRead_Check.Name = "UserRead_Check";
            this.UserRead_Check.Size = new System.Drawing.Size(15, 14);
            this.UserRead_Check.TabIndex = 3;
            this.UserRead_Check.UseVisualStyleBackColor = true;
            // 
            // GroupRead_Check
            // 
            this.GroupRead_Check.AutoSize = true;
            this.GroupRead_Check.Location = new System.Drawing.Point(88, 379);
            this.GroupRead_Check.Name = "GroupRead_Check";
            this.GroupRead_Check.Size = new System.Drawing.Size(15, 14);
            this.GroupRead_Check.TabIndex = 3;
            this.GroupRead_Check.UseVisualStyleBackColor = true;
            // 
            // OtherRead_Check
            // 
            this.OtherRead_Check.AutoSize = true;
            this.OtherRead_Check.Location = new System.Drawing.Point(88, 410);
            this.OtherRead_Check.Name = "OtherRead_Check";
            this.OtherRead_Check.Size = new System.Drawing.Size(15, 14);
            this.OtherRead_Check.TabIndex = 3;
            this.OtherRead_Check.UseVisualStyleBackColor = true;
            // 
            // OtherWrite_Check
            // 
            this.OtherWrite_Check.AutoSize = true;
            this.OtherWrite_Check.Location = new System.Drawing.Point(125, 410);
            this.OtherWrite_Check.Name = "OtherWrite_Check";
            this.OtherWrite_Check.Size = new System.Drawing.Size(15, 14);
            this.OtherWrite_Check.TabIndex = 3;
            this.OtherWrite_Check.UseVisualStyleBackColor = true;
            // 
            // GroupWrite_Check
            // 
            this.GroupWrite_Check.AutoSize = true;
            this.GroupWrite_Check.Location = new System.Drawing.Point(125, 379);
            this.GroupWrite_Check.Name = "GroupWrite_Check";
            this.GroupWrite_Check.Size = new System.Drawing.Size(15, 14);
            this.GroupWrite_Check.TabIndex = 3;
            this.GroupWrite_Check.UseVisualStyleBackColor = true;
            // 
            // UserWrite_Check
            // 
            this.UserWrite_Check.AutoSize = true;
            this.UserWrite_Check.Location = new System.Drawing.Point(125, 348);
            this.UserWrite_Check.Name = "UserWrite_Check";
            this.UserWrite_Check.Size = new System.Drawing.Size(15, 14);
            this.UserWrite_Check.TabIndex = 3;
            this.UserWrite_Check.UseVisualStyleBackColor = true;
            // 
            // OtherExecute_Check
            // 
            this.OtherExecute_Check.AutoSize = true;
            this.OtherExecute_Check.Location = new System.Drawing.Point(162, 410);
            this.OtherExecute_Check.Name = "OtherExecute_Check";
            this.OtherExecute_Check.Size = new System.Drawing.Size(15, 14);
            this.OtherExecute_Check.TabIndex = 3;
            this.OtherExecute_Check.UseVisualStyleBackColor = true;
            // 
            // GroupExecute_Check
            // 
            this.GroupExecute_Check.AutoSize = true;
            this.GroupExecute_Check.Location = new System.Drawing.Point(162, 379);
            this.GroupExecute_Check.Name = "GroupExecute_Check";
            this.GroupExecute_Check.Size = new System.Drawing.Size(15, 14);
            this.GroupExecute_Check.TabIndex = 3;
            this.GroupExecute_Check.UseVisualStyleBackColor = true;
            // 
            // UserExecute_Check
            // 
            this.UserExecute_Check.AutoSize = true;
            this.UserExecute_Check.Location = new System.Drawing.Point(162, 348);
            this.UserExecute_Check.Name = "UserExecute_Check";
            this.UserExecute_Check.Size = new System.Drawing.Size(15, 14);
            this.UserExecute_Check.TabIndex = 3;
            this.UserExecute_Check.UseVisualStyleBackColor = true;
            // 
            // ReadPerm_Label
            // 
            this.ReadPerm_Label.AutoSize = true;
            this.ReadPerm_Label.Location = new System.Drawing.Point(57, 330);
            this.ReadPerm_Label.Name = "ReadPerm_Label";
            this.ReadPerm_Label.Size = new System.Drawing.Size(46, 15);
            this.ReadPerm_Label.TabIndex = 4;
            this.ReadPerm_Label.Text = "Чтение";
            // 
            // WritePerm_Label
            // 
            this.WritePerm_Label.AutoSize = true;
            this.WritePerm_Label.Location = new System.Drawing.Point(109, 330);
            this.WritePerm_Label.Name = "WritePerm_Label";
            this.WritePerm_Label.Size = new System.Drawing.Size(46, 15);
            this.WritePerm_Label.TabIndex = 4;
            this.WritePerm_Label.Text = "Запись";
            // 
            // ExecutionPerm_Label
            // 
            this.ExecutionPerm_Label.AutoSize = true;
            this.ExecutionPerm_Label.Location = new System.Drawing.Point(161, 330);
            this.ExecutionPerm_Label.Name = "ExecutionPerm_Label";
            this.ExecutionPerm_Label.Size = new System.Drawing.Size(77, 15);
            this.ExecutionPerm_Label.TabIndex = 4;
            this.ExecutionPerm_Label.Text = "Выполнение";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 447);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Отмена";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(140, 447);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Подтвердить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Group_Label
            // 
            this.Group_Label.AutoSize = true;
            this.Group_Label.Location = new System.Drawing.Point(12, 114);
            this.Group_Label.Name = "Group_Label";
            this.Group_Label.Size = new System.Drawing.Size(106, 15);
            this.Group_Label.TabIndex = 6;
            this.Group_Label.Text = "Группа владелец: ";
            // 
            // Properties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 482);
            this.Controls.Add(this.Group_Label);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ExecutionPerm_Label);
            this.Controls.Add(this.WritePerm_Label);
            this.Controls.Add(this.ReadPerm_Label);
            this.Controls.Add(this.UserExecute_Check);
            this.Controls.Add(this.UserWrite_Check);
            this.Controls.Add(this.GroupExecute_Check);
            this.Controls.Add(this.GroupWrite_Check);
            this.Controls.Add(this.OtherExecute_Check);
            this.Controls.Add(this.OtherWrite_Check);
            this.Controls.Add(this.OtherRead_Check);
            this.Controls.Add(this.GroupRead_Check);
            this.Controls.Add(this.UserRead_Check);
            this.Controls.Add(this.OtherPerm_Label);
            this.Controls.Add(this.GroupPerm_Label);
            this.Controls.Add(this.OwnerPerm_Label);
            this.Controls.Add(this.isSystem_Check);
            this.Controls.Add(this.isReadOnly_Check);
            this.Controls.Add(this.isHidden_Check);
            this.Controls.Add(this.ReadDate_Label);
            this.Controls.Add(this.ModificationDate_Label);
            this.Controls.Add(this.CreationDate_Label);
            this.Controls.Add(this.Creator_Label);
            this.Controls.Add(this.Size_Label);
            this.Controls.Add(this.Name_Label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximumSize = new System.Drawing.Size(500, 521);
            this.MinimumSize = new System.Drawing.Size(260, 521);
            this.Name = "Properties";
            this.Text = "Properties";
            this.Load += new System.EventHandler(this.Properties_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label Name_Label;
        private Label Size_Label;
        private Label Creator_Label;
        private Label CreationDate_Label;
        private Label ModificationDate_Label;
        private Label ReadDate_Label;
        private CheckBox isHidden_Check;
        private CheckBox isReadOnly_Check;
        private CheckBox isSystem_Check;
        private Label OwnerPerm_Label;
        private Label GroupPerm_Label;
        private Label OtherPerm_Label;
        private CheckBox UserRead_Check;
        private CheckBox GroupRead_Check;
        private CheckBox OtherRead_Check;
        private CheckBox OtherWrite_Check;
        private CheckBox GroupWrite_Check;
        private CheckBox UserWrite_Check;
        private CheckBox OtherExecute_Check;
        private CheckBox GroupExecute_Check;
        private CheckBox UserExecute_Check;
        private Label ReadPerm_Label;
        private Label WritePerm_Label;
        private Label ExecutionPerm_Label;
        private Button button1;
        private Button button2;
        private Label Group_Label;
    }
}