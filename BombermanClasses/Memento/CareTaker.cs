using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Memento
{
    public class CareTaker
    {
        private Memento[] mementoList = new Memento[1];
        //private List<Memento> mementoList = new List<Memento>();

        public void Add(Memento state)
        {
            //mementoList.Add(state);
            mementoList[0] = state;
        }

        public Memento Get(int index)
        {
            //return mementoList[index];
            return mementoList[0];
        }
    }
}
