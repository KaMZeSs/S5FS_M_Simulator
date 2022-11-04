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

        CopyCutState copyCutState = CopyCutState.None;

        List<Obj> obj_to_copy = new();
        Obj copy_from;

        public enum CopyCutState { Copy, Cut, Link, None };

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
                dataGridView1["Permissions_Column", i].Value = "rwx rwx rwx";
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
            if (dataGridView1.SelectedRows.Count is 0)
                return;

            var selected_objects = this.GetSelectedObjs();

            var obj_to_delete = GetChildObjects(selected_objects);

            var size = obj_to_delete.Where(x => x.NLinks is 1).Sum(x => x.GetSize);
            var links = obj_to_delete.Where(x => x.NLinks is not 1).Count();

            var d_result = MessageBox.Show($"Будет удалено файлов: {obj_to_delete.Length}, занимающих {size} байт." +
                $"{(links is 0 ? String.Empty : $"Будет удалено ссылок: {links}")}",
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

        private Obj[] GetSelectedObjs()
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
            return selected_objects.ToArray();
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count is 0)
                return;

            copyCutState = CopyCutState.Copy;

            obj_to_copy.Clear();

            copy_from = this.path.Peek();

            var to_copy = this.GetSelectedObjs();

            foreach (var obj in to_copy)
            {
                obj_to_copy.Add(obj);
            }

            copied_label.Text = $"Скопировано элементов: {obj_to_copy.Count}";
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count is 0)
                return;

            copyCutState = CopyCutState.Cut;

            copy_from = this.path.Peek();

            var to_copy = this.GetSelectedObjs();

            foreach (var obj in to_copy)
            {
                obj_to_copy.Add(obj);
            }

            copied_label.Text = $"Вырезано элементов: {obj_to_copy.Count}";
        }

        private void создатьСсылкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count is 0)
                return;

            copyCutState = CopyCutState.Link;

            copy_from = this.path.Peek();

            var to_copy = this.GetSelectedObjs();

            foreach (var obj in to_copy)
            {
                obj_to_copy.Add(obj);
            }

            copied_label.Text = $"Элементов для ссылки: {obj_to_copy.Count}";
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (copyCutState is CopyCutState.None)
                return;

            if (obj_to_copy.Count is 0)
                return;

            if (copy_from == path.Peek())
            {
                MessageBox.Show("Корневая папка, в которую " +
                            "следует поместить файлы, является дочерней " +
                            "для папки, в которой они находятся.");
                return;
            }
                

            if (copyCutState is CopyCutState.Copy)
            {
                var to_copy = this.GetChildObjects(obj_to_copy.ToArray());
                
                Stack<Inode> new_tree = new();
                Stack<Inode> old_tree = new();
                new_tree.Push(this.path.Peek().inode);
                old_tree.Push(to_copy.First().parent_inode);

                foreach (var obj in to_copy)
                {
                    while (!obj.parent_inode.Equals(old_tree.Peek())) 
                    {
                        old_tree.Pop();
                        new_tree.Pop();
                    }
                    var vs1 = this.CopyElement(new_tree.Peek(), obj);
                    if (obj.isFolder)
                    {
                        new_tree.Push(vs1);
                        old_tree.Push(obj.inode);
                    }   
                }
            }
            else if (copyCutState is CopyCutState.Cut)
            {
                foreach (var obj in obj_to_copy)
                {
                    if (this.path.Any(x => x.inode.Equals(obj.inode)))
                    {
                        MessageBox.Show("Корневая папка, в которую " +
                            "следует поместить файлы, является дочерней " +
                            "для папки, в которой они находятся.");
                        return;
                    }
                }
                
                foreach (var obj in obj_to_copy)
                {
                    s5fs.AddFileLinkToDirectory(this.path.Peek().inode, obj.inode, obj.Name);
                    s5fs.DeleteFileLinkFromDirectory(obj.parent_inode, obj.inode);
                }
            }
            else if (copyCutState is CopyCutState.Link)
            {
                foreach (var obj in obj_to_copy)
                {
                    if (this.path.Any(x => x.inode.Equals(obj.inode)))
                    {
                        MessageBox.Show("Корневая папка, в которую " +
                            "следует поместить ссылку, является дочерней " +
                            "для папки, в которой они находятся.");
                        return;
                    }

                    s5fs.AddFileLinkToDirectory(this.path.Peek().inode, obj.inode, obj.Name);
                }
            }

            var vs = this.UpdateFolder();
            this.UpdateTable(vs);
        }

        private Inode CopyElement(Inode parent, Obj to_copy)
        {
            var inode_info = s5fs.CreateFile(parent, to_copy.Name, to_copy.isFolder);
            if (to_copy.isFolder is true)
                return inode_info.daughter;
            var data = s5fs.ReadDataByInode(to_copy.inode);
            s5fs.WriteDataByInode(inode_info.daughter, data);

            return inode_info.daughter;
        }
    }
}