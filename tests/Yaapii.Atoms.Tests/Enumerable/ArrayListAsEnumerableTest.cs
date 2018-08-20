using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public class ArrayListAsEnumerableTest
    {
        [Fact]
        public void BuildsFromStrings()
        {
            var arr = new ArrayList() { "A", "B", "C" };

            Assert.True(
                new ItemAt<object>(
                    new ArrayListAsEnumerable(arr)
                ).Value().ToString() == "A");
        }
    }
}
