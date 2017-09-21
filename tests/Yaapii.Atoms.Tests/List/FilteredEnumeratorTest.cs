using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class FilteredEnumeratorTest
    {
        [Fact]
        public void Filters()
        {
            Assert.True(
                new JoinedText(" ",
                    new EnumerableOf<string>(
                        new FilteredEnumerator<string>(
                            new EnumerableOf<string>("Hello", "cruel", "World").GetEnumerator(),
                                (str) => str != "cruel"))).AsString() == "Hello World",
                "cannot filter enumerator contents");
        }
    }
}
