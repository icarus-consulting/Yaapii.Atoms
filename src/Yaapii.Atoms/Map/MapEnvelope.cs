using System;
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
    public class MapEnvelope<Key, Value> : IDictionary<Key, Value>
    {
        private readonly IScalar<ReadOnlyDictionary<Key, Value>> _map;
        private readonly UnsupportedOperationException _readonly = new UnsupportedOperationException("Not supported, it's a read-only map");

        public MapEnvelope(Func<IDictionary<Key, Value>> fnc) : this(
            new ScalarOf<IDictionary<Key, Value>>(fnc))
        { }

        public MapEnvelope(IScalar<IDictionary<Key, Value>> scalar)
        {
            this._map = new ScalarOf<ReadOnlyDictionary<Key, Value>>(() => new ReadOnlyDictionary<Key, Value>(scalar.Value()));
        }

        public Value this[Key key] { get { return _map.Value()[key]; } set { throw this._readonly; } }

        public ICollection<Key> Keys => _map.Value().Keys;

        public ICollection<Value> Values => _map.Value().Values;

        public int Count => _map.Value().Count;

        public bool IsReadOnly => true;

        public void Add(Key key, Value value)
        {
            throw this._readonly;
        }

        public void Add(KeyValuePair<Key, Value> item)
        {
            throw this._readonly;
        }

        public void Clear()
        {
            throw this._readonly;
        }

        public bool Contains(KeyValuePair<Key, Value> item)
        {
            return this._map.Value().ContainsKey(item.Key) && this._map.Value()[item.Key].Equals(item.Value);
        }

        public bool ContainsKey(Key key)
        {
            return this._map.Value().ContainsKey(key);
        }

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

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            return this._map.Value().GetEnumerator();
        }

        public bool Remove(Key key)
        {
            throw this._readonly;
        }

        public bool Remove(KeyValuePair<Key, Value> item)
        {
            throw this._readonly;
        }

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
