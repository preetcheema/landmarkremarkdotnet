using System;
using LandmarkRemark.BusinessLogic.Users.Queries;
using LandmarkRemark.Domain.Entities;
using Shouldly;
using Xunit;

namespace LandmarkRemark.BusinessLogic.Tests.Users.Queries
{
    public class WhenRequestIsMadeToGetNonExistentUser : GetUserByIdTestBase
    {
        public WhenRequestIsMadeToGetNonExistentUser(TestBaseFixture fixture) : base(fixture)
        {
        }

     

        [Fact]
        public void UserCannotBeRetreived()
        {
            _ex.ShouldNotBeNull();
            _ex.EntityName.ShouldBe(nameof(User));
            _ex.EntityKey.ShouldBe(Int32.MaxValue);
        }

        public override GetUserByIdQuery GetRequest()
        {
            return new GetUserByIdQuery
            {
                Id = Int32.MaxValue
            };
        }
    }
}