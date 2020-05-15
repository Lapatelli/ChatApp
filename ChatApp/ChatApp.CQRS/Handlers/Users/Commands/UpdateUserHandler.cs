using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Users;
using ChatApp.CQRS.Shared;
using ChatApp.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Users.Commands
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<User> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var photo = command.Photo != null ? ImageConvertion.PictureToByteArray(command.Photo) : null;

            var userUpdateDTO = _mapper.Map<(UpdateUserCommand, byte[]), UserDTO>((command, photo));

            _unitOfWork.UserRepository.UpdateUser(userUpdateDTO);
            await _unitOfWork.CommitAsync();

            var user = await _unitOfWork.UserRepository.SearchUserById(command.Id);
            var result = _mapper.Map<UserDTO, User>(user);

            return result;
        }
    }
}
