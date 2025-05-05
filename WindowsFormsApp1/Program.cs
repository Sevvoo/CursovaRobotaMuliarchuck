using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace StackQueueAppWinForms
{
    /// <summary>
    /// Вузол двозв'язного списку для збереження цілочисельних значень.
    /// </summary>
    public class Node
    {
        public int Data;
        public Node Next;
        public Node Prev;

        public Node(int data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }

    /// <summary>
    /// Саморозширюваний двозв'язний список з операціями додавання, видалення, вставки та очищення.
    /// </summary>
    public class MyList
    {
        private Node head;
        private Node tail;

        public MyList()
        {
            head = null;
            tail = null;
        }

        public void Clear()
        {
            head = null;
            tail = null;
        }

        public void AddFirst(int data)
        {
            var node = new Node(data);
            if (head == null)
            {
                head = node;
                tail = node;
            }
            else
            {
                node.Next = head;
                head.Prev = node;
                head = node;
            }
        }

        public void AddLast(int data)
        {
            var node = new Node(data);
            if (tail == null)
            {
                head = node;
                tail = node;
            }
            else
            {
                tail.Next = node;
                node.Prev = tail;
                tail = node;
            }
        }

        public bool RemoveAll(int data)
        {
            bool removed = false;
            var current = head;
            while (current != null)
            {
                if (current.Data == data)
                {
                    removed = true;
                    var prev = current.Prev;
                    var next = current.Next;
                    if (prev != null) prev.Next = next;
                    else head = next;

                    if (next != null) next.Prev = prev;
                    else tail = prev;

                    current = next;
                }
                else
                {
                    current = current.Next;
                }
            }
            return removed;
        }

        public bool InsertAfter(int target, int data)
        {
            var current = head;
            while (current != null)
            {
                if (current.Data == target)
                {
                    var newNode = new Node(data);
                    var next = current.Next;
                    current.Next = newNode;
                    newNode.Prev = current;
                    newNode.Next = next;
                    if (next != null) next.Prev = newNode;
                    else tail = newNode;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public bool InsertBefore(int target, int data)
        {
            var current = head;
            while (current != null)
            {
                if (current.Data == target)
                {
                    var newNode = new Node(data);
                    var prev = current.Prev;
                    newNode.Next = current;
                    current.Prev = newNode;
                    newNode.Prev = prev;
                    if (prev != null) prev.Next = newNode;
                    else head = newNode;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public List<int> ToList()
        {
            var result = new List<int>();
            var current = head;
            while (current != null)
            {
                result.Add(current.Data);
                current = current.Next;
            }
            return result;
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        private MyList list;
        private TextBox textBoxValue;
        private TextBox textBoxTarget;
        private Button btnAddFirst;
        private Button btnAddLast;
        private Button btnRemoveAll;
        private Button btnInsertAfter;
        private Button btnInsertBefore;
        private Button btnShowList;
        private Button btnSaveFile;
        private Button btnLoadFile;
        private ListBox listBoxDisplay;
        private readonly string defaultFilePath;

        public MainForm()
        {
            list = new MyList();
            defaultFilePath = Path.Combine(Application.StartupPath, "listdata.txt");
            InitializeComponent();
            LoadDefaultFile();
            this.FormClosing += MainForm_FormClosing;
        }

        private void InitializeComponent()
        {
            this.Text = "Doubly Linked List Manager";
            this.ClientSize = new Size(400, 450);

            var lblValue = new Label { Text = "Value:", Location = new Point(20, 20), AutoSize = true };
            textBoxValue = new TextBox { Location = new Point(80, 18), Width = 100 };

            var lblTarget = new Label { Text = "Target:", Location = new Point(200, 20), AutoSize = true };
            textBoxTarget = new TextBox { Location = new Point(260, 18), Width = 100 };

            btnAddFirst = new Button { Text = "Add First", Location = new Point(20, 60), Width = 100 };
            btnAddLast = new Button { Text = "Add Last", Location = new Point(140, 60), Width = 100 };
            btnRemoveAll = new Button { Text = "Remove All", Location = new Point(260, 60), Width = 100 };

            btnInsertAfter = new Button { Text = "Insert After", Location = new Point(20, 100), Width = 100 };
            btnInsertBefore = new Button { Text = "Insert Before", Location = new Point(140, 100), Width = 100 };
            btnShowList = new Button { Text = "Show List", Location = new Point(260, 100), Width = 100 };

            btnSaveFile = new Button { Text = "Save to File", Location = new Point(20, 380), Width = 100 };
            btnLoadFile = new Button { Text = "Load from File", Location = new Point(140, 380), Width = 100 };

            listBoxDisplay = new ListBox { Location = new Point(20, 150), Size = new Size(340, 220) };

            this.Controls.Add(lblValue);
            this.Controls.Add(textBoxValue);
            this.Controls.Add(lblTarget);
            this.Controls.Add(textBoxTarget);
            this.Controls.Add(btnAddFirst);
            this.Controls.Add(btnAddLast);
            this.Controls.Add(btnRemoveAll);
            this.Controls.Add(btnInsertAfter);
            this.Controls.Add(btnInsertBefore);
            this.Controls.Add(btnShowList);
            this.Controls.Add(btnSaveFile);
            this.Controls.Add(btnLoadFile);
            this.Controls.Add(listBoxDisplay);

            btnAddFirst.Click += BtnAddFirst_Click;
            btnAddLast.Click += BtnAddLast_Click;
            btnRemoveAll.Click += BtnRemoveAll_Click;
            btnInsertAfter.Click += BtnInsertAfter_Click;
            btnInsertBefore.Click += BtnInsertBefore_Click;
            btnShowList.Click += BtnShowList_Click;
            btnSaveFile.Click += BtnSaveFile_Click;
            btnLoadFile.Click += BtnLoadFile_Click;
        }

        private void LoadDefaultFile()
        {
            if (File.Exists(defaultFilePath))
            {
                list.Clear();
                foreach (var line in File.ReadAllLines(defaultFilePath).Where(l => int.TryParse(l, out _)))
                {
                    list.AddLast(int.Parse(line));
                }
            }
        }

        private void SaveDefaultFile()
        {
            var items = list.ToList().Select(i => i.ToString());
            File.WriteAllLines(defaultFilePath, items);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveDefaultFile();
        }

        private void BtnAddFirst_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxValue.Text, out int val))
            {
                list.AddFirst(val);
                MessageBox.Show("Element added at the beginning.");
            }
            else MessageBox.Show("Invalid value.");
        }

        private void BtnAddLast_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxValue.Text, out int val))
            {
                list.AddLast(val);
                MessageBox.Show("Element added at the end.");
            }
            else MessageBox.Show("Invalid value.");
        }

        private void BtnRemoveAll_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxValue.Text, out int val))
            {
                bool removed = list.RemoveAll(val);
                MessageBox.Show(removed ? "Element(s) removed." : "Element not found.");
            }
            else MessageBox.Show("Invalid value.");
        }

        private void BtnInsertAfter_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxTarget.Text, out int target) && int.TryParse(textBoxValue.Text, out int val))
            {
                bool ok = list.InsertAfter(target, val);
                MessageBox.Show(ok ? "Inserted after target." : "Target not found.");
            }
            else MessageBox.Show("Invalid target or value.");
        }

        private void BtnInsertBefore_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxTarget.Text, out int target) && int.TryParse(textBoxValue.Text, out int val))
            {
                bool ok = list.InsertBefore(target, val);
                MessageBox.Show(ok ? "Inserted before target." : "Target not found.");
            }
            else MessageBox.Show("Invalid target or value.");
        }

        private void BtnShowList_Click(object sender, EventArgs e)
        {
            listBoxDisplay.Items.Clear();
            var items = list.ToList();
            if (items.Count == 0)
            {
                listBoxDisplay.Items.Add("List is empty.");
            }
            else
            {
                foreach (var data in items)
                    listBoxDisplay.Items.Add(data);
            }
        }

        private void BtnSaveFile_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog { Filter = "Text Files|*.txt|All Files|*.*" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllLines(dlg.FileName, list.ToList().Select(i => i.ToString()));
                }
            }
        }

        private void BtnLoadFile_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Filter = "Text Files|*.txt|All Files|*.*" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    list.Clear();
                    foreach (var line in File.ReadAllLines(dlg.FileName).Where(l => int.TryParse(l, out _)))
                        list.AddLast(int.Parse(line));
                }
            }
        }
    }
}
