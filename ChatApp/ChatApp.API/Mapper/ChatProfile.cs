using AutoMapper;
using ChatApp.API.ViewModels.Chat;
using ChatApp.API.ViewModels.User;
using ChatApp.Core.Entities;
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

            CreateMap<User, GetUserViewModel>()
                .ConvertUsing(src => new GetUserViewModel
                {
                    Id = src.Id.ToString(),
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    EmailAddress = src.EmailAddress,
                    TelephoneNumber = src.TelephoneNumber,
                    UserStatus = src.UserStatus
                });
        }
    }
}
