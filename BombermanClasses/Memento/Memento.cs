using BombermanClasses.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Memento
{
    public class Memento
    {
        private Item state;

        public Memento(Item state)
        {
            this.state = state;
        }

        public Item GetState()
        {
            return state;
        }
    }
}
