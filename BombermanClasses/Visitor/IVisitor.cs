using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Visitor
{
    public interface IVisitor
    {
        void Visit(IElement element);
    }
}
