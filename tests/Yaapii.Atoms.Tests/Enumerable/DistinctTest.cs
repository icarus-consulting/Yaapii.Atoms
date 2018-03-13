﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Tests.Enumerable
{
    public sealed class DistinctTest
    {
        [Fact]
        public void MergesEntries()
        {
            Assert.True(
                new LengthOf(
                    new Distinct<int>(
                        new EnumerableOf<int>(1, 2, 3),
                        new EnumerableOf<int>(10, 2, 30)
                    )
                ).Value() == 5);
        }

        [Fact]
        public void MergesEntriesWithEnumCtor()
        {
            Assert.True(
                new LengthOf(
                    new Distinct<int>(
                        new EnumerableOf<IEnumerable<int>>(
                            new EnumerableOf<int>(1, 2, 3),
                            new EnumerableOf<int>(10, 2, 30)
                        )
                    )
                ).Value() == 5);
        }

        [Fact]
        public void WorksWithEmpties()
        {
            Assert.True(
                new LengthOf(
                    new Distinct<string>(
                        new EnumerableOf<string>(),
                        new EnumerableOf<string>()
                    )
                ).Value() == 0);
        }
    }
}