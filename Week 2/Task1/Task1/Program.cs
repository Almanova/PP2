using System;
using System.IO;

namespace Task1
{
    class Program
    {
        public static string ReverseString(string s) //writing a function that reverses a string
        {
            char[] arr = s.ToCharArray(); //adding every symbol of a string to char array
            Array.Reverse(arr); //reversing an array
            return new string(arr); //returning new reversed string
        }

        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt"); //reaching the "input.txt" file
            string s = sr.ReadToEnd(); //reading a string from the file
            string ss = ReverseString(s); //reversing the string
            if (s == ss) //if the original string is the same as the reversed one, then it's palindrome
                Console.WriteLine("YES"); //printing the result
            else
                Console.WriteLine("NO"); //printing the result
            sr.Close(); //closing stream
            Console.ReadKey(); //quiting the program be pressing any key (or otherwise it closes immediately)
        }
    }
}
