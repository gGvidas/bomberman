using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Walls
{
    public class WallFactory : Factory
    {
        private WallFlyWeightFactory wallFlyWeightFactory;
        public WallFactory()
        {
            wallFlyWeightFactory = new WallFlyWeightFactory();
        }
        public override Wall CreateWall(int type)
        {
            switch (type)
            {
                case 1:
                    return (DestructableWall)wallFlyWeightFactory.GetFlyWeight(type);
                case 2:
                    return (IndestructableWall)wallFlyWeightFactory.GetFlyWeight(type);
                case 3:
                    return (ItemDropWall)wallFlyWeightFactory.GetFlyWeight(type);
                case 4:
                    return (IceWall)wallFlyWeightFactory.GetFlyWeight(type);
                default:
                    throw new NotImplementedException();
            }
        }

        //public override Wall CreateWall(int type)
        //{
        //    switch (type)
        //    {
        //        case 1:
        //            return (DestructableWall)wallFlyWeightFactory.GetFlyWeight(type);
        //        case 2:
        //            return (IndestructableWall)wallFlyWeightFactory.GetFlyWeight(type);
        //        case 3:
        //            return (ItemDropWall)wallFlyWeightFactory.GetFlyWeight(type);
        //        case 4:
        //            return (IceWall)wallFlyWeightFactory.GetFlyWeight(type);
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}
    }
}
