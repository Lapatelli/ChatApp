using MediatR;

namespace ChatApp.CQRS.Queries.Users
{
    public class IsUserExistsQuery : IRequest<bool>
    {
        public IsUserExistsQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}
