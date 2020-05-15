using AutoMapper;
using ChatApp.API.ViewModels.Chat;
using ChatApp.API.ViewModels.User;
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
                    Id = ObjectId.GenerateNewId(),
                    Name = src.model.Name,
                    Password = src.model.Password,
                    CreatedAt = DateTime.Now,
                    CreatedByUser = src.creator,
                    ChatPrivacy = src.model.ChatPrivacy,
                    ChatUsers = src.model.ChatUsers,
                    Picture =src.model.Picture
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
                    ChatPrivacy = src.model.ChatPrivacy,
                    ChatUsers = src.model.ChatUsers,
                    Picture = src.model.Picture
                });

            CreateMap<(CreateChatCommand command, List<ObjectId> chatUsers, byte[] picture), ChatDTO>()
                .ConvertUsing(src => new ChatDTO
                {
                    Id = src.command.Id,
                    Name = src.command.Name,
                    Password = src.command.Password,
                    CreatedAt = src.command.CreatedAt,
                    CreatedByUserId = ObjectId.Parse(src.command.CreatedByUser),
                    ChatPrivacy = src.command.ChatPrivacy,
                    ChatUsersId = src.chatUsers,
                    Picture = src.picture
                });

            CreateMap<(UpdateChatCommand command, List<ObjectId> chatUsers, byte[] picture), ChatDTO>()
                .ConvertUsing(src => new ChatDTO
                {
                    Id = src.command.Id,
                    Name = src.command.Name,
                    Password = src.command.Password,
                    ChatPrivacy = src.command.ChatPrivacy,
                    ChatUsersId = src.chatUsers,
                    Picture = src.picture
                });

            CreateMap<(UpdateChatCommand command, byte[] picture), ChatDTO>()
                .ConvertUsing(src => new ChatDTO
                {
                    Id = src.command.Id,
                    Name = src.command.Name,
                    Password = src.command.Password,
                    ChatPrivacy = src.command.ChatPrivacy,
                    Picture = src.picture
                });

            CreateMap<(Chat chat, IEnumerable<UserInfoViewModel> chatUsers), ChatViewModel>()
                .ConvertUsing(src => new ChatViewModel
                {
                    Id = src.chat.Id.ToString(),
                    Name = src.chat.Name,
                    Password = src.chat.Password,
                    ChatPrivacy = src.chat.ChatPrivacy,
                    Picture = src.chat.Picture,
                    ChatUsers = src.chatUsers,
                });

            CreateMap<ChatDTO, ChatInfoViewModel>()
                .ConvertUsing(src => new ChatInfoViewModel
                {
                    Id = src.Id.ToString(),
                    Name = src.Name,
                    //Password = src.Password,
                    ChatPrivacy = src.ChatPrivacy,
                    //Picture = src.Picture
                });
        }
    }
}
