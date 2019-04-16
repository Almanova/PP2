using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabTask
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        Graphics graphics;
        Pen pen = new Pen(Color.Black);
        Color[] colors = new Color[] { Color.Black, Color.Blue, Color.White, Color.Red, Color.Yellow, Color.Green };
        SolidBrush solidBrush = new SolidBrush(Color.Black);
        int i = 0;

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (i == 5)
                timer1.Enabled = false;

            solidBrush.Color = colors[i];

            if (i == 0)
            {
                graphics.FillRectangle(solidBrush, 0, 0, pictureBox1.Width - 100, pictureBox1.Height);
            }

            else if (i == 1)
            {
                graphics.FillRectangle(solidBrush, 8, 8, pictureBox1.Width - 116, pictureBox1.Height - 16);
            }

            else if (i == 2)
            {
                graphics.FillEllipse(solidBrush, 40, 60, 25, 25);
                graphics.FillEllipse(solidBrush, 280, 45, 25, 25);
                graphics.FillEllipse(solidBrush, 50, 320, 25, 25);
                graphics.FillEllipse(solidBrush, 290, 305, 25, 25);
                graphics.FillEllipse(solidBrush, 420, 80, 25, 25);
                graphics.FillEllipse(solidBrush, 600, 150, 25, 25);
                graphics.FillEllipse(solidBrush, 520, 210, 25, 25);
                graphics.FillEllipse(solidBrush, 610, 340, 25, 25);

                graphics.FillRectangle(solidBrush, 450, 15, 170, 30);

                Point p = new Point(100, 30);
                Font font = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);
                TextRenderer.DrawText(graphics, "Level", font, p, Color.Green);
            }

            else if (i == 3)
            {
                Point[] p = new Point[8];
                p[0] = new Point(0, 25);
                p[1] = new Point(20, 20);
                p[2] = new Point(25, 0);
                p[3] = new Point(30, 20);
                p[4] = new Point(50, 25);
                p[5] = new Point(30, 30);
                p[6] = new Point(25, 50);
                p[7] = new Point(20, 30);
                graphics.FillPolygon(solidBrush, p);
            }

            else if (i == 4)
            {
                Point[] spaceship = new Point[6];
                spaceship[0] = new Point(300, 200);
                spaceship[1] = new Point(350, 170);
                spaceship[2] = new Point(400, 200);
                spaceship[5] = new Point(300, 240);
                spaceship[4] = new Point(350, 270);
                spaceship[3] = new Point(400, 240);
                graphics.FillPolygon(solidBrush, spaceship);
            }

            else if (i == 5)
            {
               
            }

            pictureBox1.Refresh();
            i++;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
