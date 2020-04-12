using MongoDB.Driver;
using ChatApp.Core.Entities;

namespace ChatApp.Persistence.Context
{
    public class ChatDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public ChatDbContext(IMongoClient mongoClient, string dbConnection)
        {
            _mongoDatabase = mongoClient.GetDatabase(dbConnection);
        }

        public IMongoCollection<User> Users
        {
            get
            {
                //ensure that this Collection exists
                return _mongoDatabase.GetCollection<User>("Users");
            }
        }

        public virtual IMongoCollection<Chat> Chats
        {
            get
            {
                //ensure that this Collection exists
                return _mongoDatabase.GetCollection<Chat>("Chats");
            }
        }

    }
}
