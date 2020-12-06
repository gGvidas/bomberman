using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombermanClasses.Proxy
{
    public class LeaderboardProxy : ILeaderboardProxy
    {
        public string Id;
        public RealLeaderboard _realLeaderboard;

        public List<LeaderBoardRow> GetLeaderBoard()
        {
            var topPlayers = _realLeaderboard.GetLeaderBoard().Take(5).ToList();

            var currentPlayer = _realLeaderboard.GetLeaderBoard().Find(leaderBoard => leaderBoard.Id == Id);

            if(currentPlayer.Rank > 5) {
                topPlayers.Add(currentPlayer);
            }

            return topPlayers.Select(player =>
            {
                if (player.Id == Id)
                    player.Name = "You";
                return player;
            }).ToList();

        }
    }
}
