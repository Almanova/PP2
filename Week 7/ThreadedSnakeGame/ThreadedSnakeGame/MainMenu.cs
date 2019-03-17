using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace ThreadedSnakeGame
{
    public class MainMenu
    {
        public int cursor;
        public string[] options = new string[] 
        {
        "PLAY SINGLE GAME",
        "PLAY COOPERATIVE GAME",
        "RESUME SINGLE GAME",
        "RESUME COOPERATIVE GAME",
        "LEADERBOARDS",
        "QUIT"
        };

        public MainMenu()
        {
            cursor = 0;
        }

        public void Up()
        {
            cursor--;
            if (cursor < 0)
                cursor = 5;
        }

        public void Down()
        {
            cursor++;
            if (cursor == 6)
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
            Console.SetCursorPosition(47, 20);
            Console.WriteLine("Created by Almanova Madina");
            for (int i = 0; i < 6; i++)
            {
                Color(i);
                Console.SetCursorPosition((Console.WindowWidth - options[i].Length) / 2, 10 + i);
                Console.WriteLine(options[i]);
            }
        }

        public void StartMenu()
        {
            while (true)
            {
                Show();
                ConsoleKeyInfo consoleKey = Console.ReadKey();
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
                        Snake snake = new Snake(3, 2, 'o', ConsoleColor.Magenta, 'O', 0);
                        Food food = new Food(0, 0, '*', ConsoleColor.Cyan);
                        Wall wall = new Wall('#', ConsoleColor.DarkYellow);
                        Game game = new Game(nickname, snake, food, wall);
                        Console.Clear();
                        game.Start();
                    }

                    if (cursor == 2)
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
                            List<GameObject> ContinueGameObjects = xs.Deserialize(fs) as List<GameObject>;
                            fs.Close();
                            Console.Clear();
                            Game ContinueGame = new Game(ContinueGameObjects, nickname);
                            ContinueGame.Start();
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

                    if (cursor == 5)
                        Environment.Exit(0);
                }
            }
        }
    }
}
