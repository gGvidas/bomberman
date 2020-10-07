using System;
using System.Collections.Generic;
using System.Text;

namespace BombermanClasses.Observer
{
    public abstract class Subject
    {
        private List<IObserver> Observers  = new List<IObserver>();

        private object _lock = new object();
        public void Attach(IObserver observer)
        {
           Observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            Observers.Remove(observer);
        }

        public void Notify()
        {
            try
            {
                lock (_lock)
                {
                    foreach (var observer in Observers)
                    {
                        observer.Update();
                    }
                }
            }catch(Exception){}
            
        }
    }
}
