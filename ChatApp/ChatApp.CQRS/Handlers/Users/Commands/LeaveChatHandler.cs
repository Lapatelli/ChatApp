using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Users;
using ChatApp.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Users.Commands
{
    public class LeaveChatHandler : IRequestHandler<LeaveChatCommand, User>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LeaveChatHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User> Handle(LeaveChatCommand command, CancellationToken cancellationToken)
        {
            _unitOfWork.UserRepository.LeaveChat(command.UserId, command.ChatId);
            _unitOfWork.ChatRepository.DeleteUserFromChatAsync(command.ChatId, command.UserId);
            await _unitOfWork.CommitAsync();

            var user = await _unitOfWork.UserRepository.SearchUserById(command.UserId);
            var result = _mapper.Map<UserDTO, User>(user);

            return result;
        }
    }
}
