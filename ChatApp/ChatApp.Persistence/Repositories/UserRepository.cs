using ChatApp.Core.Entities;
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

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _db.Users.AsQueryable().ToListAsync();
        }

        public async Task<IEnumerable<User>> SearchUserByName(string name)
        {
            return await _db.Users.Find(us => us.FirstName == name).ToListAsync();
        }

        public async Task<User> SearchUserById(string id)
        {
            var idUser = ObjectId.Parse(id);
            return await _db.Users.Find(us => us.Id == idUser).FirstOrDefaultAsync();
        }
    }
}
