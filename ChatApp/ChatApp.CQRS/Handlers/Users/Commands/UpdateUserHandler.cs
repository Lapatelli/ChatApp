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
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var userUpdateDTO = _mapper.Map<UpdateUserCommand, UserDTO>(command);
            var user = await _unitOfWork.UserRepository.UpdateUser(userUpdateDTO);

            var result = _mapper.Map<UserDTO, User>(user);

            return result;
        }
    }
}
