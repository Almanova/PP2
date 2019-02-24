using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Mark
    {
        public List<MarkInfo> marks;

        public Mark() 
        {
            marks = new List<MarkInfo>();
        }

        public Mark(MarkInfo markInfo) 
        {
            marks = new List<MarkInfo>();
        }
    }

    public class MarkInfo 
    {
        public string StudentFirstName;
        public string StudentLastName;
        public string Subject;
        public int Points;
        public string Letter;

        public MarkInfo(string StudentFirstName, string StudentLastName, string Subject, int Points)
        {
            this.StudentFirstName = StudentFirstName;
            this.StudentLastName = StudentLastName;
            this.Subject = Subject;
            this.Points = Points;
        }

        public MarkInfo() { }

        public void GetLetter() 
        {
            if (Points >= 95) Letter = "A";
            else if (Points >= 90 && Points < 95) Letter = "A-";
            else if (Points >= 85 && Points < 90) Letter = "B+";
            else if (Points >= 80 && Points < 85) Letter = "B";
            else if (Points >= 75 && Points < 80) Letter = "B-";
            else if (Points >= 70 && Points < 75) Letter = "C+";
            else if (Points >= 65 && Points < 70) Letter = "C";
            else if (Points >= 60 && Points < 65) Letter = "C-";
            else if (Points >= 55 && Points < 60) Letter = "D+";
            else if (Points >= 50 && Points < 55) Letter = "D";
            else if (Points < 50) Letter = "F";
        }

        public override string ToString()
        {
            return StudentFirstName + " " + StudentLastName + " - " + Subject + ": " + Letter;
        }
    }
}