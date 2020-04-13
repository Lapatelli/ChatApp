using AutoMapper;
using ChatApp.API.ViewModels.User;
using ChatApp.Core.Entities;
using System;
using System.Collections.Generic;

namespace ChatApp.API.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
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
