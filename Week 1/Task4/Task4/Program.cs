using System;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {   //reading an input number as a string, converting it into an integer
            int n = int.Parse(Console.ReadLine());
            //creating a 2d array of strings
            string[,] a = new string[n,n];
            //running through the array
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    //condition when only elements under diagonal has to be filled filled with "[*]"
                    if (j <= i) a[i, j] = "[*]";

            for (int i = 0; i < n; i++)
            {
                //printing the 2d array
                for (int j = 0; j < n; j++)
                   Console.Write(a[i,j]);
                Console.WriteLine();
            }
            //quiting the program by pressing any key (otherwise it closes immediately) 
            Console.ReadKey();
        }
    }
}
