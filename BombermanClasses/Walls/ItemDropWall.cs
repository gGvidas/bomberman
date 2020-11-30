using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Walls
{
    [Serializable]
    public class ItemDropWall : Wall
    {
        public override int getScore()
        {
            return 150;
        }
    }
}
