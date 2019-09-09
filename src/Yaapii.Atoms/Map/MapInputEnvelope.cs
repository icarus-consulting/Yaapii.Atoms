using System;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Simplified MapInput building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapInputEnvelope : IMapInput
    {
        private readonly Func<IDictionary<string, string>, IDictionary<string, string>> apply;

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IMapInput> input) : this(
            dict => new JoinedMap(dict, new MapOf(input()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IScalar<IMapInput> input) : this(
            dict => new JoinedMap(dict, new MapOf(input.Value()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(params IKvp[] kvps) : this(
            new LiveEnumerable<IKvp>(kvps)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IEnumerable<IKvp> kvps) : this(
            input => new JoinedMap(input, new LazyDict(kvps))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IDictionary<string, string> dict) : this(
            input => new JoinedMap(input, dict)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IDictionary<string, string>, IDictionary<string, string>> apply)
        {
            this.apply = apply;
        }

        /// <summary>
        /// Apply this input to a map.
        /// </summary>
        public IDictionary<string, string> Apply(IDictionary<string, string> dict)
        {
            return this.apply.Invoke(dict);
        }

        /// <summary>
        /// Return this as IMapInput.
        /// </summary>
        /// <returns></returns>
        public IMapInput Self()
        {
            return this;
        }
    }

    /// <summary>
    /// Simplified MapInput building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapInputEnvelope<Value> : IMapInput<Value>
    {
        private readonly Func<IDictionary<string, Value>, IDictionary<string, Value>> apply;

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IMapInput<Value>> input) : this(
            dict => new JoinedMap<Value>(dict, new MapOf<Value>(input()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IScalar<IMapInput<Value>> input) : this(
            dict => new JoinedMap<Value>(dict, new MapOf<Value>(input.Value()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(params IKvp<Value>[] kvps) : this(
            new LiveEnumerable<IKvp<Value>>(kvps)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IEnumerable<IKvp<Value>> kvps) : this(
            input => new JoinedMap<Value>(input, new LazyDict<Value>(kvps))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IDictionary<string, Value> dict) : this(
            input => new JoinedMap<Value>(input, dict)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IDictionary<string, Value>, IDictionary<string, Value>> apply)
        {
            this.apply = apply;
        }

        /// <summary>
        /// Apply this input to a map.
        /// </summary>
        public IDictionary<string, Value> Apply(IDictionary<string, Value> dict)
        {
            return this.apply.Invoke(dict);
        }

        /// <summary>
        /// Return this as IMapInput.
        /// </summary>
        /// <returns></returns>
        public IMapInput<Value> Self()
        {
            return this;
        }
    }

    /// <summary>
    /// Simplified MapInput building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapInputEnvelope<Key, Value> : IMapInput<Key, Value>
    {
        private readonly Func<IDictionary<Key, Value>, IDictionary<Key, Value>> apply;

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IMapInput<Key, Value>> input) : this(
            dict => new JoinedMap<Key, Value>(dict, new MapOf<Key, Value>(input()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IScalar<IMapInput<Key, Value>> input) : this(
            dict => new JoinedMap<Key, Value>(dict, new MapOf<Key, Value>(input.Value()))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(params IKvp<Key, Value>[] kvps) : this(
            new LiveEnumerable<IKvp<Key, Value>>(kvps)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IEnumerable<IKvp<Key, Value>> kvps) : this(
            input => new JoinedMap<Key, Value>(input, new LazyDict<Key, Value>(kvps))
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(IDictionary<Key, Value> dict) : this(
            input => new JoinedMap<Key, Value>(input, dict)
        )
        { }

        /// <summary>
        /// Simplified DictInput building.
        /// </summary>
        public MapInputEnvelope(Func<IDictionary<Key, Value>, IDictionary<Key, Value>> apply)
        {
            this.apply = apply;
        }

        /// <summary>
        /// Apply this input to a map.
        /// </summary>
        public IDictionary<Key, Value> Apply(IDictionary<Key, Value> dict)
        {
            return this.apply.Invoke(dict);
        }

        /// <summary>
        /// Return this as IMapInput.
        /// </summary>
        /// <returns></returns>
        public IMapInput<Key, Value> Self()
        {
            return this;
        }
    }
}
