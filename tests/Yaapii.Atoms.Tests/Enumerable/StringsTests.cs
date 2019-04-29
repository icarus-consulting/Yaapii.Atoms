using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Enumerable
{
    public sealed class StringsTests
    {
        [Fact]
        public void Enumerates()
        {
            Assert.Equal(
                new List<string>() { "one", "two", "eight" },
                new Strings("one", "two", "eight")
            );
        }

        [Fact]
        public void Converts()
        {
            var number = 2.3f;
            var enumerator = new Strings(number).GetEnumerator();
            enumerator.MoveNext();
            Assert.Equal(
                number.ToString(),
                enumerator.Current                
            );
        }
    }
}
