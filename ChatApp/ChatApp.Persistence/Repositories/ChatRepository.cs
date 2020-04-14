using ChatApp.Core.DTO;
using ChatApp.Interfaces.Repositories;
using ChatApp.Persistence.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
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
            return await _db.Chats.Find(ch => ch.Name == name).FirstOrDefaultAsync();
        }

        public async Task<ChatDTO> DeleteUserFromChatAsync(ObjectId chatId, ObjectId userId)
        {
            List<ObjectId> chatUsers = new List<ObjectId>();
            var chatDb = await _db.Chats.Find(ch => ch.Id == chatId).FirstOrDefaultAsync();

            foreach (var chatUser in chatDb.ChatUsers)
            {
                if (chatUser != userId)
                {
                    chatUsers.Add(chatUser);
                }
            }

            chatDb.ChatUsers = chatUsers;

            await _db.Chats.ReplaceOneAsync(new BsonDocument("_id", chatId), chatDb);

            return chatDb;
        }

        public async Task<ChatDTO> AddUserToChatAsync(ObjectId chatId, ObjectId userId)
        {
            List<ObjectId> chatUsers = new List<ObjectId>();
            var chatDb = await _db.Chats.Find(ch => ch.Id == chatId).FirstOrDefaultAsync();

            foreach (var chatUser in chatDb.ChatUsers)
            {
                chatUsers.Add(chatUser);
            }

            chatUsers.Add(userId);
            chatDb.ChatUsers = chatUsers;

            await _db.Chats.ReplaceOneAsync(new BsonDocument("_id", chatId), chatDb);

            return chatDb;
        }

        public async Task DeleteChatAsync(ObjectId chatId)
        {
            await _db.Chats.DeleteOneAsync(new BsonDocument("_id", chatId));
        }

        public async Task<ChatDTO> UpdateChatAsync(ChatDTO chat)
        {
            var chatDb = await _db.Chats.Find(ch => ch.Id == chat.Id).FirstOrDefaultAsync();

            chatDb.Name = chat.Name;
            chatDb.Password = chat.Password;
            chatDb.ChatPrivacy = chat.ChatPrivacy;

            await _db.Chats.ReplaceOneAsync(new BsonDocument("_id", chat.Id), chatDb);

            return chatDb;
        }
    }
}
