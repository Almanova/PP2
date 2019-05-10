using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DifferentsDirections
{
    public enum Direction
    {
        Up,
        UpLeft,
        Left,
        LeftDown,
        Down,
        RightDown,
        Right,
        RightUp
    }

    public partial class Form1 : Form
    {
        Direction[] directions = new Direction[] { Direction.Up, Direction.UpLeft, Direction.Right, Direction.RightDown, Direction.Down, Direction.LeftDown, Direction.Left, Direction.RightUp };
        List<Circle> circles = new List<Circle>();
        SolidBrush solid = new SolidBrush(Color.Magenta);
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 500;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            PictureBox pic = new PictureBox();
            pic.Location = new Point(250, 150);
            pic.Size = new Size(50, 50);
            solid.Color = Color.FromArgb(random.Next());
            pic.Paint += new PaintEventHandler(pictureBox_Paint);
            Controls.Add(pic);
            Circle circle = new Circle(solid.Color, pic, directions[random.Next(0, directions.Length)]);
            circles.Add(circle);

            timer2.Enabled = true;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(solid, 0, 0, 50, 50);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach (Circle c in circles)
            {
                Point location = c.pic.Location;
                if (c.direction == Direction.Up)
                    location.Y -= 5;
                else if (c.direction == Direction.RightUp)
                {
                    location.X += 5;
                    location.Y -= 5;
                }
                else if (c.direction == Direction.Right)
                    location.X += 5;
                c.pic.Location = location;
            }
        }
    }

    public class Circle
    {
        public Color color;
        public PictureBox pic;
        public Direction direction;

        public Circle(Color color, PictureBox pic, Direction direction)
        {
            this.color = color;
            this.pic = pic;
            this.direction = direction;
        }
    }
}
