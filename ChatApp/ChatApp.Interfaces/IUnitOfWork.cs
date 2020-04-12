﻿using ChatApp.Interfaces.Repositories;
using System;

namespace ChatApp.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository UserRepository { get; set; }

        public IChatRepository ChatRepository { get; set; }

        //Task<int> CommitAsync();
    }
}
