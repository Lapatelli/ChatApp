using ChatApp.Core.DTO;
using ChatApp.Interfaces.Repositories;
using ChatApp.Persistence.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
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

        public async Task<ChatDTO> GetChatById(ObjectId chatId)
        {
            return await _db.Chats.Find(ch => ch.Id == chatId).FirstOrDefaultAsync();
        }

        public async Task<ChatWithUsersDTO> GetAggregateChatWithUsers(ChatDTO chat)
        {
            //.Lookup<ChatDTO, UserDTO, Chat>(_db.Users, ch => ch.CreatedByUserId, us => us.Id, ch => ch.CreatedByUser)
            return await _db.Chats.Aggregate().Match(ch => ch.Id == chat.Id)
                .Lookup<ChatDTO, UserDTO, ChatWithUsersDTO>(_db.Users, ch => ch.ChatUsersId, us => us.Id, ch => ch.ChatUsers)
                .FirstOrDefaultAsync();
        }

        public async Task<ChatWithUsersDTO> GetAggregateChatWithUsers(ObjectId chatId)
        {
            return await _db.Chats.Aggregate().Match(ch => ch.Id == chatId)
                .Lookup<ChatDTO, UserDTO, ChatWithUsersDTO>(_db.Users, ch => ch.ChatUsersId, us => us.Id, ch => ch.ChatUsers)
                .FirstOrDefaultAsync();
        }

        public async Task<ChatWithUsersDTO> SearchChatByName(string name)
        {
            return await _db.Chats.Aggregate().Match(ch => ch.Name == name)
                .Lookup<ChatDTO, UserDTO, ChatWithUsersDTO>(_db.Users, ch => ch.ChatUsersId, us => us.Id, ch => ch.ChatUsers)
                .FirstOrDefaultAsync();
        }

        public void CreateChat(ChatDTO chat)
        {
            _db.AddCommand(async () =>
                {
                    chat.ChatUsersId.Add(chat.CreatedByUserId);
                    await _db.Chats.InsertOneAsync(chat); 
                });
        }

        public void UpdateChat(ChatDTO chat)
        {
            _db.AddCommand(async () =>
            {
                if (chat.Name != null)
                    await _db.Chats.UpdateOneAsync(Builders<ChatDTO>.Filter.Eq("_id", chat.Id), Builders<ChatDTO>.Update.Set("Name", chat.Name));

                if (chat.Password != null)
                    await _db.Chats.UpdateOneAsync(Builders<ChatDTO>.Filter.Eq("_id", chat.Id), Builders<ChatDTO>.Update.Set("Password", chat.Password));

                if (chat.ChatPrivacy != null)
                    await _db.Chats.UpdateOneAsync(Builders<ChatDTO>.Filter.Eq("_id", chat.Id), Builders<ChatDTO>.Update.Set("ChatPrivacy", chat.ChatPrivacy));

                if (chat.ChatUsersId != null)
                    await _db.Chats.UpdateOneAsync(Builders<ChatDTO>.Filter.Eq("_id", chat.Id), Builders<ChatDTO>.Update.PushEach("ChatUsersId", chat.ChatUsersId));

                if (chat.Picture != null)
                    await _db.Chats.UpdateOneAsync(Builders<ChatDTO>.Filter.Eq("_id", chat.Id), Builders<ChatDTO>.Update.Set("Picture", chat.Picture));

            });
        }

        public void DeleteChatAsync(ObjectId chatId)
        {
            _db.AddCommand(async () => await _db.Chats.DeleteOneAsync(new BsonDocument("_id", chatId)));
        }

        public void AddUserToChatAsync(ObjectId chatId, ObjectId userId)
        {
            _db.AddCommand(async () => await _db.Chats.FindOneAndUpdateAsync(Builders<ChatDTO>.Filter.Eq("_id", chatId),
                Builders<ChatDTO>.Update.Push("ChatUsersId", userId)));
        }

        public void DeleteUserFromChatAsync(ObjectId chatId, ObjectId userId)
        {
            _db.AddCommand(async () => await _db.Chats.FindOneAndUpdateAsync(Builders<ChatDTO>.Filter.Eq("_id", chatId),
                Builders<ChatDTO>.Update.Pull("ChatUsersId", userId)));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
