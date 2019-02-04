using System;
using System.IO;

namespace Task4
{
    class Program
    {
        public static void Create() //method to create a file within a directory
        {
            string filePath = @"C:\Users\Asus\Documents\Directory\File.txt"; //path where new file has to be created
            File.Create(filePath).Close(); //creating a file
        }
         
        public static void Copy() //method to copy file to another depository and delete the original one 
        {
            string fileName = "File.txt"; //declaring file name
            string path = @"C:\Users\Asus\Documents\Directory"; //original path
            string path1 = @"C:\Users\Asus\Documents\PP2\Week 2"; //target path

            string sourceFile = Path.Combine(path, fileName); //combining directory and file paths
            string destFile = Path.Combine(path1, fileName);

            File.Copy(sourceFile, destFile, true); //coping file
            File.Delete(sourceFile); //deleting file
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Press UpArrow to create a file..."); //printing out text
            ConsoleKeyInfo button = Console.ReadKey(); //adding the usage of keyboard
            while (button.Key != ConsoleKey.Escape)
            {
                if (button.Key == ConsoleKey.UpArrow) //pressing UpArrow
                {
                    Create(); //calling method to create a file
                    Console.WriteLine("File has been successfully created");
                    Console.WriteLine();
                    Console.WriteLine("Press RightArrow to copy this file and delete the original one...");
                }

                if (button.Key == ConsoleKey.RightArrow) //pressing RightArrow
                {
                    Copy(); //calling method to copy this file
                    Console.WriteLine("File has been copied");
                    Console.WriteLine();
                    Console.WriteLine("Press Escape to quit...");
                }

                button = Console.ReadKey(); //quiting program by pressing Escape
            }
        }
    }
}