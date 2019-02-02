using System;
using System.IO;

namespace Task2
{
    class Program
    {
        public static bool isPrime(string n) //writing a function to check for prime numbers
        {
            int a = int.Parse(n); //converting string into an integer
            if (a <= 1) return false; //checking whether number is smaller or equal to 1(these are not considered primes)
            else
            {
                for (int i = 2; i < a; i++) //running through all the numbers between 1 and n
                    if (a % i == 0) //if there is other divider rather than 1 and n
                        return false; //function returns false
            }
            return true;
        }

        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt"); //opening file "input.txt"
            StreamWriter sw = new StreamWriter("output.txt"); //declaring the file for the output
            string s = sr.ReadToEnd(); //reading all the numbers of an input as a string
            string[] arr = s.Split(); //dividing them by space and adding each of them to an array
            foreach(string a in arr) //running through th array
            {
                if (isPrime(a)) //using function to check for primes
                    sw.Write(a + " "); //if number is prime, then printing it in "output.txt"
            }
            sr.Close(); //closing stream
            sw.Close();
        }
    }
}
