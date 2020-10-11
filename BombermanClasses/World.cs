using Bomberman;
using BombermanClasses.BombNameSpace;
using BombermanClasses.Items;
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
                    if (player.y != 0 && Objects[player.x][player.y - 1].entity == null ||
                        player.y != 0 && Objects[player.x][player.y - 1].entity is Fire && player.shield is FireShield ||
                        player.y != 0 && Objects[player.x][player.y - 1].entity is IceWall && player.shield is IceShield)
                    {
                        Objects[player.x][player.y].entity = null;
                        Objects[player.x][player.y - 1].entity = player;
                        player.y -= 1;
                    }
                    break;
                case "A":
                    if (player.x != 0 && Objects[player.x -1][player.y].entity == null ||
                        player.x != 0 && Objects[player.x -1][player.y].entity is Fire && player.shield is FireShield ||
                        player.x != 0 && Objects[player.x -1][player.y].entity is IceWall && player.shield is IceShield)
                    {
                        Objects[player.x][player.y].entity = null;
                        Objects[player.x - 1][player.y].entity = player;
                        player.x -= 1;
                    }
                    break;
                case "D":
                    if (player.x != numSquaresX - 1 && Objects[player.x + 1][player.y].entity == null ||
                        player.x != numSquaresX - 1 && Objects[player.x + 1][player.y].entity is Fire && player.shield is FireShield ||
                        player.x != numSquaresX - 1 && Objects[player.x + 1][player.y].entity is IceWall && player.shield is IceShield)
                    {
                        Objects[player.x][player.y].entity = null;
                        Objects[player.x + 1][player.y].entity = player;
                        player.x += 1;
                    }
                    //else if (player.x != numSquaresX - 1 && Objects[player.x + 1][player.y].item != null)
                    //{
                    //    player.shield = Objects[player.x + 1][player.y].item;
                    //    Objects[player.x][player.y].item = null;
                    //    Objects[player.x + 1][player.y].entity = player;
                    //    player.x += 1;
                    //}
                    break;
                case "S":
                    if (player.y != numSquaresY - 1 && Objects[player.x][player.y + 1].entity == null ||
                        player.y != numSquaresY - 1 && Objects[player.x][player.y + 1].entity is Fire && player.shield is FireShield ||
                        player.y != numSquaresY - 1 && Objects[player.x][player.y + 1].entity is IceWall && player.shield is IceShield)
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

        public void AddBomb(string id, int type = 0)
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

            type = random.Next(0, 3);
            if (type == 1)
            {
                var factory = new FireFactory();
                FireBomb bomb = (FireBomb)factory.createBomb(player.x, player.y, strategy, instance);
                Objects[player.x][player.y].bomb = bomb;
            }
            else if (type == 2)
            {
                var factory = new IceFactory();
                IceBomb bomb = (IceBomb)factory.createBomb(player.x, player.y, strategy, instance);
                Objects[player.x][player.y].bomb = bomb;
            }
            else
            {
                Bomb bomb = new Bomb(player.x, player.y, strategy, instance);
                Objects[player.x][player.y].bomb = bomb;
            }         
        }

        public async Task Explode(int x, int y)
        {
            if (Objects[x][y].bomb == null) return;
            int radius = Objects[x][y].bomb.explosionRadius(2);
            if (!(Objects[x][y].entity is Player))
                Objects[x][y].entity = null;
            var wallFactory = new WallFactory();
            bool up = false, down = false, left = false, right = false;
            for (int i = 1; i <= radius; i++)
            {
                if (x + i < numSquaresX && !(Objects[x + i][y].entity is IndestructableWall) && !right) 
                {
                    if (Objects[x][y].bomb is FireBomb)
                        Objects[x + i][y].entity = new Fire(x + i, y);
                    else if (Objects[x][y].bomb is IceBomb)
                        if (!(Objects[x][y + i].entity is DestructableWall))
                            Objects[x + i][y].entity = wallFactory.CreateWall(4);
                        else
                            right = true;
                    else 
                    {
                        if (Objects[x + i][y].item != null)
                            Objects[x + i][y].entity = wallFactory.CreateWall(3);
                        else
                            Objects[x + i][y].entity = null;
                    }
                }
                else
                    right = true;
                if (x - i >= 0 && !(Objects[x - i][y].entity is IndestructableWall) && !left)
                {
                    if (Objects[x][y].bomb is FireBomb)
                        Objects[x - i][y].entity = new Fire(x - i, y);
                    else if (Objects[x][y].bomb is IceBomb)
                        if (!(Objects[x][y + i].entity is DestructableWall))
                            Objects[x - i][y].entity = wallFactory.CreateWall(4);
                        else
                            left = true;
                    else
                    {
                        if (Objects[x - i][y].item != null)
                            Objects[x - i][y].entity = wallFactory.CreateWall(3);
                        else
                            Objects[x - i][y].entity = null;
                    }
                }
                else
                    left = true;
                if (y + i < numSquaresY && !(Objects[x][y + i].entity is IndestructableWall) && !up) 
                {
                    if (Objects[x][y].bomb is FireBomb)
                        Objects[x][y + i].entity = new Fire(x, y + i);
                    else if (Objects[x][y].bomb is IceBomb)
                        if(!(Objects[x][y + i].entity is DestructableWall))
                            Objects[x][y + i].entity = wallFactory.CreateWall(4);
                        else
                            up = true;
                    else
                    {
                        if (Objects[x][y + i].item != null)
                            Objects[x][y + i].entity = wallFactory.CreateWall(3);
                        else
                            Objects[x][y + i].entity = null;
                    }
                }
                else
                    up = true;
                if (y - i >= 0 && !(Objects[x][y - i].entity is IndestructableWall) && !down)
                {
                    if (Objects[x][y].bomb is FireBomb)
                        Objects[x][y - i].entity = new Fire(x, y - i);
                    else if (Objects[x][y].bomb is IceBomb)
                        if (!(Objects[x][y + i].entity is DestructableWall))
                            Objects[x][y - i].entity = wallFactory.CreateWall(4);
                        else
                            down = true;
                    else
                    {
                        if (Objects[x][y + i].item != null)
                            Objects[x][y + i].entity = wallFactory.CreateWall(3);
                        else
                            Objects[x][y + i].entity = null;
                    }
                }
                else
                    down = true;
            }
            Objects[x][y].bomb = null;
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
                            {
                                Objects[i][j].entity = wallFactory.CreateWall(1);
                                if (rand >= 9)
                                {
                                    AbstractFactory firefac = new FireFactory();
                                    AbstractFactory icefac = new FireFactory();
                                    int rand2 = r.Next(0, 100);
                                    if (rand2 < 25)
                                        Objects[i][j].item = firefac.createShield();
                                    else if (rand2 < 50)
                                        Objects[i][j].item = icefac.createShield();
                                    //else if (rand2 < 75)
                                    //    Objects[i][j].bomb = firefac.createBomb();
                                    //else if (rand2 < 101)
                                    //    Objects[i][j].bomb = icefac.createBomb();
                                }
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
