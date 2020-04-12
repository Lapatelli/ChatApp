using ChatApp.Core.Entities;
using ChatApp.Interfaces.Repositories;
using ChatApp.Persistence.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
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

        public async Task<Chat> CreateChatAsync(Chat chat, string userId, IEnumerable<string> chatUsers)
        {
            List<ObjectId> chatUsersObjectId = new List<ObjectId>();

            var userCreator = ObjectId.Parse(userId);

            foreach (var chatUser in chatUsers)
            {
                chatUsersObjectId.Add(ObjectId.Parse(chatUser));
            }

            chat.ChatUsers = chatUsersObjectId;
            chat.CreatedByUser = userCreator;

            await _db.Chats.InsertOneAsync(chat);

            return chat;
        }

        public async Task<Chat> SearchChatByName(string name)
        {
            return await _db.Chats.Find(us => us.Name == name).FirstOrDefaultAsync();
        }
    }
}
