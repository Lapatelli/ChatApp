using AutoMapper;
using ChatApp.API.ViewModels.Chat;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Chats;
using System;
using System.Collections.Generic;

namespace ChatApp.API.Mapper
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<CreateChatViewModel, Chat>()
                .ConvertUsing(src => new Chat
                {
                    Name = src.Name,
                    Password = src.Password,
                    CreatedAt = DateTime.Now,
                    ChatPrivacy = src.ChatPrivacy
                });

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

            CreateMap<CreateChatCommand, Chat>()
                .ConvertUsing(src => new Chat
                {
                    Name = src.Name,
                    Password = src.Password,
                    CreatedAt = src.CreatedAt,
                    ChatPrivacy = src.ChatPrivacy
                });

            CreateMap<Chat, GetChatViewModel>()
                .ConvertUsing(src => new GetChatViewModel
                {
                    Id = src.Id.ToString(),
                    Name = src.Name,
                    Password = src.Password,
                });

            CreateMap<(Chat chat, User user, IEnumerable<User> chatUsers), GetChatViewModel>()
                .ConvertUsing(src => new GetChatViewModel
                {
                    Id = src.chat.Id.ToString(),
                    Name = src.chat.Name,
                    Password = src.chat.Password,
                    CreatedBy = src.user,
                    ChatUsers = src.chatUsers
                });
        }
    }
}
