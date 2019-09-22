using System;
using LandmarkRemark.Common;

namespace LandmarkRemark.Infrastructure
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime Now() => DateTime.Now;
    }
}