using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Task1
{
    class Program
    {
        public static void In()
        {
            Complex ComplexNumbers = new Complex();
            ComplexNumber Number1 = new ComplexNumber(3, 5);
            ComplexNumber Number2 = new ComplexNumber(4, 6);
            ComplexNumbers.Numbers.Add(Number1);
            ComplexNumbers.Numbers.Add(Number2);

            FileStream fs = new FileStream("complex.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(Complex));
            xs.Serialize(fs, ComplexNumbers);
            fs.Close();
        }

        public static void Out()
        {
            FileStream fs = new FileStream("complex.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(Complex));
            Complex ComplexNumbers = xs.Deserialize(fs) as Complex;

            Console.WriteLine(ComplexNumbers.Numbers[0]);
            Console.WriteLine(ComplexNumbers.Numbers[1]);

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
