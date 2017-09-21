using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class JoinedEnumeratorTest
    {
        [Fact]
        public void JoinsEnumerators()
        {
            Assert.True(
            new LengthOfEnumerator<IEnumerator<string>>(
                new JoinedEnumerator<IEnumerator<String>>(
                    new MappedEnumerator<string, IEnumerator<string>>(
                        new EnumerableOf<string>("x", "y", "z").GetEnumerator(),
                        (input) => new EnumerableOf<string>(input).GetEnumerator()))
                    ).Value() == 3,
            "Can't concatenate mapped iterators together");
        }
    }
}
