using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class EndlessEnumeratorTest
    {
        [Fact]
        public void RepeatsTwentyTimes()
        {
            var expected = "AAAAAAAAAAAAAAAAAAAA";

            Assert.True(
                new JoinedText(
                    "",
                    new EnumerableOf<IText>(
                            new MappedEnumerator<string, IText>(
                                new LimitedEnumerator<string>(
                                    new EndlessEnumerator<string>("A"),
                                    20),
                                str => new TextOf(str)))).AsString() == expected,
                
                "cannot repeat endlessly");

        }
    }
}