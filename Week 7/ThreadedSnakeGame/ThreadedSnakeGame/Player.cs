using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedSnakeGame
{
    public class Player
    {
        public string nickname;
        public int score;

        public Player() { }

        public Player(string nickname, int score)
        {
            this.nickname = nickname;
            this.score = score;
        }

        public override string ToString()
        {
            return nickname + " >> " + score;
        }
    }
}
