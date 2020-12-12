using BombermanClasses.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Mediator
{
    public interface IMediator
    {
        public void addPlayer(Player p);
        public void removePlayer(Player p);
        public void SaveFakeItem(Player p);
    }
}
