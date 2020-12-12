using BombermanClasses.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Mediator
{
    public class ContreteMediator : IMediator
    {
        List<Player> players = new List<Player>();

        public ContreteMediator()
        {
        }

        public void addPlayer(Player p)
        {
            players.Add(p);
        }
        public void removePlayer(Player p)
        {
            players.Remove(p);
        }

        public void SaveFakeItem(Player p)
        {
            foreach (Player player in players)
            {
                if(player.Id != p.Id)
                    player.SaveItem(p.item);
            }
        }
    }
}
