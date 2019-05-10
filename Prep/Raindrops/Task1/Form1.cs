using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1
{
    public partial class Form1 : Form
    {
        int x = 0;
        int y = 0;
        int d = 25;
        SolidBrush solid = new SolidBrush(Color.Blue);
        List<PictureBox> pics;

        public Form1()
        {
            InitializeComponent();
            pics = new List<PictureBox>();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            x = e.Location.X;
            y = e.Location.Y;
            PictureBox pic = new PictureBox();
            pic.Location = new Point(x - d, y - d);
            pic.Size = new Size(50, 50);
            pic.Paint += new PaintEventHandler(pictureBox_Paint);
            Controls.Add(pic);
            pics.Add(pic);

            timer1.Enabled = true;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(solid, 0, 0, 50, 50);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (PictureBox p in pics)
            {
                Point location = p.Location;
                location.Y += 10;
                p.Location = location;
            }
        }   
    }
}
