namespace BombermanClasses.Command
{
    public class MoveDownCommand : IMoveCommand
    {
        public Player player;
        public MoveDownCommand(Player player)
        {
            this.player = player;
        }

        public void move()
        {
            player.moveDown();
        }

        public void undo()
        {
            player.moveUp();
        }
        public string getPlayerId()
        {
            return player.Id;
        }
    }
}
