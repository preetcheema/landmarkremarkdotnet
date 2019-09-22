using System;
using LandmarkRemark.Common;

namespace LandmarkRemark.BusinessLogic.Tests.Infrastructure
{
    public class FakeDatetimeProvider : ITimeProvider
    {
        public DateTime Now()
            => new DateTime(2019, 9, 20);
    }
}