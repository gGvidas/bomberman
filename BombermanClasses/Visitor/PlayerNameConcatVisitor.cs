using BombermanClasses.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombermanClasses.Visitor
{
    class PlayerNameConcatVisitor : IVisitor
    {
        public void Visit(IElement element)
        {
            var boardRow = element as LeaderBoardRow;

            boardRow.Name = boardRow.Name.Length >= 10 ? boardRow.Name.Substring(0, 10) : boardRow.Name;

        }
    }
}
