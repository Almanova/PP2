using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace WinFormsSerialization
{
    public partial class Form1 : Form
    {
        public List<string> cmbItems;
        public Form1()
        {
            InitializeComponent();
            cmbItems = new List<string>();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Add(textBox1.Text);
            cmbItems.Add(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists("cmbItems.xml"))
                File.Delete("cmbItems.xml");
            FileStream fs = new FileStream("cmbItems.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(List<string>));
            xs.Serialize(fs, cmbItems);
            fs.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("cmbItems.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(List<string>));
            List<string> list = xs.Deserialize(fs) as List<string>;

            string[] array = list.ToArray();
            comboBox1.Items.AddRange(array);
            foreach (string s in list)
                cmbItems.Add(s);

            fs.Close();
        }
    }
}
