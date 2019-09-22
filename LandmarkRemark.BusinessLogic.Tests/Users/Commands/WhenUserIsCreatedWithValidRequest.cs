using System.Threading;
using LandmarkRemark.BusinessLogic.Exceptions;
using LandmarkRemark.BusinessLogic.Users.Commands.CreateUser;
using LandmarkRemark.BusinessLogic.Users.Queries;
using Shouldly;
using Xunit;

namespace LandmarkRemark.BusinessLogic.Tests.Users.Commands
{
    public class WhenUserIsCreatedWithValidRequest : IClassFixture<TestBaseFixture>
    {
      
        private readonly TestBaseFixture _fixture;

        public WhenUserIsCreatedWithValidRequest(TestBaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void UserIsCreatedWithCorrectInformation()
        {
            UserDetailModel createdUser=null;
            UnProcessableEntityException couldNotCreateUserException = null;
            EntityNotFoundException createdUserNotFoundException = null;
            
            var command = new CreateUserCommand
            {
                FirstName = "Marco",
                LastName = "Polo",
                Password = "marcopolo",
                Username = "marco.polo"
            };
            
            try
            {
              
                var createdUserId = new CreateUserCommandHandler(_fixture.LandmarkContext).Handle(command, CancellationToken.None).GetAwaiter().GetResult();
                createdUser = new GetUserByIdQueryHandler(_fixture.LandmarkContext).Handle(new GetUserByIdQuery {Id = createdUserId}, CancellationToken.None).GetAwaiter()
                    .GetResult();
            }
            catch (UnProcessableEntityException ex)
            {
                couldNotCreateUserException = ex;
            }
            catch (EntityNotFoundException ex)
            {
                createdUserNotFoundException = ex;
            }
            
            couldNotCreateUserException.ShouldBeNull();
            createdUserNotFoundException.ShouldBeNull();
            createdUser.ShouldSatisfyAllConditions(
                () => createdUser.FirstName.ShouldBe("Marco"),
                () => createdUser.LastName.ShouldBe("Polo"),
                () => createdUser.UserName.ShouldBe("marco.polo")
            );
        }


      
    }
}