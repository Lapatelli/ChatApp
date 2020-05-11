using ChatApp.Core.DTO;
using ChatApp.Core.Enums;
using ChatApp.Interfaces.Repositories;
using ChatApp.Persistence.Context;
using MongoDB.Bson;
using MongoDB.Driver;
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
            return await _db.Users.AsQueryable().ToListAsync();
        }

        public async Task<IEnumerable<UserDTO>> SearchUserByName(string name)
        {
            return await _db.Users.Find(us => us.FirstName == name).ToListAsync();
        }

        public async Task<UserDTO> SearchUserById(ObjectId id)
        {
            return await _db.Users.Find(us => us.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserExist(string email)
        {
            return await _db.Users.Find(us => us.EmailAddress == email).AnyAsync();
        }

        public async Task<UserDTO> CreateUser(UserDTO user, byte[] photo)
        {
            user.BytePhoto = photo;
            await _db.Users.InsertOneAsync(user);

            return user;
        }

        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            List<ObjectId> chats = new List<ObjectId>();
            List<ObjectId> createdChats = new List<ObjectId>();

            var userDb = await SearchUserById(user.Id);

            if (userDb.Chats != null)
            {
                foreach (var userDbChat in userDb.Chats)
                {
                    chats.Add(userDbChat);
                }
            }
            if (userDb.CreatedChats != null)
            {
                foreach (var userDbCreatingChats in userDb.CreatedChats)
                {
                    createdChats.Add(userDbCreatingChats);
                }
            }

            if (user.Chats != null)
            {
                foreach (var userChat in user.Chats)
                {
                    chats.Add(userChat);
                }
            }

            if (user.CreatedChats != null)
            {
                foreach (var userCreatingChats in user.CreatedChats)
                {
                    createdChats.Add(userCreatingChats);
                }
            }

            user.FirstName = user.FirstName ?? userDb.FirstName;
            user.LastName = user.LastName ?? userDb.LastName;
            user.EmailAddress = user.EmailAddress ?? userDb.EmailAddress;
            user.TelephoneNumber = user.TelephoneNumber ?? userDb.TelephoneNumber;
            user.Photo = user.Photo ?? userDb.Photo;
            user.BytePhoto = user.BytePhoto ?? userDb.BytePhoto;
            user.CreatedChats = createdChats;
            user.Chats = chats;

            await _db.Users.ReplaceOneAsync(new BsonDocument("_id", user.Id), user);

            return user;
        }

        public async Task<UserDTO> UpdateUserStatus(string emailAddress, UserStatus userStatus)
        {
            var userDb = await _db.Users.Find(us => us.EmailAddress == emailAddress).FirstOrDefaultAsync();

            userDb.UserStatus = userStatus;

            await _db.Users.ReplaceOneAsync(new BsonDocument("_id", userDb.Id), userDb);

            return userDb;
        }
    }
}
