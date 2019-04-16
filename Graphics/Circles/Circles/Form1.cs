using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Circles
{
    public partial class Form1 : Form
    {
        int x = 0;
        int y = 0;
        int d = 0;
        Bitmap bitmap;
        Graphics graphics;
        Pen pen = new Pen(Color.Black, 3);

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            d += 5;
            pen.Color = Color.FromArgb(new Random().Next());
            graphics.DrawEllipse(pen, x - d, y - d, d * 2, d * 2);
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            d = 0;
            x = e.Location.X;
            y = e.Location.Y;
            timer1.Enabled = true;
        }
    }
}