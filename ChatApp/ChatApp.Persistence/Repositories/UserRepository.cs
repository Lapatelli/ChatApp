﻿using ChatApp.Core.DTO;
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

        public async Task<UserDTO> UpdateUser(UserDTO user)
        {
            var userDb = await SearchUserById(user.Id);

            user.Chats = userDb.Chats;
            user.UserStatus = userDb.UserStatus;
            user.CreatedChats = userDb.CreatedChats;

            await _db.Users.ReplaceOneAsync(new BsonDocument("_id", user.Id), user);

            return user;
        }
    }
}
