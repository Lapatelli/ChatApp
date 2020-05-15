using AutoMapper;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Chats;
using ChatApp.CQRS.Shared;
using ChatApp.Interfaces;
using MediatR;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Chats.Commands
{
    public class CreateChatHandler : IRequestHandler<CreateChatCommand, Chat>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateChatHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Chat> Handle(CreateChatCommand command, CancellationToken cancellationToken)
        {
            List<ObjectId> chatUsersObjectId = new List<ObjectId>();

            foreach (var chatUserObjectId in command.ChatUsers)
            {
                chatUsersObjectId.Add(ObjectId.Parse(chatUserObjectId));
            }

            var picture = command.Picture != null ? ImageConvertion.PictureToByteArray(command.Picture): null;
            var chat = _mapper.Map<(CreateChatCommand, List<ObjectId>, byte[]), ChatDTO>((command, chatUsersObjectId, picture));

            _unitOfWork.ChatRepository.CreateChat(chat);
            _unitOfWork.UserRepository.UpdateUserChats(chat.CreatedByUserId, chat.Id, true, true);

            foreach (var chatUser in chat.ChatUsersId)
            {
                _unitOfWork.UserRepository.UpdateUserChats(chatUser, chat.Id, false, true);
            }

            await _unitOfWork.CommitAsync();

            var result = await _unitOfWork.ChatRepository.GetAggregateChatWithUsers(chat);

            return result;
        }
    }
}
