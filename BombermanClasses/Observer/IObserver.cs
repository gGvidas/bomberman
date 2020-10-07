using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BombermanClasses.Observer
{
    public interface IObserver
    {
        Task Update();
    }
}
