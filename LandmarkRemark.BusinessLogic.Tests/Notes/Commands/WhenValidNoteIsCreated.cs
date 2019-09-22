using System.Linq;
using System.Threading;
using LandmarkRemark.BusinessLogic.Exceptions;
using LandmarkRemark.BusinessLogic.Notes.Commands;
using LandmarkRemark.BusinessLogic.Tests.Infrastructure;
using Shouldly;
using Xunit;

namespace LandmarkRemark.BusinessLogic.Tests.Notes.Commands
{
    public class WhenValidNoteIsCreated : IClassFixture<TestBaseFixture>
    {
        private readonly TestBaseFixture _fixture;

        public WhenValidNoteIsCreated(TestBaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ANoteIsCreated()
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
                    Latitude = 50,
                    Longitude = 50
                };
                createdNoteId = new CreateNoteCommandHandler(_fixture.LandmarkContext, new FakeDatetimeProvider()).Handle(command, CancellationToken.None).GetAwaiter().GetResult();
            }
            catch (UnProcessableEntityException ex)
            {
                couldNotCreateNoteException = ex;
            }


            couldNotCreateNoteException.ShouldBeNull();
            createdNoteId.ShouldBeGreaterThan(0);
        }
    }
}