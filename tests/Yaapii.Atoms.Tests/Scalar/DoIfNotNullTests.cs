using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Tests.Scalar
{
    public sealed class DoIfNotNullTests
    {
        [Fact]
        public void ExecutesFunction()
        {
            Assert.Equal(
                "executed",
                new DoIfNotNull<string, string>(
                    "input",
                    "fallback",
                    input => "executed"
                ).Value()
            );
        }

        [Fact]
        public void DeliversFallback()
        {
            string nullValue = default;

            Assert.Equal(
                "fallback",
                new DoIfNotNull<string, string>(
                    nullValue,
                    "fallback",
                    input => "executed"
                ).Value()
            );
        }
    }
}
