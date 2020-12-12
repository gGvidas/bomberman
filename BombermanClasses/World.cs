using Bomberman;
using BombermanClasses.BombNameSpace;
using BombermanClasses.Command;
using BombermanClasses.Composite;
using BombermanClasses.Items;
using BombermanClasses.Iterator;
using BombermanClasses.MapBuilder;
using BombermanClasses.Mediator;
using BombermanClasses.Memento;
using BombermanClasses.Observer;
using BombermanClasses.Proxy;
using BombermanClasses.TemplateMethod;
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
        private Int32 numSquaresX;
        private Int32 numSquaresY;
        private List<Player> Players { get; set; }

        private Map Map { get; set; }
        public BombermanHub Hub { get; set; }

        private int timeUnitInMilisec = 1000;
        private Timer _timer { get; set; }

        private MovementInvoker movementInvoker;

        private AbstractMapBuilder mapBuilder;

        private IMediator mediator;

        static World()
        {

        }

        private World()
        {
            var rand = new Random();
            mapBuilder = rand.Next(2) == 0 ? (AbstractMapBuilder)new SmallMapBuilder() : new NormalMapBuilder();
            BuildMap();
            numSquaresX = Map.Objects.GetLength(0);
            numSquaresY = Map.Objects[0].Length;
            Players = new List<Player>();
            SetTimer();
            movementInvoker = new MovementInvoker();
            mediator = new ContreteMediator();
        }

        private void BuildMap()
        {
           
            MapDirector director = new MapDirector(mapBuilder);
            director.Construct();
            Map = mapBuilder.Build();
        }

        private void SetTimer()
        {
            _timer = new Timer();
            _timer.Interval = timeUnitInMilisec;
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Notify();
        }

        public Tile[][] GetObjects()
        {
            return Map.Objects;
        }

        public void MovePlayer(string id, string keypress)
        {
            Player player = GetPlayer(id);
            if (player.isDead) return;


            int x = player.x, y = player.y;
            Map.Objects[x][y].onfiretype = null;
            Map.Objects[x][y].firetype = null;
            Map.Objects[x][y].icetype = null;
            switch (keypress)
            {
                case "W":
                    if (y != 0 && (Map.Objects[x][y - 1].entity == null ||
                        (Map.Objects[x][y - 1].entity is Fire && player.item is FireShield) ||
                        (Map.Objects[x][y - 1].entity is IceWall && player.item is IceShield)))
                    {
                        IPlayer play = new Player(id, x, y);
                        if (Map.Objects[x][y - 1].item != null)
                        {
                            player.item = Map.Objects[x][y - 1].item;
                            Map.Objects[x][y - 1].item = null;
                            AddNewItem();
                        }

                        Map.Objects[x][y].entity = null;

                        if (Map.Objects[x][y - 1].entity is Fire && player.item is FireShield)
                            Map.Objects[x][y - 1].onfiretype = new OnFirePlayer(play);
                        else if (player.item is FireShield)
                            Map.Objects[x][y - 1].firetype = new FirePlayer(play);
                        else if (player.item is IceShield)
                            Map.Objects[x][y - 1].icetype = new IcePlayer(play);
                        Map.Objects[x][y - 1].entity = player;

                        movementInvoker.setCommand(new MoveUpCommand(player));
                        movementInvoker.move();
                    }
                    break;
                case "A":
                    if (x != 0 && (Map.Objects[x -1][y].entity == null ||
                        (Map.Objects[x -1][y].entity is Fire && player.item is FireShield) ||
                        (Map.Objects[x -1][y].entity is IceWall && player.item is IceShield)))
                    {
                        IPlayer play = new Player(id, x, y);
                        if (Map.Objects[x - 1][y].item != null)
                        {
                            player.item = Map.Objects[x - 1][y].item;
                            Map.Objects[x - 1][y].item = null;
                            AddNewItem();
                        }

                        Map.Objects[x][y].entity = null;


                        if (Map.Objects[x - 1][y].entity is Fire && player.item is FireShield)
                            Map.Objects[x - 1][y].onfiretype = new OnFirePlayer(play);
                        else if (player.item is FireShield)
                            Map.Objects[x - 1][y].firetype = new FirePlayer(play);
                        else if (player.item is IceShield)
                            Map.Objects[x - 1][y].icetype = new IcePlayer(play);
                        Map.Objects[x - 1][y].entity = player;

                        movementInvoker.setCommand(new MoveLeftCommand(player));
                        movementInvoker.move();
                    }
                    break;
                case "D":
                    if (x != numSquaresX - 1 && (Map.Objects[x + 1][y].entity == null ||
                        (Map.Objects[x + 1][y].entity is Fire && player.item is FireShield) ||
                        (Map.Objects[x + 1][y].entity is IceWall && player.item is IceShield)))
                    {
                        IPlayer play = new Player(id, x, y);
                        if (Map.Objects[x + 1][y].item != null)
                        {
                            player.item = Map.Objects[x + 1][y].item;
                            Map.Objects[x + 1][y].item = null;
                            AddNewItem();
                        }

                        Map.Objects[x][y].entity = null;

                        if (Map.Objects[x + 1][y].entity is Fire && player.item is FireShield)
                            Map.Objects[x + 1][y].onfiretype = new OnFirePlayer(play);
                        else if (player.item is FireShield)
                            Map.Objects[x + 1][y].firetype = new FirePlayer(play);
                        else if (player.item is IceShield)
                            Map.Objects[x + 1][y].icetype = new IcePlayer(play);
                        Map.Objects[x + 1][y].entity = player;

                        movementInvoker.setCommand(new MoveRightCommand(player));
                        movementInvoker.move();
                    }
                    break;
                case "S":
                    if (y != numSquaresY - 1 && (Map.Objects[x][y + 1].entity == null ||
                        (Map.Objects[x][y + 1].entity is Fire && player.item is FireShield) ||
                        (Map.Objects[x][y + 1].entity is IceWall && player.item is IceShield)))
                    {
                        IPlayer play = new Player(id, x, y);
                        if (Map.Objects[x][y + 1].item != null)
                        {
                            player.item = Map.Objects[x][y + 1].item;
                            Map.Objects[x][y + 1].item = null;
                            AddNewItem();
                        }

                        Map.Objects[x][y].entity = null;

                        if (Map.Objects[x][y + 1].entity is Fire && player.item is FireShield)
                            Map.Objects[x][y + 1].onfiretype = new OnFirePlayer(play);
                        else if (player.item is FireShield)
                            Map.Objects[x][y + 1].firetype = new FirePlayer(play);
                        else if (player.item is IceShield)
                            Map.Objects[x][y + 1].icetype = new IcePlayer(play);
                        Map.Objects[x][y + 1].entity = player;

                        movementInvoker.setCommand(new MoveDownCommand(player));
                        movementInvoker.move();
                    }
                    break;
                case "F":
                    movementInvoker.undo(player.Id);
                    if (player.x >= 0 && player.y >= 0 && player.x < numSquaresX && player.y < numSquaresY && (Map.Objects[player.x][player.y].entity == null ||
                        (Map.Objects[player.x][player.y].entity is Fire && player.item is FireShield) ||
                        (Map.Objects[player.x][player.y].entity is IceWall && player.item is IceShield)))
                    {
                        Map.Objects[x][y].entity = null;
                        Map.Objects[player.x][player.y].entity = player;
                        IPlayer play1 = new Player(id, x, y);
                        if (Map.Objects[player.x][player.y].entity is Fire && player.item is FireShield)
                            Map.Objects[player.x][player.y].onfiretype = new OnFirePlayer(play1);
                        else if (player.item is FireShield)
                            Map.Objects[player.x][player.y].firetype = new FirePlayer(play1);
                        else if (player.item is IceShield)
                            Map.Objects[player.x][player.y].icetype = new IcePlayer(play1);
                    }
                    else
                    {
                        player.x = x;
                        player.y = y;
                    }
                    break;
                case "I":
                    if (player.item != null)
                    {
                        player.SaveItem();
                    }
                    break;
                case "M":
                    if (player.item != null)
                    {
                        mediator.SaveFakeItem(player);
                    }
                    break;
                case "O":
                    player.ReturnItem();
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
                Map.Objects[player.x][player.y].bomb = maker.GetFireBomb(id, player.x, player.y, strategy, instance);
                player.item = null;
            }
            else if (player.item is IceBomb)
            {
                Map.Objects[player.x][player.y].bomb = maker.GetIceBomb(id, player.x, player.y, strategy, instance);
                player.item = null;
            }
            else
            {
                Bomb bomb = new Bomb(id, player.x, player.y, strategy, instance);
                Map.Objects[player.x][player.y].bomb = bomb;
            }         
        }

        public void Explode(int x, int y)
        {
            if (Map.Objects[x][y].bomb == null) return;
            CompositeDirectory destroyedEntities = new CompositeDirectory();

            int radius = Map.Objects[x][y].bomb.explosionRadius(2);

            Map.Objects[x][y].destroy();

            var wallFactory = new WallFactory();

            bool up = false, down = false, left = false, right = false;

            for (int i = 1; i <= radius; i++)
            {
                if (x + i < numSquaresX && !(Map.Objects[x + i][y].entity is IndestructableWall) && !(Map.Objects[x + i][y].entity is Fire) && !right) 
                {
                    if (Map.Objects[x][y].bomb is FireBomb)
                        Map.Objects[x + i][y].entity = new Fire(x + i, y);
                    else if (Map.Objects[x][y].bomb is IceBomb)
                        if (!(Map.Objects[x + 1][y].entity is DestructableWall))
                            Map.Objects[x + i][y].entity = wallFactory.CreateWall(4);
                        else
                            right = true;
                    else
                    {
                        DestructionTemplate destroyedObject = Map.Objects[x + i][y].destroy();
                        if (destroyedObject != null)
                        {
                            destroyedEntities.add(destroyedObject);
                        }
                    }
                }
                else
                    right = true;
                if (x - i >= 0 && !(Map.Objects[x - i][y].entity is IndestructableWall) && !(Map.Objects[x - i][y].entity is Fire) && !left)
                {
                    if (Map.Objects[x][y].bomb is FireBomb)
                        Map.Objects[x - i][y].entity = new Fire(x - i, y);
                    else if (Map.Objects[x][y].bomb is IceBomb)
                        if (!(Map.Objects[x - 1][y].entity is DestructableWall))
                            Map.Objects[x - i][y].entity = wallFactory.CreateWall(4);
                        else
                            left = true;
                    else
                    {
                        DestructionTemplate destroyedObject = Map.Objects[x - i][y].destroy();
                        if (destroyedObject != null)
                        {
                            destroyedEntities.add(destroyedObject);
                        }
                    }
                }
                else
                    left = true;
                if (y + i < numSquaresX && !(Map.Objects[x][y + i].entity is IndestructableWall) && !(Map.Objects[x][y + i].entity is Fire) && !up)
                {
                    if (Map.Objects[x][y].bomb is FireBomb)
                        Map.Objects[x][y + i].entity = new Fire(x, y - i);
                    else if (Map.Objects[x][y].bomb is IceBomb)
                        if (!(Map.Objects[x][y + i].entity is DestructableWall))
                            Map.Objects[x][y + i].entity = wallFactory.CreateWall(4);
                        else
                            down = true;
                    else
                    {
                        DestructionTemplate destroyedObject = Map.Objects[x][y + i].destroy();
                        if (destroyedObject != null)
                        {
                            destroyedEntities.add(destroyedObject);
                        }
                    }
                }
                else
                    down = true;
                if (y - i >= 0 && !(Map.Objects[x][y - i].entity is IndestructableWall) && !(Map.Objects[x][y - i].entity is Fire) && !down)
                {
                    if (Map.Objects[x][y].bomb is FireBomb)
                        Map.Objects[x][y - i].entity = new Fire(x, y - i);
                    else if (Map.Objects[x][y].bomb is IceBomb)
                        if (!(Map.Objects[x][y - i].entity is DestructableWall))
                            Map.Objects[x][y - i].entity = wallFactory.CreateWall(4);
                        else
                            up = true;
                    else
                    {
                        DestructionTemplate destroyedObject = Map.Objects[x][y - i].destroy();
                        if (destroyedObject != null)
                        {
                            destroyedEntities.add(destroyedObject);
                        }
                    }
                }
                else
                    up = true;
            }
            Player player = GetPlayer(Map.Objects[x][y].bomb.playerId);
            if (player != null)
            {
                player.destroyedEntities.add(destroyedEntities);
            }
            Map.Objects[x][y].bomb = null;
        }

        public void AddPlayer(string id)
        {
            Random random = new Random();
            int x = 0;
            int y = 0;
            while (!(Map.Objects[x][y].entity == null))
            {
                x = random.Next(0, numSquaresX);
                y = random.Next(0, numSquaresY);
            }
            Player player = new Player(id, x, y);
            Map.Objects[x][y].entity = player;
            Players.Add(player);
            mediator.addPlayer(player);
        }
        
        public void RemovePlayer(string id)
        {
            Player player = GetPlayer(id);
            Map.Objects[player.x][player.y].entity = null;
            Players.Remove(player);
            mediator.removePlayer(player);
        }
        
        private Player GetPlayer(string id)
        {
            return Players.FirstOrDefault(player => player.Id == id);
        }

        public List<string> GetDeadPlayersIds()
        {
            return Players.Where(player => player.isDead).Select(player => player.Id).ToList();
        }

        public List<string> GetAlivePlayersIds()
        {
            return Players.Where(player => !player.isDead).Select(player => player.Id).ToList();
        }

        public RealLeaderboard GetPlayerScores()
        {
            var dummyResults = getDummyLeaderBoard();
            var currentPlayerResult = Players.Select(player => new LeaderBoardRow { Id = player.Id, Name = "New player", score = player.destroyedEntities.calculateScore(0) });

            dummyResults.AddRange(currentPlayerResult);

            var result = dummyResults.OrderByDescending(player => player.score).
             Select((player, index) => new LeaderBoardRow { Id = player.Id, Rank = index + 1, Name = player.Name, score = player.score }).ToList();

            return new RealLeaderboard { _leaderboard = result.GetRange(0, result.Count) }; ;
        }

        private List<LeaderBoardRow> getDummyLeaderBoard()
        {
            var result = new List<LeaderBoardRow>();
            for(int i = 0; i < 500; i++)
            {
                result.Add(new LeaderBoardRow { Name = $"Player{i + 1}", score = 100 * i });
            }
            return result;
        }

        private void AddNewItem()
        {
            int x = 0, y = 0;
            Random r = new Random();
            while (!(Map.Objects[x][y].entity == null)) 
            {
                x = r.Next(0, numSquaresX);
                y = r.Next(0, numSquaresY);
            }

            var wallFactory = new WallFactory();
            ItemArray itemsRepository = new ItemArray();
            var iter = itemsRepository.GetIterator();

            Map.Objects[x][y].entity = wallFactory.CreateWall(3);

            iter.HasNext();
            Map.Objects[x][y].item = iter.Next(y, numSquaresY);
        }

        public void RestartGame()
        {
            BuildMap();

            foreach (Player player in Players)
            {
                Random random = new Random();
                int x = 0;
                int y = 0;
                while (!(Map.Objects[x][y].entity == null))
                {
                    x = random.Next(0, numSquaresX);
                    y = random.Next(0, numSquaresY);
                }
                player.isDead = false;
                player.x = x;
                player.y = y;
                Map.Objects[x][y].entity = player;
            }
        }
    }
}
