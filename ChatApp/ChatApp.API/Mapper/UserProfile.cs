using AutoMapper;
using ChatApp.API.ViewModels.Chat;
using ChatApp.API.ViewModels.User;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Users;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ChatApp.API.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<(UpdateUserViewModel model, string userId), UpdateUserCommand>()
                .ConvertUsing(src => new UpdateUserCommand
                {
                    Id = ObjectId.Parse(src.userId),
                    FirstName = src.model.FirstName,
                    LastName = src.model.LastName,
                    EmailAddress = src.model.EmailAddress,
                    Photo = src.model.Photo
                });

            CreateMap<RegisterUserCommand, UserDTO>()
                .ConvertUsing(src => new UserDTO
                {
                    Id = src.Id,
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    EmailAddress = src.EmailAddress,
                    Photo = src.Photo,
                    CreatedChatsId = src.CreatedChatsId,
                    ChatsId = src.ChatsId
                });

            CreateMap<(UpdateUserCommand command, byte[] photo), UserDTO>()
                .ConvertUsing(src => new UserDTO
                {
                    Id = src.command.Id,
                    FirstName = src.command.FirstName,
                    LastName = src.command.LastName,
                    EmailAddress = src.command.EmailAddress,
                    BytePhoto = src.photo
                });

            CreateMap<UserDTO, User>()
                .ConvertUsing(src => new User
                {
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    EmailAddress = src.EmailAddress,
                    UserStatus = src.UserStatus,
                    BytePhoto = src.BytePhoto
                });

            CreateMap<User, UserViewModel>()
                .ConvertUsing(src => new UserViewModel
                {
                    Id = src.Id.ToString(),
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    EmailAddress = src.EmailAddress,
                    UserStatus = src.UserStatus,
                    //BytePhoto = src.BytePhoto
                 });

            CreateMap<(User user, IEnumerable<ChatInfoViewModel> chats), UserViewModel>()
                .ConvertUsing(src => new UserViewModel
                {
                    Id = src.user.Id.ToString(),
                    FirstName = src.user.FirstName,
                    LastName = src.user.LastName,
                    EmailAddress = src.user.EmailAddress,
                    UserStatus = src.user.UserStatus,
                    Chats = src.chats
                    //BytePhoto = src.BytePhoto
                });

            CreateMap<UserDTO, UserInfoViewModel>()
                .ConvertUsing(src => new UserInfoViewModel
                {
                    Id = src.Id.ToString(),
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    EmailAddress = src.EmailAddress,
                    UserStatus = src.UserStatus,
                    BytePhoto = src.BytePhoto
                });

        }
    }
}
