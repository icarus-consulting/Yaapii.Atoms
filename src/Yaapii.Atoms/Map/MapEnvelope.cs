﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Envelope of map.
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    public class MapEnvelope<Key, Value> : IDictionary<Key, Value>
    {
        private readonly IScalar<ReadOnlyDictionary<Key, Value>> _map;
        private readonly UnsupportedOperationException _readonly = new UnsupportedOperationException("Not supported, it's a read-only map");

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">func returning IDictionary</param>
        public MapEnvelope(Func<IDictionary<Key, Value>> fnc) : this(
            new ScalarOf<IDictionary<Key, Value>>(fnc))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="scalar">Scalar of IDictionary</param>
        public MapEnvelope(IScalar<IDictionary<Key, Value>> scalar)
        {
            this._map = new ScalarOf<ReadOnlyDictionary<Key, Value>>(() => new ReadOnlyDictionary<Key, Value>(scalar.Value()));
        }

        /// <summary>
        /// Access a value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Value this[Key key] { get { return _map.Value()[key]; } set { throw this._readonly; } }

        /// <summary>
        /// Access all keys
        /// </summary>
        public ICollection<Key> Keys => _map.Value().Keys;

        /// <summary>
        /// Access all values
        /// </summary>
        public ICollection<Value> Values => _map.Value().Values;

        /// <summary>
        /// Count entries
        /// </summary>
        public int Count => _map.Value().Count;

        /// <summary>
        /// Yes its readonly
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(Key key, Value value)
        {
            throw this._readonly;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<Key, Value> item)
        {
            throw this._readonly;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        public void Clear()
        {
            throw this._readonly;
        }

        /// <summary>
        /// Test if map contains entry
        /// </summary>
        /// <param name="item">item to check</param>
        /// <returns>true if it contains</returns>
        public bool Contains(KeyValuePair<Key, Value> item)
        {
            return this._map.Value().ContainsKey(item.Key) && this._map.Value()[item.Key].Equals(item.Value);
        }

        /// <summary>
        /// Test if map contains key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(Key key)
        {
            return this._map.Value().ContainsKey(key);
        }

        /// <summary>
        /// Copy this to an array
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">index to start</param>
        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            if (arrayIndex > this._map.Value().Count)
            {
                throw
                    new ArgumentOutOfRangeException(
                        new FormattedText(
                            "arrayIndex {0} is higher than the item count in the map {1}.",
                            arrayIndex,
                            this._map.Value().Count
                        ).AsString());
            }

            new ListOf<KeyValuePair<Key, Value>>(this).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// The enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            return this._map.Value().GetEnumerator();
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(Key key)
        {
            throw this._readonly;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<Key, Value> item)
        {
            throw this._readonly;
        }

        /// <summary>
        /// Tries to get value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">target to store value</param>
        /// <returns>true if success</returns>
        public bool TryGetValue(Key key, out Value value)
        {
            throw this._readonly;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}