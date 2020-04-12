using ChatApp.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<IEnumerable<User>> SearchUserByName(string name);

        Task<User> SearchUserById(string id);
    }
}
