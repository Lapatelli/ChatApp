using ChatApp.Core.DTO;
using System.Threading.Tasks;

namespace ChatApp.Interfaces.Repositories
{
    public interface IChatRepository
    {
        Task<ChatDTO> SearchChatByName(string name);

        Task<ChatDTO> CreateChatAsync(ChatDTO chat);
    }
}
