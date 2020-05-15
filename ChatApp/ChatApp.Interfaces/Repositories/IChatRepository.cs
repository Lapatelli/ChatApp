using ChatApp.Core.DTO;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;

namespace ChatApp.Interfaces.Repositories
{
    public interface IChatRepository : IDisposable
    {
        Task<ChatDTO> GetChatById(ObjectId chatId);

        Task<ChatWithUsersDTO> GetAggregateChatWithUsers(ChatDTO chat);

        Task<ChatWithUsersDTO> GetAggregateChatWithUsers(ObjectId chatId);

        Task<ChatWithUsersDTO> SearchChatByName(string name);

        void CreateChat(ChatDTO chat);

        void UpdateChat(ChatDTO chat);

        void DeleteChatAsync(ObjectId chatId);

        void AddUserToChatAsync(ObjectId chatId, ObjectId userId);

        void DeleteUserFromChatAsync(ObjectId chatId, ObjectId userId);
    }
}
