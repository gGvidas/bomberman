using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bomberman.GameObjects
{
    public class Player
    {
        public string Id { get; set; }
        public int x { get; set; } 
        public int y { get; set; }
        
        public Player(string id, int x, int y)
        {
            Id = id;
            this.x = x;
            this.y = y;
        }
    }
}
