namespace BombermanClasses.Command
{
    public class MoveRightCommand : IMoveCommand
    {
        public Player player;
        public MoveRightCommand(Player player)
        {
            this.player = player;
        }

        public void move()
        {
            player.moveRight();
        }

        public void undo()
        {
            player.moveLeft();
        }
        public string getPlayerId()
        {
            return player.Id;
        }
    }
}
