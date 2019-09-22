using LandmarkRemark.BusinessLogic.Users.Queries;
using MediatR;

namespace LandmarkRemark.BusinessLogic.Users.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<UserDetailModel>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}