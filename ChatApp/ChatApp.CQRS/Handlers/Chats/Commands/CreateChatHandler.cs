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
            var picture = command.Picture != null ? ImageConvertion.PictureToByteArray(command.Picture) : null;
            var chatUsersObjectId = new List<ObjectId>();
            ChatDTO chat;

            if (command.ChatUsers[0] != null)
            {
                foreach (var chatUserObjectId in command.ChatUsers)
                {
                    chatUsersObjectId.Add(ObjectId.Parse(chatUserObjectId));
                }

                chat = _mapper.Map<(CreateChatCommand, List<ObjectId>, byte[]), ChatDTO>((command, chatUsersObjectId, picture));

                foreach (var chatUser in chat.ChatUsersId)
                {
                    _unitOfWork.UserRepository.UpdateUserChats(chatUser, chat.Id, false, true);
                }
            }
            else
            {
                chat = _mapper.Map<(CreateChatCommand, byte[]), ChatDTO>((command, picture));
            }

            _unitOfWork.ChatRepository.CreateChat(chat);
            _unitOfWork.UserRepository.UpdateUserChats(chat.CreatedByUserId, chat.Id, true, true);

            await _unitOfWork.CommitAsync();

            var chatWithUsersDTO = await _unitOfWork.ChatRepository.GetAggregateChatWithUsers(chat);

            var chatUsers = _mapper.Map<IEnumerable<UserDTO>, IEnumerable<User>>(chatWithUsersDTO.ChatUsers);
            var result = _mapper.Map<(ChatWithUsersDTO, IEnumerable<User>), Chat>((chatWithUsersDTO, chatUsers));

            return result;
        }
    }
}
