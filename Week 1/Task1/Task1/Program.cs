using System;
//adding a library to use lists
using System.Collections.Generic;

namespace Task1
{
    class Program
    {
        //writing a function to check for prime numbers
        public static bool isPrime(string n)
        {
            //converting string into an integer
            int a = int.Parse(n);
            if (a<=1) return false;
            else
            {
                for (int i=2; i<a; i++)
                    if (a%i==0)
                        return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            //creating an empty list to collect only primes out of the array
            List<string> list = new List<string>();
            //reading the first line of an input, converting it into an integer
            string s1 = Console.ReadLine();
            int n = int.Parse(s1);
            //reading the second line of an input
            string s2 = Console.ReadLine();
            //adding numbers to an array by separating the second line of an input 
            string[] arr = s2.Split();
            //runnig through the array and checking every element for prime using function 
            for (int i=0; i<n; i++)
            {
                //if the function returns "true", adding an element to the list;
                if (isPrime(arr[i])) {
                    list.Add(arr[i]);
                    }
            }
            //printing the amount of elements of the list
            Console.WriteLine(list.Count);
            //joining elements into a string and separating them by space to print the list in a single line
            var combined = string.Join(" ", list);
            //printing the list in a single line
            Console.WriteLine(combined);
            //quiting the program by pressing any key (otherwise it closes immediately) 
            Console.ReadKey();
        }
    }
}
