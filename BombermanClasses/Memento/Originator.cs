using BombermanClasses.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Memento
{
    public class Originator
    {
        private Item state;

        public void SetState(Item state)
        {
            this.state = state;
        }

        public Item GetState()
        {
            return state;
        }

        public Memento SaveStateToMemento()
        {
            return new Memento(state);
        }

        public void GetStateFromMemento(Memento memento)
        {
            state = memento.GetState();
        }
    }
}
