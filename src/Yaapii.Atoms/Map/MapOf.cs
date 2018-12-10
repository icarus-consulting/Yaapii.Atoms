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
using System.Collections.ObjectModel;
using System.Text;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Makes a map.
    /// Careful: This will iterate all items in the dictionary on every method call. If you need performance, combine with <see cref="StickyMap{Key,Value}"/>
    /// </summary>
    /// <typeparam name="Key">Type of key</typeparam>
    /// <typeparam name="Value">Type of value</typeparam>
    public sealed class MapOf<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Makes a map from the given KeyValuePairs
        /// </summary>
        /// <param name="list">list of KeyValue pairs</param>
        public MapOf(params KeyValuePair<Key, Value>[] list) : this(
            new EnumerableOf<KeyValuePair<Key, Value>>(list))
        { }

        /// <summary>
        /// Makes a map from the given KeyValuePairs and appends them to the given Dictionary.
        /// </summary>
        /// <param name="src">source dictionary</param>
        /// <param name="list">KeyValuePairs to append</param>
        public MapOf(IDictionary<Key, Value> src, params KeyValuePair<Key, Value>[] list) : this(
            src,
            new EnumerableOf<KeyValuePair<Key, Value>>(list))
        { }

        /// <summary>
        /// Makes a map by merging the given KeyValuePairs to the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        public MapOf(IDictionary<Key, Value> src, IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new Joined<KeyValuePair<Key, Value>>(
                src,
                list
            ))
        { }

        /// <summary>
        /// A map by taking the given entries.
        /// </summary>
        /// <param name="entries">enumerator of KeyValuePairs</param>
        public MapOf(IEnumerator<KeyValuePair<Key, Value>> entries) : this(
            new EnumerableOf<KeyValuePair<Key, Value>>(entries))
        { }

        /// <summary>
        /// A map from the given function result
        /// </summary>
        /// <param name="fnc"></param>
        public MapOf(Func<IEnumerable<KeyValuePair<Key,Value>>> fnc) : this(
            new EnumerableOf<KeyValuePair<Key,Value>>(fnc))
        { }

        /// <summary>
        /// Makes a map by taking the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public MapOf(IEnumerable<KeyValuePair<Key, Value>> entries) : base(() =>
            {
                var temp = new Dictionary<Key, Value>();
                foreach (var entry in entries)
                {
                    temp[entry.Key] = entry.Value;
                }
                return temp;
            })
        { }

    }

    /// <summary>
    /// Makes a map.
    /// Careful: This will iterate all items in the dictionary on every method call. If you need performance, combine with <see cref="StickyMap{Key,Value}"/>
    /// </summary>
    /// <typeparam name="Source">Type of key</typeparam>
    /// <typeparam name="Key">Type of key</typeparam>
    /// <typeparam name="Value">Type of value</typeparam>
    public class MapOf<Source, Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Will make a map by taking the list of values, applying the key getting
        /// </summary>
        /// <param name="list">list of values</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public MapOf(IEnumerable<Source> list, Func<Source, Key> key, Func<Source, Value> value) : this(
            new Dictionary<Key, Value>(),
            list,
            item => new KeyValuePair<Key, Value>(key.Invoke(item), value.Invoke(item)))
        { }

        /// <summary>
        /// Makes a map from the given list and gets the KeyValuePair from it with the given function.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="entry"></param>
        public MapOf(IEnumerable<Source> list, Func<Source, KeyValuePair<Key, Value>> entry) : this(
            new Dictionary<Key,Value>(),
            new Mapped<Source, KeyValuePair<Key, Value>>(entry, list))
        { }

        /// <summary>
        /// Makes a map from the given dictionary and adds all the given keys
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        /// <param name="entry"></param>
        public MapOf(IDictionary<Key, Value> src, IEnumerable<Source> list, Func<Source, KeyValuePair<Key, Value>> entry) : this(
            src, new Enumerable.Mapped<Source, KeyValuePair<Key, Value>>(entry, list))
        { }

        /// <summary>
        /// Makes a map by merging the given KeyValuePairs into the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="entries"></param>
        public MapOf(IDictionary<Key, Value> src, IEnumerable<KeyValuePair<Key, Value>> entries) : base(
            () =>
            {
                Dictionary<Key, Value> temp = new Dictionary<Key, Value>(src);
                foreach (var entry in entries)
                {
                    temp[entry.Key] = entry.Value;
                }
                return temp;
            })
        { }
    }
}
