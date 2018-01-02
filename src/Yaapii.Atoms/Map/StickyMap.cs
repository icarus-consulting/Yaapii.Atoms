using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Makes a sticky map.
    /// </summary>
    /// <typeparam name="Key">type of Key</typeparam>
    /// <typeparam name="Value">type of Value</typeparam>
    public sealed class StickyMap<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// Makes a map from the given values.
        /// </summary>
        /// <param name="list"></param>
        public StickyMap(params KeyValuePair<Key, Value>[] list) : this(
            new EnumerableOf<KeyValuePair<Key, Value>>(list))
        { }

        /// <summary>
        /// Makes a map by merging the given values into the given dictionary.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of values to merge</param>
        public StickyMap(IDictionary<Key, Value> map, params KeyValuePair<Key, Value>[] list) : this(map, new EnumerableOf<KeyValuePair<Key, Value>>(list))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">List of values</param>        
        public StickyMap(IEnumerable<KeyValuePair<Key, Value>> list) : this(new MapOf<Key, Value>(list))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">List of values</param>
        public StickyMap(IEnumerator<KeyValuePair<Key, Value>> list) : this(new EnumerableOf<KeyValuePair<Key, Value>>(list))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of values to merge</param>
        public StickyMap(IDictionary<Key, Value> map, IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new MapOf<Key, Value>(map, list))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map">the map</param>
        public StickyMap(IDictionary<Key, Value> map) : base(
            new StickyScalar<IDictionary<Key, Value>>(
                () =>
                {
                    Dictionary<Key, Value> temp = new Dictionary<Key, Value>();
                    foreach (var kvp in map)
                    {
                        temp[kvp.Key] = kvp.Value;
                    }
                    return temp;
                }))
        { }
    }

    /// <summary>
    /// Makes a sticky map.
    /// </summary>
    /// <typeparam name="Source">Type of source values</typeparam>
    /// <typeparam name="Key">Type of Key</typeparam>
    /// <typeparam name="Value">Type of Value</typeparam>
    public class StickyMap<Source, Key, Value> : MapEnvelope<Key,Value>
    {
        /// <summary>
        /// Makes a map by generating new entries from the list of values and the given mapping functions.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of values to merge</param>
        /// <param name="key">func to retrieve key</param>
        /// <param name="value">func to retrieve value</param>
        public StickyMap(IDictionary<Key, Value> map, IEnumerable<Source> list, Func<Source, Key> key, Func<Source, Value> value) : this(
                map, list,
                item => new KeyValuePair<Key, Value>(key.Invoke(item), value.Invoke(item))
            )
        { }

        /// <summary>
        /// Makes a sticky map by getting key and value from the given function.
        /// </summary>
        /// <param name="list">list of values</param>
        /// <param name="key">function to retrieve key</param>
        /// <param name="value">function to retrieve value</param>
        public StickyMap(IEnumerable<Source> list, Func<Source, Key> key, Func<Source, Value> value) : this(
            list,
            item => new KeyValuePair<Key, Value>(key.Invoke(item), value.Invoke(item)))
        { }

        /// <summary>
        /// Makes a sticky map by retrieving key and value from the list of values using the given function.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="entry"></param>
        public StickyMap(IEnumerable<Source> list, Func<Source, KeyValuePair<Key, Value>> entry) : this(
            new Dictionary<Key, Value>(),
            new Mapped<Source, KeyValuePair<Key, Value>>(entry, list))
        { }

        /// <summary>
        /// Extends a map by merging given map and value list while retrieving the entry using the given function.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="list"></param>
        /// <param name="entry"></param>
        public StickyMap(IDictionary<Key, Value> map, IEnumerable<Source> list, Func<Source, KeyValuePair<Key, Value>> entry) : this(
            map, 
            new Mapped<Source, KeyValuePair<Key,Value>>(entry, list))
        { }

        /// <summary>
        /// Makes a sticky map by merging the given map and values together.
        /// </summary>
        /// <param name="map"></param>
        /// <param name="list"></param>
        private StickyMap(IDictionary<Key, Value> map, IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new MapOf<Key, Value>(map, list))
        { }

        /// <summary>
        /// private ctor
        /// </summary>
        /// <param name="map"></param>
        private StickyMap(IDictionary<Key, Value> map) : base(
            new StickyScalar<IDictionary<Key, Value>>(
            () =>
            {
                var temp = new Dictionary<Key, Value>();
                foreach (var kvp in map)
                {
                    temp[kvp.Key] = kvp.Value;
                }
                return temp;
            }))
        { }
    }

}
