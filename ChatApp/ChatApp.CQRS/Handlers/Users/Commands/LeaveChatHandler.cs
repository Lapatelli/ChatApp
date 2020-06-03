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
            _unitOfWork.UserRepository.LeaveChat(command.Email, command.ChatId);
            var user = await _unitOfWork.UserRepository.SearchUserByEmail(command.Email);
            _unitOfWork.ChatRepository.DeleteUserFromChatAsync(command.ChatId, user.Id);

            await _unitOfWork.CommitAsync();

            var userUpdated = await _unitOfWork.UserRepository.SearchUserByEmail(command.Email);
            var result = _mapper.Map<UserDTO, User>(userUpdated);

            return result;
        }
    }
}
