using System.Text;

using S5FS;

using static System.Net.Mime.MediaTypeNames;

namespace Emulator
{
    public partial class MainForm : Form
    {
        private Stack<Obj> path;
        private List<KeyValuePair<int, Obj>> objs;
        S5FS.S5FS s5fs;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists("qwe"))
                File.Delete("qwe");
            s5fs = S5FS.S5FS.format("qwe", 2048, 52_428_800);
            path = new();
            objs = new();
            var root_inode = s5fs.ReadInode(0);
            this.OpenFolder(new("", root_inode, null));
        }

        private void UpdateTable(Obj[] objects)
        {
            textBox1.Text = StackObjToStr(this.path);
            dataGridView1.Rows.Clear();
            objs.Clear();

            for (int i = 0; i < objects.Length; i++)
            {
                dataGridView1.Rows.Add();
                objs.Add(new KeyValuePair<int, Obj>(i, objects[i]));

                dataGridView1["FileID_Column", i].Value = i;
                dataGridView1["FileName_Column", i].Value = objects[i].Name;
                dataGridView1["FileType_Column", i].Value = objects[i].isFolder ? "Папка" : "Файл";
                dataGridView1["FileSize_Column", i].Value = objects[i].GetSize;
                dataGridView1["FileCreation_Column", i].Value = objects[i].CreationTime;
                dataGridView1["FileModification_Column", i].Value = objects[i].ChangeDateTime;
                dataGridView1["FileRead_Column", i].Value = objects[i].ReadDateTime;
                dataGridView1["FileOwner_Column", i].Value = objects[i].UserID;
                dataGridView1["FileUPerm_Column", i].Value = objects[i].OwnerPermissions;
                dataGridView1["FileGPerm_Column", i].Value = objects[i].GroupPermissions;
                dataGridView1["FileOPerm_Column", i].Value = objects[i].OtherPermissions;
                dataGridView1["IsSystem_Column", i].Value = objects[i].IsSystem;
                dataGridView1["IsReadOnly_Column", i].Value = objects[i].IsReadOnly;
                dataGridView1["IsVisible_Column", i].Value = objects[i].IsVisible;
            }            
        }

        private String StackObjToStr(Stack<Obj> path)
        {
            var arr = path.ToList();
            arr.Reverse();
            StringBuilder sb = new StringBuilder();


            foreach (var obj in arr)
            {
                if (obj.Name.Length is 0)
                {

                }
                else
                {
                    sb.Append(obj.Name + "\\");
                }
            }

            return sb.ToString();
        }

        Obj[] OpenFolder(Obj folder)
        {
            if (!folder.isFolder)
            {
                throw new Exception($"{folder.Name} - не папка");
            }
            var root_inode = folder.inode;
            var root_data = s5fs.ReadDataByInode(root_inode);
            var files = s5fs.GetFilesFromFolderData(root_data);

            //var root_obj = new Obj("", root_inode, null);

            objs.Clear();
            path.Push(folder);

            List<Obj> file_objs = new();

            foreach (var file in files)
            {
                var file_inode = s5fs.ReadInode(file.Key);

                file_objs.Add(new(file.Value, file_inode, root_inode));
            }

            return file_objs.ToArray();
        }

        Obj[] UpdateFolder()
        {
            var root_inode = this.path.Peek().inode;
            var root_data = s5fs.ReadDataByInode(root_inode);
            var files = s5fs.GetFilesFromFolderData(root_data);

            var root_obj = new Obj("", root_inode, null);

            objs.Clear();

            List<Obj> file_objs = new();

            foreach (var file in files)
            {
                var file_inode = s5fs.ReadInode(file.Key);

                file_objs.Add(new(file.Value, file_inode, root_inode));
            }

            return file_objs.ToArray();
        }

        void CreateFile(String name, bool isFolder = false)
        {
            try
            {
                var a = s5fs.CreateFile(path.Peek().inode, name, isFolder);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            var vs = this.UpdateFolder();
            this.UpdateTable(vs);
        }

        void OpenFile(Obj file)
        {
            if (file.isFolder)
            {
                throw new Exception($"{file.Name} - не файл");
            }
            var text = Obj.ByteArrToString(s5fs.ReadDataByInode(file.inode));

            var max_text_size = (int)(s5fs.max_file_size / 2);

            TextViewer textViewer = new(text);

            while (true)
            {
                var dialogResult = textViewer.ShowDialog();
                if (dialogResult is DialogResult.OK) // Перезапись файла
                {
                    try
                    {
                        var arr = Obj.StringToByteArr(textViewer.TextView);
                        if (arr.Length >= s5fs.max_file_size)
                        {
                            throw new Exception("");
                        }
                        s5fs.WriteDataByInode(file.inode, arr);
                        break;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Недостаточно места. Уменьшите размер файла.");
                    }
                }
                else // Передумал перезаписывать
                {
                    break;
                }
            }
        }
        Obj[] GetObjsInFolder(Obj root_obj)
        {
            var root_data = s5fs.ReadDataByInode(root_obj.inode);
            var files = s5fs.GetFilesFromFolderData(root_data);

            List<Obj> file_objs = new();

            foreach (var file in files)
            {
                var file_inode = s5fs.ReadInode(file.Key);

                file_objs.Add(new(file.Value, file_inode, root_obj.inode));

                if (file_objs.Last().isFolder)
                    file_objs.AddRange(this.GetObjsInFolder(file_objs.Last()));
            }

            return file_objs.ToArray();
        }

        Obj[] GetChildObjects(Obj[] files)
        {
            List<Obj> objs = new();

            foreach (var file in files)
            {
                objs.Add(file);
                if (file.isFolder)
                {
                    objs.AddRange(this.GetObjsInFolder(file));
                }
            }

            return objs.ToArray();
        }

        String[] GetFileNamesInCurr()
        {
            List<String> list = new();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                list.Add(dataGridView1["FileName_Column", i].Value.ToString());
            }
            return list.ToArray();
        }

        private void CreateFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var root_obj = this.path.Peek();
            var names = this.GetFileNamesInCurr();
            NameGetForm form = new(names);
            var result = form.ShowDialog();
            if (result is not DialogResult.OK)
            {
                return;
            }

            var isFolder = (sender as ToolStripMenuItem) != файлToolStripMenuItem;

            this.CreateFile(form.Result, isFolder);

        }

        /// <summary>
        /// Даблклик по строке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = (int)dataGridView1["FileID_Column", e.RowIndex].Value;
            var objects = this.objs.Where(x => x.Key == index);
            if (objects.Count() is 0)
                throw new Exception("IDK What's going on");
            var file = objects.First().Value;
            if (file.isFolder)
            {
                var vs = this.OpenFolder(file);
                this.UpdateTable(vs);
            }
            else
            {
                this.OpenFile(file);
                var vs = this.UpdateFolder();
                this.UpdateTable(vs);
            }

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            if (path.Count is 1)
            {
                return;
            }
            path.Pop();
            var vs = this.OpenFolder(path.Pop());
            this.UpdateTable(vs);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected_objects = new List<Obj>();
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                var index = (int)dataGridView1["FileID_Column", dataGridView1.SelectedRows[i].Index].Value;
                var objects = this.objs.Where(x => x.Key == index);
                if (objects.Count() is 0)
                    throw new Exception("IDK What's going on");
                var file = objects.First().Value;

                selected_objects.Add(file);
            }

            var obj_to_delete = GetChildObjects(selected_objects.ToArray());

            var size = obj_to_delete.Sum(x => x.GetSize);

            var d_result = MessageBox.Show($"Будет удалено файлов: {obj_to_delete.Length}, занимающих {size} байт.",
                    "Вы точно хотите удалить папку?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (d_result is DialogResult.No)
            {
                return;
            }

            obj_to_delete.Reverse();

            foreach (var obj in obj_to_delete)
            {
                s5fs.DeleteFileLinkFromDirectory(obj.parent_inode, obj.inode);
            }

            var vs = this.UpdateFolder();
            this.UpdateTable(vs);
        }

        private void вывестиРазмерФСToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var root_obj = this.path.Last();
            new SystemInfo(this.s5fs).ShowDialog(this);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count is not 1)
                return;

            this.dataGridView1_CellDoubleClick(sender,
                new(0, dataGridView1.SelectedRows[0].Index));
        }

        private void переименоватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count is not 1)
                return;

            var selected_obj_id = dataGridView1.SelectedRows[0].Cells["FileID_Column"].Value;
            var selected_obj = this.objs.Find(x => x.Key.Equals(selected_obj_id)).Value;

            var names = this.GetFileNamesInCurr();
            NameGetForm form = new(names);
            var result = form.ShowDialog();
            if (result is not DialogResult.OK)
            {
                return;
            }

            var new_name = form.Result;

            s5fs.AddFileLinkToDirectory(selected_obj.parent_inode, selected_obj.inode, new_name);
            s5fs.DeleteFileLinkFromDirectory(selected_obj.parent_inode, selected_obj.inode);

            var vs = this.UpdateFolder();
            this.UpdateTable(vs);
        }
    }
}