using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JustPictureBoxes
{
    public partial class Form1 : Form
    {
        SolidBrush solid = new SolidBrush(Color.Magenta);
        SolidBrush solid2 = new SolidBrush(Color.Gold);
        int x = 10;

        public Form1()
        {
            InitializeComponent();
            pictureBox2.Parent = pictureBox1;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(solid, 50, 50, 300, 200);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point location = pictureBox2.Location;
            location.X += x;
            if (location.X + pictureBox2.Width > Width || location.X < 0)
                x *= -1;
            pictureBox2.Location = location;
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    e.Graphics.FillEllipse(solid2, 100 * i, 70 * j, 60, 60);
                }
            }
        }
    }
}
