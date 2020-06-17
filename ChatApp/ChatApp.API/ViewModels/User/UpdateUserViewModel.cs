
using Microsoft.AspNetCore.Http;

namespace ChatApp.API.ViewModels.User
{
    public class UpdateUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile Photo { get; set; }
    }
}
