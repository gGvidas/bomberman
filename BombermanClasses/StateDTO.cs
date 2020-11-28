using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses
{
    public class StateDTO
    {
        public Tile[][] Objects { get; set; }

        public List<string> DeadPlayersIds { get; set; }
        public List<string> AlivePlayersIds { get; set; }
        public Dictionary<string, int> PlayerScores { get; set; }

    }
}
