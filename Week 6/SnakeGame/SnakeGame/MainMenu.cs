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
                        Game game = new Game();
                        game.Draw();
                        game.Start();
                    }

                    if (cursor == 1)
                    {
                        FileStream fs = new FileStream("savethegame.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                        XmlSerializer xs = new XmlSerializer(typeof(List<GameObject>));
                        List<GameObject> GameObjects2 = xs.Deserialize(fs) as List<GameObject>;
                        fs.Close();
                        Game game2 = new Game(GameObjects2);
                        game2.Start();
                    }

                    if (cursor == 2)
                        Environment.Exit(0);
                }
            }
        }
    }
}
