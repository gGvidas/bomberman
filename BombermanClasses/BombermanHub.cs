using BombermanClasses;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Bomberman
{
    public class BombermanHub : Hub
    {
        public BombermanHub()
        {
            World.Instance.hub = this;
        }

        public async Task UpdateClients()
        {

             await Clients.All.SendAsync("StateUpdate",
                                         JsonConvert.SerializeObject(World.Instance.GetObjects(),
                                         typeof(Map), new JsonSerializerSettings
                                        { TypeNameHandling = TypeNameHandling.Auto }));
        }
        public async Task Movement(string KeyPress)
        {
            World.Instance.MovePlayer(Context.ConnectionId, KeyPress);
            await UpdateClients();
        }
        public async Task PutDownBomb()
        {
            World.Instance.AddBomb(Context.ConnectionId);
            await UpdateClients();
        }
        public async override Task OnConnectedAsync()
        {
            World.Instance.AddPlayer(Context.ConnectionId);
            await UpdateClients();
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            World.Instance.RemovePlayer(Context.ConnectionId);
            await UpdateClients();
            await base.OnDisconnectedAsync(exception);
        }
    }
}
