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
        public string nickname;
        public Snake snake;
        public Food food;
        public Wall wall;

        public Game() { }

        public Game(string nickname)
        {
            GameObjects = new List<GameObject>();
            snake = new Snake(3, 2, 'o', ConsoleColor.Magenta, 0);
            food = new Food(0, 0, '*', ConsoleColor.Cyan);
            wall = new Wall('#', ConsoleColor.DarkYellow);
            snake2 = new Snake(117, 2, 'o', ConsoleColor.Blue, 0);
            GameObjects.Add(snake);
            GameObjects.Add(food);
            GameObjects.Add(wall);
            GameObjects.Add(snake2);
            this.nickname = nickname;
            isAlive = true;
        }

        public Game(List<GameObject> GameObjects, string nickname) 
        {
            this.GameObjects = GameObjects;
            isAlive = true;
            this.snake = (Snake)GameObjects[0];
            this.food = (Food)GameObjects[1];
            this.wall = (Wall)GameObjects[2];
            this.nickname = nickname;
        }

        public void Start()
        {
            food.GenerateByCoordinates(60, 6);
            wall.LoadLevel();
            Draw();
            ConsoleKeyInfo consoleKey = Console.ReadKey();
            while (isAlive && consoleKey.Key != ConsoleKey.Escape) 
            {
                Draw();
                string author = "SNAKE GAME > > > Author : Almanova Madina";
                Console.SetCursorPosition((Console.WindowWidth - author.Length) / 2, 23);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(author);
                Console.SetCursorPosition(3, 25);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (snake.body.Count == 1)
                    Console.WriteLine("YOUR SCORE: 0");
                else Console.WriteLine("YOUR SCORE: " + ((snake.body.Count * 10) - 10));
                Console.SetCursorPosition(108, 25);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                if (wall.gameLevel == Wall.GameLevel.First)
                    Console.WriteLine("LEVEL: 1");
                else if (wall.gameLevel == Wall.GameLevel.Second)
                    Console.WriteLine("LEVEL: 2");
                else if (wall.gameLevel == Wall.GameLevel.Third)
                    Console.WriteLine("LEVEL: 3");
                string name = "| " + nickname + " |";
                Console.SetCursorPosition((Console.WindowWidth - name.Length) / 2, 25);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(name);
                string press = "Press ESC to go back to Menu";
                Console.SetCursorPosition((Console.WindowWidth - press.Length) / 2, 27);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(press);
                consoleKey = Console.ReadKey();
                if (snake.CollisionWithObject(food))
                {
                    snake.body.Add(new Point(0, 0));

                    if (snake.body.Count < 11 && snake.body.Count % 5 == 0)
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
                if (snake.OutOfConsole > 3)
                    isAlive = false;
                if (snake.CollisionWithSnake(snake))
                    isAlive = false;
                if (consoleKey.Key == ConsoleKey.Escape) 
                {
                    string fileName = nickname + ".xml";
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                    FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
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
