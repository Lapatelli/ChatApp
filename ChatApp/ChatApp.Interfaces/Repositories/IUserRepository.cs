using ChatApp.Core.DTO;
using ChatApp.Core.Enums;
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

        Task<bool> IsUserExist(string email);

        Task<UserDTO> UpdateUserStatus(string emailAddress, UserStatus userStatus);

        Task<UserDTO> CreateUser(UserDTO user, byte[] photo);

        Task<UserDTO> UpdateUser(UserDTO user);
    }
}
