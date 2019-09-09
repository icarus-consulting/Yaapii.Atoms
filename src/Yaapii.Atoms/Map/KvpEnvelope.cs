using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Dict
{
    /// <summary>
    /// Simplification of Kvp-class-building.
    /// Since 9.9.2019
    /// </summary>
    public class KvpEnvelope : IKvp
    {
        private readonly IKvp origin;

        public KvpEnvelope(IKvp origin)
        {
            this.origin = origin;
        }

        public string Key()
        {
            return this.origin.Key();
        }

        public string Value()
        {
            return this.origin.Value();
        }
    }

    /// <summary>
    /// Simplification of Kvp-class-building.
    /// Since 9.9.2019
    /// </summary>
    public class KvpEnvelope<TValue> : IKvp<TValue>
    {
        private readonly IKvp<TValue> origin;

        /// <summary>
        /// Simplification of KVP building
        /// </summary>
        public KvpEnvelope(IKvp<TValue> origin)
        {
            this.origin = origin;
        }

        public string Key()
        {
            return this.origin.Key();
        }

        public TValue Value()
        {
            return this.origin.Value();
        }
    }

    /// <summary>
    /// Simplification of Kvp-class-building.
    /// Since 9.9.2019
    /// </summary>
    public class KvpEnvelope<TKey, TValue> : IKvp<TKey, TValue>
    {
        private readonly IKvp<TKey, TValue> origin;

        /// <summary>
        /// Simplification of KVP building
        /// </summary>
        public KvpEnvelope(IKvp<TKey, TValue> origin)
        {
            this.origin = origin;
        }

        public TKey Key()
        {
            return this.origin.Key();
        }

        public TValue Value()
        {
            return this.origin.Value();
        }
    }
}
