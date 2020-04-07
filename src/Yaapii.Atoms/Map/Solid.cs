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
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map
{

    /// <summary>
    /// A map that is both threadsafe and sticky.
    /// </summary>
    /// <typeparam name="Key">type of key</typeparam>
    /// <typeparam name="Value">type of value</typeparam>
    public sealed class Solid<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// A map from the given Tuple pairs.
        /// </summary>
        /// <param name="pairs">Pairs of mappings</param>
        public Solid(Tuple<Key, Value>[] pairs) : this(
            new LiveMany<Tuple<Key, Value>>(pairs)
        )
        { }

        /// <summary>
        /// A map from the given Tuple pairs.
        /// </summary>
        /// <param name="pairs">Pairs of mappings</param>
        public Solid(IEnumerable<Tuple<Key, Value>> pairs) : this(
            new Mapped<Tuple<Key, Value>, KeyValuePair<Key, Value>>(
                tpl => new KeyValuePair<Key, Value>(tpl.Item1, tpl.Item2),
                pairs
            )
        )
        { }

        /// <summary>
        /// Makes a map from the given values.
        /// </summary>
        /// <param name="list"></param>
        public Solid(params KeyValuePair<Key, Value>[] list) : this(
            new LiveMany<KeyValuePair<Key, Value>>(list)
        )
        { }

        /// <summary>
        /// Makes a map by merging the given values into the given dictionary.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of values to merge</param>
        public Solid(IDictionary<Key, Value> map, params KeyValuePair<Key, Value>[] list) : this(
            map, 
            new LiveMany<KeyValuePair<Key, Value>>(list)
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">List of values</param>        
        public Solid(IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new LiveMap<Key, Value>(() =>
                new MapOf<Key, Value>(list)
            )
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">List of values</param>
        public Solid(IEnumerator<KeyValuePair<Key, Value>> list) : this(
            new LiveMany<KeyValuePair<Key, Value>>(() => list)
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of values to merge</param>
        public Solid(IDictionary<Key, Value> map, IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new LiveMap<Key, Value>(() =>
                new MapOf<Key, Value>(
                    new Enumerable.Joined<KeyValuePair<Key, Value>>(map, list)
                )
            )
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map"></param>
        public Solid(IDictionary<Key, Value> map) : base(
            () =>
                new Synced<Key, Value>(
                    new MapOf<Key, Value>(map)
                ),
            false
        )
        { }
    }
}