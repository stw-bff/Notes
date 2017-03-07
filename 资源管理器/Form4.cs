using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;

namespace 资源管理器
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private class IconIndexes
        {
            public const int MyComputer = 2;      //我的电脑
            public const int ClosedFolder = 1;    //文件夹关闭
            public const int OpenFolder = 0;      //文件夹打开
            //public const int FixedDrive = 3;      //磁盘盘符
            public const int MyDocuments = 1;     //我的文档
            public const int recycle = 4;
            //public const int
        }
        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)// 在结点展开后发生 展开子结点
        {

        }
        private void directoryTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)// 在将要展开结点时发生 加载子结点
        {

        }




        private void treeView1_AfterExpand_1(object sender, TreeViewEventArgs e)
        {
            e.Node.Nodes.Clear();
            if (e.Node.Tag.ToString() == "桌面")
            {
                //实例化TreeNode类 TreeNode(string text,int imageIndex,int selectImageIndex)            
                TreeNode rootNode = new TreeNode("我的电脑",
                IconIndexes.MyComputer, IconIndexes.MyComputer);  //载入显示 选择显示
                rootNode.Tag = "我的电脑";                            //树节点数据
                rootNode.Text = "我的电脑";                           //树节点标签内容
                e.Node.Nodes.Add(rootNode);


                //显示MyDocuments(我的文档)结点
                var myDocuments = Environment.GetFolderPath           //获取计算机我的文档文件夹
                (Environment.SpecialFolder.MyDocuments);
                TreeNode DocNode = new TreeNode(myDocuments);
                DocNode.Tag = "我的文档";                            //设置结点名称
                DocNode.Text = "我的文档";
                DocNode.ImageIndex = IconIndexes.MyDocuments;         //设置获取结点显示图片
                DocNode.SelectedImageIndex = IconIndexes.MyDocuments; //设置选择显示图片
                e.Node.Nodes.Add(DocNode);                          //rootNode目录下加载
            }
            /*TreeNode reNode = new TreeNode("回收站",
           IconIndexes.MyComputer, IconIndexes.MyComputer);  //载入显示 选择显示
            reNode.Tag = "回收站";                            //树节点数据
            reNode.Text = "回收站";                           //树节点标签内容
            DocNode.ImageIndex = IconIndexes.recycle;         //设置获取结点显示图片
            DocNode.SelectedImageIndex = IconIndexes.recycle; //设置选择显示图片
            e.Node.Nodes.Add(reNode);*/

            string path;
            if (e.Node.Tag.ToString() == "桌面")
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  //获取桌面地址 
            }
            else
            {
                path = e.Node.Tag.ToString();
            }
            string[] dics = Directory.GetDirectories(path);
            foreach (string dic in dics)
            {
                TreeNode subNode = new TreeNode(new DirectoryInfo(dic).Name); //实例化
                subNode.Name = new DirectoryInfo(dic).FullName;               //完整目录
                subNode.Tag = subNode.Name;
                //subNode.ImageIndex = IconIndexes.ClosedFolder;       //获取节点显示图片
                subNode.SelectedImageIndex = IconIndexes.OpenFolder; //选择节点显示图片

                e.Node.Nodes.Add(subNode);
                subNode.Nodes.Add("");   //加载空节点 实现+号

            }
            DirectoryInfo dir = new DirectoryInfo(path);//实例目录与子目录
            FileInfo[] fileInfo = dir.GetFiles();//获取当前目录文件列表
            long length;
            for (int i = 0; i < fileInfo.Length; i++)
            {
                //int itemNumber = this.listView1.Items.Count;
                ListViewItem listItem = new ListViewItem();
                listItem.Text = "[" + (i + 1) + "] " + fileInfo[i].Name;    //显示文件名
                length = fileInfo[i].Length;                                //获取当前文件大小
                listItem.SubItems.Add(Math.Ceiling(decimal.Divide(fileInfo[i].Length, 1024)) + " KB");

                listItem.SubItems.Add(fileInfo[i].Extension + "文件");//获取文件扩展名时可用Substring除去点 否则显示".txt文件"
                listItem.SubItems.Add(fileInfo[i].LastWriteTime.ToString());//获取文件最后访问时间
                this.listView1.Items.Add(listItem); //加载数据至filesList
            }

        }

        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            try
            {
                //定义变量
                long length;                        //文件大小
                string path;                        //文件路径
                listView1.Items.Clear();
                if (e.Node.Tag.ToString() == "桌面")
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  //获取桌面地址 
                }
                else
                {
                    path = e.Node.Tag.ToString();
                }
                DirectoryInfo dir = new DirectoryInfo(path);//实例目录与子目录
                FileInfo[] fileInfo = dir.GetFiles();//获取当前目录文件列表

                for (int i = 0; i < fileInfo.Length; i++)
                {
                    //int itemNumber = this.listView1.Items.Count;
                    ListViewItem listItem = new ListViewItem();
                    listItem.Text = "[" + (i + 1) + "] " + fileInfo[i].Name;    //显示文件名
                    length = fileInfo[i].Length;                                //获取当前文件大小
                    listItem.SubItems.Add(Math.Ceiling(decimal.Divide(fileInfo[i].Length, 1024)) + " KB");

                    listItem.SubItems.Add(fileInfo[i].Extension + "文件");//获取文件扩展名时可用Substring除去点 否则显示".txt文件"
                    listItem.SubItems.Add(fileInfo[i].LastWriteTime.ToString());//获取文件最后访问时间
                    this.listView1.Items.Add(listItem); //加载数据至filesList
                }

            }
            catch (Exception msg)  //异常处理
            {
                MessageBox.Show(msg.Message);
            }
        }

        private void treeView1_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            treeView1.Nodes[0].Tag = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            

        }

        private void 新建文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path;
             
            path = treeView1.SelectedNode.Tag.ToString();
         
            Directory.CreateDirectory(path + "\\" + "新建文件夹1");
            
           
            treeView1.SelectedNode.Nodes.Add("");   //加载空节点 实现+号
            treeView1.SelectedNode.Collapse();
            treeView1.SelectedNode.Expand();
           
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            

        }

        private void treeView1_NodeMouseClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {

            treeView1.SelectedNode = e.Node;
            e.Node.ContextMenuStrip = contextMenuStrip1;
               
            
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path;

            path = treeView1.SelectedNode.Tag.ToString();
            Directory.Delete(path );

            treeView1.SelectedNode.Remove();   //加载空节点 实现+号//
            //treeView1.SelectedNode.Parent.Collapse();
            
            //Directory.treeView1.SelectedNode.Parent.Expand();
            
        }

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.LabelEdit = true;
            treeView1.SelectedNode.BeginEdit();
            
           
        }
       
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            string path;
            path = treeView1.SelectedNode.Tag.ToString();
            string rename = path.Substring(0, path.LastIndexOf("\\") + 1);//获取文件路径
            string Rename = rename + e.Label;
            Directory.Move(path,Rename );
            e.Node.EndEdit(true);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            treeView1.Visible = false;
            
            label1.Visible = false;
            button1.Visible = false;
            listView1.Dock = DockStyle.Fill;
        }
        

    }
}
