using System;
using System.Threading;
using LandmarkRemark.BusinessLogic.Exceptions;
using LandmarkRemark.BusinessLogic.Users.Queries;
using Xunit;

namespace LandmarkRemark.BusinessLogic.Tests.Users.Queries
{
    public abstract class GetUserByIdTestBase :IClassFixture<TestBaseFixture>
    {
        protected TestBaseFixture _fixture;
        protected UserDetailModel _createdUser;
        protected EntityNotFoundException _ex;

        public GetUserByIdTestBase(TestBaseFixture fixture)
        {
            _fixture = fixture;

            try
            {
                var query = GetRequest();
                
                _createdUser = new GetUserByIdQueryHandler(_fixture.LandmarkContext).Handle(query, CancellationToken.None).GetAwaiter().GetResult();
            }
            catch (EntityNotFoundException ex)
            {
                _ex = ex;
            }
        }

        public abstract GetUserByIdQuery GetRequest();

    }
}