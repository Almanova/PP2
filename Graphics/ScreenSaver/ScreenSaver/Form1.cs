using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSaver
{
    public partial class Form1 : Form
    {
        List<Circle> circles;
        SolidBrush solid = new SolidBrush(Color.Magenta);

        public Form1()
        {
            InitializeComponent();
            circles = new List<Circle>();
            for (int i = 0; i < 3; i++)
            {
                PictureBox pic = new PictureBox();
                pic.Location = new Point(90 * i + 10, 90 * i + 10);
                pic.Size = new Size(90, 90);
                pic.BackColor = Color.Transparent;
                pic.Paint += new PaintEventHandler(Pic_Paint);
                Controls.Add(pic);
                Circle circle = new Circle(2, 2, pic);
                circles.Add(circle);
            }
        }

        private void Pic_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(solid, 0, 0, 90, 90);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach(Circle c in circles)
            {
                Point location = c.pic.Location;

                if (location.X + c.pic.Width > Width || location.X < 0)
                {
                    c.dx *= -1;
                    solid.Color = Color.FromArgb(new Random().Next());
                }

                if (location.Y + c.pic.Height > Height || location.Y < 0)
                {
                    c.dy *= -1;
                    solid.Color = Color.FromArgb(new Random().Next());
                }

                location.X += c.dx;
                location.Y += c.dy;

                c.pic.Location = location;
            }
        }
    }

    public class Circle
    {
        public int dx, dy;
        public PictureBox pic;

        public Circle(int dx, int dy, PictureBox pic)
        {
            this.dx = dx;
            this.dy = dy;
            this.pic = pic;
        }
    }
}
