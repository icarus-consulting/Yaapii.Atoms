using System;
using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Enumerable.Test
{
    public sealed class StrictOnceTests
    {
        [Fact]
        public void RejectsBuildingTwice()
        {
            var once = new StrictOnce<string>(() => new List<string>() { "A" });
            once.GetEnumerator();
            Assert.Throws<InvalidOperationException>(() => once.GetEnumerator());
        }

        [Fact]
        public void AllowsFirstBuild()
        {
            var once = new StrictOnce<string>(() => new List<string>() { "A" });

            Assert.True(once.GetEnumerator().MoveNext());
        }
    }
}
