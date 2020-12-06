using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace BombermanClasses.Walls
{
    [Serializable]
    public class ItemDropWall : Wall
    {
        public override Image GetImage()
        {
            try
            {
                return Image.FromFile(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\.."), @"Sprites\BlockItem.png"));
            }
            catch (Exception e) { return null; }
        }
        public override int calculateScore(int score)
        {
            return base.calculateScore(score + 150);
        }
    }
}
