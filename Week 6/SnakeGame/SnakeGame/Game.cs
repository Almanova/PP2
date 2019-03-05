using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace SnakeGame
{
    public class Game
    {
        public List<GameObject> GameObjects;
        public bool isAlive;
        public Snake snake;
        public Food food;
        public Wall wall;

        public Game()
        {
            GameObjects = new List<GameObject>();
            snake = new Snake(10, 5, 'o', ConsoleColor.Magenta, 0);
            food = new Food(0, 0, '*', ConsoleColor.Cyan);
            wall = new Wall('#', ConsoleColor.DarkYellow);
            GameObjects.Add(snake);
            GameObjects.Add(food);
            GameObjects.Add(wall);
            isAlive = true;
        }

        public Game(List<GameObject> GameObjects) 
        {
            this.GameObjects = GameObjects;
            isAlive = true;
            this.snake = (Snake)GameObjects[0];
            this.food = (Food)GameObjects[1];
            this.wall = (Wall)GameObjects[2];
        }

        public void Start()
        {
            food.GenerateByCoordinates(60, 1);
            wall.LoadLevel();
            Draw();
            ConsoleKeyInfo consoleKey = Console.ReadKey();
            while (isAlive && consoleKey.Key != ConsoleKey.Escape) 
            {
                Draw();
                Console.SetCursorPosition(38, 23);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("SNAKE GAME > > > Author : Almanova Madina");
                Console.SetCursorPosition(3, 25);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (snake.body.Count == 1)
                    Console.WriteLine("YOUR SCORE: 0");
                else Console.WriteLine("YOUR SCORE: " + ((snake.body.Count * 10) - 10));
                Console.SetCursorPosition(44, 27);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Press ESC to go back to Menu");
                consoleKey = Console.ReadKey();
                if (snake.CollisionWithObject(food))
                {
                    snake.body.Add(new Point(0, 0));

                    if (snake.body.Count < 16 && snake.body.Count % 5 == 0)
                    {
                        snake.ToTheCorner();
                        wall.NextLevel();
                        food.Generate();
                    }

                    while (food.CollisionWithObject(snake) || food.CollisionWithObject(wall))
                        food.Generate();
                }
                if (snake.CollisionWithObject(wall))
                    isAlive = false;
                if (snake.OutOfConsole > 2)
                    isAlive = false;
                if (snake.CollisionWithSnake(snake))
                    isAlive = false;
                if (consoleKey.Key == ConsoleKey.Escape) 
                {
                    if (File.Exists("savethegame.xml"))
                        File.Delete("savethegame.xml");
                    FileStream fs = new FileStream("savethegame.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    XmlSerializer xs = new XmlSerializer(typeof(List<GameObject>));
                    xs.Serialize(fs, GameObjects);
                    fs.Close();
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.Show();
                    mainMenu.StartMenu();
                    break;
                }
                if (snake.SnakeDirection(consoleKey))
                    snake.Move(consoleKey);
            }
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
            Console.SetCursorPosition (44, 20);
            Console.WriteLine("Press any key to go back to Menu");
            Console.ReadKey();
        }

        public void Draw()
        {
            Console.Clear();
            foreach (GameObject g in GameObjects)
            {
                if (g.GetType() == typeof(Snake))
                    g.DrawSnake();
                else g.DrawFoodOrWall();
            }
        }
    }
}
