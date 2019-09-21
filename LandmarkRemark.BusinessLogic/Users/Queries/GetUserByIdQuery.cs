using MediatR;

namespace LandmarkRemark.BusinessLogic.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDetailModel>
    {
        public int Id { get; set; }
    }
}