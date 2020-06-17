using ChatApp.Core.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.Infrastructure.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        public async Task AddToChat(string chatId)
        {
            var message = $"{Context.ConnectionId} addded to {chatId}.";

            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);

            await Clients.Group(chatId).SendAsync("OnAddToChat", message);
        }

        public async Task SendMessage(string chatId, Message message)
        {
            await Clients.Group(chatId).SendAsync("OnSendMessage", message);
        }
    }
}
