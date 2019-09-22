using System.Linq;
using System.Threading;
using LandmarkRemark.BusinessLogic.Exceptions;
using LandmarkRemark.BusinessLogic.Tests.Infrastructure;
using LandmarkRemark.BusinessLogic.Users.Commands.CreateUser;
using LandmarkRemark.BusinessLogic.Users.Queries;
using Shouldly;
using Xunit;

namespace LandmarkRemark.BusinessLogic.Tests.Users.Commands
{
    public class WhenUserIsCreatedWithoutFirstName : IClassFixture<TestBaseFixture>
    {
        private readonly TestBaseFixture _fixture;

        public WhenUserIsCreatedWithoutFirstName(TestBaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void UserIsNotCreatedAndExceptionWithDetailsIsThrown()
        {
            UserDetailModel createdUser;
            UnProcessableEntityException couldNotCreateUserException = null;
            EntityNotFoundException createdUserNotFoundException = null;
            try
            {
                var command = new CreateUserCommand
                {
                    LastName = "Polo",
                    Password = "marcopolo",
                    Username = "marco.polo"
                };
                var createdUserId = new CreateUserCommandHandler(_fixture.LandmarkContext,new FakeDatetimeProvider()).Handle(command, CancellationToken.None).GetAwaiter().GetResult();
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

            couldNotCreateUserException.ShouldNotBeNull();
            couldNotCreateUserException.ModelStateErrors.Count().ShouldBe(1);
            var firstError = couldNotCreateUserException.ModelStateErrors.First();
            firstError.ShouldSatisfyAllConditions(
                () => { firstError.PropertyName.ShouldBe(nameof(CreateUserCommand.FirstName)); });
        }
    }
}