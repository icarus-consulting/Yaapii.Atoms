using System;
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// A map whose contents can be changed.
    /// (Our normal objects are immutable)
    /// </summary>
    public sealed class MutableMap<TValue> : IDictionary<string, TValue>
    {
        private readonly IDictionary<string, TValue> map;

        /// <summary>
        /// A map whose contents can be changed.
        /// (Our normal objects are immutable)
        /// </summary>
        public MutableMap(IEnumerable<IKvp<TValue>> kvps) : this(() =>
        {
            var map = new Dictionary<string, TValue>();
            foreach (var kvp in kvps)
            {
                map[kvp.Key()] = kvp.Value();
            }
            return map;
        }
        )
        { }

        /// <summary>
        /// A map whose contents can be changed.
        /// (Our normal objects are immutable)
        /// </summary>
        public MutableMap(IEnumerable<KeyValuePair<string, TValue>> kvps) : this(() =>
        {
            var map = new Dictionary<string, TValue>();
            foreach (var kvp in kvps)
            {
                map[kvp.Key] = kvp.Value;
            }
            return map;
        }
        )
        { }

        /// <summary>
        /// A map whose contents can be changed.
        /// (Our normal objects are immutable)
        /// </summary>
        private MutableMap(Func<IDictionary<string, TValue>> map)
        {
            this.map = new MutableMap<string, TValue>(map);
        }

        public TValue this[string key] { get => Map()[key]; set => Map()[key] = value; }

        public ICollection<string> Keys => Map().Keys;

        public ICollection<TValue> Values => Map().Values;

        public int Count => Map().Keys.Count;

        public bool IsReadOnly => Map().Keys.IsReadOnly;

        public void Add(string key, TValue value)
        {
            Map().Add(key, value);
        }

        public void Add(KeyValuePair<string, TValue> item)
        {
            Map().Add(item);
        }

        public void Clear()
        {
            Map().Clear();
        }

        public bool Contains(KeyValuePair<string, TValue> item)
        {
            return Map().Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return Map().ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, TValue>[] array, int arrayIndex)
        {
            Map().CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        {
            return Map().GetEnumerator();
        }

        public bool Remove(string key)
        {
            return Map().Remove(key);
        }

        public bool Remove(KeyValuePair<string, TValue> item)
        {
            return Map().Remove(item);
        }

        public bool TryGetValue(string key, out TValue value)
        {
            return Map().TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Map().GetEnumerator();
        }

        private IDictionary<string, TValue> Map()
        {
            return this.map;
        }
    }

    /// <summary>
    /// A map whose contents can be changed.
    /// (Our normal objects are immutable)
    /// </summary>
    public sealed class MutableMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly IScalar<IDictionary<TKey, TValue>> map;

        /// <summary>
        /// A map whose contents can be changed.
        /// (Our normal objects are immutable)
        /// </summary>
        public MutableMap(IEnumerable<IKvp<TKey, TValue>> kvps) : this(() =>
        {
            var map = new Dictionary<TKey, TValue>();
            foreach (var kvp in kvps)
            {
                map[kvp.Key()] = kvp.Value();
            }
            return map;
        }
        )
        { }

        /// <summary>
        /// A map whose contents can be changed.
        /// (Our normal objects are immutable)
        /// </summary>
        public MutableMap(IEnumerable<KeyValuePair<TKey, TValue>> kvps) : this(() =>
        {
            var map = new Dictionary<TKey, TValue>();
            foreach (var kvp in kvps)
            {
                map[kvp.Key] = kvp.Value;
            }
            return map;
        }
        )
        { }

        /// <summary>
        /// A map whose contents can be changed.
        /// (Our normal objects are immutable)
        /// </summary>
        internal MutableMap(Func<IDictionary<TKey, TValue>> map)
        {
            this.map = new ScalarOf<IDictionary<TKey, TValue>>(map);
        }

        public TValue this[TKey key] { get => Map()[key]; set => Map()[key] = value; }

        public ICollection<TKey> Keys => Map().Keys;

        public ICollection<TValue> Values => Map().Values;

        public int Count => Map().Keys.Count;

        public bool IsReadOnly => Map().Keys.IsReadOnly;

        public void Add(TKey key, TValue value)
        {
            Map().Add(key, value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Map().Add(item);
        }

        public void Clear()
        {
            Map().Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return Map().Contains(item);
        }

        public bool ContainsKey(TKey key)
        {
            return Map().ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Map().CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Map().GetEnumerator();
        }

        public bool Remove(TKey key)
        {
            return Map().Remove(key);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Map().Remove(item);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return Map().TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Map().GetEnumerator();
        }

        private IDictionary<TKey, TValue> Map()
        {
            return this.map.Value();
        }

        /// <summary>
        /// A map whose contents can be changed.
        /// (Our normal objects are immutable)
        /// </summary>
        public static IDictionary<string, TValue> New<TValue>(IEnumerable<IKvp<TValue>> kvps)
            => new MutableMap<TValue>(kvps);

        /// <summary>
        /// A map whose contents can be changed.
        /// (Our normal objects are immutable)
        /// </summary>
        public static IDictionary<string, TValue> New<TValue>(IEnumerable<KeyValuePair<string, TValue>> kvps)
            => new MutableMap<TValue>(kvps);

        /// <summary>
        /// A map whose contents can be changed.
        /// (Our normal objects are immutable)
        /// </summary>
        public static IDictionary<TKey, TValue> New<TKey, TValue>(IEnumerable<IKvp<TKey, TValue>> kvps)
            => new MutableMap<TKey, TValue>(kvps);

        /// <summary>
        /// A map whose contents can be changed.
        /// (Our normal objects are immutable)
        /// </summary>
        public static IDictionary<TKey, TValue> New<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> kvps)
            => new MutableMap<TKey, TValue>(kvps);
    }
}
