using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
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

        public async Task<Chat> GetAggregateChatWithUsers(ChatDTO chat)
        {
            var result = await _db.Chats.Aggregate().Match(ch => ch.Id == chat.Id)
                .Lookup<ChatDTO, UserDTO, Chat>(_db.Users, ch => ch.CreatedByUserId, us => us.Id, ch => ch.CreatedByUser)
                .Lookup<Chat, UserDTO, Chat>(_db.Users, ch => ch.ChatUsersId, us => us.Id, ch => ch.ChatUsers)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Chat> GetAggregateChatWithUsers(ObjectId chatId)
        {
            var result = await _db.Chats.Aggregate().Match(ch => ch.Id == chatId)
                .Lookup<ChatDTO, UserDTO, Chat>(_db.Users, ch => ch.CreatedByUserId, us => us.Id, ch => ch.CreatedByUser)
                .Lookup<Chat, UserDTO, Chat>(_db.Users, ch => ch.ChatUsersId, us => us.Id, ch => ch.ChatUsers)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<Chat> SearchChatByName(string name)
        {
            var result = await _db.Chats.Aggregate().Match(ch => ch.Name == name)
                .Lookup<ChatDTO, UserDTO, Chat>(_db.Users, ch => ch.CreatedByUserId, us => us.Id, ch => ch.CreatedByUser)
                .Lookup<Chat, UserDTO, Chat>(_db.Users, ch => ch.ChatUsersId, us => us.Id, ch => ch.ChatUsers)
                .FirstOrDefaultAsync();

            return result;
        }

        public void CreateChat(ChatDTO chat)
        {
            _db.AddCommand(async () => await _db.Chats.InsertOneAsync(chat));
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
                Builders<ChatDTO>.Update.Push("ChatUsersId", userId),
                new FindOneAndUpdateOptions<ChatDTO, ChatDTO> { ReturnDocument = ReturnDocument.After }));
        }

        public void DeleteUserFromChatAsync(ObjectId chatId, ObjectId userId)
        {
            _db.AddCommand(async () => await _db.Chats.FindOneAndUpdateAsync(Builders<ChatDTO>.Filter.Eq("_id", chatId),
                Builders<ChatDTO>.Update.Pull("ChatUsersId", userId),
                new FindOneAndUpdateOptions<ChatDTO, ChatDTO> { ReturnDocument = ReturnDocument.After }));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
