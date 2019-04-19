using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace ScreenSaver2
{
    public partial class Form1 : Form
    {
        Point prevPoint;
        Point curPoint;
        Bitmap bitmap;
        Graphics graphics;
        Pen pen = new Pen(Color.Maroon, 3);
        List<PictureBox> pictures;
        int i = 2;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
            pictures = new List<PictureBox>();
            pictures.Add(pictureBox2);
            pictures.Add(pictureBox3);
            pictures.Add(pictureBox4);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            prevPoint = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                curPoint = e.Location;
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            graphics.DrawEllipse(pen, prevPoint.X, prevPoint.Y, curPoint.X - prevPoint.X, curPoint.Y - prevPoint.Y);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawEllipse(pen, prevPoint.X, prevPoint.Y, curPoint.X - prevPoint.X, curPoint.Y - prevPoint.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < pictures.Count; j++)
            {
                if (pictures[j].Name == "pictureBox" + i.ToString())
                {
                    RectangleF cloneRect = new RectangleF(prevPoint.X, prevPoint.Y, curPoint.X - prevPoint.X, curPoint.Y - prevPoint.Y);
                    PixelFormat format = bitmap.PixelFormat;
                    Bitmap cloneBitmap = bitmap.Clone(cloneRect, format);

                    pictures[j].Location = new Point(prevPoint.X, prevPoint.Y);
                    pictures[j].Size = new Size(curPoint.X - prevPoint.X, curPoint.Y - prevPoint.Y);
                    pictures[j].BackColor = Color.Transparent;
                    pictures[j].Image = cloneBitmap;
                }
            }

            i++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
        }
    }
}
