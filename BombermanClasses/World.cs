using Bomberman;
using BombermanClasses.BombNameSpace;
using BombermanClasses.Command;
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

        private MovementInvoker movementInvoker;

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
            movementInvoker = new MovementInvoker();
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
            if (player.isDead) return;


            int x = player.x, y = player.y;
            Objects[x][y].onfiretype = null;
            Objects[x][y].firetype = null;
            Objects[x][y].icetype = null;
            switch (keypress)
            {
                case "W":
                    if (y != 0 && (Objects[x][y - 1].entity == null ||
                        (Objects[x][y - 1].entity is Fire && player.item is FireShield) ||
                        (Objects[x][y - 1].entity is IceWall && player.item is IceShield)))
                    {
                        IPlayer play = new Player(id, x, y);
                        if (Objects[x][y - 1].item != null)
                        {
                            player.item = Objects[x][y - 1].item;
                            Objects[x][y - 1].item = null;
                        }

                        Objects[x][y].entity = null;

                        if (Objects[x][y - 1].entity is Fire && player.item is FireShield)
                            Objects[x][y - 1].onfiretype = new OnFirePlayer(play);
                        else if (player.item is FireShield)
                            Objects[x][y - 1].firetype = new FirePlayer(play);
                        else if (player.item is IceShield)
                            Objects[x][y - 1].icetype = new IcePlayer(play);
                        Objects[x][y - 1].entity = player;

                        movementInvoker.setCommand(new MoveUpCommand(player));
                        movementInvoker.move();
                    }
                    break;
                case "A":
                    if (x != 0 && (Objects[x -1][y].entity == null ||
                        (Objects[x -1][y].entity is Fire && player.item is FireShield) ||
                        (Objects[x -1][y].entity is IceWall && player.item is IceShield)))
                    {
                        IPlayer play = new Player(id, x, y);
                        if (Objects[x - 1][y].item != null)
                        {
                            player.item = Objects[x - 1][y].item;
                            Objects[x - 1][y].item = null;
                        }

                        Objects[x][y].entity = null;


                        if (Objects[x - 1][y].entity is Fire && player.item is FireShield)
                            Objects[x - 1][y].onfiretype = new OnFirePlayer(play);
                        else if (player.item is FireShield)
                            Objects[x - 1][y].firetype = new FirePlayer(play);
                        else if (player.item is IceShield)
                            Objects[x - 1][y].icetype = new IcePlayer(play);
                        Objects[x - 1][y].entity = player;

                        movementInvoker.setCommand(new MoveLeftCommand(player));
                        movementInvoker.move();
                    }
                    break;
                case "D":
                    if (x != numSquaresX - 1 && (Objects[x + 1][y].entity == null ||
                        (Objects[x + 1][y].entity is Fire && player.item is FireShield) ||
                        (Objects[x + 1][y].entity is IceWall && player.item is IceShield)))
                    {
                        IPlayer play = new Player(id, x, y);
                        if (Objects[x + 1][y].item != null)
                        {
                            player.item = Objects[x + 1][y].item;
                            Objects[x + 1][y].item = null;
                        }

                        Objects[x][y].entity = null;

                        if (Objects[x + 1][y].entity is Fire && player.item is FireShield)
                            Objects[x + 1][y].onfiretype = new OnFirePlayer(play);
                        else if (player.item is FireShield)
                            Objects[x + 1][y].firetype = new FirePlayer(play);
                        else if (player.item is IceShield)
                            Objects[x + 1][y].icetype = new IcePlayer(play);
                        Objects[x + 1][y].entity = player;

                        movementInvoker.setCommand(new MoveRightCommand(player));
                        movementInvoker.move();
                    }
                    break;
                case "S":
                    if (y != numSquaresY - 1 && (Objects[x][y + 1].entity == null ||
                        (Objects[x][y + 1].entity is Fire && player.item is FireShield) ||
                        (Objects[x][y + 1].entity is IceWall && player.item is IceShield)))
                    {
                        IPlayer play = new Player(id, x, y);
                        if (Objects[x][y + 1].item != null)
                        {
                            player.item = Objects[x][y + 1].item;
                            Objects[x][y + 1].item = null;
                        }

                        Objects[x][y].entity = null;

                        if (Objects[x][y + 1].entity is Fire && player.item is FireShield)
                            Objects[x][y + 1].onfiretype = new OnFirePlayer(play);
                        else if (player.item is FireShield)
                            Objects[x][y + 1].firetype = new FirePlayer(play);
                        else if (player.item is IceShield)
                            Objects[x][y + 1].icetype = new IcePlayer(play);
                        Objects[x][y + 1].entity = player;

                        movementInvoker.setCommand(new MoveDownCommand(player));
                        movementInvoker.move();
                    }
                    break;
                case "F":
                    Objects[x][y].entity = null;
                    movementInvoker.undo(player.Id);
                    Objects[player.x][player.y].entity = player;
                    IPlayer play1 = new Player(id, x, y);
                    if (Objects[player.x][player.y].entity is Fire && player.item is FireShield)
                        Objects[player.x][player.y].onfiretype = new OnFirePlayer(play1);
                    else if (player.item is FireShield)
                        Objects[player.x][player.y].firetype = new FirePlayer(play1);
                    else if (player.item is IceShield)
                        Objects[player.x][player.y].icetype = new IcePlayer(play1);
                    break;
                default:
                    break;
            }
        }

        public void AddBomb(string id)
        {
            Player player = GetPlayer(id);
            if (player.isDead) return;

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
            ItemsMaker maker = new ItemsMaker();
            if (player.item is FireBomb)
            {
                Objects[player.x][player.y].bomb = maker.GetFireBomb(player.x, player.y, strategy, instance);
                player.item = null;
            }
            else if (player.item is IceBomb)
            {
                Objects[player.x][player.y].bomb = maker.GetIceBomb(player.x, player.y, strategy, instance);
                player.item = null;
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

            Objects[x][y].destroy();

            var wallFactory = new WallFactory();

            bool up = false, down = false, left = false, right = false;

            for (int i = 1; i <= radius; i++)
            {
                if (x + i < numSquaresX && !(Objects[x + i][y].entity is IndestructableWall) && !right) 
                {
                    if (Objects[x][y].bomb is FireBomb)
                        Objects[x + i][y].entity = new Fire(x + i, y);
                    else if (Objects[x][y].bomb is IceBomb)
                        if (!(Objects[x + 1][y].entity is DestructableWall))
                            Objects[x + i][y].entity = wallFactory.CreateWall(4);
                        else
                            right = true;
                    else 
                        Objects[x + i][y].destroy();
                }
                else
                    right = true;
                if (x - i >= 0 && !(Objects[x - i][y].entity is IndestructableWall) && !left)
                {
                    if (Objects[x][y].bomb is FireBomb)
                        Objects[x - i][y].entity = new Fire(x - i, y);
                    else if (Objects[x][y].bomb is IceBomb)
                        if (!(Objects[x - 1][y].entity is DestructableWall))
                            Objects[x - i][y].entity = wallFactory.CreateWall(4);
                        else
                            left = true;
                    else
                        Objects[x - i][y].destroy();
                }
                else
                    left = true;
                if (y + i < numSquaresX && !(Objects[x][y + i].entity is IndestructableWall) && !up)
                {
                    if (Objects[x][y].bomb is FireBomb)
                        Objects[x][y + i].entity = new Fire(x, y - i);
                    else if (Objects[x][y].bomb is IceBomb)
                        if (!(Objects[x][y + i].entity is DestructableWall))
                            Objects[x][y + i].entity = wallFactory.CreateWall(4);
                        else
                            down = true;
                    else
                        Objects[x][y + i].destroy();
                }
                else
                    down = true;
                if (y - i >= 0 && !(Objects[x][y - i].entity is IndestructableWall) && !down)
                {
                    if (Objects[x][y].bomb is FireBomb)
                        Objects[x][y - i].entity = new Fire(x, y - i);
                    else if (Objects[x][y].bomb is IceBomb)
                        if (!(Objects[x][y - i].entity is DestructableWall))
                            Objects[x][y - i].entity = wallFactory.CreateWall(4);
                        else
                            up = true;
                    else
                        Objects[x][y - i].destroy();
                }
                else
                    up = true;
            }
            Objects[x][y].bomb = null;
            CheckIfEndgame();
        }

        public void AddPlayer(string id)
        {
            Random random = new Random();
            int x = 0;
            int y = 0;
            while (!(Objects[x][y].entity == null))
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
        private void CheckIfEndgame()
        {
            int aliveCount = Players.Where(player => !player.isDead).Count();
            if ((Players.Count == 1 && aliveCount == 0) || (Players.Count > 1 && aliveCount == 1))
            {
                RestartGame();
            }
        }
        private void RestartGame()
        {
            foreach (Tile[] row in Objects)
            {
                foreach (Tile tile in row)
                {
                    tile.clear();
                }
            }
            GenerateWorld();
            foreach (Player player in Players)
            {
                Random random = new Random();
                int x = 0;
                int y = 0;
                while (!(Objects[x][y].entity == null))
                {
                    x = random.Next(0, numSquaresX);
                    y = random.Next(0, numSquaresY);
                }
                player.isDead = false;
                player.x = x;
                player.y = y;
                Objects[x][y].entity = player;
            }
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
                            else if (rand >= 9)
                            {
                                Objects[i][j].entity = wallFactory.CreateWall(3);
                                ItemsMaker maker = new ItemsMaker();
                                int rand2 = r.Next(0, 100);
                                if (rand2 < 25)
                                    Objects[i][j].item = maker.GetFireShield();
                                else if (rand2 < 50)
                                    Objects[i][j].item = maker.GetIceShield();
                                else if (rand2 < 75)
                                    Objects[i][j].item = maker.GetFireBomb();
                                else if (rand2 < 101)
                                    Objects[i][j].item = maker.GetIceBomb();
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
