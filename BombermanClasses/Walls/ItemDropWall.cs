using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Walls
{
    [Serializable]
    public class ItemDropWall : Wall
    {
        public override int calculateScore(int score)
        {
            return base.calculateScore(score + 150);
        }
    }
}
