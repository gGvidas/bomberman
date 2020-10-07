using Bomberman;
using BombermanClasses.BombNameSpace;
using BombermanClasses.Observer;
using BombermanClasses.Walls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace BombermanClasses
{
    [Serializable]
    public sealed class World : Subject
    {
        private static readonly World instance = new World();
        public static World Instance { get { return instance; } }
        private Int32 squareSize = 20;
        private Int32 numSquaresX = 32;
        private Int32 numSquaresY = 32;
        private List<Player> Players { get; set; }
        private Tile[][] Objects { get; set; }

        public BombermanHub hub { get; set; }

        private int timeUnitInMilisec = 1000;
        private Timer _timer { get; set; }

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
            SetTimer();
        }

        private void SetTimer()
        {
            _timer = new Timer();
            _timer.Interval = timeUnitInMilisec;
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();
        }

        private async void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Notify();
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
                    if (player.y != 0 && Objects[player.x][player.y - 1].entity == null)
                    {
                        Objects[player.x][player.y].entity = null;
                        Objects[player.x][player.y - 1].entity = player;
                        player.y -= 1;
                    }
                    break;
                case "A":
                    if (player.x != 0 && Objects[player.x -1][player.y].entity == null)
                    {
                        Objects[player.x][player.y].entity = null;
                        Objects[player.x - 1][player.y].entity = player;
                        player.x -= 1;
                    }
                    break;
                case "D":
                    if (player.x != numSquaresX - 1 && Objects[player.x + 1][player.y].entity == null)
                    {
                        Objects[player.x][player.y].entity = null;
                        Objects[player.x + 1][player.y].entity = player;
                        player.x += 1;
                    }
                    break;
                case "S":
                    if (player.y != numSquaresY - 1 && Objects[player.x][player.y + 1].entity == null)
                    {
                        Objects[player.x][player.y].entity = null;
                        Objects[player.x][player.y + 1].entity = player;
                        player.y += 1;
                    }
                    break;
                default:
                    break;
            }
        }

        public void AddBomb(string id)
        {
            Player player = GetPlayer(id);
            Random random = new Random();
            int randNumber = random.Next(0, 100);
            IBombRadiusStrategy strategy;
            if (randNumber < 50)
            {
                strategy = new SmallBombRadiusStrategy();
            }
            else if (randNumber < 75)
            {
                strategy = new MediumBombRadiusStrategy();
            }
            else if (randNumber < 93)
            {
                strategy = new LargeBombRadiusStrategy();
            }
            else
            {
                strategy = new NuclearBombRadiusStrategy();
            }
            Bomb bomb = new Bomb(player.x, player.y, strategy, instance);
            Objects[player.x][player.y].bomb = bomb;

         
        }

        public async Task Explode(int x, int y)
        {
            if (Objects[x][y].bomb == null) return;
            int radius = Objects[x][y].bomb.explosionRadius(2);
            Objects[x][y].bomb = null;
            if (!(Objects[x][y].entity is Player))
                Objects[x][y].entity = null;
            for (int i = 1; i <= radius; i++)
            {
                if (x + i < numSquaresX && !(Objects[x+i][y].entity is IndestructableWall) && !(Objects[x + i][y].entity is Player))
                    Objects[x+i][y].entity = null;
                if (x - i >= 0 && !(Objects[x - i][y].entity is IndestructableWall) && !(Objects[x - i][y].entity is Player))
                    Objects[x-i][y].entity = null;
                if (y + i < numSquaresY && !(Objects[x][y + i].entity is IndestructableWall) && !(Objects[x][y + i].entity is Player))
                    Objects[x][y+i].entity = null;
                if (y - i >= 0 && !(Objects[x][y - i].entity is IndestructableWall) && !(Objects[x][y - i].entity is Player))
                    Objects[x][y-i].entity = null;
            }
        }

        public void AddPlayer(string id)
        {
            Random random = new Random();
            int x = 0;
            int y = 0;
            while (Objects[x][y].entity is Wall)
            {
                x = random.Next(0, numSquaresX);
                y = random.Next(0, numSquaresY);
            }
            Player player = new Player(id, x, y);
            Objects[x][y].entity = player;
            Players.Add(player);
        }
        public void RemovePlayer(string id)
        {
            Player player = GetPlayer(id);
            Objects[player.x][player.y].entity = null;
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
                        Objects[i][j].entity = wallFactory.CreateWall(2);
                    else
                    {
                        if (i % 2 == 0 && j % 2 == 0)
                            Objects[i][j].entity = wallFactory.CreateWall(2);

                        else
                        {
                            if (((i == 1 && (j == 1 || j == 2)) || (i == 2 && j == 1)
                                || (i == (Objects.GetLength(0) - 1) - 2 && j == (Objects.GetLength(0) - 1) - 1) || (i == (Objects.GetLength(0) - 1) - 1 && (j == (Objects.GetLength(0) - 1) - 1 || j == (Objects.GetLength(0) - 1) - 2))))
                            {
                                //empty path
                                continue;
                            }
                            else if (rand >= 6)
                                Objects[i][j].entity = wallFactory.CreateWall(1);
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
