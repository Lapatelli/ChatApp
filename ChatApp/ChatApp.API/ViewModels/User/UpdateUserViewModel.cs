﻿using System.Collections.Generic;

namespace ChatApp.API.ViewModels.User
{
    public class UpdateUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string TelephoneNumber { get; set; }
    }
}