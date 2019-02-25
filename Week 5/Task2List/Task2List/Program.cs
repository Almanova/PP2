using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Task2List
{
    class Program
    {
        public static void In()
        {
            Mark Mark1 = new Mark("Madina", "Almanova", "Programming Principles 2", 99); //creating objects of class
            Mark Mark2 = new Mark("Fox", "Mulder", "History", 67);
            Mark Mark3 = new Mark("Dana", "Scully", "Calculus 2", 86);
            Mark1.GetLetter(); //calling method to get letter
            Mark2.GetLetter();
            Mark3.GetLetter();

            List<Mark> Marks = new List<Mark>(); //creating empty list, to store objects
            Marks.Add(Mark1); //adding objects to list
            Marks.Add(Mark2);
            Marks.Add(Mark3);

            FileStream fs = new FileStream("mark.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite); //opening filestream
            XmlSerializer xs = new XmlSerializer(typeof(List<Mark>)); //declaring serializer
            xs.Serialize(fs, Marks); //serializing
            fs.Close(); //closing stream
        }

        public static void Out()
        {
            FileStream fs = new FileStream("mark.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite); //opening filestream
            XmlSerializer xs = new XmlSerializer(typeof(List<Mark>)); //declaring serializer
            List<Mark> Marks = xs.Deserialize(fs) as List<Mark>; //deserializing

            Marks.ForEach(Console.WriteLine); //proving everything went right by printing out information about marks

            fs.Close(); //closing stream
        }

        static void Main(string[] args)
        {
            In(); //calling function to serialize
            Out(); //calling function to deserialize

            Console.ReadKey(); 
        }
    }
}
