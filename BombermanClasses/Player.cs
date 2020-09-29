namespace BombermanClasses
{
    public class Player
    {
        public string Id { get; set; }
        public int x { get; set; } 
        public int y { get; set; }
        
        public Player(string id, int x, int y)
        {
            Id = id;
            this.x = x;
            this.y = y;
        }
    }
}
