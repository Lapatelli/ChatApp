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
    public class UpdateChatHandler : IRequestHandler<UpdateChatCommand, Chat>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateChatHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Chat> Handle(UpdateChatCommand command, CancellationToken cancellationToken)
        {
            var picture = command.Picture != null ? ImageConvertion.PictureToByteArray(command.Picture) : null;
            ChatDTO chatUpdate;

            //[FromForm] always make command.ChatUsers.Count = 1 (if even we don't give any info or long list new ChatUsers)
            if (command.ChatUsers[0] != null)
            {
               var chatUsersObjectId = new List<ObjectId>();

                foreach (var chatUserObjectId in command.ChatUsers)
                {
                    chatUsersObjectId.Add(ObjectId.Parse(chatUserObjectId));
                }

                chatUpdate = _mapper.Map<(UpdateChatCommand, List<ObjectId>, byte[]), ChatDTO>((command, chatUsersObjectId, picture));

                foreach (var chatUser in chatUpdate.ChatUsersId)
                {
                    _unitOfWork.UserRepository.UpdateUserChats(chatUser, chatUpdate.Id, false, true);
                }
            }
            else
            {
                chatUpdate = _mapper.Map<(UpdateChatCommand, byte[]), ChatDTO>((command, picture));
            }

            _unitOfWork.ChatRepository.UpdateChat(chatUpdate);
            await _unitOfWork.CommitAsync();

            var chatWithUsersDTO = await _unitOfWork.ChatRepository.GetAggregateChatWithUsers(chatUpdate);

            var chatUsers = _mapper.Map<IEnumerable<UserDTO>, IEnumerable<User>>(chatWithUsersDTO.ChatUsers);
            var result = _mapper.Map<(ChatWithUsersDTO, IEnumerable<User>), Chat>((chatWithUsersDTO, chatUsers));

            return result;
        }
    }
}
