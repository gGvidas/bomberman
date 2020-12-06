using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace BombermanClasses.Walls
{
    [Serializable]
    public class IndestructableWall : Wall
    {
        public override Image GetImage()
        {
            try
            {
                return Image.FromFile(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\.."), @"Sprites\BlockNonDestructible.png"));
            }
            catch (Exception e) { return null; }
        }
    }
}
