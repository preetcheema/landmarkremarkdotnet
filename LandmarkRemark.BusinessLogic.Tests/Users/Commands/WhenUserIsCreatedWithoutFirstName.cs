using System.Linq;
using LandmarkRemark.BusinessLogic.Users.Commands.CreateUser;
using Shouldly;
using Xunit;

namespace LandmarkRemark.BusinessLogic.Tests.Users.Commands
{
    public class WhenUserIsCreatedWithoutFirstName : CreateUserTestBase
    {
        public WhenUserIsCreatedWithoutFirstName(TestBaseFixture fixture) : base(fixture)
        {
        }

        public override CreateUserCommand GetCommand()
        {
            var command = new CreateUserCommand
            {
                LastName = "Polo",
                Password = "marcopolo",
                Username = "marco.polo"
            };
            return command;
        }

        [Fact]
        public void UserIsNotCreatedAndExceptionWithDetailsIsThrown()
        {
            CouldNotCreateUserException.ShouldNotBeNull();
            CouldNotCreateUserException.ModelStateErrors.Count().ShouldBe(1);
            var firstError = CouldNotCreateUserException.ModelStateErrors.First();
            firstError.ShouldSatisfyAllConditions(
                () => { firstError.PropertyName.ShouldBe(nameof(CreateUserCommand.FirstName)); });
        }
    }
}