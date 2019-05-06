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
    public partial class AddSubjects : Form
    {
        Database database = new Database();
        FilterSubjectsBy filterSubjects = FilterSubjectsBy.SubjectName;
        string StudentID;
        string SubjectID;
        string FirstName;
        string LastName;

        public AddSubjects(string StudentID, string FirstName, string LastName)
        {
            InitializeComponent();
            this.StudentID = StudentID;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        public void DataBind(string condition)
        {
            database = new Database();
            string query = "SELECT * FROM Subjects";
            if (condition != "")
            {
                switch (filterSubjects)
                {
                    case FilterSubjectsBy.SubjectID:
                        query += " WHERE SubjectID LIKE '%" + condition + "%'";
                        break;
                    case FilterSubjectsBy.SubjectName:
                        query += " WHERE SubjectName LIKE '%" + condition + "%'";
                        break;
                    case FilterSubjectsBy.Teacher:
                        query += " WHERE Teacher LIKE '%" + condition + "%'";
                        break;
                    case FilterSubjectsBy.Room:
                        query += " WHERE Room LIKE '%" + condition + "%'";
                        break;
                }
            }
            dataGridView1.DataSource = database.GetDataTable(query, 0);
        }

        private void AddSubjects_Load(object sender, EventArgs e)
        {
            label1.Text = FirstName;
            label2.Text = LastName;
            DataBind("");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                int columnIndex = cell.ColumnIndex;
                int rowIndex = cell.RowIndex;
                if (columnIndex == 0)
                    filterSubjects = FilterSubjectsBy.SubjectID;
                else if (columnIndex == 1)
                    filterSubjects = FilterSubjectsBy.SubjectName;
                else if (columnIndex == 2)
                    filterSubjects = FilterSubjectsBy.Teacher;
                else if (columnIndex == 3)
                    filterSubjects = FilterSubjectsBy.Room;
                SubjectID = dataGridView1[0, rowIndex].Value.ToString();
                dataGridView1.CurrentCell = dataGridView1[columnIndex, rowIndex];
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataBind(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO SelectedSubjects (StudentID, SubjectID) VALUES (" +
                    "'" + StudentID + "'," +
                    "'" + SubjectID + "')";

            database.ExecuteSQL(query);
            MessageBox.Show("Subject has been successfully added");
        }
    }
}
