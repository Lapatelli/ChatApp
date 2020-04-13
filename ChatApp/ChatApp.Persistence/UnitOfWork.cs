using ChatApp.Interfaces;
using ChatApp.Interfaces.Repositories;
using ChatApp.Persistence.Context;
using System;

namespace ChatApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatDbContext _context;
        private bool disposed;

        public UnitOfWork(IUserRepository userRepository, IChatRepository chatRepository, ChatDbContext context)
        {
            UserRepository = userRepository;
            ChatRepository = chatRepository;
            _context = context;
        }

        public IUserRepository UserRepository { get; set; }
        public IChatRepository ChatRepository { get; set; }

        //public async Task<int> CommitAsync()
        //{
        //}

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        //public virtual void Dispose(bool disposing)
        //{
        //    if (!disposed && disposing)
        //    {
        //        _context.Dispose();
        //    }
        //    disposed = true;
        //}
    }
}
