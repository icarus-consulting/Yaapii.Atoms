using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class LimitedEnumeratorTest
    {
        [Fact]
        public void LimitsContent()
        {
            Assert.True(
                new JoinedText(", ",
                new EnumerableOf<IText>(
                    new MappedEnumerator<int, IText>(
                        new LimitedEnumerator<int>(
                            new EnumerableOf<int>(1, 2, 3, 4).GetEnumerator(),
                            2), 
                        str => new TextOf(str + "")))).AsString() == "1, 2",
            "cannot limit enumertor contents");
                
        }
    }
}
