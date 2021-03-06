﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Xml.Serialization;

namespace ThreadedSnakeGame
{
    public class Game
    {
        public List<GameObject> GameObjects;
        public bool isAlive;
        public string nickname;
        public Snake snake;
        public Food food;
        public Wall wall;
        public int SleepingTime;

        public Game() { }

        public Game(string nickname, Snake snake, Food food, Wall wall)
        {
            GameObjects = new List<GameObject>();
            isAlive = true;
            this.nickname = nickname;
            this.snake = snake;
            this.food = food;
            this.wall = wall;
            SleepingTime = 150;
            GameObjects.Add(snake);
            GameObjects.Add(food);
            GameObjects.Add(wall);
        }

        public Game(List<GameObject> GameObjects, string nickname)
        {
            this.GameObjects = GameObjects;
            isAlive = true;
            snake = (Snake)GameObjects[0];
            food = (Food)GameObjects[1];
            wall = (Wall)GameObjects[2];
            this.nickname = nickname;
            SleepingTime = 150;
        }

        public void Start()
        {
            wall.DrawWall();
            food.GenerateByCoordinates(60, 6);
            food.DrawFood();
            snake.DrawSnake();
            Labels();
            PrintScore();
            PrintLevel();

            Thread thread = new Thread(MoveSnake);
            thread.Start();

            while (isAlive)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey(true);

                if (snake.CheckDirection(consoleKey))
                    snake.ChangeDirection(consoleKey);

                if (consoleKey.Key == ConsoleKey.Escape)
                {
                    thread.Abort();
                    Console.Clear();
                    SaveScore();
                    Serialize();
                }
            }
        }

        public void MoveSnake()
        {
            while (isAlive)
            {
                snake.Move();

                if (snake.CollisionWithObject(food))
                {
                    snake.body.Add(new Point(1, 29));
                    PrintScore();

                    if (snake.body.Count < 11 && snake.body.Count % 5 == 0)
                    {
                        snake.ToTheRightCorner();
                        wall.NextLevel();
                        wall.DrawWall();
                        PrintLevel();
                        food.Clear();
                        food.GenerateByCoordinates(60, 2);
                        SleepingTime -= 50;
                    }

                    else
                    {
                        while (food.CollisionWithObject(snake) || food.CollisionWithObject(wall))
                            food.Generate();
                    }

                    food.DrawFood();
                }

                if (snake.CollisionWithObject(wall))
                    isAlive = false;
                if (snake.OutOfConsole > 3)
                    isAlive = false;
                if (snake.CollisionWithSnake(snake))
                    isAlive = false;

                if (isAlive == false)
                {
                    GameOver();
                    SaveScore();
                    break;
                }

                snake.ClearSnake();
                snake.DrawSnake();

                Thread.Sleep(SleepingTime);
            }
        }

        public void Labels()
        {
            string[] labels = new string[]
            {
                "SNAKE GAME > > > Author : Almanova Madina",
                "| " + nickname + " |",
                "Press ESC to go back to Menu"
            };

            ConsoleColor[] colors = new ConsoleColor[]
            {
                ConsoleColor.Cyan,
                ConsoleColor.Magenta,
                ConsoleColor.DarkGray
            };

            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - labels[i].Length) / 2, 23 + 2 * i);
                Console.ForegroundColor = colors[i];
                Console.WriteLine(labels[i]);
            }

            Console.SetCursorPosition(3, 25);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("YOUR SCORE: ");

            Console.SetCursorPosition(109, 25);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("LEVEL: ");
        }

        public void PrintScore()
        {
            Console.SetCursorPosition(15, 25);
            Console.Write("     ");
            Console.SetCursorPosition(15, 25);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write((snake.body.Count * 10) - 10);
        }

        public void PrintLevel()
        {
            Console.SetCursorPosition(116, 25);
            Console.Write(' ');
            Console.SetCursorPosition(116, 25);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            if (wall.gameLevel == Wall.GameLevel.First)
                Console.Write('1');
            else if (wall.gameLevel == Wall.GameLevel.Second)
                Console.Write('2');
            else if (wall.gameLevel == Wall.GameLevel.Third)
                Console.Write('3');
        }

        public void GameOver()
        {
            if (File.Exists(nickname + ".xml"))
                File.Delete(nickname + ".xml");

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(55, 7);
            Console.WriteLine("SNAKE GAME");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(55, 11);
            Console.WriteLine("GAME OVER");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(53, 12);
            Console.WriteLine("YOUR SCORE: " + ((snake.body.Count * 10) - 10));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(44, 20);
            Console.WriteLine("Press any key to go back to Menu");
        }

        public void Serialize()
        {
            string fileName = nickname + ".xml";
            if (File.Exists(fileName))
                File.Delete(fileName);

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(List<GameObject>));
            xs.Serialize(fs, GameObjects);
            fs.Close();

            MainMenu mainMenu = new MainMenu();
            Console.Clear();
            mainMenu.Show();
            mainMenu.StartMenu();
        }
        
        public void SaveScore()
        {
            Player player = new Player(nickname, (snake.body.Count * 10) - 10);

            if (File.Exists("Scores.xml"))
            {
                FileStream fs = new FileStream("Scores.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                XmlSerializer xs = new XmlSerializer(typeof(List<Player>));
                List<Player> players = xs.Deserialize(fs) as List<Player>;
                fs.Close();

                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].nickname == nickname)
                        players.Remove(players[i]);
                }

                players.Add(player);

                File.Delete("Scores.xml");

                FileStream filestream = new FileStream("Scores.xml", FileMode.Create, FileAccess.ReadWrite);
                xs.Serialize(filestream, players);
                filestream.Close();
            }

            else
            {
                List<Player> players = new List<Player>();
                players.Add(player);
                FileStream fs = new FileStream("Scores.xml", FileMode.Create, FileAccess.ReadWrite);
                XmlSerializer xs = new XmlSerializer(typeof(List<Player>));
                xs.Serialize(fs, players);
                fs.Close();
            }
        }
    }
}
