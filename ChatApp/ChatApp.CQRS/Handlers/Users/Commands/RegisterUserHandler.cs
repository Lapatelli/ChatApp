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
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var userCreateDTO = _mapper.Map<RegisterUserCommand, UserDTO>(command);
            var user = await _unitOfWork.UserRepository.CreateUser(userCreateDTO);

            var result = _mapper.Map<UserDTO, User>(user);

            return result;
        }
    }
}
