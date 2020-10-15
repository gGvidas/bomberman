namespace BombermanClasses.Command
{
    public class MoveUpCommand : IMoveCommand
    {
        public Player player;
        public MoveUpCommand(Player player)
        {
            this.player = player;
        }

        public void move()
        {
            player.moveUp();
        }

        public void undo()
        {
            player.moveDown();
        }
        public string getPlayerId()
        {
            return player.Id;
        }
    }
}
