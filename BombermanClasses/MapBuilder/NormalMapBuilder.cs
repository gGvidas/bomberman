using BombermanClasses.Items;
using BombermanClasses.Iterator;
using BombermanClasses.Walls;
using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.MapBuilder
{
    public class NormalMapBuilder : AbstractMapBuilder
    {
        private Int32 numSquaresX = 32;
        private Int32 numSquaresY = 32;

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
            ItemArray itemsRepository = new  ItemArray();
            var iter = itemsRepository.GetIterator();

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

                                iter.HasNext();
                                Map.Objects[i][j].item = iter.Next(j, numSquaresY);
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
