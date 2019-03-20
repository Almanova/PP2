using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml.Serialization;
using System.IO;

namespace ThreadedSnakeGame
{
    public class CooperativeGame
    {
        public List<GameObject> GameObjects;
        public bool isAlive;
        public string FirstPlayer;
        public string SecondPlayer;
        public Snake FirstSnake;
        public Snake SecondSnake;
        public Food food;
        public Wall wall;
        public int SleepingTime;

        public CooperativeGame() { }

        public CooperativeGame(string FirstPlayer, 
            string SecondPlayer, 
            Snake FirstSnake, 
            Snake SecondSnake, 
            Food food,
            Wall wall)
        {
            GameObjects = new List<GameObject>();
            isAlive = true;
            this.FirstPlayer = FirstPlayer;
            this.SecondPlayer = SecondPlayer;
            this.FirstSnake = FirstSnake;
            this.SecondSnake = SecondSnake;
            this.food = food;
            this.wall = wall;
            SleepingTime = 150;
            GameObjects.Add(FirstSnake);
            GameObjects.Add(SecondSnake);
            GameObjects.Add(food);
            GameObjects.Add(wall);
        }

        public CooperativeGame(List<GameObject> GameObjects, string FirstPlayer, string SecondPlayer)
        {
            this.GameObjects = GameObjects;
            isAlive = true;
            FirstSnake = (Snake)GameObjects[0];
            SecondSnake = (Snake)GameObjects[1];
            food = (Food)GameObjects[2];
            wall = (Wall)GameObjects[3];
            this.FirstPlayer = FirstPlayer;
            this.SecondPlayer = SecondPlayer;
            SleepingTime = 150;
        }

        public void Start()
        {
            wall.DrawWall();
            food.GenerateByCoordinates(60, 6);
            food.DrawFood();
            FirstSnake.DrawSnake();
            SecondSnake.DrawSnake();
            Labels();
            PrintScore();
            PrintLevel();

            Thread thread = new Thread(MoveSnakes);
            thread.Start();

            while (isAlive)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey(true);

                if (consoleKey.Key == ConsoleKey.W || consoleKey.Key == ConsoleKey.S || consoleKey.Key == ConsoleKey.A || consoleKey.Key == ConsoleKey.D)
                {
                    if (FirstSnake.CheckDirection(consoleKey))
                        FirstSnake.ChangeDirection(consoleKey);
                }

                else
                {
                    if (SecondSnake.CheckDirection(consoleKey))
                        SecondSnake.ChangeDirection(consoleKey);
                }

                if (consoleKey.Key == ConsoleKey.Escape)
                {
                    thread.Abort();
                    Console.Clear();
                    SaveScore();
                    Serialize();
                }
            }
        }

        public void MoveSnakes()
        {
            while (isAlive)
            {
                FirstSnake.Move();
                SecondSnake.Move();

                if (FirstSnake.CollisionWithObject(food))
                {
                    FirstSnake.body.Add(new Point(1, 29));
                    PrintScore();

                    if ((FirstSnake.body.Count + SecondSnake.body.Count) < 21 && (FirstSnake.body.Count + SecondSnake.body.Count % 10) == 0)
                    {
                        FirstSnake.ToTheLeftCorner();
                        SecondSnake.ToTheRightCorner();
                        wall.NextLevel();
                        wall.DrawWall();
                        PrintLevel();
                        food.Clear();
                        food.GenerateByCoordinates(60, 2);
                        SleepingTime -= 50;
                    }

                    else
                    {
                        while (food.CollisionWithObject(FirstSnake) || food.CollisionWithObject(SecondSnake) || food.CollisionWithObject(wall))
                            food.Generate();
                    }

                    food.DrawFood();
                }

                if (SecondSnake.CollisionWithObject(food))
                {
                    SecondSnake.body.Add(new Point(1, 29));
                    PrintScore();

                    if ((FirstSnake.body.Count + SecondSnake.body.Count) < 21 && (FirstSnake.body.Count + SecondSnake.body.Count) % 10 == 0)
                    {
                        FirstSnake.ToTheLeftCorner();
                        SecondSnake.ToTheRightCorner();
                        wall.NextLevel();
                        wall.DrawWall();
                        PrintLevel();
                        food.Clear();
                        food.GenerateByCoordinates(60, 2);
                        SleepingTime -= 50;
                    }

                    else
                    {
                        while (food.CollisionWithObject(FirstSnake) || food.CollisionWithObject(SecondSnake) || food.CollisionWithObject(wall))
                            food.Generate();
                    }

                    food.DrawFood();
                }

                if (FirstSnake.CollisionWithObject(wall) || SecondSnake.CollisionWithObject(wall))
                    isAlive = false;
                if (FirstSnake.OutOfConsole > 3 || SecondSnake.OutOfConsole > 3)
                    isAlive = false;
                if (FirstSnake.CollisionWithSnake(FirstSnake) || SecondSnake.CollisionWithSnake(SecondSnake))
                    isAlive = false;
                if (FirstSnake.CollisionWithObject(SecondSnake) || SecondSnake.CollisionWithObject(FirstSnake))
                    isAlive = false;

                if (isAlive == false)
                {
                    GameOver();
                    SaveScore();
                    break;
                }

                FirstSnake.ClearSnake();
                FirstSnake.DrawSnake();
                SecondSnake.ClearSnake();
                SecondSnake.DrawSnake();

                Thread.Sleep(SleepingTime);
            }
        }

        public void Labels()
        {
            string[] labels = new string[]
            {
                "SNAKE GAME > > > Author : Almanova Madina",
                "| " + FirstPlayer + " & " + SecondPlayer + " |",
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
            Console.Write((FirstSnake.body.Count * 10) + (SecondSnake.body.Count * 10) - 20);
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
            if (File.Exists(FirstPlayer + SecondPlayer + ".xml"))
                File.Delete(FirstPlayer + SecondPlayer + ".xml");

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(55, 7);
            Console.WriteLine("SNAKE GAME");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(55, 11);
            Console.WriteLine("GAME OVER");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(53, 12);
            Console.WriteLine("YOUR SCORE: " + ((FirstSnake.body.Count * 10) + (SecondSnake.body.Count * 10) - 20));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(44, 20);
            Console.WriteLine("Press any key to go back to Menu");
        }

        public void Serialize()
        {
            string fileName = FirstPlayer + SecondPlayer + ".xml";
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
            Player player = new Player(FirstPlayer +  " & " + SecondPlayer, (FirstSnake.body.Count * 10) + (SecondSnake.body.Count * 10) - 20);

            if (File.Exists("Scores.xml"))
            {
                FileStream fs = new FileStream("Scores.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                XmlSerializer xs = new XmlSerializer(typeof(List<Player>));
                List<Player> players = xs.Deserialize(fs) as List<Player>;
                fs.Close();

                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].nickname == FirstPlayer + " & " + SecondPlayer)
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
