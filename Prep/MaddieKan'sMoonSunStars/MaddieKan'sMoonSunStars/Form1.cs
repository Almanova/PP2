using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaddieKan_sMoonSunStars
{
    public partial class Form1 : Form
    {
        List<PictureBox> list;
        Bitmap bitmap;
        Graphics graphics;
        int day = 1;

        public Form1()
        {
            InitializeComponent();
            list = new List<PictureBox>();
            list.Add(moon);
            list.Add(sun);
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
            sun.BackColor = Color.Transparent;
            moon.BackColor = Color.Transparent;
            sun.Parent = pictureBox1;
            moon.Parent = pictureBox1;
            timer1.Interval = 100;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawBackground(day);
            foreach (PictureBox pic in list)
            {
                Point location = pic.Location;
                location.X += 10;
                if (location.X > Width)
                {
                    location.X = 0;
                }
                pic.Location = location;
            }
            if (moon.Location.X == 0)
                day++;
        }
        private void DrawBackground(int day)
        {
            if (day % 2 == 0)
            {
                sun.Visible = true;
                moon.Visible = false;
                graphics.FillRectangle(new SolidBrush(Color.Orange), 0, 0, Width, Height);
                graphics.FillRectangle(new SolidBrush(Color.Green), 0, 200, Width, Height);
            }

            else
            {
                sun.Visible = false;
                moon.Visible = true;
                graphics.FillRectangle(new SolidBrush(Color.Blue), 0, 0, Width, Height);
                graphics.FillRectangle(new SolidBrush(Color.Yellow), 50, 50, 20, 20);
                graphics.FillRectangle(new SolidBrush(Color.Yellow), 150, 100, 20, 20);
                graphics.FillRectangle(new SolidBrush(Color.Yellow), 250, 50, 20, 20);
                graphics.FillRectangle(new SolidBrush(Color.Yellow), 350, 100, 20, 20);
                graphics.FillRectangle(new SolidBrush(Color.Yellow), 450, 50, 20, 20);
                graphics.FillRectangle(new SolidBrush(Color.Green), 0, 200, Width, Height);
            }
            pictureBox1.Refresh();
        }
        private void sun_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(Color.Yellow), 0, 0, sun.Width, sun.Height);
        }

        private void moon_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(Color.Yellow), 0, 0, moon.Width, moon.Height);
            e.Graphics.FillEllipse(new SolidBrush(Color.Blue), 15, 0, 50, 50);
        }
    }
}