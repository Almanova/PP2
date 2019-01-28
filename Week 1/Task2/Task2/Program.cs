using System;

namespace Task2
{
    class Student //creating a class
    {
        public string name;
        public string id;
        public int yearofstudy;

        public Student(string name, string id) //creating a constructor with two parameters
            {
            this.name = name;
            this.id = id;
            }

        public Student() { } //defining empty constructor

        public void Print() //creating function to access name and id
            {
            Console.WriteLine("Student's name: " + name); //printing it out
            Console.WriteLine("Student's id: " + id);
            }

        public void Increment() //creating function to increase a year of study 
            { 
            yearofstudy++; //increasing a year of study by one
            Console.WriteLine("Increased year of study: " + yearofstudy); //printing it out
            }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Student st1 = new Student("aaa", "18BDxxxxxx"); //creating new Student within a class
            st1.Print(); //accessing name and id
            st1.Increment(); //accessing increased year of study
            Console.ReadKey(); ////quiting the program by pressing any key (otherwise it closes immediately)
        }
    }
}
