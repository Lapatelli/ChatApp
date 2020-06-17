using ChatApp.Core.DTO;
using ChatApp.Core.Enums;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Interfaces.Repositories
{
    public interface IUserRepository : IDisposable
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();

        Task<UserWithChatsDTO> GetAllChatsForUser(string  email);

        Task<IEnumerable<UserDTO>> SearchUserByName(string name);

        Task<UserDTO> SearchUserById(ObjectId id);

        Task<UserDTO> SearchUserByEmail(string email);

        Task<bool> IsUserExist(string email);

        void CreateUser(UserDTO user, byte[] photo);

        void UpdateUser(UserDTO user);

        void UpdateUserChats(ObjectId userId, ObjectId chatId, bool isChatCreator, bool isUserAddedToChat);

        void UpdateUserStatus(string email, UserStatus userStatus);

        void LeaveChat(string email, ObjectId chatId);
    }
}
