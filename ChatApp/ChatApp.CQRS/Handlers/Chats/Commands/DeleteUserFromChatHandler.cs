using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Chats;
using ChatApp.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Chats.Commands
{
    public class DeleteUserFromChatHandler : IRequestHandler<DeleteUserFromChatCommand, Chat>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteUserFromChatHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Chat> Handle(DeleteUserFromChatCommand command, CancellationToken cancellationToken)
        {
            _unitOfWork.ChatRepository.DeleteUserFromChatAsync(command.ChatId, command.UserId);
            _unitOfWork.UserRepository.UpdateUserChats(command.UserId, command.ChatId, false, false);
            await _unitOfWork.CommitAsync();

            var chatWithUsersDTO = await _unitOfWork.ChatRepository.GetAggregateChatWithUsers(command.ChatId);

            var chatUsers = _mapper.Map<IEnumerable<UserDTO>, IEnumerable<User>>(chatWithUsersDTO.ChatUsers);
            var result = _mapper.Map<(ChatWithUsersDTO, IEnumerable<User>), Chat>((chatWithUsersDTO, chatUsers));

            return result;
        }
    }
}
