using MongoDB.Driver;
using ChatApp.Core.DTO;

namespace ChatApp.Persistence.Context
{
    public class ChatDbContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public ChatDbContext(IMongoClient mongoClient, string dbConnection)
        {
            _mongoDatabase = mongoClient.GetDatabase(dbConnection);
        }

        public IMongoCollection<UserDTO> Users
        {
            get
            {
                //ensure that this Collection exists
                return _mongoDatabase.GetCollection<UserDTO>("Users");
            }
        }

        public virtual IMongoCollection<ChatDTO> Chats
        {
            get
            {
                //ensure that this Collection exists
                return _mongoDatabase.GetCollection<ChatDTO>("Chats");
            }
        }

    }
}
