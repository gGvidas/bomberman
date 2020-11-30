using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Walls
{
    public class WallFlyWeightFactory
    {
        public Dictionary<int, IWallFlyweight> Walls;


        public WallFlyWeightFactory()
        {
            Walls = new Dictionary<int, IWallFlyweight>();
        }

        public IWallFlyweight GetFlyWeight(int key)
        {
            if(Walls.ContainsKey(key))
            {
                return Walls[key];
            }

            Wall wall = null;

            switch (key)
            {
                case 1:
                    wall = new DestructableWall();
                    break;
                case 2:
                    wall = new IndestructableWall();
                    break;
                case 3:
                    wall = new ItemDropWall();
                    break;
                case 4:
                    wall = new IceWall();
                    break;
                default:
                    throw new NotImplementedException();
            }

            Walls[key] = wall;

            return wall;
        }
    }
}
