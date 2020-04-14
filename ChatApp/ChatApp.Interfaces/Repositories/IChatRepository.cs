using ChatApp.Core.DTO;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace ChatApp.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Task<ChatDTO> SearchChatByName(string name);

        Task<ChatDTO> CreateChatAsync(ChatDTO chat);

        Task DeleteChatAsync(ObjectId chatId);

        Task<ChatDTO> DeleteUserFromChatAsync(ObjectId chatId, ObjectId userId);

        Task<ChatDTO> AddUserToChatAsync(ObjectId chatId, ObjectId userId);

        Task<ChatDTO> UpdateChatAsync(ChatDTO chat);
    }
}
