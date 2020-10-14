namespace BombermanClasses.Command
{
    public class MoveLeftCommand : IMoveCommand
    {
        public Player player;
        public MoveLeftCommand(Player player)
        {
            this.player = player;
        }

        public void move()
        {
            player.moveLeft();
        }

        public void undo()
        {
            player.moveRight();
        }
        public string getPlayerId()
        {
            return player.Id;
        }
    }
}
