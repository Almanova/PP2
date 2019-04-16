using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tiles
{
    public partial class Form1 : Form
    {
        int dx = 10, dy = 10;
        PictureBox ball = new PictureBox();
        List<PictureBox> tiles;

        public Form1()
        {
            InitializeComponent();
            tiles = new List<PictureBox>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Color[] colors = new Color[] { Color.Gold, Color.Salmon, Color.Cyan, Color.Maroon, Color.MediumSeaGreen, Color.MediumTurquoise };
            Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(50, 30);
                    pic.Location = new Point(j * 50, i * 30);
                    pic.BackColor = colors[random.Next(0, colors.Length)];
                    Controls.Add(pic);
                    tiles.Add(pic);
                }
            }

            ball.Size = new Size(30, 30);
            ball.Location = new Point(button1.Location.X + button1.Width / 2 - 15, button1.Location.Y - 30);
            ball.Paint += new PaintEventHandler(pictureBox_Paint);
            Controls.Add(ball);
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush solid = new SolidBrush(Color.Purple);
            e.Graphics.FillEllipse(solid, 0, 0, 30, 30);
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
            Point location = button1.Location;
            string btn_pressed = e.KeyCode + " ";
            if (btn_pressed == "A " && location.X > 0)
                location.X -= 10;
            else if (btn_pressed == "D " && location.X + button1.Width < Width)
                location.X += 10;
            button1.Location = location;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point location = ball.Location;

            if (location.X + 30 > Width || location.X < 0)
                dx *= -1;

            if (location.Y < 0)
                dy *= -1;

            if (IsCollision())
                dy *= -1;

            location.X += dx;
            location.Y += dy;

            IsCollisionWithTile();

            ball.Location = location;
        }

        private bool IsCollision()
        {
            if (ball.Location.X > button1.Location.X && ball.Location.X + 30 < button1.Location.X + button1.Width && ball.Location.Y + 30 == button1.Location.Y)
                return true;
            return false;
        }

        private void IsCollisionWithTile()
        {
            int k = 0;
            for (int i = 1; i <= tiles.Count; i++)
            {
                if (ball.Location.X > tiles[i-1].Location.X && ball.Location.X + 30 < tiles[i-1].Location.X + tiles[i-1].Width && ball.Location.Y - 30 < tiles[i-1].Location.Y)
                    k = i;
            }
            if (k != 0)
            {
                dy *= -1;
                tiles[k - 1].Visible = false;
                tiles.Remove(tiles[k - 1]);
            }
        }
    }
}
