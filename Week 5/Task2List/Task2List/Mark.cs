using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2List
{
    public class Mark //creating new class Mark
    {
        public string StudentFirstName; //declaring class properties
        public string StudentLastName;
        public string Subject;
        public int Points;
        public string Letter;

        public Mark(string StudentFirstName, string StudentLastName, string Subject, int Points) //consructor with 4 parameters
        {
            this.StudentFirstName = StudentFirstName;
            this.StudentLastName = StudentLastName;
            this.Subject = Subject;
            this.Points = Points;
        }

        public Mark() { } //empty constructor

        public void GetLetter() //method to get letter according to points
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

        public override string ToString() //ToString() method
        {
            return StudentFirstName + " " + StudentLastName + " - " + Subject + ": " + Letter; //returning in one line 
        }
    }
}
