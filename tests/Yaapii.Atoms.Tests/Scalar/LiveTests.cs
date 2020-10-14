using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Scalar.Tests
{
    public sealed class LiveTests
    {
        [Fact]
        public void ReloadsFunc()
        {
            var counts = 0;
            var live = new Live<bool>(() =>
            {
                ++counts;
                return true;
            });
            live.Value();
            live.Value();
            Assert.Equal(2, counts);
        }
    }
}
