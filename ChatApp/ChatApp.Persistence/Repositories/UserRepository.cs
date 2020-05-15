using ChatApp.Core.DTO;
using ChatApp.Core.Enums;
using ChatApp.Interfaces.Repositories;
using ChatApp.Persistence.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _db;

        public UserRepository(ChatDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            return await _db.Users.Aggregate().SortByDescending(us => us.UserStatus).ThenBy(us => us.LastName).ToListAsync();
        }

        public async Task<UserWithChatsDTO> GetAllChatsForUser(ObjectId id)
        {
            return await _db.Users.Aggregate().Match(us => us.Id == id)
                .Lookup<UserDTO, ChatDTO, UserWithChatsDTO>(_db.Chats, us => us.ChatsId, ch => ch.Id, us => us.Chats)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserDTO>> SearchUserByName(string name)
        {
            return await _db.Users.Find(us => us.FirstName == name).ToListAsync();
        }

        public async Task<UserDTO> SearchUserById(ObjectId id)
        {
            return await _db.Users.Find(us => us.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UserDTO> SearchUserByEmail(string email)
        {
            return await _db.Users.Find(us => us.EmailAddress == email).FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserExist(string email)
        {
            return await _db.Users.Find(us => us.EmailAddress == email).AnyAsync();
        }

        public void CreateUser(UserDTO user, byte[] photo)
        {
            user.BytePhoto = photo;
            _db.AddCommand(async () => await _db.Users.InsertOneAsync(user));
        }

        public void UpdateUser(UserDTO user)
        {
            _db.AddCommand(async () =>
            {
                UserDTO userDb = await SearchUserById(user.Id);

                user.FirstName = user.FirstName ?? userDb.FirstName;
                user.LastName = user.LastName ?? userDb.LastName;
                user.EmailAddress = user.EmailAddress ?? userDb.EmailAddress;
                user.Photo = user.Photo ?? userDb.Photo;
                user.BytePhoto = user.BytePhoto ?? userDb.BytePhoto;
                user.CreatedChatsId = userDb.CreatedChatsId;
                user.ChatsId = userDb.ChatsId;

                await _db.Users.ReplaceOneAsync(new BsonDocument("_id", user.Id), user);
            });
        }

        public void UpdateUserChats(ObjectId userId, ObjectId chatId, bool isChatCreator, bool isUserAddedToChat)
        {
            _db.AddCommand(async () =>
            {
                if (!isChatCreator)
                {
                    if (isUserAddedToChat)
                    {
                        await _db.Users.FindOneAndUpdateAsync(Builders<UserDTO>.Filter.Eq("_id", userId),
                            Builders<UserDTO>.Update.Push("ChatsId", chatId));
                    }
                    else
                    {
                        await _db.Users.FindOneAndUpdateAsync(Builders<UserDTO>.Filter.Eq("_id", userId),
                            Builders<UserDTO>.Update.Pull("ChatsId", chatId));
                    }
                }
                else
                {
                    await _db.Users.FindOneAndUpdateAsync(Builders<UserDTO>.Filter.Eq("_id", userId),
                       Builders<UserDTO>.Update.Push("ChatsId", chatId));

                    await _db.Users.FindOneAndUpdateAsync(Builders<UserDTO>.Filter.Eq("_id", userId),
                       Builders<UserDTO>.Update.Push("CreatedChatsId", chatId));
                }
            });
        }

        public void UpdateUserStatus(string emailAddress, UserStatus userStatus)
        {
            _db.AddCommand(async () => await _db.Users.FindOneAndUpdateAsync(Builders<UserDTO>.Filter.Eq("EmailAddress", emailAddress),
                Builders<UserDTO>.Update.Set("UserStatus", userStatus)));
        }

        public void LeaveChat(ObjectId userId, ObjectId chatId)
        {
            _db.AddCommand(async () => await _db.Users.FindOneAndUpdateAsync(Builders<UserDTO>.Filter.Eq("_id", userId),
                Builders<UserDTO>.Update.Pull("ChatsId", chatId)));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
