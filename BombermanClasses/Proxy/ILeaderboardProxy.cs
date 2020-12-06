using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Proxy
{
    public interface ILeaderboardProxy
    {
        public List<LeaderBoardRow> GetLeaderBoard();
    }
}
