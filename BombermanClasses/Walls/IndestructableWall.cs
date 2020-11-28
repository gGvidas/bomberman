using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Walls
{
    [Serializable]
    public class IndestructableWall : Wall
    {
        public override int getScore()
        {
            return 0;
        }
    }
}
