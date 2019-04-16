using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectangle
{
    public partial class Form1 : Form
    {
        Point prevPoint;
        Point curPoint;
        Bitmap bitmap;
        Graphics graphics;
        Pen pen = new Pen(Color.Black, 2);

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
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
            graphics.DrawRectangle(pen, prevPoint.X, prevPoint.Y, curPoint.X - prevPoint.X, curPoint.Y - prevPoint.X);
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(pen, prevPoint.X, prevPoint.Y, curPoint.X - prevPoint.X, curPoint.Y - prevPoint.X);
        }
    }
}
