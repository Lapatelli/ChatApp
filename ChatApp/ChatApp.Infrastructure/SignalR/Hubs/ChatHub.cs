using ChatApp.Core.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("OnSendMessage", message);
        }
    }
}
