using System.Collections.Generic;

namespace BombermanClasses.Composite
{
    public class CompositeDirectory : Composite
    {
        public Composite children { get; set; }

        public CompositeDirectory()
        {}

        public void add(Composite child)
        {
            if (children != null)
                children.setNext(child);
            else
                children = child;
        }

        public override int calculateScore(int score)
        {
            if (children != null)
            {
                int childrenScore = children.calculateScore(score);
                int baseScore = base.calculateScore(childrenScore);
                return baseScore;
            } else
            {
                int baseScore = base.calculateScore(score);
                return baseScore;
            }
//            return children != null ? base.calculateScore(children.calculateScore(score)) : base.calculateScore(score);
        }
    }
}
