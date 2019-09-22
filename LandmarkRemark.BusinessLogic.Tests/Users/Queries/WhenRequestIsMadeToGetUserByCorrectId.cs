using System;
using System.Linq;
using LandmarkRemark.BusinessLogic.Users.Queries;
using LandmarkRemark.Domain.Entities;
using Shouldly;
using Xunit;

namespace LandmarkRemark.BusinessLogic.Tests.Users.Queries
{
    public class WhenRequestIsMadeToGetUserByCorrectId : GetUserByIdTestBase
    {
        private User _existingUser;

        public WhenRequestIsMadeToGetUserByCorrectId(TestBaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public void CorrectUserIsRetrievedAndResultIsCorrect()
        {
            _createdUser.Id.ShouldBe(_existingUser.Id);
            _createdUser.FirstName.ShouldBe(_existingUser.FirstName);
        }

        public override GetUserByIdQuery GetRequest()
        {
           _existingUser = _fixture.LandmarkContext.Users.First();
            return new GetUserByIdQuery
            {
                Id=_existingUser.Id
            };
        }
    }
}