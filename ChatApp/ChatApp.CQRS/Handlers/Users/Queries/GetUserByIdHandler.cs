using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Queries.Users;
using ChatApp.Interfaces;
using MediatR;
using MongoDB.Bson;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Queries.Users
{
    public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<User> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var idUser = ObjectId.Parse(query.Id);
            var user = await _unitOfWork.UserRepository.SearchUserById(idUser);

            var result = _mapper.Map<UserDTO, User>(user);

             return result;
        }
    }
}
