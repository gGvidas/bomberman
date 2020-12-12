using BombermanClasses.Proxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Visitor
{
    public class PlayerStatusVisitor : IVisitor
    {
        public void Visit(IElement element)
        {
            var boardRow = element as LeaderBoardRow;

            if(World.Instance.GetDeadPlayersIds().Contains(boardRow.Id))
            {
                boardRow.PlayerStatus = "Dead";
            }
            else if(World.Instance.GetAlivePlayersIds().Contains(boardRow.Id))
            {
                boardRow.PlayerStatus = "Alive";
            }
            else
            {
                boardRow.PlayerStatus = "Disconnected";
            }

            
        }
    }
}
