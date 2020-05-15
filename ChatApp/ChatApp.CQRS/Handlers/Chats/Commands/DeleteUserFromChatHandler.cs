using AutoMapper;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Chats;
using ChatApp.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Chats.Commands
{
    public class DeleteUserFromChatHandler : IRequestHandler<DeleteUserFromChatCommand, Chat>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserFromChatHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Chat> Handle(DeleteUserFromChatCommand command, CancellationToken cancellationToken)
        {
            _unitOfWork.ChatRepository.DeleteUserFromChatAsync(command.ChatId, command.UserId);
            _unitOfWork.UserRepository.UpdateUserChats(command.UserId, command.ChatId, false, false);
            await _unitOfWork.CommitAsync();

            var result = await _unitOfWork.ChatRepository.GetAggregateChatWithUsers(command.ChatId);

            return result;
        }
    }
}
