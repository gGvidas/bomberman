namespace BombermanClasses.ChainOfResponsibility
{
    public abstract class Handler
    {
        public Handler nextHandler;
        public virtual int calculateScore(int score)
        {
            return nextHandler != null ? nextHandler.calculateScore(score) : score;
        }

        public void setNext(Handler handler)
        {
            if (nextHandler != null)
                nextHandler.setNext(handler);
            else
                nextHandler = handler;
        }
    }
}
