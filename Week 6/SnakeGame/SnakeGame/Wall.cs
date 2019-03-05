using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SnakeGame
{
    public class Wall:GameObject
    {
        public Wall() { }
        public Wall(char sign, ConsoleColor color) : base(0, 0, sign, color)
        {
            body = new List<Point>();
        }

        public enum GameLevel
        {
            First,
            Second,
            Third
        }

        public GameLevel gameLevel = GameLevel.First;

        public void LoadLevel()
        {
            body = new List<Point>();
            string fileName = "level1.txt";
            if (gameLevel == GameLevel.Second)
                fileName = "level2.txt";
            if (gameLevel == GameLevel.Third)
                fileName = "level3.txt";

            StreamReader sr = new StreamReader(fileName);
            string[] walls = sr.ReadToEnd().Split('\n');
            for (int i = 0; i < walls.Length; i++)
                for (int j = 0; j < walls[i].Length; j++)
                    if (walls[i][j] == '#')
                        body.Add(new Point(j, i));
        }

        public void NextLevel()
        {
            if (gameLevel == GameLevel.First)
                gameLevel = GameLevel.Second;
            else if (gameLevel == GameLevel.Second)
                gameLevel = GameLevel.Third;
            LoadLevel();
        }
    }
}
