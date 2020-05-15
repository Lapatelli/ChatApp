using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Users.Queries
{
    public class GetUserByNameHandler : IRequestHandler<GetUserByNameQuery, IEnumerable<User>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByNameHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> Handle(GetUserByNameQuery query, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.UserRepository.SearchUserByName(query.Name);
            var result = _mapper.Map<IEnumerable<UserDTO>, IEnumerable<User>>(users);

            return result;
        }
    }
}
