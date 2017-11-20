using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Map.Tests
{
    public sealed class StickyMapTest
    {
        [Fact]
        public void BehavesAsMap()
        {
            var map =
                new StickyMap<int, int>(
                    new KeyValuePair<int, int>(0, -1),
                    new KeyValuePair<int, int>(1, 1));

            Assert.True(map[0] == -1 && map[1] == 1);
        }

        [Fact]
        public void IgnoresChanges()
        {
            int size = 1;
            var random = new Random();

            var map =
                new StickyMap<int, int>(
                    new MapOf<int, int>(
                    () =>
                    new Enumerable.Repeated<KeyValuePair<int, int>>(
                        new ScalarOf<KeyValuePair<int, int>>(
                            () => new KeyValuePair<int, int>(random.Next(), 1)),
                        new ScalarOf<int>(() =>
                        {
                            Interlocked.Increment(ref size);
                            return size;
                        }))));

            var a = map.Count;
            var b = map.Count;

            Assert.Equal(a, b);
        }

        [Fact]
        public void DecoratesEntries()
        {
            Assert.True(
                    new StickyMap<String, String>(
                        new KeyValuePair<string, string>("first", "Jeffrey"),
                        new KeyValuePair<string, string>("last", "Lebowski")
                    )["first"] == "Jeffrey",
                    "cannot decorate entries"
                );
        }

        [Fact]
        public void ExtendsExistingMap()
        {
            var map =
                    new StickyMap<String, String>(
                        new StickyMap<String, String>(
                            new KeyValuePair<string, string>("make", "Mercedes-Benz"),
                            new KeyValuePair<string, string>("cost", "$95,000")
                        ),
                        new KeyValuePair<string, string>("year", "2017"),
                        new KeyValuePair<string, string>("mileage", "12,000")
                    );

            Assert.True(
                map.ContainsKey("make") && map["make"] == "Mercedes-Benz"
                && map.ContainsKey("mileage") && map["mileage"] == "12,000",
                "cannot merge maps");
        }

        [Fact]
        public void ExtendsExistingMapWithFunc()
        {
            var map =
                new StickyMap<string, string, string>(
                    new StickyMap<string, string>(
                        new KeyValuePair<string, string>("black", "BLACK"),
                        new KeyValuePair<string, string>("white", "WHITE")
                    ),
                    new EnumerableOf<string>("yellow", "red", "blue"),
                    color => new KeyValuePair<string, string>(
                        color, color.ToUpper()
                    ));

            Assert.True(
                map.ContainsKey("black") && map["black"] == "BLACK" &&
                map.ContainsKey("red") && map["red"] == "RED",
                "Can't transform and decorate a list of entries");
        }

        [Fact]
        public void ExtendsExistingMapWithTwoFuncs()
        {
            var map =
                new StickyMap<string, string, string>(
                    new StickyMap<String, String>(
                        new KeyValuePair<string, string>("black!", "Black!"),
                        new KeyValuePair<string, string>("white!", "White!")
                    ),
                    new EnumerableOf<string>("yellow!", "red!", "blue!"),
                    color => String.Format("[{0}]", color),
                    color => color.ToUpper()
                );

            Assert.True(
                map.ContainsKey("black!") && map["black!"] == "Black!" &&
                map.ContainsKey("[red!]") && map["[red!]"] == "RED!",
                "cannot decorate with transformed entries");
        }
    }
}