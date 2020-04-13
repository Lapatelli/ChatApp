using ChatApp.Core.DTO;
using ChatApp.Interfaces.Repositories;
using ChatApp.Persistence.Context;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace ChatApp.Persistence.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChatDbContext _db;

        public ChatRepository(ChatDbContext context)
        {
            _db = context;
        }

        public async Task<ChatDTO> CreateChatAsync(ChatDTO chat)
        {
            await _db.Chats.InsertOneAsync(chat);

            return chat;
        }

        public async Task<ChatDTO> SearchChatByName(string name)
        {
            return await _db.Chats.Find(us => us.Name == name).FirstOrDefaultAsync();
        }
    }
}
