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
            Complex ComplexNumbers = new Complex(); //creating new object of class(list containing complex numbers)
            ComplexNumber Number1 = new ComplexNumber(3, 5); //creating objects of ComplexNumbers
            ComplexNumber Number2 = new ComplexNumber(4, 6);
            ComplexNumbers.Numbers.Add(Number1); //storing them in the list
            ComplexNumbers.Numbers.Add(Number2);

            FileStream fs = new FileStream("complex.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite); //opening filestream
            XmlSerializer xs = new XmlSerializer(typeof(Complex)); //declaring serializer
            xs.Serialize(fs, ComplexNumbers); //serializing
            fs.Close(); //closing stream
        }

        public static void Out()
        {
            FileStream fs = new FileStream("complex.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite); //opening filestream
            XmlSerializer xs = new XmlSerializer(typeof(Complex)); //declaring serializer
            Complex ComplexNumbers = xs.Deserialize(fs) as Complex; //deserializing

            Console.WriteLine(ComplexNumbers.Numbers[0]); //printing out deserialized information to prove everything went right
            Console.WriteLine(ComplexNumbers.Numbers[1]);

            fs.Close(); //closing stream
        }

        static void Main(string[] args)
        {
            In(); //calling function to serialize
            Out(); //calling funcion to deserialize

            Console.ReadKey();
        }
    }
}
