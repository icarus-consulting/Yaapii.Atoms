using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class SortedEnumeratorTest
    {
        [Fact]
        public void Sorts()
        {
            Assert.True(
                new JoinedText(
                    " ",
                    new EnumerableOf<string>(
                        new SortedEnumerator<string>(
                            Comparer<string>.Default,
                                new EnumerableOf<string>("B", "A", "C", "F", "E", "D").GetEnumerator()
                        ))).AsString() == "A B C D E F",
                "cannot sort contents of iterator");
        }
    }
}