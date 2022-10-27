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
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists("qwe"))
                File.Delete("qwe");
            s5fs = S5FS.S5FS.format("qwe", 2048, 20_971_520);
            path = new();
            objs = new();
            var root_inode = s5fs.ReadInode(0);
            this.OpenFolder(new("", "", root_inode, null));
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
                dataGridView1["FileType_Column", i].Value = objects[i].isFolder ? "�����" : "����";
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
                throw new Exception($"{folder.Name} - �� �����");
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
                throw new Exception($"{file.Name} - �� ��������� ����");
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
            var vs = this.OpenFolder(path.Peek());
            this.UpdateTable(vs);
        }

        void OpenFile(Obj file)
        {
            if (file.isFolder)
            {
                throw new Exception($"{file.Name} - �� ����");
            }
            var text = Obj.ByteArrToString(s5fs.ReadDataByInode(file.inode));
            TextViewer textViewer = new(text);
            var dialogResult = textViewer.ShowDialog();
            if (dialogResult is DialogResult.OK) // ���������� �����
            {
                s5fs.WriteDataByInode(file.inode, Obj.StringToByteArr(textViewer.TextView));
            }
            else // ��������� ��������������
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

            var isFolder = (sender as ToolStripMenuItem) != ����ToolStripMenuItem;

            this.CreateFile(form.Result, isFolder);

        }

        /// <summary>
        /// �������� �� ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}