// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Map.Tests
{
    public class MapOfTest
    {
        [Fact]
        public void MakesMapFromTupleArray()
        {
            Assert.Equal(
                "B",
                new MapOf<string, string>(
                    new Tuple<string, string>[]
                    {
                        new Tuple<string, string>("A", "B"),
                        new Tuple<string, string>("C", "D")
                    }
                )["A"]
            );
        }

        [Fact]
        public void MakesMapFromEnumerableSequence()
        {
            Assert.Equal(
                "D",
                new MapOf<string,string>(
                    new EnumerableOf<Tuple<string,string>>(
                        new Tuple<string, string>("A", "B"),
                        new Tuple<string, string>("C", "D")
                    )
                )["C"]
            );
        }

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