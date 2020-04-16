using MediatR;

namespace ChatApp.CQRS.Queries.Users
{
    public class GetUserByEmailQuery : IRequest<bool>
    {
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}
