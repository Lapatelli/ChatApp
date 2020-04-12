using ChatApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Task<Chat> SearchChatByName(string name);

        Task<Chat> CreateChatAsync(Chat chat, string userId, IEnumerable<string> chatUsers);
    }
}
