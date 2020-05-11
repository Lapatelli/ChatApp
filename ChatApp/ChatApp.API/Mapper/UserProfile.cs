using AutoMapper;
using ChatApp.API.ViewModels.User;
using ChatApp.Core.DTO;
using ChatApp.Core.Entities;
using ChatApp.CQRS.Commands.Users;
using MongoDB.Bson;

namespace ChatApp.API.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>()
                .ConvertUsing(src => new UserViewModel
                {
                    Id = src.Id.ToString(),
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    EmailAddress = src.EmailAddress,
                    TelephoneNumber = src.TelephoneNumber,
                    UserStatus = src.UserStatus,
                    Photo = src.Photo
                });

            CreateMap<UserDTO, User>()
                .ConvertUsing(src => new User
                {
                    Id = src.Id.ToString(),
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    TelephoneNumber = src.TelephoneNumber,
                    EmailAddress = src.EmailAddress,
                    UserStatus = src.UserStatus,
                    Photo = src.Photo 
                });

            CreateMap<(UpdateUserViewModel model, string userId), UpdateUserCommand>()
                .ConvertUsing(src => new UpdateUserCommand
                {
                    Id = ObjectId.Parse(src.userId),
                    FirstName = src.model.FirstName,
                    LastName = src.model.LastName,
                    TelephoneNumber = src.model.TelephoneNumber,
                    EmailAddress = src.model.EmailAddress
                });

            CreateMap<UpdateUserCommand, UserDTO>()
                .ConvertUsing(src => new UserDTO
                {
                    Id = src.Id,
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    TelephoneNumber = src.TelephoneNumber,
                    EmailAddress = src.EmailAddress,
                    CreatedChats = src.CreatedChats,
                    Chats = src.Chats
                });

            CreateMap<RegisterUserCommand, UserDTO>()
                .ConvertUsing(src => new UserDTO
                {
                    Id = src.Id,
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    TelephoneNumber = src.TelephoneNumber,
                    EmailAddress = src.EmailAddress,
                    Photo = src.Photo
                });
        }
    }
}
