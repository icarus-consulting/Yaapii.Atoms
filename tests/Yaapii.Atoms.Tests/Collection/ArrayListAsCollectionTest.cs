using System.Collections;
using Xunit;
using Yaapii.Atoms.Collection;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public class ArrayListAsCollectionTest
    {
        [Fact]
        public void BuildsFromStrings()
        {
            var arr = new ArrayList() { "A", "B", "C" };

            Assert.True(
                new ItemAt<object>(
                    new ArrayListAsCollection(arr)
                ).Value().ToString() == "A");
        }
    }
}
