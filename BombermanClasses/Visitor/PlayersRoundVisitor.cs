using BombermanClasses.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombermanClasses.Visitor
{
    class PlayersRoundVisitor : IVisitor
    {
        public void Visit(IElement element)
        {
            var boardRow = element as LeaderBoardRow;

            var player =  World.Instance.Players.FirstOrDefault(player => player.Id == boardRow.Id);

            if(player != null)
                boardRow.RoundsPlayed = player.RoundsPlayed;
        }
    }
}
