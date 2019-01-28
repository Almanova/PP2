using System;
using System.Collections.Generic; //adding a library to use lists

namespace Task1
{
    class Program
    {
        public static bool isPrime(string n) //writing a function to check for prime numbers
        {
            int a = int.Parse(n); //converting string into an integer
            if (a<=1) return false; //checking whether number is smaller or equal to 1(these are not considered primes)
            else
            {
                for (int i=2; i<a; i++) //running through all the numbers between 1 and n
                    if (a%i==0) //if there is other divider rather than 1 and n
                        return false; //function returns false
            }
            return true;
        }

        static void Main(string[] args)
        {
            List<string> list = new List<string>(); //creating an empty list to collect only primes out of the array
            string s1 = Console.ReadLine(); //reading the first line of an input
            int n = int.Parse(s1); //converting it into an integer
            string s2 = Console.ReadLine(); //reading the second line of an input
            string[] arr = s2.Split(); //adding numbers to an array by separating the second line of an input 
            for (int i=0; i<n; i++) //runnig through the array and checking every element for prime using function 
            {
                if (isPrime(arr[i])) { //if the function returns "true", adding an element to the list;
                    list.Add(arr[i]);
                    }
            }
            Console.WriteLine(list.Count); //printing the amount of elements of the list
            var combined = string.Join(" ", list); //joining elements into a string and separating them by space to print the list in a single line
            Console.WriteLine(combined); //printing the list in a single line
            Console.ReadKey(); //quiting the program by pressing any key (otherwise it closes immediately)
        }
    }
}