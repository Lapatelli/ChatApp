using ChatApp.Core.Enums;

namespace ChatApp.API.ViewModels.User
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public UserStatus UserStatus { get; set; }
        public byte[] BytePhoto { get; set; }
    }
}
