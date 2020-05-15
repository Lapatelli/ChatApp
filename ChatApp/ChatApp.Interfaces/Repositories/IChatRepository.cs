using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;

namespace ChatApp.Interfaces.Repositories
{
    public interface IChatRepository : IDisposable
    {
        Task<Chat> GetAggregateChatWithUsers(ChatDTO chat);

        Task<Chat> GetAggregateChatWithUsers(ObjectId chatId);

        Task<Chat> SearchChatByName(string name);

        void CreateChat(ChatDTO chat);

        void UpdateChat(ChatDTO chat);

        void DeleteChatAsync(ObjectId chatId);

        void AddUserToChatAsync(ObjectId chatId, ObjectId userId);

        void DeleteUserFromChatAsync(ObjectId chatId, ObjectId userId);
    }
}
