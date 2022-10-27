using System.Text;

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
            path = new();
            objs = new();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            s5fs = S5FS.S5FS.format("qwe", 2048, 20_971_520);
            var root_inode = s5fs.ReadInode(0);
            this.OpenFolder(new("", "", root_inode, null));
        }

        private void Update(Obj[] objects)
        {
            textBox1.Text = StackObjToStr(this.path);
            dataGridView1.Rows.Clear();
            objs.Clear();

            for (int i = 0; i < path.Count; i++)
            {
                var templ = (DataGridViewRow)dataGridView1.RowTemplate.Clone();
                objs.Add(new KeyValuePair<int, Obj>(i, objects[i]));

                templ.Cells["FileID_Column"].Value = i;
                templ.Cells["FileType_Column"].Value = objects[i].isFolder ? "Папка" : "Файл";
                templ.Cells["FileSize_Column"].Value = objects[i].GetSize;
                templ.Cells["FileCreation_Column"].Value = objects[i].CreationTime;
                templ.Cells["FileModification_Column"].Value = objects[i].ChangeDateTime;
                templ.Cells["FileRead_Column"].Value = objects[i].ReadDateTime;
                templ.Cells["FileOwner_Column"].Value = objects[i].UserID;
                templ.Cells["FileUPerm_Column"].Value = objects[i].OwnerPermissions;
                templ.Cells["FileGPerm_Column"].Value = objects[i].GroupPermissions;
                templ.Cells["FileOPerm_Column"].Value = objects[i].OtherPermissions;
                templ.Cells["IsSystem_Column"].Value = objects[i].IsSystem;
                templ.Cells["IsReadOnly_Column"].Value = objects[i].IsReadOnly;
                templ.Cells["IsVisible_Column"].Value = objects[i].IsVisible;

                dataGridView1.Rows.Add(templ);
            }            
        }

        private String StackObjToStr(Stack<Obj> path)
        {
            var arr = path.ToArray();
            StringBuilder sb = new StringBuilder();


            foreach (var obj in path)
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

            var root_obj = new Obj("", "", root_inode, null);

            objs.Clear();
            path.Clear();
            path.Push(root_obj);

            List<Obj> file_objs = new();

            foreach (var file in files)
            {
                var file_inode = s5fs.ReadInode(file.Key);

                file_objs.Add(new(file.Value, this.StackObjToStr(path), file_inode, root_inode));
            }

            return file_objs.ToArray();
        }

        String GetTextFromFile(Obj file)
        {
            if (file.isFolder)
            {
                throw new Exception($"{file.Name} - не текстовый файл");
            }

            var data = s5fs.ReadDataByInode(file.inode);
            var text = Obj.ByteArrToString(data);
            return text;
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
        }

        void OpenFile(Obj file)
        {
            if (file.isFolder)
            {
                throw new Exception($"{file.Name} - не файл");
            }
            var text = Obj.ByteArrToString(s5fs.ReadDataByInode(file.inode));
            TextViewer textViewer = new(text);
            var dialogResult = textViewer.ShowDialog();
            if (dialogResult is DialogResult.OK) // Перезапись файла
            {
                s5fs.WriteDataByInode(file.inode, Obj.StringToByteArr(textViewer.TextView));
            }
            else // Передумал перезаписывать
            {

            }
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

            //var isFolder = sender as 

            this.CreateFile(form.Result);

        }
    }
}