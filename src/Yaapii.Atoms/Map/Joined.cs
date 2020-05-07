// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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

using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Joined map.
    /// Since 9.9.2019
    /// </summary>
    public sealed class Joined : MapEnvelope
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IKvp kvp, IDictionary<string, string> origin) : this(
            new LiveMap(() =>
                new LiveMany<IKvp>(kvp)
            ),
            origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IMapInput input, IDictionary<string, string> origin) : this(
            new LiveMap(() =>
                new MapOf(input)
            ),
            origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(params IDictionary<string, string>[] dicts) : this(
            new LiveMany<IDictionary<string, string>>(dicts)
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IDictionary<string, string>> dicts, bool rejectBuildingAllValues = true) : base(
            () =>
            new LazyDict(
                new Enumerable.Joined<IKvp>(
                    new Mapped<IDictionary<string, string>, IEnumerable<IKvp>>(dict =>
                        new Mapped<KeyValuePair<string, string>, IKvp>(entry =>
                            new KvpOf(entry.Key, entry.Value),
                                dict
                            ),
                            dicts
                    )
                )
            ),
            rejectBuildingAllValues
        )
        { }
    }

    /// <summary>
    /// Joined map.
    /// Since 9.9.2019
    /// </summary>
    public sealed class Joined<Value> : MapEnvelope<Value>
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IKvp<Value> kvp, IDictionary<string, Value> origin, bool live = false) : this(
            live,
            new MapOf<Value>(kvp), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IMapInput<Value> input, IDictionary<string, Value> origin, bool live = false) : this(
            live,
            new MapOf<Value>(input), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(params IDictionary<string, Value>[] dicts) : this(
            false,
            dicts
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(bool live, params IDictionary<string, Value>[] dicts) : this(
            new LiveMany<IDictionary<string, Value>>(dicts),
            live
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IDictionary<string, Value>> dicts, bool live = false) : base(
            () =>
                new LazyDict<Value>(
                    new Enumerable.Joined<IKvp<Value>>(
                        new Mapped<IDictionary<string, Value>, IEnumerable<IKvp<Value>>>(
                            dict => new Mapped<KeyValuePair<string, Value>, IKvp<Value>>(
                                entry => new KvpOf<Value>(entry.Key, entry.Value),
                                dict
                            ),
                            dicts
                        )
                    )
                ),
            live
        )
        { }
    }

    /// <summary>
    /// Joined map.
    /// Since 9.9.2019
    /// </summary>
    public sealed class Joined<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IKvp<Key, Value> kvp, IDictionary<Key, Value> origin, bool live = false) : this(
            live,
            new MapOf<Key, Value>(kvp), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IMapInput<Key, Value> input, IDictionary<Key, Value> origin, bool live = false) : this(
            live,
            new MapOf<Key, Value>(input), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(bool live, params IDictionary<Key, Value>[] dicts) : this(
            new LiveMany<IDictionary<Key, Value>>(dicts),
            live
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(params IDictionary<Key, Value>[] dicts) : this(
            new LiveMany<IDictionary<Key, Value>>(dicts),
            false
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IDictionary<Key, Value>> dicts, bool live = false) : base(
            () =>
                new LazyDict<Key, Value>(
                new Enumerable.Joined<IKvp<Key, Value>>(
                    new Mapped<IDictionary<Key, Value>, IEnumerable<IKvp<Key, Value>>>(
                        dict => new Mapped<KeyValuePair<Key, Value>, IKvp<Key, Value>>(
                            entry => new KvpOf<Key, Value>(entry.Key, entry.Value),
                            dict
                        ),
                        dicts
                    )
                )
            ),
            live
        )
        { }
    }
}
