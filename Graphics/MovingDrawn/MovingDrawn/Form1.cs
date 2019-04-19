using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovingDrawn
{
    public partial class Form1 : Form
    {
        Point prevPoint;
        Point curPoint;
        Bitmap bitmap;
        Graphics graphics;
        Pen pen = new Pen(Color.Magenta, 4);
        int minX, minY, maxX, maxY;
        int xx = 10;
        int xxx = 10;

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox2.Image = bitmap;
            pictureBox2.Parent = pictureBox1;
            pictureBox3.Parent = pictureBox1;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            prevPoint = e.Location;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                curPoint = e.Location;
                pictureBox2.Refresh();
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            DrawRectangle(e.Graphics);
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            DrawRectangle(graphics);
            pictureBox2.Refresh();
        }

        private void DrawRectangle(Graphics g)
        {
            minX = Math.Min(prevPoint.X, curPoint.X);
            maxX = Math.Max(prevPoint.X, curPoint.X);
            minY = Math.Min(prevPoint.Y, curPoint.Y);
            maxY = Math.Max(prevPoint.Y, curPoint.Y);

            g.DrawRectangle(pen, minX, minY, maxX - minX, maxY - minY);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RectangleF cloneRect = new RectangleF(minX, minY, maxX - minX, maxY - minY);
            PixelFormat format = bitmap.PixelFormat;
            Bitmap cloneBitmap = bitmap.Clone(cloneRect, format);

            pictureBox3.Location = new Point(minX, minY);
            pictureBox3.Size = new Size(maxX - minX, maxY - minY);
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = cloneBitmap;

            pictureBox2.Visible = false;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point location = pictureBox3.Location;
            location.X += xx;
            if (location.X + pictureBox3.Width > Width || location.X < 0)
                xx *= -1;
            pictureBox3.Location = location;
        }
    }
}
