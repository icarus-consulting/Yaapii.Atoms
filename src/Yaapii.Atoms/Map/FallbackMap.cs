﻿// MIT License
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

using System;
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Map which can return a fallback value generated by a fallback function
    /// </summary>
    /// <typeparam name="Key">Key Type of the Map</typeparam>
    /// <typeparam name="Value">Value Type of the Map</typeparam>
    public sealed class FallbackMap<Key, Value> : IDictionary<Key, Value>
    {
        private readonly UnsupportedOperationException rejectWriteExc = new UnsupportedOperationException("Writing is not supported, it's a read-only map");

        private readonly ScalarOf<IDictionary<Key, Value>> origin;
        private readonly Func<Key, Value> fallback;

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallbackMap">Fallback map containing missing values</param>
        public FallbackMap(IDictionary<Key, Value> map, IDictionary<Key, Value> fallbackMap)
            : this(() => map, fallbackMap)
        { }

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallbackMap">Fallback map containing missing values</param>
        public FallbackMap(Func<IDictionary<Key, Value>> map, IDictionary<Key, Value> fallbackMap)
            : this(map, key => fallbackMap[key])
        { }

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallback">Fallback generating missing values</param>
        public FallbackMap(IDictionary<Key, Value> map, Func<Key, Value> fallback)
            : this(() => map, fallback)
        { }

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallback">Fallback generating missing values</param>
        public FallbackMap(Func<IDictionary<Key, Value>> map, Func<Key, Value> fallback)
        {
            this.origin = new ScalarOf<IDictionary<Key, Value>>(map);
            this.fallback = fallback;
        }

        public Value this[Key key]
        {
            get
            {
                if (!TryGetValue(key, out Value result))
                {
                    result = this.fallback(key);
                }

                return result;
            }
            set => throw this.rejectWriteExc;
        }

        public ICollection<Key> Keys => this.origin.Value().Keys;

        public ICollection<Value> Values => this.origin.Value().Values;

        public int Count => this.origin.Value().Count;

        public bool IsReadOnly => true;

        public void Add(Key key, Value value)
        {
            throw this.rejectWriteExc;
        }

        public void Add(KeyValuePair<Key, Value> item)
        {
            throw this.rejectWriteExc;
        }

        public void Clear()
        {
            throw this.rejectWriteExc;
        }

        public bool Contains(KeyValuePair<Key, Value> item)
        {
            return this.origin.Value().Contains(item);
        }

        public bool ContainsKey(Key key)
        {
            return this.origin.Value().ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            this.origin.Value().CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            return this.origin.Value().GetEnumerator();
        }

        public bool Remove(Key key)
        {
            throw this.rejectWriteExc;
        }

        public bool Remove(KeyValuePair<Key, Value> item)
        {
            throw this.rejectWriteExc;
        }

        public bool TryGetValue(Key key, out Value value)
        {
            return this.origin.Value().TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.origin.Value().GetEnumerator();
        }
    }

    /// <summary>
    /// Map which can return a fallback value generated by a fallback function
    /// </summary>
    /// <typeparam name="Value">Value Type of the Map</typeparam>
    public sealed class FallbackMap<Value> : IDictionary<string, Value>
    {
        private readonly UnsupportedOperationException rejectWriteExc = new UnsupportedOperationException("Writing is not supported, it's a read-only map");

        private readonly ScalarOf<IDictionary<string, Value>> origin;
        private readonly Func<string, Value> fallback;

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallbackMap">Fallback map containing missing values</param>
        public FallbackMap(IDictionary<string, Value> map, IDictionary<string, Value> fallbackMap)
            : this(() => map, fallbackMap)
        { }

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallbackMap">Fallback map containing missing values</param>
        public FallbackMap(Func<IDictionary<string, Value>> map, IDictionary<string, Value> fallbackMap)
            : this(map, key => fallbackMap[key])
        { }

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallback">Fallback generating missing values</param>
        public FallbackMap(IDictionary<string, Value> map, Func<string, Value> fallback)
            : this(() => map, fallback)
        { }

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallback">Fallback generating missing values</param>
        public FallbackMap(Func<IDictionary<string, Value>> map, Func<string, Value> fallback)
        {
            this.origin = new ScalarOf<IDictionary<string, Value>>(map);
            this.fallback = fallback;
        }

        public Value this[string key]
        {
            get
            {
                if (!TryGetValue(key, out Value result))
                {
                    result = this.fallback(key);
                }

                return result;
            }
            set => throw this.rejectWriteExc;
        }

        public ICollection<string> Keys => this.origin.Value().Keys;

        public ICollection<Value> Values => this.origin.Value().Values;

        public int Count => this.origin.Value().Count;

        public bool IsReadOnly => true;

        public void Add(string key, Value value)
        {
            throw this.rejectWriteExc;
        }

        public void Add(KeyValuePair<string, Value> item)
        {
            throw this.rejectWriteExc;
        }

        public void Clear()
        {
            throw this.rejectWriteExc;
        }

        public bool Contains(KeyValuePair<string, Value> item)
        {
            return this.origin.Value().Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return this.origin.Value().ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, Value>[] array, int arrayIndex)
        {
            this.origin.Value().CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, Value>> GetEnumerator()
        {
            return this.origin.Value().GetEnumerator();
        }

        public bool Remove(string key)
        {
            throw this.rejectWriteExc;
        }

        public bool Remove(KeyValuePair<string, Value> item)
        {
            throw this.rejectWriteExc;
        }

        public bool TryGetValue(string key, out Value value)
        {
            return this.origin.Value().TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.origin.Value().GetEnumerator();
        }
    }

    /// <summary>
    /// Map which can return a fallback value generated by a fallback function
    /// </summary>
    public sealed class FallbackMap : IDictionary<string, string>
    {
        private readonly UnsupportedOperationException rejectWriteExc = new UnsupportedOperationException("Writing is not supported, it's a read-only map");

        private readonly ScalarOf<IDictionary<string, string>> origin;
        private readonly Func<string, string> fallback;

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallbackMap">Fallback map containing missing values</param>
        public FallbackMap(IDictionary<string, string> map, IDictionary<string, string> fallbackMap)
            : this(() => map, fallbackMap)
        { }

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallbackMap">Fallback map containing missing values</param>
        public FallbackMap(Func<IDictionary<string, string>> map, IDictionary<string, string> fallbackMap)
            : this(map, key => fallbackMap[key])
        { }

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallback">Fallback generating missing values</param>
        public FallbackMap(IDictionary<string, string> map, Func<string, string> fallback)
            : this(() => map, fallback)
        { }

        /// <summary>
        /// Map which can return a fallback value generated by a fallback function
        /// </summary>
        /// <param name="map">Map returning existing values</param>
        /// <param name="fallback">Fallback generating missing values</param>
        public FallbackMap(Func<IDictionary<string, string>> map, Func<string, string> fallback)
        {
            this.origin = new ScalarOf<IDictionary<string, string>>(map);
            this.fallback = fallback;
        }

        public string this[string key]
        {
            get
            {
                if (!TryGetValue(key, out string result))
                {
                    result = this.fallback(key);
                }

                return result;
            }
            set => throw this.rejectWriteExc;
        }

        public ICollection<string> Keys => this.origin.Value().Keys;

        public ICollection<string> Values => this.origin.Value().Values;

        public int Count => this.origin.Value().Count;

        public bool IsReadOnly => true;

        public void Add(string key, string value)
        {
            throw this.rejectWriteExc;
        }

        public void Add(KeyValuePair<string, string> item)
        {
            throw this.rejectWriteExc;
        }

        public void Clear()
        {
            throw this.rejectWriteExc;
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return this.origin.Value().Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return this.origin.Value().ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            this.origin.Value().CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return this.origin.Value().GetEnumerator();
        }

        public bool Remove(string key)
        {
            throw this.rejectWriteExc;
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            throw this.rejectWriteExc;
        }

        public bool TryGetValue(string key, out string value)
        {
            return this.origin.Value().TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.origin.Value().GetEnumerator();
        }
    }
}
