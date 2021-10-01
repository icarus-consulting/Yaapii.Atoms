// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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
    public class MapLiveTests
    {
        [Fact]
        public void BehavesAsMap()
        {
            var one = new KeyValuePair<string, string>("hello", "map");
            var two = new KeyValuePair<string, string>("goodbye", "dictionary");

            var m =
                new LiveMap(() =>
                    new LiveMany<IKvp>(
                        new KvpOf(() => one),
                        new KvpOf(() => two)
                    )
                );

            Assert.True(m[one.Key] == one.Value && m[two.Key] == two.Value);
        }

        [Fact]
        public void DoesNotEnumerateDictionary()
        {
            var failed = false;
            var unused =
                new LiveMap(() =>
                    new LiveMany<IKvp>(
                        new KvpOf("key a", "value a"),
                        new KvpOf("key b", () =>
                        {
                            failed = true;
                            return "value b";
                        })
                    )
                )["key a"];
            Assert.False(failed);
        }

        [Fact]
        public void ConvertsEnumerableToMap()
        {
            var m =
                new LiveMap(() =>
                    new MapOf(
                        new KeyValuePair<string, string>("0", "hello, "),
                        new KeyValuePair<string, string>("1", "world!")
                    )
                );


            Assert.True(m["0"] == "hello, ");
            Assert.True(m["1"] == "world!");
        }

        [Fact]
        public void SensesChangesInMap()
        {
            int size = 1;
            var random = new Random();

            var map =
                new LiveMap<int, int>(() =>
                    new MapOf<int, int>(
                        new Repeated<KeyValuePair<int, int>>(
                            new Live<KeyValuePair<int, int>>(() =>
                                new KeyValuePair<int, int>(random.Next(), 1)),
                                new Live<int>(() =>
                                {
                                    Interlocked.Increment(ref size);
                                    return size;
                                })
                            )
                        )
                    );

            var a = map.Count;
            var b = map.Count;

            Assert.NotEqual(a, b);
        }

        [Fact]
        public void SensesChangesInValues()
        {
            var value = 0;
            var map =
                new LiveMap(() =>
                    new LiveMany<IKvp>(
                        new KvpOf("key", () => (value++).ToString())
                    )
                );
            var a = map["key"];
            var b = map["key"];
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void BehavesAsMapTypedValue()
        {
            var one = new KeyValuePair<string, int>("hello", 0);
            var two = new KeyValuePair<string, int>("goodbye", 1);

            var m =
                new LiveMap<int>(() =>
                    new LiveMany<IKvp<int>>(
                        new KvpOf<int>(() => one),
                        new KvpOf<int>(() => two)
                    )
                );

            Assert.True(m[one.Key] == one.Value && m[two.Key] == two.Value);
        }

        [Fact]
        public void DoesNotEnumerateDictionaryTypedValue()
        {
            var failed = false;
            var unused =
                new LiveMap<int>(() =>
                    new LiveMany<IKvp<int>>(
                        new KvpOf<int>("key a", 0),
                        new KvpOf<int>("key b", () =>
                        {
                            failed = true;
                            return 1;
                        })
                    )
                )["key a"];
            Assert.False(failed);
        }

        [Fact]
        public void ConvertsEnumerableToMapTypedValue()
        {
            var m =
                new LiveMap<int>(() =>
                    new MapOf<int>(
                        new ManyOf<KeyValuePair<string, int>>(
                            new KeyValuePair<string, int>("hello", 0),
                            new KeyValuePair<string, int>("world", 1)
                        )
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
                new LiveMap<int>(() =>
                    new MapOf<int>(
                        new Repeated<KeyValuePair<string, int>>(
                            new Live<KeyValuePair<string, int>>(() =>
                                new KeyValuePair<string, int>(random.Next() + "", 1)),
                                new Live<int>(() =>
                                {
                                    Interlocked.Increment(ref size);
                                    return size;
                                })
                            )
                        )
                    );

            var a = map.Count;
            var b = map.Count;

            Assert.NotEqual(a, b);
        }

        [Fact]
        public void SensesChangesInValuesTypedValue()
        {
            var value = 0;
            var map =
                new LiveMap<int>(() =>
                    new LiveMany<IKvp<int>>(
                        new KvpOf<int>("key", () => value++)
                    )
                );
            var a = map["key"];
            var b = map["key"];
            Assert.NotEqual(a, b);
        }

        [Fact]
        public void BehavesAsMapTypedKeyValue()
        {
            var one = new KeyValuePair<string, string>("hello", "map");
            var two = new KeyValuePair<string, string>("goodbye", "dictionary");

            var m =
                new LiveMap<string, string>(() =>
                    new LiveMany<IKvp<string, string>>(
                        new KvpOf<string, string>(() => one),
                        new KvpOf<string, string>(() => two)
                    )
                );

            Assert.True(m[one.Key] == one.Value && m[two.Key] == two.Value);
        }

        [Fact]
        public void DoesNotEnumerateDictionaryTypedKeyValue()
        {
            var failed = false;
            var unused =
                new LiveMap<int, int>(() =>
                    new LiveMany<IKvp<int, int>>(
                        new KvpOf<int, int>(10, 0),
                        new KvpOf<int, int>(11, () =>
                        {
                            failed = true;
                            return 1;
                        })
                    )
                )[10];
            Assert.False(failed);
        }

        [Fact]
        public void ConvertsEnumerableToMapTypedKeyValue()
        {
            var m =
                new LiveMap<int, String>(() =>
                    new MapOf<int, string>(
                        new KeyValuePair<int, string>(0, "hello, "),
                        new KeyValuePair<int, string>(1, "world!")
                    )
                );


            Assert.True(m[0] == "hello, ");
            Assert.True(m[1] == "world!");
        }

        [Fact]
        public void SensesChangesInMapTypedKeyValue()
        {
            int size = 1;
            var random = new Random();

            var map =
                new LiveMap<int, int>(() =>
                    new MapOf<int, int>(
                        new Repeated<KeyValuePair<int, int>>(
                            new Live<KeyValuePair<int, int>>(() =>
                                new KeyValuePair<int, int>(random.Next(), 1)),
                                new Live<int>(() =>
                                {
                                    Interlocked.Increment(ref size);
                                    return size;
                                })
                            )
                        )
                    );

            var a = map.Count;
            var b = map.Count;

            Assert.NotEqual(a, b);
        }

        [Fact]
        public void SensesChangesInValuesTypedKeyValue()
        {
            var value = 0;
            var map =
                new LiveMap<int, int>(() =>
                    new LiveMany<IKvp<int, int>>(
                        new KvpOf<int, int>(0, () => value++)
                    )
                );
            var a = map[0];
            var b = map[0];
            Assert.NotEqual(a, b);
        }
    }
}
