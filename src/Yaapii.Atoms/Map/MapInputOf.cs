using System.Collections.Generic;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// MapInput from key-value pairs.
    /// Since 9.9.2019
    /// </summary>
    public sealed class MapInputOf : MapInputEnvelope
    {
        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(params IKvp[] kvps) : this(new List<IKvp>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(IEnumerable<IKvp> kvps) : base(kvps)
        { }
    }

    /// <summary>
    /// MapInput from key-value pairs.
    /// Since 9.9.2019
    /// </summary>
    public sealed class MapInputOf<Value> : MapInputEnvelope<Value>
    {
        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(params IKvp<Value>[] kvps) : this(new List<IKvp<Value>>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(IEnumerable<IKvp<Value>> kvps) : base(kvps)
        { }
    }

    /// <summary>
    /// MapInput from key-value pairs.
    /// Since 9.9.2019
    /// </summary>
    public sealed class MapInputOf<Key, Value> : MapInputEnvelope<Key, Value>
    {
        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(params IKvp<Key, Value>[] kvps) : this(new List<IKvp<Key, Value>>(kvps))
        { }

        /// <summary>
        /// MapInput from key-value pairs.
        /// </summary>
        public MapInputOf(IEnumerable<IKvp<Key, Value>> kvps) : base(kvps)
        { }
    }
}
