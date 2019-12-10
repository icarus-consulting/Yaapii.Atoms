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
    public class MapOfTest
    {
        [Fact]
        public void BehavesAsMap()
        {
            var one = new KeyValuePair<string, string>("hello", "map");
            var two = new KeyValuePair<string, string>("goodbye", "dictionary");

            var m = new Map.Of(one, two);

            Assert.True(m.Contains(one) && m.Contains(two));
        }

        [Theory]
        [InlineData("A", "V")]
        [InlineData("B", "Y")]
        public void BuildsFromInputs(string key, string value)
        {
            Assert.Equal(
                value,
                new Map.Of(
                    new MapInput.Of(new Kvp.Of("A", "V")),
                    new MapInput.Of(new Kvp.Of("B", "Y"))
                )[key]
            );
        }

        [Fact]
        public void ConvertsEnumerableToMap()
        {
            var m =
                new Map.Of(
                    new KeyValuePair<string, string>("0", "hello, "),
                    new KeyValuePair<string, string>("1", "world!")
                );


            Assert.True(m["0"] == "hello, ");
            Assert.True(m["1"] == "world!");
        }

        [Fact]
        public void MakesMapFromArraySequence()
        {
            Assert.Equal(
                "B",
                new Map.Of(
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
                new Map.Of(
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
                new Map.Of(
                    new Many.Of<string>(
                        "A", "B",
                        "C"
                    )
                )["A"]
            );
        }

        [Fact]
        public void IsSticky()
        {
            int size = 1;
            var random = new Random();

            var map =
                new Map.Of(
                    new Repeated<KeyValuePair<string, string>>(
                        new ScalarOf<KeyValuePair<string, string>>(
                            () => new KeyValuePair<string, string>(random.Next() + "", "1")),
                            new ScalarOf<int>(() =>
                            {
                                Interlocked.Increment(ref size);
                                return size;
                            })
                        )
                    );

            var a = map.Count;
            var b = map.Count;

            Assert.Equal(a, b);
        }

        [Fact]
        public void BehavesAsMapTypedValue()
        {
            var one = new KeyValuePair<string, int>("hello", 10);
            var two = new KeyValuePair<string, int>("goodbye", 20);

            var m = new Map.Of<int>(one, two);

            Assert.True(m.Contains(one) && m.Contains(two));
        }

        [Theory]
        [InlineData("A", 39478624)]
        [InlineData("B", 60208801)]
        public void BuildsFromInputsTypedValue(string key, int value)
        {
            Assert.Equal(
                value,
                new Map.Of<int>(
                    new MapInput.Of<int>(new Kvp.Of<int>("A", 39478624)),
                    new MapInput.Of<int>(new Kvp.Of<int>("B", 60208801))
                )[key]
            );
        }

        [Theory]
        [InlineData("hello", 0)]
        [InlineData("world", 1)]
        public void ConvertsEnumerableToMapTypedValue(string key, int value)
        {
            var m =
                new Map.Of<int>(
                    new KeyValuePair<string, int>("hello", 0),
                    new KeyValuePair<string, int>("world", 1)
                );


            Assert.Equal(m[key], value);
        }

        [Fact]
        public void IsStickyTypedValue()
        {
            int size = 1;
            var random = new Random();

            var map =
                new Map.Of<int>(
                    new Repeated<IKvp<int>>(
                        new ScalarOf<IKvp<int>>(
                            () => new Kvp.Of<int>(random.Next() + "", 1)),
                            new ScalarOf<int>(() =>
                            {
                                Interlocked.Increment(ref size);
                                return size;
                            }
                        )
                    )
                );

            var a = map.Count;
            var b = map.Count;

            Assert.Equal(a, b);
        }

        [Fact]
        public void BehavesAsMapTypedKeyValue()
        {
            var one = new KeyValuePair<int, int>(45, 10);
            var two = new KeyValuePair<int, int>(33, 20);

            var m = new Map.Of<int, int>(one, two);

            Assert.True(m.Contains(one) && m.Contains(two));
        }

        [Theory]
        [InlineData(12, 39478624)]
        [InlineData(24, 60208801)]
        public void BuildsFromInputsTypedKeyValue(int key, int value)
        {
            Assert.Equal(
                value,
                new Map.Of<int, int>(
                    new MapInput.Of<int, int>(new Kvp.Of<int, int>(12, 39478624)),
                    new MapInput.Of<int, int>(new Kvp.Of<int, int>(24, 60208801))
                )[key]
            );
        }

        [Theory]
        [InlineData(9, 0)]
        [InlineData(10, 1)]
        public void ConvertsEnumerableToMapTypedKeyValue(int key, int value)
        {
            var m =
                new Map.Of<int, int>(
                    new KeyValuePair<int, int>(9, 0),
                    new KeyValuePair<int, int>(10, 1)
                );


            Assert.Equal(m[key], value);
        }

        [Fact]
        public void IsStickyTypedKeyValue()
        {
            int size = 1;
            var random = new Random();

            var map =
                new Map.Of<int, int>(
                    new Repeated<IKvp<int, int>>(
                        new ScalarOf<IKvp<int, int>>(
                            () => new Kvp.Of<int, int>(random.Next(), 1)),
                            new ScalarOf<int>(() =>
                            {
                                Interlocked.Increment(ref size);
                                return size;
                            }
                        )
                    )
                );

            var a = map.Count;
            var b = map.Count;

            Assert.Equal(a, b);
        }
    }
}