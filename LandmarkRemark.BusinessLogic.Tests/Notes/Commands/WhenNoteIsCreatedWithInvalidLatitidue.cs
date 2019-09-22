using System.Linq;
using System.Threading;
using LandmarkRemark.BusinessLogic.Exceptions;
using LandmarkRemark.BusinessLogic.Notes.Commands;
using LandmarkRemark.BusinessLogic.Tests.Infrastructure;
using Shouldly;
using Xunit;

namespace LandmarkRemark.BusinessLogic.Tests.Notes.Commands
{
    public class WhenNoteIsCreatedWithInvalidLatitidue : IClassFixture<TestBaseFixture>
    {
        private readonly TestBaseFixture _fixture;

        public WhenNoteIsCreatedWithInvalidLatitidue(TestBaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ExceptionIsThrownWithCorrectDetaisl()
        {
            int createdNoteId = 0;
            UnProcessableEntityException couldNotCreateNoteException = null;

            try
            {
                var userId = _fixture.LandmarkContext.Users.First().Id;
                var command = new CreateNoteCommand
                {
                    UserId =userId , 
                    Text = "a sample note",
                    Latitude = 150,
                    Longitude = 50
                };
                createdNoteId = new CreateNoteCommandHandler(_fixture.LandmarkContext, new FakeDatetimeProvider()).Handle(command, CancellationToken.None).GetAwaiter().GetResult();
            }
            catch (UnProcessableEntityException ex)
            {
                couldNotCreateNoteException = ex;
            }
            
            couldNotCreateNoteException.ShouldNotBeNull();
            couldNotCreateNoteException.ModelStateErrors.Count().ShouldBe(1);
            var modelStateError = couldNotCreateNoteException.ModelStateErrors.First();
            modelStateError.PropertyName.ShouldBe(nameof(CreateNoteCommand.Latitude));
        }
    }
}