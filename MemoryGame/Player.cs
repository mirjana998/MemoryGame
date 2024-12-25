using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    class Player
    {
        public string Name { get; set; }
        public int Time { get; set; }

        public Player(string name, int time)
        {
            Name = name;
            Time = time;
        }
    }
}
