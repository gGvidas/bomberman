namespace BombermanClasses.Command
{
    public interface IMoveCommand
    {
        void move();
        void undo();
        string getPlayerId();
    }
}
