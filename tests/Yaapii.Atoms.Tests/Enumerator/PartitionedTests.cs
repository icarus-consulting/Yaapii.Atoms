using System;
using System.Collections.Generic;
using Xunit;

namespace Yaapii.Atoms.Enumerator
{
    public class PartitionedTests
    {
        [Fact]
        public void CountsPartititions()
        {
            IEnumerator<int> enumerator =
                new List<int>()
                {
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                    8,
                    9,
                    10
                }.GetEnumerator();

            var partition =
                new Partitioned<int>(
                    3,
                    enumerator
                );

            partition.MoveNext();
            partition.MoveNext();
            partition.MoveNext();
            partition.MoveNext();

            Assert.Equal(
                new List<int>() { 10 },
                partition.Current
            );
        }


        [Fact]
        public void Partionate()
        {
            IEnumerator<string> enumerator =
                new List<string>()
                {
                    "Christian",
                    "Sauer"
                }.GetEnumerator();

            var partition =
                new Partitioned<string>(
                    1,
                    enumerator
                );

            partition.MoveNext();
            Assert.Equal(
                new List<string>() { "Christian" },
                partition.Current
            );
        }

        [Fact]
        public void ThrowsExceptionOnInvalidSize()
        {
            IEnumerator<string> enumerator = new List<string>() { }.GetEnumerator();

            Assert.Throws<ArgumentException>(() =>
                new Partitioned<string>(
                    0,
                    enumerator
                ).MoveNext()
            );
        }

        [Fact]
        public void CurrentThrowsException()
        {
            IEnumerator<string> enumerator = new List<string>() { }.GetEnumerator();

            Assert.Throws<InvalidOperationException>(() =>
                new Partitioned<string>(
                    2,
                    enumerator
                ).Current
            );
        }

        [Fact]
        public void FailsOnEnd()
        {
            IEnumerator<int> enumerator =
                new List<int>()
                {
                    1,
                    2,
                    3,
                    4,
                    5
                }.GetEnumerator();

            var partition =
                new Partitioned<int>(
                    5,
                    enumerator
                );

            partition.MoveNext();
            partition.MoveNext();

            Assert.Throws<InvalidOperationException>(() =>
            partition.Current
            );
        }
    }
}
