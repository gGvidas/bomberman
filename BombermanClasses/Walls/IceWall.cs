﻿using System;

namespace BombermanClasses.Walls
{
    [Serializable]
    public class IceWall : Wall
    {
        public bool DamageTaken { get; set; } = false;

        protected sealed override bool canDestroy()
        {
            if (DamageTaken)
                return true;
            DamageTaken = true;
            return false;
        }

        public override int calculateScore(int score)
        {
            return base.calculateScore(score + 125);
        }
    }
}
