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

using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Lookup
{
    /// <summary>
    /// Joined map.
    /// Since 9.9.2019
    /// </summary>
    public sealed class Joined : Map.Envelope
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IKvp kvp, IDictionary<string, string> origin) : this(
            new Map.Live(kvp), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IMapInput input, IDictionary<string, string> origin) : this(
            new Map.Live(input), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(params IDictionary<string, string>[] dicts) : this(
            new LiveEnumerable<IDictionary<string, string>>(dicts)
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IDictionary<string, string>> dicts) : base(
            () =>
            new LazyDict(
                new Enumerable.Joined<IKvp>(
                    new Mapped<IDictionary<string, string>, IEnumerable<IKvp>>(dict => 
                        new Mapped<KeyValuePair<string, string>, IKvp>(entry => 
                            new Kvp.Of(entry.Key, entry.Value),
                                dict
                            ),
                            dicts
                    )
                )
            )
        )
        { }

        /// <summary>
        /// A live joined map.
        /// </summary>
        public sealed class Live : Map.LiveEnvelope
        {
            /// <summary>
            /// Joined map.
            /// </summary>
            public Live(IKvp kvp, IDictionary<string, string> origin) : this(
                new Map.Live(kvp), origin
            )
            { }

            /// <summary>
            /// Joined map.
            /// </summary>
            public Live(IMapInput input, IDictionary<string, string> origin) : this(
                new Map.Live(input), origin
            )
            { }

            /// <summary>
            /// Joined map.
            /// </summary>
            public Live(params IDictionary<string, string>[] dicts) : this(
                new LiveEnumerable<IDictionary<string, string>>(dicts)
            )
            { }

            /// <summary>
            /// Joined map.
            /// </summary>
            public Live(IEnumerable<IDictionary<string, string>> dicts) : base(() =>
                new Map.Live(
                    new Enumerable.Joined<IKvp>(
                        new Mapped<IDictionary<string, string>, IEnumerable<IKvp>>(dict =>
                            new Mapped<KeyValuePair<string, string>, IKvp>(entry =>
                                new Kvp.Of(entry.Key, entry.Value),
                                    dict
                                ),
                                dicts
                        )
                    )
                )
            )
            { }
        }
    }

    /// <summary>
    /// Joined map.
    /// Since 9.9.2019
    /// </summary>
    public sealed class Joined<Value> : Map.Envelope<Value>
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IKvp<Value> kvp, IDictionary<string, Value> origin) : this(
            new Map.Of<Value>(kvp), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IMapInput<Value> input, IDictionary<string, Value> origin) : this(
            new Map.Of<Value>(input), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(params IDictionary<string, Value>[] dicts) : this(
            new LiveEnumerable<IDictionary<string, Value>>(dicts)
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IDictionary<string, Value>> dicts) : base(
            () =>
                new LazyDict<Value>(
                    new Enumerable.Joined<IKvp<Value>>(
                        new Mapped<IDictionary<string, Value>, IEnumerable<IKvp<Value>>>(
                            dict => new Mapped<KeyValuePair<string, Value>, IKvp<Value>>(
                                entry => new Kvp.Of<Value>(entry.Key, entry.Value),
                                dict
                            ),
                            dicts
                        )
                    )
                )
        )
        { }
    }

    /// <summary>
    /// Joined map.
    /// Since 9.9.2019
    /// </summary>
    public sealed class Joined<Key, Value> : Map.Envelope<Key, Value>
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IKvp<Key, Value> kvp, IDictionary<Key, Value> origin) : this(
            new Map.Of<Key, Value>(kvp), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IMapInput<Key, Value> input, IDictionary<Key, Value> origin) : this(
            new Map.Of<Key, Value>(input), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(params IDictionary<Key, Value>[] dicts) : this(
            new LiveEnumerable<IDictionary<Key, Value>>(dicts)
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public Joined(IEnumerable<IDictionary<Key, Value>> dicts) : base(
            () =>
            new LazyDict<Key, Value>(
                new Enumerable.Joined<IKvp<Key, Value>>(
                    new Mapped<IDictionary<Key, Value>, IEnumerable<IKvp<Key, Value>>>(
                        dict => new Mapped<KeyValuePair<Key, Value>, IKvp<Key, Value>>(
                            entry => new Kvp.Of<Key, Value>(entry.Key, entry.Value),
                            dict
                        ),
                        dicts
                    )
                )
            )
        )
        { }
    }
}
