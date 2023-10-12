using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMqSignalR.Hubs
{
    public class MessageHub: Hub
    {
        public async Task SendMessageAsync(string message, string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("reciveMessage", message);
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("userJoined", Context.ConnectionId);
        }
    }
}
