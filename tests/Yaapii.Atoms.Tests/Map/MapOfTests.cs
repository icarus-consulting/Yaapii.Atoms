using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Map.Tests
{
    public class MapOfTests
    {
        [Fact]
        public void BehavesAsMap()
        {
            var one = new KeyValuePair<string, string>("hello", "map");
            var two = new KeyValuePair<string, string>("goodbye", "dictionary");

            var m =
                new MapOf<string, string>(
                    one, two
                    );

            Assert.True(m.Contains(one) && m.Contains(two));
        }

        [Fact]
        public void ConvertsEnumerableToMap()
        {
            var m =
                new MapOf<int, String>(
                    new KeyValuePair<int, string>(0, "hello, "),
                    new KeyValuePair<int, string>(1, "world!")
                );


            Assert.True(m[0] == "hello, ");
            Assert.True(m[1] == "world!");
        }

        [Fact]
        public void SensesChangesInMap()
        {
            int size = 1;
            var random = new Random();

            var map =
                new MapOf<int, int>(
                    () =>
                    new Enumerable.Repeated<KeyValuePair<int, int>>(
                        new ScalarOf<KeyValuePair<int, int>>(
                            () => new KeyValuePair<int, int>(random.Next(), 1)),
                        new ScalarOf<int>(() =>
                        {
                            Interlocked.Increment(ref size);
                            return size;
                        })));

            var a = map.Count;
            var b = map.Count;

            Assert.NotEqual(a, b);
        }
    }
}