using System;
using System.IO;

namespace FarManager
{
    class FarManager //creating new class
    {
        public int cursor; //declaring global class variables, cursor for pointing strings
        public int size; //size of directory
        public string path; //string path for the main directory

        public FarManager(string path) //creating a constructor for new object of a class
        {
            this.path = path; //parametr for string path
            cursor = 0; //placing cursor to zero line
            DirectoryInfo directory = new DirectoryInfo(path); //specifying directory to manipulate
            size = directory.GetFileSystemInfos().Length; //number of files and directories within main directory
        }

        public void Up() //method to replace cursor up
        {
            cursor--;
            if (cursor < 0)
                cursor = size - 1;
        }

        public void Down() //method to replace cursor down
        {
            cursor++;
            if (cursor == size)
                cursor = 0;
        }

        public void Color(FileSystemInfo fs, int index) //a way to define directories and files with color
        {
            if (index == cursor) //color for cursor
            {
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (fs.GetType() == typeof(FileInfo)) //files will be represented in yellow
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else //directories will be represented in white
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void Show() //method to print subdirectories and files within a directory
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.SetCursorPosition(6, 0);
            Console.WriteLine(path); //adding header with directory string path
            Console.WriteLine();
            DirectoryInfo directory = new DirectoryInfo(path);
            FileSystemInfo[] fs = directory.GetFileSystemInfos(); //getting all the files and directories
            size = fs.Length; //getting their total number
            for (int i = 0; i < fs.Length; i++) //running through every file and folder
            {
                Color(fs[i], i); //coloring them
                Console.WriteLine(i + 1 + ". " + fs[i].Name); //printing them and adding number of order to every line
            }
        }

        public void Start() //function to manipulate directory
        {
            DirectoryInfo directory = new DirectoryInfo(path); //getting information about particular directory
            ConsoleKeyInfo consoleKey = Console.ReadKey(); //adding variable to work with keyboard
            FileSystemInfo fs = null; //empty file system
            while (true) //endless loop
            {
                Show(); //printing files and directories of a main directory
                consoleKey = Console.ReadKey(); //reading keys
                int k = 0;
                for (int i = 0; i < directory.GetFileSystemInfos().Length; i++) //running through main directory
                {
                    if (cursor == k) 
                    {
                        fs = directory.GetFileSystemInfos()[i]; //assigning a file or directory to fs where cursor is placed
                        break;
                    }
                    k++;
                }
                if (consoleKey.Key == ConsoleKey.Escape) //pressing Escape to go back to parent folder
                {
                    cursor = 0;
                    directory = directory.Parent;
                    path = directory.FullName;
                }
                if (consoleKey.Key == ConsoleKey.UpArrow) //pressing UpArrow to move cursor up
                    Up();
                if (consoleKey.Key == ConsoleKey.DownArrow) //pressing DownArrow to move cursor down
                    Down();
                if (consoleKey.Key == ConsoleKey.Enter) //pressing Enter to open a directory or open a text file
                {
                    if (fs.GetType() == typeof(DirectoryInfo)) //case for directory
                    {
                        cursor = 0;
                        directory = new DirectoryInfo(fs.FullName);
                        path = fs.FullName;
                    }
                    if (fs.GetType() == typeof(FileInfo)) //case for file (opening txt file)
                    {
                        Console.BackgroundColor = ConsoleColor.White; //coloring console in white
                        Console.ForegroundColor = ConsoleColor.Black; //printing text in black
                        Console.Clear();
                        StreamReader sr = new StreamReader(fs.FullName); //opening file
                        string s = sr.ReadToEnd(); //reading the whole text
                        Console.WriteLine(s); //printing it
                        sr.Close(); //closing stream
                        Console.ReadKey();
                    }
                }
                if (consoleKey.Key == ConsoleKey.Delete) //pressing Delete to to delete files and directories
                {
                    if (fs.GetType() == typeof(FileInfo)) //checking for file
                        File.Delete(fs.FullName); //deleting file
                    else if (fs.GetType() == typeof(DirectoryInfo)) //checking for directory
                        Directory.Delete(fs.FullName, true); //deleting directory
                }
                if (consoleKey.Key == ConsoleKey.F1)  //pressing F1 to rename files and directories
                {
                    if (fs.GetType() == typeof(FileInfo)) //checking for file
                    {
                        Console.BackgroundColor = ConsoleColor.Gray; //coloring console in gray
                        Console.Clear(); //clearing it
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Type in new file name:");
                        string newFileName = Console.ReadLine(); //reading new file name
                        string sourceFile = fs.FullName; //declaring original path
                        string destFile = path + "/" + newFileName; //declaring target path
                        File.Move(sourceFile, destFile); //moving file within the same directory but with new name
                    }
                    if (fs.GetType() == typeof(DirectoryInfo)) //checking for directory
                    {
                        Console.BackgroundColor = ConsoleColor.Gray; //coloring console in gray
                        Console.Clear(); //clearing it
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("Type in new directory name:");
                        string newDirectoryName = Console.ReadLine(); //reading new directory name
                        string sourceDirectory = fs.FullName; //declaring original path
                        string destDirectory = path + "/" + newDirectoryName; //declaring target path
                        Directory.Move(sourceDirectory, destDirectory); //moving directory within the same parent directory but with new name
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:/Users/Asus/Documents/Directory"; //declaring string path to work with
            FarManager farManager = new FarManager(path); //creating a new object within a class
            farManager.Show(); //printing files, directories and header
            farManager.Start(); //calling main function to manipulate directories
        }
    }
}