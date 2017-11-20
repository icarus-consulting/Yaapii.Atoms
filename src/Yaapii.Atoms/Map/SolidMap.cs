using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map
{

    /// <summary>
    /// A map that is both threadsafe and sticky.
    /// </summary>
    /// <typeparam name="Key">type of key</typeparam>
    /// <typeparam name="Value">type of value</typeparam>
    public sealed class SolidMap<Key, Value> : MapEnvelope<Key, Value>
    {

        /// <summary>
        /// Makes a map from the given values.
        /// </summary>
        /// <param name="list"></param>
        public SolidMap(params KeyValuePair<Key, Value>[] list) : this(
            new EnumerableOf<KeyValuePair<Key, Value>>(list))
        { }

        /// <summary>
        /// Makes a map by merging the given values into the given dictionary.
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of values to merge</param>
        public SolidMap(IDictionary<Key, Value> map, params KeyValuePair<Key, Value>[] list) : this(map, new EnumerableOf<KeyValuePair<Key, Value>>(list))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">List of values</param>        
        public SolidMap(IEnumerable<KeyValuePair<Key, Value>> list) : this(new MapOf<Key, Value>(list))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="list">List of values</param>
        public SolidMap(IEnumerator<KeyValuePair<Key, Value>> list) : this(new EnumerableOf<KeyValuePair<Key, Value>>(list))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map">map to merge to</param>
        /// <param name="list">list of values to merge</param>
        public SolidMap(IDictionary<Key, Value> map, IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new MapOf<Key, Value>(map, list))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map"></param>
        public SolidMap(IDictionary<Key, Value> map) : base(
            () =>
                new SyncMap<Key, Value>(
                    new StickyMap<Key, Value>(map)))
        { }
    }
}