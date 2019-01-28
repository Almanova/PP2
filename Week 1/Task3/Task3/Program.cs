using System;
using System.Collections.Generic; //adding a library for using lists

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1 = Console.ReadLine(); //reading the fisrt line of an input
            string s2 = Console.ReadLine(); //reading the second line of an input
            string[] arr = s2.Split(); //adding numbers from the second string to an array by splitting them
            List<string> list = new List<string>(); //creating a new list to collect duplicated array
            for (int i=0; i<arr.Length; i++) //runnig through an array
            {
                list.Add(arr[i]); //adding twice every element of an array to the list
                list.Add(arr[i]);
            }
            var combined = string.Join(" ", list); //joining elements into a string and separating them by space to print the list in a single line
            Console.WriteLine(); //adding a space between input and output(just like in sample)
            Console.WriteLine(combined); //printing the list in a single line
            Console.ReadKey(); //quiting the program by pressing any key (otherwise it closes immediately)
        }
    }
}
