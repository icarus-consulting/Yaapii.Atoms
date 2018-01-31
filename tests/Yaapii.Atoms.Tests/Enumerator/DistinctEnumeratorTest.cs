using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Tests.Enumerator
{
    public sealed class DistinctEnumeratorTest
    {
        [Fact]
        public void MergesEntries()
        {
            Assert.True(
                new LengthOf(
                    new EnumerableOf<string>(
                        new DistinctEnumerator<string>(
                            new EnumerableOf<IEnumerator<string>>(
                                new EnumerableOf<string>("A", "B", "F").GetEnumerator(),
                                new EnumerableOf<string>("A", "E", "F").GetEnumerator()
                            )
                        )
                    )
                ).Value() == 4);
        }

        [Fact]
        public void Resets()
        {
            var e =
                new DistinctEnumerator<string>(
                    new EnumerableOf<IEnumerator<string>>(
                        new EnumerableOf<string>("A").GetEnumerator(),
                        new EnumerableOf<string>("A").GetEnumerator()
                    )
                );

            e.MoveNext();
            e.Reset();

            Assert.True(e.MoveNext());
        }

        [Fact]
        public void WorksWithEmpties()
        {
            Assert.True(
                new LengthOf(
                    new EnumerableOf<string>(
                        new DistinctEnumerator<string>(
                            new EnumerableOf<IEnumerator<string>>(
                                new EnumerableOf<string>().GetEnumerator(),
                                new EnumerableOf<string>().GetEnumerator()
                            )
                        )
                    )
                ).Value() == 0);
        }
    }
}
