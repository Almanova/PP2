using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sizing
{
    public partial class Form1 : Form
    {
        SolidBrush solid = new SolidBrush(Color.Black);
        int d = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            d = trackBar1.Value;
            solid.Color = Color.FromArgb(new Random().Next());
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(solid, 200 - d, 100 - d, 20 * d, 20 * d);
        }
    }
}
