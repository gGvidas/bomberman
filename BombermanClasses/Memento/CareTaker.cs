using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Memento
{
    public class CareTaker
    {
        private List<Memento> mementoList = new List<Memento>();
        int index = 0;

        public void Add(Memento state)
        {
            mementoList.Add(state);
        }

        public Memento Get()
        {
            if (mementoList.Count >= index+1)
                return mementoList[index++];
            else
            {
                index = 0;
                return mementoList[index];
            }          
        }
    }
}
