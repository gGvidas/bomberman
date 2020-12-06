using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Proxy
{
    public class RealLeaderboard : ILeaderboardProxy
    {
        public List<LeaderBoardRow> _leaderboard;
       
        public List<LeaderBoardRow> GetLeaderBoard()
        {
            return _leaderboard;
        }
    }
}
