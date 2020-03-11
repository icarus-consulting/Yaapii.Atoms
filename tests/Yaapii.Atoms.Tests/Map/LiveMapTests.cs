// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Lookup.Tests
{
    public class MapLiveTests
    {
        [Fact]
        public void BehavesAsMap()
        {
            var one = new KeyValuePair<string, string>("hello", "map");
            var two = new KeyValuePair<string, string>("goodbye", "dictionary");

            var m = new Map.Live(one, two);

            Assert.True(m.Contains(one) && m.Contains(two));
        }

        [Fact]
        public void ConvertsEnumerableToMap()
        {
            var m =
                new Map.Live(
                    new Many.Live<KeyValuePair<string, string>>(
                        new KeyValuePair<string, string>("0", "hello, "),
                        new KeyValuePair<string, string>("1", "world!")
                    )
                );


            Assert.True(m["0"] == "hello, ");
            Assert.True(m["1"] == "world!");
        }

        [Fact]
        public void MakesMapFromArraySequence()
        {
            Assert.Equal(
                "B",
                new Map.Live(
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
                new Map.Live(
                    new Many.Of<string>(
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
                new Map.Live(
                    new Many.Of<string>(
                        "A", "B",
                        "C"
                    )
                )["A"]
            );
        }

        [Fact]
        public void SensesChangesInMap()
        {
            int size = 1;
            var random = new Random();

            var map =
                new Map.Live<int, int>(
                    new Repeated<KeyValuePair<int, int>>(
                        new LiveScalar<KeyValuePair<int, int>>(() =>
                            new KeyValuePair<int, int>(random.Next(), 1)),
                            new LiveScalar<int>(() =>
                            {
                                Interlocked.Increment(ref size);
                                return size;
                            })
                        )
                    );

            var a = map.Count;
            var b = map.Count;

            Assert.NotEqual(a, b);
        }

        [Fact]
        public void BehavesAsMapTypedValue()
        {
            var one = new KeyValuePair<string, int>("hello", 0);
            var two = new KeyValuePair<string, int>("goodbye", 1);

            var m = new Map.Live<int>(one, two);

            Assert.True(m.Contains(one) && m.Contains(two));
        }

        [Fact]
        public void ConvertsEnumerableToMapTypedValue()
        {
            var m =
                new Map.Live<int>(
                    new Many.Of<KeyValuePair<string, int>>(
                        new KeyValuePair<string, int>("hello", 0),
                        new KeyValuePair<string, int>("world", 1)
                    )
                );


            Assert.True(m["hello"] == 0);
            Assert.True(m["world"] == 1);
        }

        [Fact]
        public void SensesChangesInMapTypedValue()
        {
            int size = 1;
            var random = new Random();

            var map =
                new Map.Live<int>(
                    new Repeated<KeyValuePair<string, int>>(
                        new LiveScalar<KeyValuePair<string, int>>(() =>
                            new KeyValuePair<string, int>(random.Next() + "", 1)),
                            new LiveScalar<int>(() =>
                            {
                                Interlocked.Increment(ref size);
                                return size;
                            })
                        )
                    );

            var a = map.Count;
            var b = map.Count;

            Assert.NotEqual(a, b);
        }

        [Fact]
        public void BehavesAsMapTypedKeyValue()
        {
            var one = new KeyValuePair<string, string>("hello", "map");
            var two = new KeyValuePair<string, string>("goodbye", "dictionary");

            var m =
                new Map.Live<string, string>(
                    one, two
                    );

            Assert.True(m.Contains(one) && m.Contains(two));
        }

        [Fact]
        public void ConvertsEnumerableToMapTypedKeyValue()
        {
            var m =
                new Map.Live<int, String>(
                    new KeyValuePair<int, string>(0, "hello, "),
                    new KeyValuePair<int, string>(1, "world!")
                );


            Assert.True(m[0] == "hello, ");
            Assert.True(m[1] == "world!");
        }

        [Fact]
        public void MakesMapFromArraySequenceTypedKeyValue()
        {
            Assert.Equal(
                "B",
                new Map.Live(
                    "A", "B",
                    "C", "D"
                )["A"]
            );
        }

        [Fact]
        public void MakesMapFromEnumerableSequenceTypedKeyValue()
        {
            Assert.Equal(
                "B",
                new Map.Live(
                    new Many.Of<string>(
                        "A", "B",
                        "C", "D"
                    )
                )["A"]
            );
        }

        [Fact]
        public void RejectsOddValueCountTypedKeyValue()
        {
            Assert.Throws<ArgumentException>(() =>
                new Map.Live(
                    new Many.Of<string>(
                        "A", "B",
                        "C"
                    )
                )["A"]
            );
        }

        [Fact]
        public void SensesChangesInMapTypedKeyValue()
        {
            int size = 1;
            var random = new Random();

            var map =
                new Map.Live<int, int>(
                    new Repeated<KeyValuePair<int, int>>(
                        new LiveScalar<KeyValuePair<int, int>>(() =>
                            new KeyValuePair<int, int>(random.Next(), 1)),
                            new LiveScalar<int>(() =>
                            {
                                Interlocked.Increment(ref size);
                                return size;
                            })
                        )
                    );

            var a = map.Count;
            var b = map.Count;

            Assert.NotEqual(a, b);
        }
    }
}