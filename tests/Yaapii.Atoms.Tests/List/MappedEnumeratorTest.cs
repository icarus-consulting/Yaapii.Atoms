using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class MappedEnumeratorTest
    {
        [Fact]
        public void MapsContents()
        {
            Assert.True(
                new ItemAtEnumerator<string>(
                    new MappedEnumerator<int, string>(
                        new EnumerableOf<int>(1).GetEnumerator(),
                        i => new TextOf(i).AsString()),
                0).Value() == "1",
            "cannot map contents of enumerator");
        }
    }
}
