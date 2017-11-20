using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// A map that is threadsafe.
    /// </summary>
    /// <typeparam name="Key">type of key</typeparam>
    /// <typeparam name="Value">type of value</typeparam>
    public sealed class SyncMap<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="list"></param>
        public SyncMap(KeyValuePair<Key, Value>[] list) : this(
            new EnumerableOf<KeyValuePair<Key, Value>>(list))
        { }

        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of entries to merge</param>
        public SyncMap(Dictionary<Key, Value> map, KeyValuePair<Key, Value>[] list) : this(
            map,
            new EnumerableOf<KeyValuePair<Key, Value>>(list))
        { }

        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="list">list of entries</param>
        public SyncMap(IEnumerable<KeyValuePair<Key, Value>> list) : this(new MapOf<Key, Value>(list))
        { }

        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="list">list of entries</param>
        public SyncMap(IEnumerator<KeyValuePair<Key, Value>> list) : this(
            new EnumerableOf<KeyValuePair<Key, Value>>(
                () => list))
        { }

        /// <summary>
        /// A merged map that is treadsafe.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">items to merge</param>
        public SyncMap(IDictionary<Key, Value> map, IEnumerable<KeyValuePair<Key, Value>> list) : this(new MapOf<Key, Value>(map, list))
        { }

        /// <summary>
        /// A merged map that is threadsafe.
        /// </summary>
        /// <param name="map">Map to make threadsafe</param>
        public SyncMap(IDictionary<Key, Value> map) : base(
                new SyncScalar<IDictionary<Key, Value>>(
                    new ScalarOf<IDictionary<Key, Value>>(
                    () => new ConcurrentDictionary<Key, Value>(map))))
        { }
    }

    /// <summary>
    /// Makes a threadsafe map
    /// </summary>
    /// <typeparam name="Source">source value type</typeparam>
    /// <typeparam name="Key">type of key</typeparam>
    /// <typeparam name="Value">type of value</typeparam>
    public sealed class SyncMap<Source, Key, Value> : MapEnvelope<Key,Value>
    {
        /// <summary>
        /// Makes a threadsafe map.
        /// </summary>
        /// <param name="map">source map to merge to</param>
        /// <param name="list">list of values to merge</param>
        /// <param name="key">func to get the key</param>
        /// <param name="value">func to get the value</param>
        public SyncMap(IDictionary<Key, Value> map, IEnumerable<Source> list, Func<Source, Key> key, Func<Source, Value> value) : this(
                map, 
                list,
                item => new KeyValuePair<Key, Value>(key.Invoke(item), value.Invoke(item))
            )
        { }

        /// <summary>
        /// Makes a threadsafe map.
        /// </summary>
        /// <param name="list">list of values to merge</param>
        /// <param name="key">func to get the key</param>
        /// <param name="value">func to get the value</param>
        public SyncMap(IEnumerable<Source> list, Func<Source, Key> key, Func<Source, Value> value) :
            this(list, item => new KeyValuePair<Key, Value>(key.Invoke(item), value.Invoke(item)))
        { }

        /// <summary>
        /// Makes a threadsafe map.
        /// </summary>
        /// <param name="list">list of values to merge</param>
        /// <param name="entry">func to get the entry</param>
        public SyncMap(IEnumerable<Source> list, Func<Source, KeyValuePair<Key, Value>> entry) : this(
            new Mapped<Source, KeyValuePair<Key,Value>>(list, entry))
        { }

        /// <summary>
        /// Makes a threadsafe map.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="list"></param>
        /// <param name="entry"></param>
        public SyncMap(IDictionary<Key, Value> map, IEnumerable<Source> list, Func<Source, KeyValuePair<Key, Value>> entry) : this(
            map, 
            new Mapped<Source,KeyValuePair<Key, Value>>(list, entry))
        { }

        /// <summary>
        /// A merged map that is treadsafe.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">items to merge</param>
        public SyncMap(IDictionary<Key, Value> map, IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new MapOf<Key, Value>(map, list))
        { }

        /// <summary>
        /// Makes a map that is threadsafe.
        /// </summary>
        /// <param name="list">list of entries</param>
        public SyncMap(IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new MapOf<Key, Value>(list))
        { }

        /// <summary>
        /// A merged map that is threadsafe.
        /// </summary>
        /// <param name="map">Map to make threadsafe</param>
        public SyncMap(IDictionary<Key, Value> map) : base(
                new SyncScalar<IDictionary<Key, Value>>(
                    new ScalarOf<IDictionary<Key, Value>>(
                    () => new ConcurrentDictionary<Key, Value>(map))))
        { }
    }

}