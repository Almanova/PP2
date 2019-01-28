using System;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine()); //reading an input number as a string, converting it into an integer
            string[,] a = new string[n,n]; //creating a 2d array of strings
            for (int i = 0; i < n; i++) //running through the array
                for (int j = 0; j < n; j++)
                    if (j <= i) a[i, j] = "[*]"; //condition when only elements under diagonal has to be filled filled with "[*]"

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++) //printing the 2d array
                   Console.Write(a[i,j]);
                Console.WriteLine();
            }
            Console.ReadKey(); //quiting the program by pressing any key (otherwise it closes immediately)
        }
    }
}
