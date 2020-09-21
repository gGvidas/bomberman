using Bomberman.GameObjects;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Bomberman
{
    public class BombermanHub : Hub
    {
        private IWorld World;

        public BombermanHub(IWorld world)
        {
            World = world;
        }

        public async Task UpdateClients()
        {
            await Clients.All.SendAsync("StateUpdate", World.GetObjects());
        }
        public async Task Movement(string KeyPress)
        {
            World.MovePlayer(Context.ConnectionId, KeyPress);
            await UpdateClients();
        }
        public async override Task OnConnectedAsync()
        {
            World.AddPlayer(Context.ConnectionId);
            await UpdateClients();
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            //World.RemovePlayer(Context.ConnectionId);
            //await UpdateClients();
            //await base.OnDisconnectedAsync(exception);
        }
    }
}
