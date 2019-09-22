using LandmarkRemark.BusinessLogic.Users.Commands.CreateUser;
using Shouldly;
using Xunit;

namespace LandmarkRemark.BusinessLogic.Tests.Users.Commands
{
    public class WhenUserIsCreatedWithValidRequest : CreateUserTestBase
    {
        private readonly CreateUserTestBase _fixture;

        public WhenUserIsCreatedWithValidRequest(TestBaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void UserIsCreatedWithCorrectInformation()
        {
            CouldNotCreateUserException.ShouldBeNull();
            CreatedUserNotFoundException.ShouldBeNull();
            Createduser.ShouldSatisfyAllConditions(
                () => Createduser.FirstName.ShouldBe("Marco"),
                () => Createduser.LastName.ShouldBe("Polo"),
                () => Createduser.UserName.ShouldBe("marco.polo")
            );
        }


        public override CreateUserCommand GetCommand()
        {
            var command = new CreateUserCommand
            {
                FirstName = "Marco",
                LastName = "Polo",
                Password = "marcopolo",
                Username = "marco.polo"
            };
            return command;
        }
    }
}