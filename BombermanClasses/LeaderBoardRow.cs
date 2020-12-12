using BombermanClasses.Visitor;
using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Proxy
{
    public class LeaderBoardRow : IElement
    {
        public string Id { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public int score { get; set; }
        public string PlayerStatus { get; set; }

        public int RoundsPlayed { get; set; } = 1;

        public void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
