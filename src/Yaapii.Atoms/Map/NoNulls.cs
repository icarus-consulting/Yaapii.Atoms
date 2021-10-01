// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections;
using System.Collections.Generic;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// A decorator of map that tolerates no NULLs.
    /// </summary>
    /// <typeparam name="Key">type of key</typeparam>
    /// <typeparam name="Value">type of value</typeparam>
    public sealed class NoNulls<Key, Value> : IDictionary<Key, Value>
    {
        private readonly IDictionary<Key, Value> map;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="map">IDictionary</param>
        public NoNulls(IDictionary<Key, Value> map)
        {
            this.map = map;
        }

        /// <summary>
        /// Access a value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Value this[Key key]
        {
            get
            {
                new Error.FailNull(key, "key can't be null.").Go();
                var value = map[key];
                new Error.FailNull(value, $"Value returned by [{key}] is null.").Go();
                return map[key];
            }
            set
            {
                new Error.FailNull(key, "key can't be null.").Go();
                new Error.FailNull(value, "value can't be null.").Go();
                map[key] = value;
            }
        }

        /// <summary>
        /// Access all keys
        /// </summary>
        public ICollection<Key> Keys => map.Keys;

        /// <summary>
        /// Access all values
        /// </summary>
        public ICollection<Value> Values => map.Values;

        /// <summary>
        /// Count entries
        /// </summary>
        public int Count => map.Count;

        /// <summary>
        /// Gets a value indicating whether the map is read-only.
        /// </summary>
        public bool IsReadOnly => map.IsReadOnly;

        /// <summary>
        /// Adds an element with the provided key and value to the map
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(Key key, Value value)
        {
            new Error.FailNull(key, "key can't be null.").Go();
            new Error.FailNull(value, "value can't be null.").Go();
            map.Add(key, value);
        }

        /// <summary>
        /// Adds an element with the provided entry to the map
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<Key, Value> item)
        {
            new Error.FailNull(item.Key, "key can't be null.").Go();
            new Error.FailNull(item.Value, "value can't be null.").Go();
            map.Add(item);
        }

        /// <summary>
        /// Removes all elements from the map
        /// </summary>
        public void Clear()
        {
            map.Clear();
        }

        /// <summary>
        /// Test if map contains entry
        /// </summary>
        /// <param name="item">item to check</param>
        /// <returns>true if it contains the entry</returns>
        public bool Contains(KeyValuePair<Key, Value> item)
        {
            new Error.FailNull(item.Key, "key can't be null.").Go();
            new Error.FailNull(item.Value, "value can't be null.").Go();
            return map.Contains(item);
        }

        /// <summary>
        /// Test if map contains key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>true if it contains the key</returns>
        public bool ContainsKey(Key key)
        {
            new Error.FailNull(key, "key can't be null.").Go();
            return map.ContainsKey(key);
        }

        /// <summary>
        /// Copy this to an array
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">index to start</param>
        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            map.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// The enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            return map.GetEnumerator();
        }

        /// <summary>
        ///  Removes the entry with the specified key from the map
        /// </summary>
        public bool Remove(Key key)
        {
            return map.Remove(key);
        }

        /// <summary>
        /// Removes the first occurrence of a specific entry from the map
        /// </summary>
        public bool Remove(KeyValuePair<Key, Value> item)
        {
            return map.Remove(item);
        }

        /// <summary>
        /// Tries to get value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">target to store value</param>
        /// <returns>true if success</returns>
        public bool TryGetValue(Key key, out Value value)
        {
            new Error.FailNull(key, "key can't be null.").Go();
            var result = map.TryGetValue(key, out value);
            new Error.FailNull(value, $"Value returned by [{key}] is null.").Go();
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return map.GetEnumerator();
        }
    }
}