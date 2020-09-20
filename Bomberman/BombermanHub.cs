using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Bomberman
{
    public class BombermanHub : Hub
    {
        public void UpdateClients()
        {
             Clients.All.SendAsync("StateUpdate", "Hi");
        }
    }
}
