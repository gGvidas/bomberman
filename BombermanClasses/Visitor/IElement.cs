using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Visitor
{
    public interface IElement
    {
        public abstract void Accept(IVisitor visitor);
    }
}
