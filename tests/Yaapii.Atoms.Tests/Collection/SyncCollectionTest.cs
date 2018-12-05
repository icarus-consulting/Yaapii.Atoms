using Xunit;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class SyncCollectionTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                -1,
                new SyncCollection<int>(1, 2, 0, -1)
            );
        }

    }
}
