using ChatApp.Interfaces;
using ChatApp.Interfaces.Repositories;
using ChatApp.Persistence.Context;
using System.Threading.Tasks;

namespace ChatApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatDbContext _context;

        public UnitOfWork(IUserRepository userRepository, IChatRepository chatRepository, ChatDbContext context)
        {
            UserRepository = userRepository;
            ChatRepository = chatRepository;
            _context = context;
        }

        public IUserRepository UserRepository { get; set; }
        public IChatRepository ChatRepository { get; set; }

        public Task<int> CommitAsync()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
