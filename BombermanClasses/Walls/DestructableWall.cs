﻿using System;
using System.Drawing;
using System.IO;

namespace BombermanClasses.Walls
{
    [Serializable]
    public class DestructableWall : Wall
    {

        public override Image GetImage()
        {
            try
            {
                return Image.FromFile(Path.Combine(Directory.GetCurrentDirectory())); 
            }catch(Exception e) { return null; }
        }

        public override int calculateScore(int score)
        {
            return base.calculateScore(score + 50);
        }
    }
}
