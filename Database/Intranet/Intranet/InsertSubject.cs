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
    public partial class InsertSubject : Form
    {
        Database database = new Database();

        public InsertSubject()
        {
            InitializeComponent();
        }

        public void DataBind()
        {
            database = new Database();
            string query = "SELECT * FROM Subjects";
            dataGridView1.DataSource = database.GetDataTable(query, 0);
        }

        private void InsertSubject_Load(object sender, EventArgs e)
        {
            DataBind();
        }

        public void InsertData()
        {
            if (textSubjectName.Text == "" || textTeacher.Text == "" || textRoom.Text == "")
                MessageBox.Show("Please, fill all the fields");
            else
            {
                string query = "INSERT INTO Subjects (SubjectName, Teacher, Room) VALUES (" +
                    "'" + textSubjectName.Text + "'," +
                    "'" + textTeacher.Text + "', " +
                    textRoom.Text + ")";

                database.ExecuteSQL(query);
                MessageBox.Show("New Subject has been successfully added");
                DataBind();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InsertData();
        }
    }
}
