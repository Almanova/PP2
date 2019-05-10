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

namespace Directories
{
    public partial class Form1 : Form
    {
        int folders = 0;
        int files = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            DirectoryInfo dir = new DirectoryInfo(path);
            FileSystemInfo[] fs = dir.GetFileSystemInfos();
            for (int i = 0; i < fs.Length; i++)
            {
                if (fs[i].GetType() == typeof(FileInfo))
                    files++;
                else
                    folders++;
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            /*for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    files--;
                    e.Graphics.FillRectangle(new SolidBrush(Color.Black), 40 * j + 2, 40 * i + 2, 40, 40);
                    if (files == 0)
                        break;
                }
            }*/
            for (int j = 0; j < files; j++)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), j * 40, 0, 35, 35);
            }
            for (int j = 0; j < folders; j++)
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.Red), j * 40, 0, 35, 35);
            }
        }
    }
}
