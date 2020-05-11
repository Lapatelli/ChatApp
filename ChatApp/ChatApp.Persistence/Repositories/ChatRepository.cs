using ChatApp.Core.DTO;
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

        public async Task<ChatDTO> CreateChatAsync(ChatDTO chat)
        {
            await _db.Chats.InsertOneAsync(chat);

            return chat;
        }

        public async Task<ChatDTO> SearchChatByName(string name)
        {

            ChatsWithUsers result = await _db.Chats.Aggregate().Match(ch => ch.Name == name).Lookup<ChatDTO, UserDTO, ChatsWithUsers>(
                _db.Users,
                ch => ch.ChatUsers,
                us => us.Id,
                ch => ch.ChatUsersFull)
                .FirstOrDefaultAsync();

            return await _db.Chats.Find(ch => ch.Name == name).FirstOrDefaultAsync();
        }

        public async Task DeleteChatAsync(ObjectId chatId)
        {
            await _db.Chats.DeleteOneAsync(new BsonDocument("_id", chatId));
        }

        public async Task<ChatDTO> DeleteUserFromChatAsync(ObjectId chatId, ObjectId userId)
        {
            var chatDb = await _db.Chats.Find(ch => ch.Id == chatId).FirstOrDefaultAsync();

            chatDb.ChatUsers.Remove(userId);

            await _db.Chats.ReplaceOneAsync(new BsonDocument("_id", chatId), chatDb);

            return chatDb;
        }

        public async Task<ChatDTO> AddUserToChatAsync(ObjectId chatId, ObjectId userId)
        {
            var chatDb = await _db.Chats.Find(ch => ch.Id == chatId).FirstOrDefaultAsync();

            chatDb.ChatUsers.Add(userId);

            await _db.Chats.ReplaceOneAsync(new BsonDocument("_id", chatId), chatDb);

            return chatDb;
        }

        public async Task<ChatDTO> UpdateChatAsync(ChatDTO chat)
        {
            var chatDb = await _db.Chats.Find(ch => ch.Id == chat.Id).FirstOrDefaultAsync();

            chatDb.Name = chat.Name ?? chatDb.Name;
            chatDb.Password = chat.Password ?? chatDb.Password;
            chatDb.ChatPrivacy = chat.ChatPrivacy ?? chatDb.ChatPrivacy;

            await _db.Chats.ReplaceOneAsync(new BsonDocument("_id", chat.Id), chatDb);

            return chatDb;
        }
    }
}
