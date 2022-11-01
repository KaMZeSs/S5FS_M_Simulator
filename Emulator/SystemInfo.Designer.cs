namespace Emulator
{
    partial class SystemInfo
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
            this.UsedData_Label = new System.Windows.Forms.Label();
            this.FreeData_Label = new System.Windows.Forms.Label();
            this.FullSize_Label = new System.Windows.Forms.Label();
            this.CreationDate_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // UsedData_Label
            // 
            this.UsedData_Label.AutoSize = true;
            this.UsedData_Label.Location = new System.Drawing.Point(12, 13);
            this.UsedData_Label.Name = "UsedData_Label";
            this.UsedData_Label.Size = new System.Drawing.Size(38, 15);
            this.UsedData_Label.TabIndex = 0;
            this.UsedData_Label.Text = "label1";
            // 
            // FreeData_Label
            // 
            this.FreeData_Label.AutoSize = true;
            this.FreeData_Label.Location = new System.Drawing.Point(12, 38);
            this.FreeData_Label.Name = "FreeData_Label";
            this.FreeData_Label.Size = new System.Drawing.Size(38, 15);
            this.FreeData_Label.TabIndex = 1;
            this.FreeData_Label.Text = "label2";
            // 
            // FullSize_Label
            // 
            this.FullSize_Label.AutoSize = true;
            this.FullSize_Label.Location = new System.Drawing.Point(12, 63);
            this.FullSize_Label.Name = "FullSize_Label";
            this.FullSize_Label.Size = new System.Drawing.Size(38, 15);
            this.FullSize_Label.TabIndex = 2;
            this.FullSize_Label.Text = "label3";
            // 
            // CreationDate_Label
            // 
            this.CreationDate_Label.AutoSize = true;
            this.CreationDate_Label.Location = new System.Drawing.Point(12, 88);
            this.CreationDate_Label.Name = "CreationDate_Label";
            this.CreationDate_Label.Size = new System.Drawing.Size(38, 15);
            this.CreationDate_Label.TabIndex = 3;
            this.CreationDate_Label.Text = "label4";
            // 
            // SystemInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 114);
            this.Controls.Add(this.CreationDate_Label);
            this.Controls.Add(this.FullSize_Label);
            this.Controls.Add(this.FreeData_Label);
            this.Controls.Add(this.UsedData_Label);
            this.Name = "SystemInfo";
            this.Text = "SystemInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label UsedData_Label;
        private Label FreeData_Label;
        private Label FullSize_Label;
        private Label CreationDate_Label;
    }
}