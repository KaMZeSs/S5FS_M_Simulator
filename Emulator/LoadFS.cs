using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Emulator
{
    public partial class LoadFS : Form
    {
        public S5FS.S5FS s5FS
        {
            get
            {
                return fs;
            }
        }
        
        S5FS.S5FS fs;

        int last_index = 0;

        public LoadFS()
        {
            InitializeComponent();
        }

        private void LoadFS_Load(object sender, EventArgs e)
        {
            LoadFSType_comboBox.SelectedIndex = 0;
            ClusterSize_comboBox.SelectedIndex = 0;
        }

        private void LoadFSType_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadFSType_comboBox.SelectedIndex == last_index)
                return;

            panel1.Visible = !panel1.Visible;


            this.Size = new Size(this.Width, panel1.Visible ? this.Height + panel1.Height : this.Height - panel1.Height);

            last_index = LoadFSType_comboBox.SelectedIndex;
        }

        private void Accept_button_Click(object sender, EventArgs e)
        {
            if (panel1.Visible) //Если форматирование
            {
                var saveFile = new SaveFileDialog()
                {
                    Filter = "s5fs file (*.s5fs)|*.s5fs",
                    AddExtension = true,
                    CheckPathExists = true,
                    DefaultExt = "s5fs",
                };

                var result = saveFile.ShowDialog(this);

                if (result is not DialogResult.OK)
                    return;

                var cluster_size = (uint)(ClusterSize_comboBox.SelectedText switch
                {
                    "512" => 512,
                    "1024" => 1024,
                    "2048" => 2048,
                    "4096" => 4096,
                    _ => 2048
                });

                try
                {
                    this.fs = S5FS.S5FS.format(saveFile.FileName, cluster_size, 
                        Convert.ToUInt32(this.DiskSize_UpDown.Value) * 1024 * 1024);
                }
                catch (Exception)
                {
                    MessageBox.Show("Во время загрузки системы возникла ошибка. Повторите попытку позже.");
                }
            }
            else // Если загрузка из файла
            {
                var openFile = new OpenFileDialog()
                {
                    Filter = "s5fs file (*.s5fs)|*.s5fs",
                    AddExtension = true,
                    CheckFileExists = true,
                    CheckPathExists = true,
                    DefaultExt = "s5fs",
                    Multiselect = false
                };

                var result = openFile.ShowDialog(this);

                if (result is not DialogResult.OK)
                    return;

                try
                {
                    this.fs = S5FS.S5FS.load_from_file(openFile.FileName);
                }
                catch (Exception)
                {
                    MessageBox.Show("Во время загрузки системы возникла ошибка. Возможно данный файл поврежден.");
                }
            }
            this.Close();
        }
    }
}
