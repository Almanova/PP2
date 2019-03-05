using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace SnakeGame
{
    public class MainMenu
    {
        public int cursor;
        public string[] options = new string[] { "NEW GAME", "CONTINUE", "QUIT" };

        public MainMenu()
        {
            cursor = 0;
        }

        public void Up()
        {
            cursor--;
            if (cursor < 0)
                cursor = 2;
        }

        public void Down()
        {
            cursor++;
            if (cursor == 3)
                cursor = 0;
        }

        public void Color(int index)
        {
            if (index == cursor)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Magenta;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public void Show()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(55, 7);
            Console.WriteLine("SNAKE GAME");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(48, 20);
            Console.WriteLine("Created by Almanova Madina");
            for (int i = 0; i < 3; i++)
            {
                Color(i);
                Console.SetCursorPosition(55, 10 + i);
                Console.WriteLine(options[i]);
            }
        }

        public void StartMenu()
        {
            ConsoleKeyInfo consoleKey = Console.ReadKey();
            while (true)
            {
                Show();
                consoleKey = Console.ReadKey();
                if (consoleKey.Key == ConsoleKey.UpArrow)
                    Up();
                if (consoleKey.Key == ConsoleKey.DownArrow)
                    Down();
                if (consoleKey.Key == ConsoleKey.Enter)
                {
                    if (cursor == 0)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.SetCursorPosition(55, 7);
                        Console.WriteLine("SNAKE GAME");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.SetCursorPosition(50, 11);
                        Console.WriteLine("Enter your Nickname:");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.SetCursorPosition(45, 20);
                        Console.WriteLine("Press ENTER to start a new game");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(55, 12);
                        string nickname = Console.ReadLine();
                        Game game = new Game(nickname);
                        game.Draw();
                        game.Start();
                    }

                    if (cursor == 1)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.SetCursorPosition(55, 7);
                        Console.WriteLine("SNAKE GAME");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.SetCursorPosition(50, 11);
                        Console.WriteLine("Enter your Nickname:");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.SetCursorPosition(44, 20);
                        Console.WriteLine("Press ENTER to continue the game");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(55, 12);
                        string nickname = Console.ReadLine();
                        string fileName = nickname + ".xml";
                        if (File.Exists(fileName)) 
                        {
                            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                            XmlSerializer xs = new XmlSerializer(typeof(List<GameObject>));
                            List<GameObject> GameObjects2 = xs.Deserialize(fs) as List<GameObject>;
                            fs.Close();
                            Game game2 = new Game(GameObjects2, nickname);
                            game2.Start();
                        }
                        else 
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.SetCursorPosition(55, 7);
                            Console.WriteLine("SNAKE GAME");
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.SetCursorPosition(53, 11);
                            Console.WriteLine("NO SAVED GAMES");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.SetCursorPosition(44, 20);
                            Console.WriteLine("Press any key to go back to Menu");
                            Console.ReadKey();
                        }
                    }

                    if (cursor == 2)
                        Environment.Exit(0);
                }
            }
        }
    }
}
