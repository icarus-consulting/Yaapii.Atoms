using Xunit;

namespace Yaapii.Atoms.Map.Tests
{
    public sealed class EmptyMapTests
    {
        [Fact]
        public void CreatesEmptyMap()
        {
            Assert.Empty(
                new EmptyMap()
            );
        }
    }
}
