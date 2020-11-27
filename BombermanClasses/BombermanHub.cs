using BombermanClasses;
using Microsoft.AspNetCore.Http.Connections.Features;
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
            World.Instance.Hub = this;
        }

        public async Task UpdateClients()
        {
             var stateDTO = new StateDTO { Objects = World.Instance.GetObjects(), 
                                            DeadPlayersIds = World.Instance.GetDeadPlayersIds(), 
                                            AlivePlayersIds = World.Instance.GetAlivePlayersIds() };
            
             await Clients.All.SendAsync("StateUpdate",
                                         JsonConvert.SerializeObject(stateDTO,
                                         typeof(StateDTO), new JsonSerializerSettings
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
        public async Task RestartGame()
        {
            World.Instance.RestartGame();
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
