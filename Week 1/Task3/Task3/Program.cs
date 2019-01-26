using System;
//adding a library for using lists
using System.Collections.Generic;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            //reading the fisrt line of an input
            string s1 = Console.ReadLine();
            //reading the second line of an input
            string s2 = Console.ReadLine();
            //adding numbers from the second string to an array by splitting them
            string[] arr = s2.Split();
            //creating a new list to collect duplicated array
            List<string> list = new List<string>();
            //runnig through an array
            for (int i=0; i<arr.Length; i++)
            {
                //adding twice every element of an array to the list
                list.Add(arr[i]);
                list.Add(arr[i]);
            }
            //joining elements into a string and separating them by space to print the list in a single line
            var combined = string.Join(" ", list);
            //printing the list in a single line
            Console.WriteLine(combined);
            //quiting the program by pressing any key (otherwise it closes immediately)
            Console.ReadKey();
        }
    }
}
