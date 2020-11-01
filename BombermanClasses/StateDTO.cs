using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses
{
    public class StateDTO
    {
        public Tile[][] Objects { get; set; }

        public List<string> DeadPlayersIds { get; set; }
    }
}
