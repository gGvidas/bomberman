using BombermanClasses.Walls;
using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.MapBuilder
{
    class SmallMapBuilder : AbstractMapBuilder
    {
        private Int32 numSquaresX = 16;
        private Int32 numSquaresY = 16;

        public override void BuildDestructableWalls()
        {
            Random r = new Random();
            int rand = 0;

            var wallFactory = new WallFactory();

            for (int i = 0; i < Map.Objects.GetLength(0); i++)
            {
                for (int j = 0; j < Map.Objects.GetLength(0); j++)
                {
                    rand = r.Next(0, 10);

                    if (j == 0 || j == (Map.Objects.GetLength(0) - 1) || i == 0 || i == (Map.Objects.GetLength(0) - 1))
                        continue;
                    else
                    {
                        if (i % 2 == 0 && j % 2 == 0)
                            continue;

                        else
                        {
                            if (((i == 1 && (j == 1 || j == 2)) || (i == 2 && j == 1)
                                || (i == (Map.Objects.GetLength(0) - 1) - 2 && j == (Map.Objects.GetLength(0) - 1) - 1) || (i == (Map.Objects.GetLength(0) - 1) - 1 && (j == (Map.Objects.GetLength(0) - 1) - 1 || j == (Map.Objects.GetLength(0) - 1) - 2))))
                            {
                                //empty path
                                continue;
                            }

                            else if (rand >= 6)
                                Map.Objects[i][j].entity = wallFactory.CreateWall(1);

                        }
                    }
                }
            }
        }

        public override void BuildIndestructableWalls()
        {
            Random r = new Random();

            var wallFactory = new WallFactory();

            for (int i = 0; i < Map.Objects.GetLength(0); i++)
            {
                for (int j = 0; j < Map.Objects.GetLength(0); j++)
                {

                    if (j == 0 || j == (Map.Objects.GetLength(0) - 1) || i == 0 || i == (Map.Objects.GetLength(0) - 1))
                        Map.Objects[i][j].entity = wallFactory.CreateWall(2);
                    else
                    {
                        if (i % 2 == 0 && j % 2 == 0)
                            Map.Objects[i][j].entity = wallFactory.CreateWall(2);
                    }
                }
            }
        }

        public override void BuildItemDropWalls()
        {
            Random r = new Random();
            int rand = 0;

            var wallFactory = new WallFactory();

            for (int i = 0; i < Map.Objects.GetLength(0); i++)
            {
                for (int j = 0; j < Map.Objects.GetLength(0); j++)
                {
                    rand = r.Next(0, 10);

                    if (j == 0 || j == (Map.Objects.GetLength(0) - 1) || i == 0 || i == (Map.Objects.GetLength(0) - 1))
                        continue;
                    else
                    {
                        if (i % 2 == 0 && j % 2 == 0)
                            continue;

                        else
                        {
                            if (((i == 1 && (j == 1 || j == 2)) || (i == 2 && j == 1)
                                || (i == (Map.Objects.GetLength(0) - 1) - 2 && j == (Map.Objects.GetLength(0) - 1) - 1) || (i == (Map.Objects.GetLength(0) - 1) - 1 && (j == (Map.Objects.GetLength(0) - 1) - 1 || j == (Map.Objects.GetLength(0) - 1) - 2))))
                            {
                                //empty path
                                continue;
                            }
                            else if (rand >= 9)
                            {
                                Map.Objects[i][j].entity = wallFactory.CreateWall(3);
                                ItemsMaker maker = new ItemsMaker();
                                int rand2 = r.Next(0, 100);
                                if (rand2 < 25)
                                    Map.Objects[i][j].item = maker.GetFireShield();
                                else if (rand2 < 50)
                                    Map.Objects[i][j].item = maker.GetIceShield();
                                else if (rand2 < 75)
                                    Map.Objects[i][j].item = maker.GetFireBomb();
                                else if (rand2 < 101)
                                    Map.Objects[i][j].item = maker.GetIceBomb();
                            }

                        }
                    }
                }
            }
        }

        public override void InitEmpty()
        {
            base.Map.Objects = new Tile[numSquaresY][];
            for (int i = 0; i < numSquaresY; i++)
            {
                base.Map.Objects[i] = new Tile[numSquaresX];
                for (int j = 0; j < numSquaresX; j++)
                {
                    base.Map.Objects[i][j] = new Tile();
                }
            }
        }
    }
}
