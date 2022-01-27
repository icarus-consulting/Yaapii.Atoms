using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Tests.Enumerable
{
    public sealed class NoneTests
    {
        [Fact]
        public void StringIsEmpty()
        {
            Assert.False(
                new None().GetEnumerator().MoveNext()
            );
        }

        [Fact]
        public void GenericIsEmpty()
        {
            Assert.False(
                new None<int>().GetEnumerator().MoveNext()
            );
        }
    }
}
