using AutoMapper;
using ChatApp.API.ViewModels.Chat;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Chats;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace ChatApp.API.Mapper
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<(CreateChatViewModel model, string creator), CreateChatCommand>()
                .ConvertUsing(src => new CreateChatCommand
                {
                    Name = src.model.Name,
                    Password = src.model.Password,
                    CreatedAt = DateTime.Now,
                    CreatedByUser = src.creator,
                    ChatPrivacy = src.model.ChatPrivacy,
                    ChatUsers = src.model.ChatUsers
                });

            CreateMap<Chat, ChatViewModel>()
                .ConvertUsing(src => new ChatViewModel
                {
                    Id = src.Id,
                    Name = src.Name,
                    Password = src.Password,
                    CreatedBy = src.CreatedByUser,
                    ChatUsers = src.ChatUsers
                });

            CreateMap<(ChatDTO chat, User user, List<User> chatUsers), Chat>()
                .ConvertUsing(src => new Chat
                {
                    Id = src.chat.Id.ToString(),
                    Name = src.chat.Name,
                    Password = src.chat.Password,
                    CreatedAt = src.chat.CreatedAt,
                    CreatedByUser = src.user,
                    ChatPrivacy = src.chat.ChatPrivacy,
                    ChatUsers = src.chatUsers
                });

            CreateMap<(CreateChatCommand command, List<ObjectId> list), ChatDTO>()
                .ConvertUsing(src => new ChatDTO
                {
                    Name = src.command.Name,
                    Password = src.command.Password,
                    CreatedAt = src.command.CreatedAt,
                    CreatedByUser = ObjectId.Parse(src.command.CreatedByUser),
                    ChatPrivacy = src.command.ChatPrivacy,
                    ChatUsers = src.list
                });

            CreateMap<UpdateChatCommand, ChatDTO>()
                .ConvertUsing(src => new ChatDTO
                {
                    Id = src.Id,
                    Name = src.Name,
                    Password = src.Password,
                    ChatPrivacy = src.ChatPrivacy
                });

            CreateMap<(DeleteUserFromChatViewModel model, string chatId), DeleteUserFromChatCommand>()
                .ConvertUsing(src => new DeleteUserFromChatCommand
                {
                    ChatId = ObjectId.Parse(src.chatId),
                    UserId = ObjectId.Parse(src.model.UserId)
                });

            CreateMap<(AddUserToChatViewModel model, string chatId), AddUserToChatCommand>()
                .ConvertUsing(src => new AddUserToChatCommand
                {
                    ChatId = ObjectId.Parse(src.chatId),
                    UserId = ObjectId.Parse(src.model.UserId)
                });

            CreateMap<(UpdateChatViewModel model, string chatId), UpdateChatCommand>()
                .ConvertUsing(src => new UpdateChatCommand
                {
                    Id = ObjectId.Parse(src.chatId),
                    Name = src.model.Name,
                    Password = src.model.Password,
                    ChatPrivacy = src.model.ChatPrivacy
                });
        }
    }
}
