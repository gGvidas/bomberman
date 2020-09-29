using BombermanClasses.Walls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BombermanClasses
{
    [Serializable]
    public sealed class World
    {
        private static readonly World instance = new World();
        public static World Instance { get { return instance; } }
        private Int32 squareSize = 20;
        private Int32 numSquaresX = 32;
        private Int32 numSquaresY = 32;
        private List<Player> Players { get; set; }
        private Tile[][] Objects { get; set; }

        static World()
        {

        }
        private World()
        {
            Objects = new Tile[numSquaresY][];
            for (int i = 0; i < numSquaresY; i++)
            {
                Objects[i] = new Tile[numSquaresX];
                for (int j = 0; j < numSquaresX; j++)
                {
                    Objects[i][j] = new Tile();
                }
            }
            Players = new List<Player>();
            GenerateWorld();
        }

        public Tile[][] GetObjects()
        {
            return Objects;
        }

        public void MovePlayer(string id, string keypress)
        {
            Player player = GetPlayer(id);

            switch (keypress)
            {
                case "W":
                    if (player.y != 0 && !(Objects[player.x][player.y -1].mapObject is Wall) && Objects[player.x][player.y - 1].player == null)
                    {
                        Objects[player.x][player.y].player = null;
                        Objects[player.x][player.y - 1].player = player;
                        player.y -= 1;
                    }
                    break;
                case "A":
                    if (player.x != 0 && !(Objects[player.x -1][player.y].mapObject is Wall) && Objects[player.x -1][player.y].player == null)
                    {
                        Objects[player.x][player.y].player = null;
                        Objects[player.x - 1][player.y].player = player;
                        player.x -= 1;
                    }
                    break;
                case "D":
                    if (player.x != numSquaresX - 1 && !(Objects[player.x + 1][player.y].mapObject is Wall) && Objects[player.x + 1][player.y].player == null)
                    {
                        Objects[player.x][player.y].player = null;
                        Objects[player.x + 1][player.y].player = player;
                        player.x += 1;
                    }
                    break;
                case "S":
                    if (player.y != numSquaresY - 1 && !(Objects[player.x][player.y + 1].mapObject is Wall) && Objects[player.x][player.y + 1].player == null)
                    {
                        Objects[player.x][player.y].player = null;
                        Objects[player.x][player.y + 1].player = player;
                        player.y += 1;
                    }
                    break;
                default:
                    break;
            }
        }

        public void AddPlayer(string id)
        {
            Random random = new Random();
            int x = 0;
            int y = 0;
            while (Objects[x][y].mapObject is Wall)
            {
                x = random.Next(0, numSquaresX);
                y = random.Next(0, numSquaresY);
            }
            Player player = new Player(id, x, y);
            Objects[x][y].mapObject = player;
            Players.Add(player);
        }
        public void RemovePlayer(string id)
        {
            Player player = GetPlayer(id);
            Objects[player.x][player.y].player = null;
            Players.Remove(player);
        }
        private Player GetPlayer(string id)
        {
            return Players.FirstOrDefault(player => player.Id == id);
        }

        private void GenerateWorld()
        {
            Random r = new Random();
            int rand = 0;

            var wallFactory = new WallFactory();

            for (int i = 0; i < Objects.GetLength(0); i++)
            {
                for (int j = 0; j < Objects.GetLength(0); j++)
                {
                    rand = r.Next(0, 10);

                    if (j == 0 || j == (Objects.GetLength(0) - 1) || i == 0 || i == (Objects.GetLength(0) - 1))
                    {
                        Objects[i][j].mapObject = wallFactory.CreateWall(2);
                    }
                    else
                    {
                        if (i % 2 == 0 && j % 2 == 0)
                        {
                            Objects[i][j].mapObject = wallFactory.CreateWall(2);
                        }

                        else
                        {
                            if (((i == 1 && (j == 1 || j == 2)) || (i == 2 && j == 1)
                                || (i == (Objects.GetLength(0) - 1) - 2 && j == (Objects.GetLength(0) - 1) - 1) || (i == (Objects.GetLength(0) - 1) - 1 && (j == (Objects.GetLength(0) - 1) - 1 || j == (Objects.GetLength(0) - 1) - 2)))) // les cases adjacentes au point de spawn du joueurs sont exemptes de blocks destructibles
                            {
                                //empty path
                                continue;
                            }
                            else if (rand >= 6)
                            {
                                Objects[i][j].mapObject = wallFactory.CreateWall(1);

                            }
                            else
                            {
                            }

                        }
                    }
                }
            }
        }
    }
}
