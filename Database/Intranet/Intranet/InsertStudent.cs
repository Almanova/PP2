using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intranet
{
    public partial class InsertStudent : Form
    {
        Database database = new Database();

        public InsertStudent()
        {
            InitializeComponent();
        }

        public void DataBind()
        {
            database = new Database();
            string query = "SELECT * FROM Students";
            dataGridView1.DataSource = database.GetDataTable(query, 0);
        }

        private void InsertStudent_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        public void InsertData()
        {
            if (textFirstName.Text == "" || textLastName.Text == "")
                MessageBox.Show("Please, fill all the fields");
            else
            {
                string query = "INSERT INTO Students (FirstName, LastName) VALUES (" +
                    "'" + textFirstName.Text + "'," +
                    "'" + textLastName.Text + "')";

                database.ExecuteSQL(query);
                MessageBox.Show("New Student has been successfully added");
                DataBind();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertData();
        }
    }
}
