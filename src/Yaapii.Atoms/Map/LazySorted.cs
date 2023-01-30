// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Sorts the given map with the given comparer
    /// </summary>
    /// <typeparam name="Key">Key Type of the Map</typeparam>
    /// <typeparam name="Value">Value Type of the Map</typeparam>
    public sealed class LazySorted<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        public LazySorted(IDictionary<Key, Value> dict)
            : this(dict, Comparer<Key>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public LazySorted(IDictionary<Key, Value> dict, Func<Key, Key, int> compare)
            : this(dict, new SimpleComparer<Key>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public LazySorted(IEnumerable<KeyValuePair<Key, Value>> pairs)
            : this(pairs, Comparer<Key>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given key compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public LazySorted(IEnumerable<KeyValuePair<Key, Value>> pairs, Func<Key, Key, int> compare)
            : this(pairs, new SimpleComparer<Key>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public LazySorted(IEnumerable<KeyValuePair<Key, Value>> pairs, Func<KeyValuePair<Key, Value>, KeyValuePair<Key, Value>, int> compare)
            : this(pairs, new SimpleComparer<KeyValuePair<Key, Value>>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public LazySorted(IEnumerable<KeyValuePair<Key, Value>> pairs, IComparer<Key> cmp)
            : this(pairs, new KeyComparer<Key, Value>(cmp))
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public LazySorted(IEnumerable<KeyValuePair<Key, Value>> pairs, IComparer<KeyValuePair<Key, Value>> cmp)
            : base(
                () =>
                {
                    var items = new List<KeyValuePair<Key, Value>>(pairs);
                    items.Sort(cmp);
                    return new MapOf<Key, Value>(items);
                },
                false
            )
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public LazySorted(IDictionary<Key, Value> dict, IComparer<Key> cmp)
            : base(
                () =>
                {
                    var keys = new List<Key>(dict.Keys);
                    keys.Sort(cmp);
                    var result = new LazyDict<Key, Value>(
                        new Mapped<Key, IKvp<Key, Value>>(
                            key => new KvpOf<Key, Value>(key, () => dict[key]),
                            keys
                        )
                    );
                    return result;
                },
                false
            )
        { }
    }

    /// <summary>
    /// Sorts the given map with the given comparer
    /// </summary>
    /// <typeparam name="Value">Value Type of the Map</typeparam>
    public sealed class LazySorted<Value> : MapEnvelope<Value>
    {
        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        public LazySorted(IDictionary<string, Value> dict)
            : this(dict, Comparer<string>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public LazySorted(IDictionary<string, Value> dict, Func<string, string, int> compare)
            : this(dict, new SimpleComparer<string>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public LazySorted(IEnumerable<KeyValuePair<string, Value>> pairs)
            : this(pairs, Comparer<string>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given key compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public LazySorted(IEnumerable<KeyValuePair<string, Value>> pairs, Func<string, string, int> compare)
            : this(pairs, new SimpleComparer<string>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public LazySorted(IEnumerable<KeyValuePair<string, Value>> pairs, Func<KeyValuePair<string, Value>, KeyValuePair<string, Value>, int> compare)
            : this(pairs, new SimpleComparer<KeyValuePair<string, Value>>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public LazySorted(IEnumerable<KeyValuePair<string, Value>> pairs, IComparer<string> cmp)
            : this(pairs, new KeyComparer<string, Value>(cmp))
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public LazySorted(IEnumerable<KeyValuePair<string, Value>> pairs, IComparer<KeyValuePair<string, Value>> cmp)
            : base(
                () =>
                {
                    var items = new List<KeyValuePair<string, Value>>(pairs);
                    items.Sort(cmp);
                    return new MapOf<string, Value>(items);
                },
                false
            )
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public LazySorted(IDictionary<string, Value> dict, IComparer<string> cmp)
            : base(
                () =>
                {
                    var keys = new List<string>(dict.Keys);
                    keys.Sort(cmp);
                    var result = new LazyDict<string, Value>(
                        new Mapped<string, IKvp<string, Value>>(
                            key => new KvpOf<string, Value>(key, () => dict[key]),
                            keys
                        )
                    );
                    return result;
                },
                false
            )
        { }
    }

    /// <summary>
    /// Sorts the given map with the given comparer
    /// </summary>
    public sealed class LazySorted : MapEnvelope
    {
        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        public LazySorted(IDictionary<string, string> dict)
            : this(dict, Comparer<string>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public LazySorted(IDictionary<string, string> dict, Func<string, string, int> compare)
            : this(dict, new SimpleComparer<string>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public LazySorted(IEnumerable<KeyValuePair<string, string>> pairs)
            : this(pairs, Comparer<string>.Default)
        { }

        /// <summary>
        /// Sorts the given map with the given key compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public LazySorted(IEnumerable<KeyValuePair<string, string>> pairs, Func<string, string, int> compare)
            : this(pairs, new SimpleComparer<string>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public LazySorted(IEnumerable<KeyValuePair<string, string>> pairs, Func<KeyValuePair<string, string>, KeyValuePair<string, string>, int> compare)
            : this(pairs, new SimpleComparer<KeyValuePair<string, string>>(compare))
        { }

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public LazySorted(IEnumerable<KeyValuePair<string, string>> pairs, IComparer<string> cmp)
            : this(pairs, new KeyComparer<string, string>(cmp))
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public LazySorted(IEnumerable<KeyValuePair<string, string>> pairs, IComparer<KeyValuePair<string, string>> cmp)
            : base(
                () =>
                {
                    var items = new List<KeyValuePair<string, string>>(pairs);
                    items.Sort(cmp);
                    return new MapOf<string, string>(items);
                },
                false
            )
        { }

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public LazySorted(IDictionary<string, string> dict, IComparer<string> cmp)
            : base(
                () =>
                {
                    var keys = new List<string>(dict.Keys);
                    keys.Sort(cmp);
                    var result = new LazyDict<string, string>(
                        new Mapped<string, IKvp<string, string>>(
                            key => new KvpOf<string, string>(key, () => dict[key]),
                            keys
                        )
                    );
                    return result;
                },
                false
            )
        { }

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        public static IDictionary<Key, Value> New<Key, Value>(IDictionary<Key, Value> dict)
            => new LazySorted<Key, Value>(dict);

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public static IDictionary<Key, Value> New<Key, Value>(IDictionary<Key, Value> dict, Func<Key, Key, int> compare)
            => new LazySorted<Key, Value>(dict, compare);

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> pairs)
            => new LazySorted<Key, Value>(pairs);

        /// <summary>
        /// Sorts the given map with the given key compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> pairs, Func<Key, Key, int> compare)
            => new LazySorted<Key, Value>(pairs, compare);

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> pairs, Func<KeyValuePair<Key, Value>, KeyValuePair<Key, Value>, int> compare)
            => new LazySorted<Key, Value>(pairs, compare);

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> pairs, IComparer<Key> cmp)
            => new LazySorted<Key, Value>(pairs, cmp);

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> pairs, IComparer<KeyValuePair<Key, Value>> cmp)
            => new LazySorted<Key, Value>(pairs, cmp);

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public static IDictionary<Key, Value> New<Key, Value>(IDictionary<Key, Value> dict, IComparer<Key> cmp)
            => new LazySorted<Key, Value>(dict, cmp);

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        public static IDictionary<string, Value> New<Value>(IDictionary<string, Value> dict)
            => new LazySorted<Value>(dict);

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public static IDictionary<string, Value> New<Value>(IDictionary<string, Value> dict, Func<string, string, int> compare)
            => new LazySorted<Value>(dict, compare);

        /// <summary>
        /// Sorts the given map with the default comperator of the key
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        public static IDictionary<string, Value> New<Value>(IEnumerable<KeyValuePair<string, Value>> pairs)
            => new LazySorted<Value>(pairs);

        /// <summary>
        /// Sorts the given map with the given key compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two keys</param>
        public static IDictionary<string, Value> New<Value>(IEnumerable<KeyValuePair<string, Value>> pairs, Func<string, string, int> compare)
            => new LazySorted<Value>(pairs, compare);

        /// <summary>
        /// Sorts the given map with the given compare function
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="compare">Function to compare two elements</param>
        public static IDictionary<string, Value> New<Value>(IEnumerable<KeyValuePair<string, Value>> pairs, Func<KeyValuePair<string, Value>, KeyValuePair<string, Value>, int> compare)
            => new LazySorted<Value>(pairs, compare);

        /// <summary>
        /// Sorts the given map with the given key comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public static IDictionary<string, Value> New<Value>(IEnumerable<KeyValuePair<string, Value>> pairs, IComparer<string> cmp)
            => new LazySorted<Value>(pairs, cmp);

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="pairs">Map elements to be sorted</param>
        /// <param name="cmp">Comparer comparing elements</param>
        public static IDictionary<string, Value> New<Value>(IEnumerable<KeyValuePair<string, Value>> pairs, IComparer<KeyValuePair<string, Value>> cmp)
            => new LazySorted<Value>(pairs, cmp);

        /// <summary>
        /// Sorts the given map with the given comparer
        /// </summary>
        /// <param name="dict">Map to be sorted</param>
        /// <param name="cmp">Comparer comparing keys</param>
        public static IDictionary<string, Value> New<Value>(IDictionary<string, Value> dict, IComparer<string> cmp)
            => new LazySorted<Value>(dict, cmp);
    }
}
