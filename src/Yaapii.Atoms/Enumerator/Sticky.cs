// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// An enumerator which is sticky. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Sticky<T> : IEnumerator<T>
    {
        private readonly int[] position;
        private readonly IDictionary<int, T> cache;

        /// In order to allow enumerables to not pre-compute/copy all elements,
        /// this ctor allows injecting and therefore re-using the caching elements.
        /// An enumerable like <see cref="ManyEnvelope"/> can then issue multiple 
        /// Enumerators while the same cache is filled when advancing them.
        public Sticky(IEnumerator<T> origin) : this(() => origin)
        { }

        /// In order to allow enumerables to not pre-compute/copy all elements,
        /// this ctor allows injecting and therefore re-using the caching elements.
        /// An enumerable like <see cref="ManyEnvelope"/> can then issue multiple 
        /// Enumerators while the same cache is filled when advancing them.
        public Sticky(Func<IEnumerator<T>> origin) : this(new Cache<T>(origin))
        { }

        /// In order to allow enumerables to not pre-compute/copy all elements,
        /// this ctor allows injecting and therefore re-using the caching elements.
        /// An enumerable like <see cref="ManyEnvelope"/> can then issue multiple 
        /// Enumerators while the same cache is filled when advancing them.
        public Sticky(IDictionary<int, T> cache)
        {
            this.position = new int[1] { -1 };
            this.cache = cache;
        }

        public T Current
        {
            get
            {
                if (this.position[0] == -1)
                {
                    throw new InvalidOperationException("Cannot get current element - move the enumerator first.");
                }
                return this.cache[this.position[0]];
            }
        }

        object IEnumerator.Current => this.Current;

        public bool MoveNext()
        {
            bool moved = false;
            var next = this.position[0] + 1;
            if (this.cache.ContainsKey(next))
            {
                this.position[0] = next;
                moved = true;
            }
            return moved;
        }

        public void Reset()
        {
            this.position[0] = -1;
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// A cache for the enumerator, realized as map.
        /// When asking for a key, the enumerator will be advanced until the 
        /// position, if existing, is found.
        /// All values which are visited will be cached.
        /// </summary>
        public sealed class Cache<T> : IDictionary<int, T>
        {
            private readonly IScalar<IEnumerator<T>> origin;
            private readonly List<T> cache;
            private readonly bool[] reachedEnd;
            private readonly int[] count;

            public Cache(Func<IEnumerator<T>> origin)
            {
                this.count = new int[1] { 0 };
                this.reachedEnd = new bool[1] { false };
                this.origin = new ScalarOf<IEnumerator<T>>(origin);
                this.cache = new List<T>();
            }

            public bool ContainsKey(int itemIndex)
            {
                var enumerator = this.origin.Value();
                while (!this.reachedEnd[0] && this.count[0] - 1 < itemIndex)
                {
                    if (enumerator.MoveNext())
                    {
                        this.count[0]++;
                        this.cache.Add(enumerator.Current);
                    }
                    else
                    {
                        this.reachedEnd[0] = true;
                        break;
                    }
                }
                return this.count[0] > itemIndex;
            }

            public T this[int key]
            {
                get
                {
                    if (this.ContainsKey(key))
                    {
                        return this.cache[key];
                    }
                    else
                    {
                        throw new ArgumentException($"Cannot get value number {key} the origin enumerator did only have {this.cache.Count} elements.");
                    }
                }
                set
                {
                    throw new InvalidOperationException("Setting a value is not supported.");
                }
            }

            public ICollection<int> Keys => throw new InvalidOperationException("Enumerating keys is not supported.");

            public ICollection<T> Values => throw new InvalidOperationException("Enumerating values is not supported.");

            public int Count
            {
                get
                {
                    var count = 0;
                    if (!this.reachedEnd[0])
                    {
                        while (this.ContainsKey(count))
                        {
                            count++;
                            this.count[0] = count;
                        }
                    }
                    return this.count[0];
                }
            }

            public bool IsReadOnly => true;

            public void Add(int key, T value) => throw new InvalidOperationException("Adding values is not supported.");

            public void Add(KeyValuePair<int, T> item) => throw new InvalidOperationException("Adding values is not supported.");

            public void Clear() => throw new InvalidOperationException("Clearing is not supported.");

            public bool Contains(KeyValuePair<int, T> item) => throw new InvalidOperationException("Testing contained items is not supported.");

            public void CopyTo(KeyValuePair<int, T>[] array, int arrayIndex) => throw new InvalidOperationException("Copying this map is not supported.");

            public IEnumerator<KeyValuePair<int, T>> GetEnumerator() => throw new InvalidOperationException("Getting the enumerator is not supported.");

            public bool Remove(int key) => throw new InvalidOperationException("Removing elements is not supported.");

            public bool Remove(KeyValuePair<int, T> item) => throw new InvalidOperationException("Removing elements is not supported.");

            public bool TryGetValue(int key, out T value) => throw new InvalidOperationException("Trying to get values is not supported.");

            IEnumerator IEnumerable.GetEnumerator() => throw new InvalidOperationException("Getting the enumerator is not supported.");
        }
    }
}
