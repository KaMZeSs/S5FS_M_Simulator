namespace Emulator
{
    partial class LoadFS
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
            this.LoadFSType_comboBox = new System.Windows.Forms.ComboBox();
            this.ClusterSize_comboBox = new System.Windows.Forms.ComboBox();
            this.DiskSize_UpDown = new System.Windows.Forms.NumericUpDown();
            this.Accept_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DiskSize_UpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoadFSType_comboBox
            // 
            this.LoadFSType_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LoadFSType_comboBox.FormattingEnabled = true;
            this.LoadFSType_comboBox.Items.AddRange(new object[] {
            "Форматировать",
            "Загрузить существующий"});
            this.LoadFSType_comboBox.Location = new System.Drawing.Point(12, 37);
            this.LoadFSType_comboBox.Name = "LoadFSType_comboBox";
            this.LoadFSType_comboBox.Size = new System.Drawing.Size(173, 23);
            this.LoadFSType_comboBox.TabIndex = 0;
            this.LoadFSType_comboBox.SelectedIndexChanged += new System.EventHandler(this.LoadFSType_comboBox_SelectedIndexChanged);
            // 
            // ClusterSize_comboBox
            // 
            this.ClusterSize_comboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ClusterSize_comboBox.FormattingEnabled = true;
            this.ClusterSize_comboBox.Items.AddRange(new object[] {
            "512",
            "1024",
            "2048",
            "4096"});
            this.ClusterSize_comboBox.Location = new System.Drawing.Point(0, 20);
            this.ClusterSize_comboBox.Name = "ClusterSize_comboBox";
            this.ClusterSize_comboBox.Size = new System.Drawing.Size(173, 23);
            this.ClusterSize_comboBox.TabIndex = 1;
            // 
            // DiskSize_UpDown
            // 
            this.DiskSize_UpDown.Location = new System.Drawing.Point(0, 68);
            this.DiskSize_UpDown.Maximum = new decimal(new int[] {
            5120,
            0,
            0,
            0});
            this.DiskSize_UpDown.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.DiskSize_UpDown.Name = "DiskSize_UpDown";
            this.DiskSize_UpDown.Size = new System.Drawing.Size(173, 23);
            this.DiskSize_UpDown.TabIndex = 2;
            this.DiskSize_UpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // Accept_button
            // 
            this.Accept_button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Accept_button.Location = new System.Drawing.Point(38, 181);
            this.Accept_button.Name = "Accept_button";
            this.Accept_button.Size = new System.Drawing.Size(121, 23);
            this.Accept_button.TabIndex = 3;
            this.Accept_button.Text = "Подтвердить";
            this.Accept_button.UseVisualStyleBackColor = true;
            this.Accept_button.Click += new System.EventHandler(this.Accept_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Тип загрузки диска";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Размер кластера";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Размер диска (Мб)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DiskSize_UpDown);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.ClusterSize_comboBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 99);
            this.panel1.TabIndex = 6;
            // 
            // LoadFS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(197, 221);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Accept_button);
            this.Controls.Add(this.LoadFSType_comboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadFS";
            this.ShowIcon = false;
            this.Text = "LoadFS";
            this.Load += new System.EventHandler(this.LoadFS_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DiskSize_UpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox LoadFSType_comboBox;
        private ComboBox ClusterSize_comboBox;
        private NumericUpDown DiskSize_UpDown;
        private Button Accept_button;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel1;
    }
}