using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Task2
{
    class Program
    {
        public static void In() 
        {
            Mark Marks = new Mark();
            MarkInfo Mark1 = new MarkInfo("Madina", "Almanova", "Programming Principles 2", 99);
            MarkInfo Mark2 = new MarkInfo("Fox", "Mulder", "History", 67);
            MarkInfo Mark3 = new MarkInfo("Dana", "Scully", "Calculus 2", 86);
            Mark1.GetLetter();
            Mark2.GetLetter();
            Mark3.GetLetter();
            Marks.marks.Add(Mark1);
            Marks.marks.Add(Mark2);
            Marks.marks.Add(Mark3);

            FileStream fs = new FileStream("mark.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(Mark));
            xs.Serialize(fs, Marks);
            fs.Close();
        }

        public static void Out() 
        {
            FileStream fs = new FileStream("mark.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(Mark));
            Mark Marks = xs.Deserialize(fs) as Mark;

            Console.WriteLine(Marks.marks[0]);
            Console.WriteLine(Marks.marks[1]);
            Console.WriteLine(Marks.marks[2]);

            fs.Close();
        }

        static void Main(string[] args)
        {
            In();
            Out();

            Console.ReadKey();
        }
    }
}
