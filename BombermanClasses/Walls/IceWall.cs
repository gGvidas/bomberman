using System;
using System.Drawing;
using System.IO;

namespace BombermanClasses.Walls
{
    [Serializable]
    public class IceWall : Wall
    {
        public bool DamageTaken { get; set; } = false;


        public override Image GetImage()
        {
            try
            {
                return Image.FromFile(Path.Combine(Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), @"Sprites\BlockIce.png"))));
            }
            catch (Exception e) { return null; }
        }

        protected sealed override bool canDestroy()
        {
            if (DamageTaken)
                return true;
            DamageTaken = true;
            return false;
        }

        public override int getScore()
        {
            return 125;
        }
    }
}
