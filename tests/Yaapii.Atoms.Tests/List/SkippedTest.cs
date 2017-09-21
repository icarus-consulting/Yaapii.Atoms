using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class SkippedTest
    {
        [Fact]
        public void SkipIterable()
        {
            Assert.True(
                new JoinedText(
                    ", ",
                    new Skipped<string>(
                        new EnumerableOf<string>("one", "two", "three", "four"),
                        2)
                    ).AsString() == "three, four");
        }
    }
}