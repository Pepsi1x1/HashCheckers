using System;
using System.IO;
using System.Windows.Forms;
using HashCheckers;

namespace GUIHashChecker
{
    public partial class HashCheckerForm : Form
    {
        private string _fullPath;
        private static RichTextBoxWriter _rtbWriter;
        public HashCheckerForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void OpenFile()
        {
            var dialogResult = openFileDialog1.ShowDialog();
            if(dialogResult == DialogResult.OK && File.Exists(openFileDialog1.FileName))
            {
                _fullPath = openFileDialog1.FileName;
                toolStripStatusLabel1.Text = openFileDialog1.SafeFileName;
            }
            else
            {
                toolStripStatusLabel1.Text = "Invalid File";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _rtbWriter = new RichTextBoxWriter(richTextBox1);
            foreach (var val in Enum.GetValues(typeof(HashTypes)))
            {
                hashType.Items.Add(val);
            }
            hashType.SelectedIndex = 0;
        }

        private void checkHashButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_fullPath))
            {
                toolStripStatusLabel1.Text = "No file selected";
                MessageBox.Show("Please select a file to check.");
                return;
            }

            Console.SetOut(_rtbWriter);
            switch(ConsoleApp.Run(new[] {_fullPath, expectedHash.Text}, (HashTypes) hashType.SelectedItem))
            {
                case ErrorCodes.InvalidUsage:
                    {
                        toolStripStatusLabel1.Text = "Invalid File";
                    }break;
                case ErrorCodes.InvalidHash:
                    {
                        toolStripStatusLabel1.Text = "File hash does not match expceted hash.";
                    }
                    break;
                case ErrorCodes.Success:
                    {
                        toolStripStatusLabel1.Text = "Success!";
                    }
                    break;
            }
        }

        private void outputLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            richTextBox1.Clear();
        }

        private void clearExpectedLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            expectedHash.Clear();
        }

        private void hashCheckerForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void hashCheckerForm_DragDrop(object sender, DragEventArgs e)
        {
            var filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
            if (File.Exists(filePaths[0]))
                _fullPath = filePaths[0];

            toolStripStatusLabel1.Text = Path.GetFileName(_fullPath);
        }
    }
}
