using LandmarkRemark.Domain;
using LandmarkRemark.Domain.Entities;

namespace LandmarkRemark.BusinessLogic.Users.Queries
{
    public class UserDetailModel
    {
        public string UserName { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public int Id { get; set; }
        
        public static UserDetailModel Create(User user)
        {
            return new UserDetailModel
            {
                Id=user.Id,
                FirstName=user.FirstName,
                LastName=user.LastName,
                UserName=user.UserName
            };
        }
    }
}