using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Complex //creating new class
    {
        public List<ComplexNumber> Numbers; //one property - list of complex numbers

        public Complex() //empty constructor
        {
            Numbers = new List<ComplexNumber>();
        }

        public Complex(ComplexNumber complexNumber) //constructor with one parameter
        {
            Numbers = new List<ComplexNumber>();
        }
    }

    public class ComplexNumber //creating class ComplexNumber
    {
        public double Re; //real part of a number
        public double Im; //imaginary part of a number

        public ComplexNumber(double Re, double Im) //consrtuctor with 2 parameters
        {
            this.Re = Re;
            this.Im = Im;
        }

        public ComplexNumber() { } //empty constructor

        public override string ToString() //providing ToString() method
        {
            return Re + " + " + Im + "i"; //returning whole number
        }
    }
}
