using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using S5FS;

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

        Point? click_position;

        ushort curr_user_id = 0;
        ushort curr_group_id = 0;

        bool isLogged = false;

        (UInt16, String, UInt16, String)[] users;
        (UInt16, String, UInt16[])[] groups;

        List<UInt16> currUserGroups = new();

        Obj groups_obj, users_obj;

        public enum CopyCutState { Copy, Cut, Link, None };

        public MainForm(S5FS.S5FS s5)
        {
            InitializeComponent();
            this.s5fs = s5;
            dataGridView1.ShowCellToolTips = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            path = new();
            objs = new();
            var root_inode = s5fs.ReadInode(0);
            var files = this.OpenFolder(new("", root_inode, null));

            groups_obj = files.First(x => x.Name.Equals("groups"));
            users_obj = files.First(x => x.Name.Equals("users"));

            this.ReadUsers();
            this.ReadGroups();

            this.сменитьПользователяToolStripMenuItem.PerformClick();

            if (!isLogged)
            {
                Application.Exit();
            }

            this.UpdateTable(this.UpdateFolder());
        }

        #region Everything

        private void UpdateTable(Obj[] objects)
        {
            //Проверка на то, что открытая папка открыта по ссылке
            var last_folder = path.Peek();
            if (path.Count(x => x.inode.index == last_folder.inode.index) is not 1)
            {
                path.Pop();

                while (!path.Pop().inode.index.Equals(last_folder.inode.index)) ;
                path.Push(last_folder);
            }

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
                dataGridView1["FileSize_Column", i].Value = objects[i].isFolder ? "" : objects[i].GetSize;
                dataGridView1["FileCreation_Column", i].Value = objects[i].CreationTime;
                dataGridView1["FileModification_Column", i].Value = objects[i].ChangeDateTime;
                dataGridView1["FileRead_Column", i].Value = objects[i].ReadDateTime;
                var owner = users.FirstOrDefault(x => x.Item1 == objects[i].UserID);
                dataGridView1["FileOwner_Column", i].Value = owner.Item2.Replace("$", "") ?? "";
                var group = groups.FirstOrDefault(x => x.Item1 == objects[i].GroupID);
                dataGridView1["FileGroup_Column", i].Value = group.Item2.Replace("$", "") ?? "";
                dataGridView1["Permissions_Column", i].Value = String.Join(' ',
                    Obj.PermToString(objects[i].OwnerPermissions.Data),
                    Obj.PermToString(objects[i].GroupPermissions.Data),
                    Obj.PermToString(objects[i].OtherPermissions.Data));

                dataGridView1["IsSystem_Column", i].Value = objects[i].IsSystem;
                dataGridView1["IsReadOnly_Column", i].Value = objects[i].IsReadOnly;
                dataGridView1["IsHidden_Column", i].Value = objects[i].IsHidden;

                if (!(curr_user_id is 0))
                {
                    if (objects[i].UserID != curr_user_id && objects[i].IsHidden)
                    {
                        dataGridView1.Rows[i].Visible = false;
                    }
                }
            }

            click_position = null;
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
            var rootFold = path.Peek();

            if (!this.AccessChecker(rootFold, AccessType.toWrite))
            {
                MessageBox.Show("У вас нет доступа на запись в данную папку");
                return;
            }

            try
            {
                var a = s5fs.CreateFile(path.Peek().inode, name, isFolder);
                a.daughter.di_uid = this.curr_user_id;
                a.daughter.di_gid = this.curr_group_id;
                s5fs.WriteInode(a.daughter);
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

            if (!this.AccessChecker(file, AccessType.toRead))
            {
                MessageBox.Show("У вас нет доступа на чтение данного файла");
                return;
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
                        if (!this.AccessChecker(file, AccessType.toWrite))
                        {
                            if (file.IsReadOnly || file.IsSystem)
                                throw new ArgumentException();
                            throw new OutOfMemoryException();
                        }
                        var arr = Obj.StringToByteArr(textViewer.TextView);
                        if (arr.Length >= s5fs.max_file_size)
                        {
                            throw new Exception("");
                        }
                        s5fs.WriteDataByInode(file.inode, arr);
                        break;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Файл доступен только для чтения");
                    }
                    catch (OutOfMemoryException) { }
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
        Obj[] GetObjsInFolder(Obj root_obj, int layer = 0)
        {
            if (layer is 255)
            {
                throw new Exception("Карамба");
            }
            var root_data = s5fs.ReadDataByInode(root_obj.inode);
            var files = s5fs.GetFilesFromFolderData(root_data);

            List<Obj> file_objs = new();

            foreach (var file in files)
            {
                var file_inode = s5fs.ReadInode(file.Key);

                file_objs.Add(new(file.Value, file_inode, root_obj.inode));

                if (file_objs.Last().isFolder)
                    file_objs.AddRange(this.GetObjsInFolder(file_objs.Last(), layer + 1));
            }

            return file_objs.ToArray();
        }

        Obj[] GetChildObjects(Obj[] files, int layer = 0)
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

            var isFolder = (sender as ToolStripMenuItem) != файлToolStripMenuItem && (sender as ToolStripMenuItem) != файлToolStripMenuItem1;

            this.CreateFile(form.Result, isFolder);
        }

        /// <summary>
        /// Даблклик по строке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex is -1)
                return;

            var index = (int)dataGridView1["FileID_Column", e.RowIndex].Value;
            var objects = this.objs.Where(x => x.Key == index);
            if (objects.Count() is 0)
                throw new Exception("IDK What's going on");
            var file = objects.First().Value;

            if (!this.AccessChecker(file, AccessType.toRead))
            {
                MessageBox.Show($"У вас нет доступа на чтение {(file.isFolder ? "данной папки" : "данного файла")}");
                return;
            }

            if (file.isFolder)
            {
                var vs = this.OpenFolder(file);
                this.UpdateTable(vs);
            }
            else
            {
                this.OpenFile(file);
                this.UpdateTable(this.UpdateFolder());
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

            Obj[] obj_to_delete = null;
            try
            {
                obj_to_delete = GetChildObjects(selected_objects);
            }
            catch
            {
                MessageBox.Show("Обнаружены зацикленные ссылки. Удаление невозможно.");
                return;
            }

            if (!this.AccessChecker(this.path.Peek(), AccessType.toWrite))
            {
                MessageBox.Show("У вас нет права на запись в родительскую папку");
                return;
            }

            if (obj_to_delete.Any(x => this.AccessChecker(x, AccessType.toWrite) is false))
            {
                MessageBox.Show("У вас нет права на запись, или удаление одного из файлов/папок");
                return;
            }

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

            object selected_obj_id;

            if (click_position is not null)
            {
                var vs1 = dataGridView1.HitTest((int)click_position?.X, (int)click_position?.Y);

                selected_obj_id = this.dataGridView1["FileID_Column", vs1.RowIndex].Value;
            }
            else if (dataGridView1.SelectedRows.Count is not 1)
            {
                return;
            }
            else
            {
                selected_obj_id = dataGridView1.SelectedRows[0].Cells["FileID_Column"].Value;
            }

            var selected_obj = this.objs.Find(x => x.Key.Equals(selected_obj_id)).Value;
            
            if (!this.AccessChecker(this.path.Peek(), AccessType.toWrite))
            {
                MessageBox.Show("У вас нет доступа к изменению имен в данной папке");
                return;
            }

            if (!this.AccessChecker(selected_obj, AccessType.toWrite))
            {
                MessageBox.Show("У вас нет доступа к редактированию данного файла");
                return;
            }

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

            copy_from = this.path.Peek();

            var to_copy = this.GetSelectedObjs();

            if (to_copy.Any(x => this.AccessChecker(x, AccessType.toRead) is false))
            {
                MessageBox.Show("У вас нет права на чтение одного из файлов/папок");
                return;
            }

            obj_to_copy.Clear();

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

            if (!this.AccessChecker(this.path.Peek(), AccessType.toWrite))
            {
                MessageBox.Show("У вас нет права удаления файлов из данной папки");
                return;
            }

            //if (to_copy.Any(x => this.AccessChecker(x, AccessType.toRead) is false) 
            //    || to_copy.Any(x => this.AccessChecker(x, AccessType.toWrite) is false))
            //{
            //    MessageBox.Show("У вас нет права вырезания как минимум одного из файлов");
            //    return;
            //}

            obj_to_copy.Clear();

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
            obj_to_copy.Clear();

            copy_from = this.path.Peek();

            var to_copy = this.GetSelectedObjs();

            if (to_copy.Any(x => x.IsSystem))
            {
                MessageBox.Show("Нельзя создать ссылку на системный файл");
                return;
            }

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

            if (!this.AccessChecker(this.path.Peek(), AccessType.toWrite))
            {
                MessageBox.Show("У вас нет права записи в данную папку");
                return;
            }

            if (obj_to_copy.Any(x => this.AccessChecker(x, AccessType.toRead) == false))
            {
                MessageBox.Show("У вас нет доступа к чтению одного из файлов");
                return;
            }

            if (copyCutState is CopyCutState.Copy)
            {
                var to_copy = this.GetChildObjects(obj_to_copy.ToArray());

                // Проверка, вдруг права изменились
                // А еще проверка, всех дочерних файлов
                if (to_copy.Any(x => this.AccessChecker(new("", s5fs.ReadInode(x.inode.index), null), AccessType.toRead) is false))
                {
                    MessageBox.Show("У вас нет права чтения как минимум одного из файлов");
                    return;
                }
                
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

                var vs1 = new Obj("", s5fs.ReadInode(obj_to_copy[0].parent_inode.index), null);

                if (!this.AccessChecker(vs1, AccessType.toWrite))
                {
                    MessageBox.Show("У вас нет права на удаление файлов из папки-источника");
                    return;
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

                    try
                    {
                        s5fs.AddFileLinkToDirectory(this.path.Peek().inode, obj.inode, obj.Name);
                        this.GetChildObjects(new Obj[] { this.path.Peek() });
                    }
                    catch
                    {
                        s5fs.DeleteFileLinkFromDirectory(this.path.Peek().inode, obj.inode);
                        MessageBox.Show("Невозможно создать ссылку. Обнаружено зацикливание");
                    }
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

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button is not MouseButtons.Right)
                return;

            var test = dataGridView1.HitTest(e.X, e.Y);

            if (test.RowIndex is -1)
            {
                if (copyCutState is CopyCutState.None)
                {
                    System_ContextMenu.Items["вставитьToolStripMenuItem1"].Enabled = false;
                }
                else
                {
                    System_ContextMenu.Items["вставитьToolStripMenuItem1"].Enabled = true;
                }
                System_ContextMenu.Show(Cursor.Position.X, Cursor.Position.Y);
            }
            else
            {
                if (!dataGridView1[test.ColumnIndex, test.RowIndex].Selected)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1[test.ColumnIndex, test.RowIndex].Selected = true;
                }

                if (dataGridView1.SelectedRows.Count is not 1)
                {
                    File_ContextMenu.Items["открытьToolStripMenuItem1"].Enabled = false;
                    File_ContextMenu.Items["переименоватьToolStripMenuItem1"].Enabled = false;
                    File_ContextMenu.Items["свойстваToolStripMenuItem1"].Enabled = false;
                }
                else
                {
                    File_ContextMenu.Items["открытьToolStripMenuItem1"].Enabled = true;
                    File_ContextMenu.Items["переименоватьToolStripMenuItem1"].Enabled = true;
                    File_ContextMenu.Items["свойстваToolStripMenuItem1"].Enabled = true;
                }

                File_ContextMenu.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        private void очиститьБуферОбменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            obj_to_copy.Clear();
            copied_label.Text = "Нет скопированных элементов";
            copyCutState = CopyCutState.None;
        }

        private void свойстваToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count is not 1)
                return;
            object selected_obj_id;

            if (click_position is not null)
            {
                var vs1 = dataGridView1.HitTest((int)click_position?.X, (int)click_position?.Y);

                selected_obj_id = this.dataGridView1["FileID_Column", vs1.RowIndex].Value;
            }
            else if (dataGridView1.SelectedRows.Count is not 1)
            {
                return;
            }
            else
            {
                selected_obj_id = dataGridView1.SelectedRows[0].Cells["FileID_Column"].Value;
            }

            var selected_obj = this.objs.Find(x => x.Key.Equals(selected_obj_id)).Value;

            var form = new Properties(selected_obj, curr_user_id, ref users);
            var d_result = form.ShowDialog();
            if (d_result is not DialogResult.OK)
            {
                return;
            }

            var obj = form.obj;

            s5fs.WriteInode(obj.inode);

            this.UpdateTable(this.UpdateFolder());
        }

        enum AccessType { toRead, toWrite, toExecute };

        private bool AccessChecker(Obj obj, AccessType accessType)
        {
            if (obj.inode.index is 0)
                return true;

            if (obj.IsReadOnly && accessType is not AccessType.toRead)
            {
                return false;
            }

            //Если рут - может все
            //PS:
            //root - ты не сможешь меня победить
            //Системный файл: знаю, но он сможет
            //*ReadOnly выпрыгивает*

            
            //root не может изменить системный файл, тк он только для чтения
            //root не может изменить атрибуты файла, тк он системный
            if (curr_user_id is 0)  
                return true;

            if (obj.IsSystem)
                return false;

            switch (accessType)
            {
                case AccessType.toRead:
                {
                    if (obj.UserID == this.curr_user_id)
                    {
                        if (obj.OwnerPermissions.CanRead)
                            return true;
                    }
                    else if (this.currUserGroups.Contains(obj.GroupID))
                    {
                        if (obj.GroupPermissions.CanRead)
                            return true;
                    }
                    else
                    {
                        if (obj.OtherPermissions.CanRead)
                            return true;
                    }

                    break;
                }
                case AccessType.toWrite:
                {
                    if (obj.UserID == this.curr_user_id)
                    {
                        if (obj.OwnerPermissions.CanWrite)
                            return true;
                    }
                    else if (this.currUserGroups.Contains(obj.GroupID))
                    {
                        if (obj.GroupPermissions.CanWrite)
                            return true;
                    }
                    else
                    {
                        if (obj.OtherPermissions.CanWrite)
                            return true;
                    }
                    break;
                }
                case AccessType.toExecute:
                {
                    if (obj.UserID == this.curr_user_id)
                    {
                        if (obj.OwnerPermissions.CanExecute)
                            return true;
                    }
                    else if (this.currUserGroups.Contains(obj.GroupID))
                    {
                        if (obj.GroupPermissions.CanExecute)
                            return true;
                    }
                    else
                    {
                        if (obj.OtherPermissions.CanExecute)
                            return true;
                    }
                    break;
                }
            }
            return false;
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.UpdateTable(this.OpenFolder(path.Peek()));
        }

        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column != this.FileSize_Column)
                return;
            var value1 = e.CellValue1.ToString();
            var value2 = e.CellValue2.ToString();

            int value1_to_compare, value2_to_compare;

            Int32.TryParse(value1, out value1_to_compare);
            Int32.TryParse(value2, out value2_to_compare);

            e.SortResult = value1_to_compare.CompareTo(value2_to_compare);

            if (e.SortResult is 0)
            {
                e.SortResult = String.Compare(
                    dataGridView1.Rows[e.RowIndex1].Cells["FileName_Column"].Value.ToString(),
                    dataGridView1.Rows[e.RowIndex2].Cells["FileName_Column"].Value.ToString());
            }

            e.Handled = true;
        }

        #endregion

        #region Accounts

        #region Accecc from file
        private void ReadUsers()
        {
            var text = Obj.ByteArrToString(s5fs.ReadDataByInode(users_obj.inode));
            users = StaticMethods.GetUsersFromString(text);
        }

        private void WriteUsers()
        {
            var text = StaticMethods.GetStringFromUsers(users);
            s5fs.WriteDataByInode(users_obj.inode, Obj.StringToByteArr(text));
        }

        private void ReadGroups()
        {
            var text = Obj.ByteArrToString(s5fs.ReadDataByInode(groups_obj.inode));
            groups = StaticMethods.GetGroupsFromString(text);
        }

        private void WriteGroups()
        {
            var text = StaticMethods.GetStringFromGroups(groups);
            s5fs.WriteDataByInode(groups_obj.inode, Obj.StringToByteArr(text));
        }

        private void UpdateUsersOnDisk()
        {
            this.WriteUsers();
            this.WriteGroups();
        }

        #endregion

        #region WorkWithGroups

        private void DeleteUserFromGroup(UInt16 group_id, UInt16 user_id)
        {
            var index = Array.FindIndex(groups, x => x.Item1 == group_id);
            groups[index] = new(groups[index].Item1, groups[index].Item2,
                groups[index].Item3.Where(x => x != user_id).ToArray());
            this.UpdateUsersOnDisk();
        }

        #endregion

        private void вывестиСписокПользователейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Accounts(ref users, ref groups);
            form.ShowDialog(this);
        }

        private void удалитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Accounts(ref users, ref groups, true);
            var dialog_result = form.ShowDialog();
            if (dialog_result is not DialogResult.OK)
                return;

            if (form.user_id is 0)
            {
                MessageBox.Show("Суперпользователь не может быть удален!!!");
                return;
            }

            if (form.user_id == curr_user_id)
            {
                MessageBox.Show("Нельзя удалить пользователя, активного в данный момент!!!");
                return;
            }

            //var user = users.First(x => x.Item1 == form.user_id);

            var index = Array.FindIndex(users, x => x.Item1 == form.user_id);
            users[index] = new(users[index].Item1, '$' + users[index].Item2, users[index].Item3, users[index].Item4);

            //users = users.Where(x => x.Item1 != form.user_id).ToArray();
            //this.DeleteUserFromGroup(user.Item3, user.Item1);

            this.UpdateUsersOnDisk();
            if (path.Count is 1)
                this.UpdateTable(this.UpdateFolder());
        }

        private void вывестиСписокГруппToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Groups(ref users, ref groups);
            form.ShowDialog(this);
        }

        private void создатьГруппуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AddGroup((from gr in groups select gr.Item2).ToArray());

            var dialog_result = form.ShowDialog(this);
            if (dialog_result is not DialogResult.OK)
                return;

            UInt16 newGroupId;
            try
            {
                newGroupId = StaticMethods.GetNextId((from group_ in groups select group_.Item1).ToArray());
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно выполнить операцию. Достигнут лимит количества групп.");
                return;
            }

            groups = groups.Append(new(newGroupId, form.GroupName, new UInt16[0])).ToArray();
            groups.OrderBy(x => x.Item1);

            this.UpdateUsersOnDisk();

            if (path.Count is 1)
                this.UpdateTable(this.UpdateFolder());
        }

        private void изменитьГруппуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UInt16 gid;
            var form = new Groups(ref users, ref groups, true);
            while (true)
            {
                var dialog_result = form.ShowDialog(this);
                if (dialog_result is not DialogResult.OK)
                    return;
                gid = form.group_id;
                if (this.curr_user_id is 0)
                    break;
                if (this.curr_group_id == gid)
                    break;
                MessageBox.Show("У вас нет доступа на изменение групп, не являющихся вашей главной группой.");
            }
            using (var change_form = new ReductGroup(ref groups, ref users, gid))
            {
                var dialog_result = change_form.ShowDialog(this);
                if (dialog_result is not DialogResult.OK)
                    return;
            }
            this.UpdateUsersOnDisk();
            if (path.Count is 1)
                this.UpdateTable(this.UpdateFolder());
        }

        private void создатьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AddUser((from user in users select user.Item2).ToArray());
            var dialog_result = form.ShowDialog(this);
            if (dialog_result is not DialogResult.OK)
                return;

            var newUserInfo = form.User;

            UInt16 newUserId;
            try
            {
                newUserId = StaticMethods.GetNextId((from user in users select user.Item1).ToArray());
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно выполнить операцию. Достигнут лимит количества пользователей.");
                return;
            }

            var newGroupName = $"Группа {newUserInfo.Item1}";

            UInt16 newGroupId;
            try
            {
                newGroupId = StaticMethods.GetNextId((from group_ in groups select group_.Item1).ToArray());
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно выполнить операцию. Достигнут лимит количества групп.");
                return;
            }

            users = users.Append(new(newUserId, newUserInfo.Item1, newGroupId, newUserInfo.Item2)).ToArray();
            users.OrderBy(x => x.Item1);

            groups = groups.Append(new(newGroupId, newGroupName, new UInt16[] { newUserId })).ToArray();
            groups.OrderBy(x => x.Item1);

            this.UpdateUsersOnDisk();
            if (path.Count is 1)
                this.UpdateTable(this.UpdateFolder());
        }

        private void изменитьГруппуToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count is 0)
                return;

            var selected_objects = this.GetSelectedObjs();

            if (selected_objects.Any(x => x.IsSystem))
            {
                var dr_system =
                    MessageBox.Show("Пропустить их?",
                    "Один, или несколько файлов - системные",
                    MessageBoxButtons.OKCancel);
                if (dr_system is DialogResult.Cancel)
                    return;
            }
            if (this.curr_user_id is not 0 &&
                selected_objects.Any(x => x.UserID != this.curr_user_id))
            {
                var dr_user =
                    MessageBox.Show("Пропустить их?",
                    "Один, или несколько файлов не принадлежат вам",
                    MessageBoxButtons.OKCancel);
                if (dr_user is DialogResult.Cancel)
                    return;
            }
            if (selected_objects.All(x => x.IsSystem || (x.UserID != this.curr_user_id && this.curr_user_id is not 0)))
                return;

            var form = new Groups(ref users, ref groups, true);
            var dialog_result = form.ShowDialog();
            if (dialog_result is not DialogResult.OK)
                return;

            foreach (var obj in selected_objects)
            {
                if (obj.IsSystem)
                    continue;
                if (obj.UserID != this.curr_user_id && this.curr_user_id is not 0)
                    continue;
                obj.inode.di_gid = form.group_id;
                s5fs.WriteInode(obj.inode);
            }

            this.UpdateTable(this.UpdateFolder());
        }

        private void изменитьВладельцаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count is 0)
                return;

            var selected_objects = this.GetSelectedObjs();

            if (selected_objects.Any(x => x.IsSystem))
            {
                var dr_system = 
                    MessageBox.Show("Пропустить их?",
                    "Один, или несколько файлов - системные", 
                    MessageBoxButtons.OKCancel);
                if (dr_system is DialogResult.Cancel)
                    return;
            }
            if (this.curr_user_id is not 0 && 
                selected_objects.Any(x => x.UserID != this.curr_user_id))
            {
                var dr_user =
                    MessageBox.Show("Пропустить их?",
                    "Один, или несколько файлов не принадлежат вам", 
                    MessageBoxButtons.OKCancel);
                if (dr_user is DialogResult.Cancel)
                    return;
            }
            if (selected_objects.All(x => x.IsSystem || (x.UserID != this.curr_user_id && this.curr_user_id is not 0)))
                return;

            var form = new Accounts(ref users, ref groups, true);
            var dialog_result = form.ShowDialog();
            if (dialog_result is not DialogResult.OK)
                return;

            foreach (var obj in selected_objects)
            {
                if (obj.IsSystem)
                    continue;
                if (obj.UserID != this.curr_user_id && this.curr_user_id is not 0)
                    continue;
                obj.inode.di_uid = form.user_id;
                s5fs.WriteInode(obj.inode);
            }

            this.UpdateTable(this.UpdateFolder());
        }

        private void сменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Login(ref this.users);

            while (true)
            {
                var dialog_result = form.ShowDialog(this);
                if (dialog_result is not DialogResult.OK)
                    return;

                var UserInfo = form.LoggedUser;

                var isNewUser = form.LoggedUser.Item1 is null;

                if (isNewUser) 
                {
                    UInt16 newUserId;
                    try
                    {
                        newUserId = StaticMethods.GetNextId((from user in users select user.Item1).ToArray());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Достигнут лимит пользователей.");
                        continue;
                    }

                    var newGroupName = $"Группа {UserInfo.Item2}";

                    UInt16 newGroupId;
                    try
                    {
                        newGroupId = StaticMethods.GetNextId((from group_ in groups select group_.Item1).ToArray());
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Достигнут лимит групп.");
                        continue;
                    }

                    users = users.Append(new(newUserId, UserInfo.Item2, newGroupId, UserInfo.Item4)).ToArray();
                    users.OrderBy(x => x.Item1);

                    groups = groups.Append(new(newGroupId, newGroupName, new UInt16[] { newUserId })).ToArray();
                    groups.OrderBy(x => x.Item1);

                    curr_user_id = newUserId;
                    curr_group_id = newGroupId;

                    currUserGroups = (from gr in this.groups
                                      .Where(x => x.Item3.Contains(newUserId)).ToArray()
                                      select gr.Item1).ToList();

                    this.UpdateUsersOnDisk();
                    if (path.Count is 1)
                        this.UpdateTable(this.UpdateFolder());
                    break;
                }
                else
                {
                    try
                    {
                        curr_user_id = UserInfo.Item1 ?? throw new Exception();
                        curr_group_id = UserInfo.Item3 ?? throw new Exception();

                        currUserGroups = (from gr in this.groups
                                      .Where(x => x.Item3.Contains(curr_user_id)).ToArray()
                                          select gr.Item1).ToList();
                        if (path.Count is 1)
                            this.UpdateTable(this.UpdateFolder());
                        break;
                    }
                    catch
                    {
                        MessageBox.Show("Произошла непредвиденная ошибка при входе в аккаунт");
                        return;
                    }
                }
            }
            this.Text = users.First(x => x.Item1 == this.curr_user_id).Item2;


            var isRoot = this.curr_user_id is 0;
            this.создатьПользователяToolStripMenuItem.Enabled = isRoot;
            this.удалитьПользователяToolStripMenuItem.Enabled = isRoot;
            this.создатьГруппуToolStripMenuItem.Enabled = isRoot;
            this.удалитьГруппуToolStripMenuItem.Enabled = isRoot;

            isLogged = true;
        }

        private void удалитьГруппуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new Groups(ref users, ref groups, true);
            var dialog_result = form.ShowDialog();
            if (dialog_result is not DialogResult.OK)
                return;

            var group = groups.First(x => x.Item1 == form.group_id);

            if (group.Item3.Length is not 0)
            {
                MessageBox.Show("Невозможно удалить группу, в которой состоят пользователи.");
                return;
            }

            groups = groups.Where(x => x.Item1 != form.group_id).ToArray();

            var index = Array.FindIndex(groups, x => x.Item1 == form.group_id);
            groups[index] = new(groups[index].Item1, '$' + groups[index].Item2, groups[index].Item3);

            this.UpdateUsersOnDisk();
            if (path.Count is 1)
                this.UpdateTable(this.UpdateFolder());
        }

        #endregion
    }
}