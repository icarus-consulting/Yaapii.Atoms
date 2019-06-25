using System;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;

namespace Yaapii.Atoms.Tests.Map
{
    public sealed class StringMapTests
    {
        [Fact]
        public void MakesMapFromArraySequence()
        {
            Assert.Equal(
                "B",
                new StringMap(
                    "A", "B",
                    "C", "D"
                )["A"]
            );
        }

        [Fact]
        public void MakesMapFromEnumerableSequence()
        {
            Assert.Equal(
                "B",
                new StringMap(
                    new EnumerableOf<string>(
                        "A", "B",
                        "C", "D"
                    )
                )["A"]
            );
        }

        [Fact]
        public void RejectsOddValueCount()
        {
            Assert.Throws<ArgumentException>(() =>
                new StringMap(
                    new EnumerableOf<string>(
                        "A", "B",
                        "C"
                    )
                )["A"]
            );
        }

        [Fact]
        public void MakesMapFromTupleArray()
        {
            Assert.Equal(
                "B",
                new StringMap(
                    new Tuple<string, string>[]
                    {
                        new Tuple<string,string>("A", "B"),
                        new Tuple<string,string>("C", "D")
                    }
                )["A"]
            );
        }

        [Fact]
        public void MakesMapFromTupleEnumerable()
        {
            Assert.Equal(
                "B",
                new StringMap(
                    new EnumerableOf<Tuple<string, string>>(
                        new Tuple<string,string>("A", "B"),
                        new Tuple<string,string>("C", "D")
                    )
                )["A"]
            );
        }

        [Fact]
        public void MakesMapFromExistingMapAndArraySequence()
        {
            var map =
                new StringMap(
                    new StringMap(
                        "A", "B",
                        "C", "D"
                    ),
                    "E", "F",
                    "G", "H"
                );
            Assert.True(
                map.ContainsKey("A") && map.ContainsKey("E")
            );
        }
    }
}
