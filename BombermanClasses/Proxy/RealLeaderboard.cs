using BombermanClasses.Visitor;
using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Proxy
{
    public class RealLeaderboard : ILeaderboardProxy, IElement
    {
        public List<LeaderBoardRow> _leaderboard;

        public void Accept(IVisitor visitor)
        {
            foreach(var leaderBoardRow in _leaderboard)
            {
                leaderBoardRow.Accept(visitor);
            }
        }

        public List<LeaderBoardRow> GetLeaderBoard()
        {
            return _leaderboard;
        }
    }
}
