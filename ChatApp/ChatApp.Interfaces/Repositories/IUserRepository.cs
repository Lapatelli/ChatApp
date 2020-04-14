using ChatApp.Core.DTO;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();

        Task<IEnumerable<UserDTO>> SearchUserByName(string name);

        Task<UserDTO> SearchUserById(ObjectId id);

        Task<UserDTO> UpdateUser(UserDTO user);
    }
}
