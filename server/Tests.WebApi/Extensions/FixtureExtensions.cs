using AutoFixture;
using Moq;

namespace Tests.WebApi.Extensions
{
    public static class FixtureExtensions
    {
        public static Mock<T> FreezeMoq<T>(this IFixture fixture) where T : class
        {
            return fixture.Freeze<Mock<T>>();
        }
    }
}