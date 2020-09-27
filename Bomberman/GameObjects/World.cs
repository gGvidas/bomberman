using System;
using System.Collections.Generic;
using System.Linq;

namespace Bomberman.GameObjects
{
    public class World : IWorld
    {
        Int32 squareSize = 20;
        Int32 numSquaresX = 32;
        Int32 numSquaresY = 32;
        private List<Player> Players { get; set; }
        public int[][] Objects { get; set; }

        public World()
        {
            Objects = new int[numSquaresY][];
            for (int i = 0; i < numSquaresY; i++)
            {
                Objects[i] = new int[numSquaresX];
            }
            Players = new List<Player>();
            GenerateWorld();
        }

        public int[][] GetObjects()
        {
            return Objects;
        }

        public void MovePlayer(string id, string keypress)
        {
            Player player = GetPlayer(id);
            int up = Objects[player.x][player.y - 1];
            int down = Objects[player.x][player.y + 1];
            int left = Objects[player.x-1][player.y];
            int right = Objects[player.x+1][player.y];
            switch (keypress)
            {
                case "W":
                    if (up != 3 && up != 4)
                    {
                        Objects[player.x][player.y] = 2;
                        Objects[player.x][player.y - 1] = 1;
                        player.y -= 1;
                    }
                    break;
                case "A":
                    if (left != 3 && left != 4)
                    {
                        Objects[player.x][player.y] = 2;
                        Objects[player.x - 1][player.y] = 1;
                        player.x -= 1;
                    }
                    break;
                case "S":
                    if (down != 3 && down != 4)
                    {
                        Objects[player.x][player.y] = 2;
                        Objects[player.x][player.y + 1] = 1;
                        player.y += 1;
                    }
                    break;
                case "D":
                    if (right != 3 && right != 4)
                    {
                        Objects[player.x][player.y] = 2;
                        Objects[player.x + 1][player.y] = 1;
                        player.x += 1;
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
            while (Objects[x][y] != 2)
            {
                x = random.Next(0, Objects.GetLength(0));
                y = random.Next(0, Objects.GetLength(0));
            }
            Objects[x][y] = 1;
            Players.Add(new Player(id, x, y));
        }
        public void RemovePlayer(string id)
        {
            Player player = GetPlayer(id);
            Objects[player.x][player.y] = 2;
            Players.Remove(player);
        }
        private Player GetPlayer(string id)
        {
            return Players.FirstOrDefault(player => player.Id == id);
        }

        // 1 = player
        // 2 = nothing
        // 3 = destructable wall
        // 4 = indestructable wall
        private void GenerateWorld()
        {
            Random r = new Random();
            int rand = 0;
            for (int i = 0; i < Objects.GetLength(0); i++)
            {
                for (int j = 0; j < Objects.GetLength(0); j++)
                {
                    rand = r.Next(0, 10);

                    if (j == 0 || j == (Objects.GetLength(0) - 1) || i == 0 || i == (Objects.GetLength(0) - 1))
                        Objects[i][j] = 4;
                    else
                    {
                        if (i % 2 == 0 && j % 2 == 0)
                            Objects[i][j] = 4;
                        else
                        {
                            if (((i == 1 && (j == 1 || j == 2)) || (i == 2 && j == 1)
                                || (i == (Objects.GetLength(0) - 1) - 2 && j == (Objects.GetLength(0) - 1) - 1) || (i == (Objects.GetLength(0) - 1) - 1 && (j == (Objects.GetLength(0) - 1) - 1 || j == (Objects.GetLength(0) - 1) - 2)))) // les cases adjacentes au point de spawn du joueurs sont exemptes de blocks destructibles
                                Objects[i][j] = 2;
                            else if (rand >= 6)
                                Objects[i][j] = 3;
                            else
                            {
                                //TEMP
                                //Objects[i][j] = 1;
                                Objects[i][j] = 2;
                            }

                        }
                    }
                }
            }
        }
    }
    public interface IWorld
    {
        public void MovePlayer(string id, string keyPress);
        public void AddPlayer(string id);
        public void RemovePlayer(string id);
        public int[][] GetObjects();
    }
}
