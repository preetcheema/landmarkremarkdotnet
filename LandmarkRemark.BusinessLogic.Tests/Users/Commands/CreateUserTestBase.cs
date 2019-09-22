using System;
using System.Threading;
using LandmarkRemark.BusinessLogic.Exceptions;
using LandmarkRemark.BusinessLogic.Users.Commands.CreateUser;
using LandmarkRemark.BusinessLogic.Users.Queries;
using Xunit;

namespace LandmarkRemark.BusinessLogic.Tests.Users.Commands
{
    public abstract class CreateUserTestBase : IClassFixture<TestBaseFixture>
    {
        private readonly TestBaseFixture _fixture;
        public UnProcessableEntityException CouldNotCreateUserException { get; set; }
        public UserDetailModel Createduser { get; set; }

        public EntityNotFoundException CreatedUserNotFoundException { get; set; }

        public CreateUserTestBase(TestBaseFixture fixture)
        {
            _fixture = fixture;
            try
            {
                var command = GetCommand();
                var createdUserId = new CreateUserCommandHandler(_fixture.LandmarkContext).Handle(command, CancellationToken.None).GetAwaiter().GetResult();
                Createduser = new GetUserByIdQueryHandler(_fixture.LandmarkContext).Handle(new GetUserByIdQuery {Id = createdUserId}, CancellationToken.None).GetAwaiter()
                    .GetResult();
            }
            catch (UnProcessableEntityException ex)
            {
                CouldNotCreateUserException = ex;
            }
            catch (EntityNotFoundException ex)
            {
                CreatedUserNotFoundException = ex;
            }
        }

        public abstract CreateUserCommand GetCommand();
    }
}