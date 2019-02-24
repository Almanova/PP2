using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Complex
    {
        public List<ComplexNumber> Numbers;

        public Complex()
        {
            Numbers = new List<ComplexNumber>();
        }

        public Complex(ComplexNumber complexNumber)
        {
            Numbers = new List<ComplexNumber>();
        }
    }

    public class ComplexNumber
    {
        public double Re;
        public double Im;

        public ComplexNumber(double Re, double Im)
        {
            this.Re = Re;
            this.Im = Im;
        }

        public ComplexNumber() { }

        public override string ToString()
        {
            return Re + " + " + Im + "i";
        }
    }
}
