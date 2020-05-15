using MongoDB.Driver;
using ChatApp.Core.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;

namespace ChatApp.Persistence.Context
{
    public class ChatDbContext : IDisposable
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly List<Func<Task>> _commands;
        public IMongoClient MongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }

        public ChatDbContext(IMongoClient mongoClient, string dbConnection)
        {
            MongoClient = mongoClient;
            _mongoDatabase = mongoClient.GetDatabase(dbConnection);
            _commands = new List<Func<Task>>();
        }

        public IMongoCollection<UserDTO> Users
        {
            get
            {
                return _mongoDatabase.GetCollection<UserDTO>("Users");
            }
        }

        public virtual IMongoCollection<ChatDTO> Chats
        {
            get
            {
                return _mongoDatabase.GetCollection<ChatDTO>("Chats");
            }
        }

        public async Task SaveChanges()
        {
            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

             _commands.Clear();
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
