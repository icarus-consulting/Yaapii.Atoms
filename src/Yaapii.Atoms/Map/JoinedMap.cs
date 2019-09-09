using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Joined map.
    /// Since 9.9.2019
    /// </summary>
    public sealed class JoinedMap : MapEnvelope
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public JoinedMap(IKvp kvp, IDictionary<string, string> origin) : this(
            new MapOf(kvp), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public JoinedMap(IMapInput input, IDictionary<string, string> origin) : this(
            new MapOf(input), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public JoinedMap(params IDictionary<string, string>[] dicts) : this(
            new LiveEnumerable<IDictionary<string, string>>(dicts)
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public JoinedMap(IEnumerable<IDictionary<string, string>> dicts) : base(
            () =>
                new LazyDict(
                    new Joined<IKvp>(
                        new Mapped<IDictionary<string, string>, IEnumerable<IKvp>>(
                            dict => new Mapped<KeyValuePair<string, string>, IKvp>(
                                entry => new KvpOf(entry.Key, entry.Value),
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
    public sealed class JoinedMap<Value> : MapEnvelope<Value>
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public JoinedMap(IKvp<Value> kvp, IDictionary<string, Value> origin) : this(
            new MapOf<Value>(kvp), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public JoinedMap(IMapInput<Value> input, IDictionary<string, Value> origin) : this(
            new MapOf<Value>(input), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public JoinedMap(params IDictionary<string, Value>[] dicts) : this(
            new LiveEnumerable<IDictionary<string, Value>>(dicts)
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public JoinedMap(IEnumerable<IDictionary<string, Value>> dicts) : base(
            () =>
                new LazyDict<Value>(
                    new Joined<IKvp<Value>>(
                        new Mapped<IDictionary<string, Value>, IEnumerable<IKvp<Value>>>(
                            dict => new Mapped<KeyValuePair<string, Value>, IKvp<Value>>(
                                entry => new KvpOf<Value>(entry.Key, entry.Value),
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
    public sealed class JoinedMap<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Joined map.
        /// </summary>
        public JoinedMap(IKvp<Key, Value> kvp, IDictionary<Key, Value> origin) : this(
            new MapOf<Key, Value>(kvp), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public JoinedMap(IMapInput<Key, Value> input, IDictionary<Key, Value> origin) : this(
            new MapOf<Key, Value>(input), origin
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public JoinedMap(params IDictionary<Key, Value>[] dicts) : this(
            new LiveEnumerable<IDictionary<Key, Value>>(dicts)
        )
        { }

        /// <summary>
        /// Joined map.
        /// </summary>
        public JoinedMap(IEnumerable<IDictionary<Key, Value>> dicts) : base(
            () =>
            new LazyDict<Key,Value>(
                new Joined<IKvp<Key, Value>>(
                    new Mapped<IDictionary<Key, Value>, IEnumerable<IKvp<Key, Value>>>(
                        dict => new Mapped<KeyValuePair<Key, Value>, IKvp<Key, Value>>(
                            entry => new KvpOf<Key, Value>(entry.Key, entry.Value),
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
