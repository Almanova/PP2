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
    public partial class ViewSubjects : Form
    {
        Database database = new Database();
        string StudentID;
        string FirstName;
        string LastName;

        public ViewSubjects(string StudentID, string FirstName, string LastName)
        {
            InitializeComponent();
            this.StudentID = StudentID;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        public void DataBind()
        {
            database = new Database();
            string query = "SELECT SubjectID, SubjectName, Teacher, Room FROM Subjects " +
                "WHERE SubjectID IN (SELECT SubjectID FROM SelectedSubjects WHERE StudentID = " +
                StudentID + ")";
            dataGridView1.DataSource = database.GetDataTable(query, 0);
        }

        private void ViewSubjects_Load(object sender, EventArgs e)
        {
            label1.Text = FirstName + " " + LastName;
            DataBind();
        }
    }
}
