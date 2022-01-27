using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// A map which is empty.
    /// </summary>
    public sealed class EmptyMap : MapEnvelope
    {
        /// <summary>
        /// A map which is empty.
        /// </summary>
        public EmptyMap() : base(() => new MapOf(), false)
        { }
    }

    /// <summary>
    /// A map which is empty.
    /// </summary>
    public sealed class EmptyMap<Value> : MapEnvelope<Value>
    {
        /// <summary>
        /// A map which is empty.
        /// </summary>
        public EmptyMap() : base(() => new MapOf<Value>(), false)
        { }
    }

    /// <summary>
    /// A map which is empty.
    /// </summary>
    public sealed class EmptyMap<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// A map which is empty.
        /// </summary>
        public EmptyMap() : base(() => new MapOf<Key, Value>(), false)
        { }
    }
}
