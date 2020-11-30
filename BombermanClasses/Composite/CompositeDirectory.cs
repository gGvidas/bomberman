using System.Collections.Generic;

namespace BombermanClasses.Composite
{
    public class CompositeDirectory : IComposite
    {
        public List<IComposite> children { get; set; }

        public CompositeDirectory()
        {
            children = new List<IComposite>();
        }

        public void add(IComposite child)
        {
            children.Add(child);
        }

        public int getScore()
        {
            int sum = 0;
            children.ForEach(child => sum += child.getScore());
            return sum;
        }
    }
}
