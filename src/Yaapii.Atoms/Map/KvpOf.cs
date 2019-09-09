using System;
using System.Collections.Generic;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Key-value pair made of strings.
    /// Since 9.9.2019
    /// </summary>
    public sealed class KvpOf : IKvp
    {
        private readonly Lazy<KeyValuePair<string, Func<string>>> entry;
        private readonly Lazy<string> value;

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(string key, string value) : this(
            new TextOf(key),
            value
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(string key, Func<string> value) : this(
            () => new KeyValuePair<string, Func<string>>(key, value)
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IText key, Func<string> value) : this(
            () => new KeyValuePair<string, Func<string>>(
                key.AsString(), value
            )
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IText key, string value) : this(
            () => new KeyValuePair<string, Func<string>>(
                key.AsString(),
                () => value
            )
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IScalar<KeyValuePair<string, string>> kvp)
        { }

        private KvpOf(Func<KeyValuePair<string, Func<string>>> kvp)
        {
            this.entry =
                new Lazy<KeyValuePair<string, Func<string>>>(
                    () => kvp.Invoke()
                );
            this.value = new Lazy<string>(() => this.entry.Value.Value.Invoke());
        }

        public string Key()
        {
            return this.entry.Value.Key;
        }

        public string Value()
        {
            return this.value.Value;
        }
    }

    /// <summary>
    /// Key-value pair matching a string to specified type value.
    /// </summary>
    public sealed class KvpOf<TValue> : IKvp<TValue>
    {
        private readonly Lazy<KeyValuePair<string, Func<TValue>>> entry;
        private readonly Lazy<TValue> value;

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(string key, TValue value) : this(
            new TextOf(key),
            value
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(string key, Func<TValue> value) : this(
            () => new KeyValuePair<string, Func<TValue>>(key, value)
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IText key, Func<TValue> value) : this(
            () => new KeyValuePair<string, Func<TValue>>(
                key.AsString(), value
            )
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IText key, TValue value) : this(
            () => new KeyValuePair<string, Func<TValue>>(
                key.AsString(),
                () => value
            )
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IScalar<KeyValuePair<string, TValue>> kvp)
        { }

        private KvpOf(Func<KeyValuePair<string, Func<TValue>>> kvp)
        {
            this.entry =
                new Lazy<KeyValuePair<string, Func<TValue>>>(
                    () => kvp.Invoke()
                );
            this.value = new Lazy<TValue>(() => this.entry.Value.Value.Invoke());
        }

        public string Key()
        {
            return this.entry.Value.Key;
        }

        public TValue Value()
        {
            return this.value.Value;
        }
    }

    /// <summary>
    /// Key-value pair matching a key type to specified type value.
    /// </summary>
    public sealed class KvpOf<TKey, TValue> : IKvp<TKey, TValue>
    {
        private readonly Lazy<KeyValuePair<TKey, Func<TValue>>> entry;
        private readonly Lazy<TValue> value;

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(TKey key, Func<TValue> value) : this(
            () => new KeyValuePair<TKey, Func<TValue>>(key, value)
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(TKey key, TValue value) : this(
            () => new KeyValuePair<TKey, Func<TValue>>(key, () => value)
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IScalar<KeyValuePair<TKey, TValue>> kvp)
        { }

        private KvpOf(Func<KeyValuePair<TKey, Func<TValue>>> kvp)
        {
            this.entry =
                new Lazy<KeyValuePair<TKey, Func<TValue>>>(
                    () => kvp.Invoke()
                );
            this.value = new Lazy<TValue>(() => this.entry.Value.Value.Invoke());
        }

        public TKey Key()
        {
            return this.entry.Value.Key;
        }

        public TValue Value()
        {
            return this.value.Value;
        }
    }
}
