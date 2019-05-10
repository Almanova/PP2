using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectCircles
{
    public partial class Form1 : Form
    {
        Stack<Circle> red;
        Stack<Circle> green;
        SolidBrush solid = new SolidBrush(Color.Magenta);
        Color[] colors = new Color[] { Color.Green, Color.Red };

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (red.Count > 0 && green.Count > 0)
            {
                red.Peek().pic.Visible = false;
                green.Peek().pic.Visible = false;
                red.Pop();
                green.Pop();
            }   
        }

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 2000;
            timer2.Interval = 2000;
            timer1.Enabled = true;
            red = new Stack<Circle>();
            green = new Stack<Circle>();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            PictureBox pic = new PictureBox();
            solid.Color = colors[random.Next(0, colors.Length)];
            pic.Location = new Point(random.Next(0, Width - 40), random.Next(0, Height - 40));
            pic.Paint += new PaintEventHandler(pictureBox_Paint);
            pic.Size = new Size(30, 30);
            Controls.Add(pic);
            Circle circle = new Circle(solid.Color, pic);
            if (circle.color == Color.Red)
                red.Push(circle);
            else
                green.Push(circle);
            timer2.Enabled = true;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(solid, 0, 0, 30, 30);
        }
    }

    public class Circle
    {
        public Color color;
        public PictureBox pic;

        public Circle(Color color, PictureBox pic)
        {
            this.color = color;
            this.pic = pic;
        }
    }
}
