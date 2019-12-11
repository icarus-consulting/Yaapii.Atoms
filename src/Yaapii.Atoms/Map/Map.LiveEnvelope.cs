﻿// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Lookup
{
    public partial class Map
    {
        /// <summary>
        /// Simplified map building.
        /// You must understand, that this map will build every time when any method is called.
        /// If you do not want this, use <see cref="Map.Of"/>
        /// Since 9.9.2019
        /// </summary>
        public abstract class LiveEnvelope : IDictionary<string, string>
        {
            private readonly UnsupportedOperationException rejectWriteExc = new UnsupportedOperationException("Writing is not supported, it's a read-only map");
            private readonly Func<IDictionary<string, string>> origin;

            /// <summary>
            /// Simplified map building.
            /// </summary>
            public LiveEnvelope(Func<IDictionary<string, string>> origin)
            {
                this.origin = new Func<IDictionary<string, string>>(origin);
            }

            public string this[string key] { get => this.origin()[key]; set => throw this.rejectWriteExc; }

            public ICollection<string> Keys => this.origin().Keys;

            public ICollection<string> Values => this.origin().Values;

            public int Count => this.origin().Count;

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
                return this.origin().Contains(item);
            }

            public bool ContainsKey(string key)
            {
                return this.origin().ContainsKey(key);
            }

            public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
            {
                this.origin().CopyTo(array, arrayIndex);
            }

            public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
            {
                return this.origin().GetEnumerator();
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
                value = default(string);
                var result = this.origin().TryGetValue(key, out value);
                return result;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.origin().GetEnumerator();
            }
        }

        /// <summary>
        /// Simplified live map building.
        /// You must understand, that this map will build every time when any method is called.
        /// If you do not want this, use <see cref="Map.Of"/>
        /// Since 9.9.2019
        /// </summary>
        public abstract class LiveEnvelope<Value> : IDictionary<string, Value>
        {
            private readonly UnsupportedOperationException rejectWriteExc = new UnsupportedOperationException("Writing is not supported, it's a read-only map");
            private readonly Func<IDictionary<string, Value>> origin;

            /// <summary>
            /// Simplified map building.
            /// </summary>
            public LiveEnvelope(Func<IDictionary<string, Value>> origin)
            {
                this.origin = origin;
            }

            public Value this[string key] { get => this.origin()[key]; set => throw this.rejectWriteExc; }

            public ICollection<string> Keys => this.origin().Keys;

            public ICollection<Value> Values => this.origin().Values;

            public int Count => this.origin().Count;

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
                return this.origin().Contains(item);
            }

            public bool ContainsKey(string key)
            {
                return this.origin().ContainsKey(key);
            }

            public void CopyTo(KeyValuePair<string, Value>[] array, int arrayIndex)
            {
                this.origin().CopyTo(array, arrayIndex);
            }

            public IEnumerator<KeyValuePair<string, Value>> GetEnumerator()
            {
                return this.origin().GetEnumerator();
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
                value = default(Value);
                var result = this.origin().TryGetValue(key, out value);
                return result;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.origin().GetEnumerator();
            }
        }

        /// <summary>
        /// Simplified map building.
        /// You must understand, that this map will build every time when any method is called.
        /// If you do not want this, use <see cref="Map.Of"/>
        /// Since 9.9.2019
        /// </summary>
        public abstract class LiveMapEnvelope<Key, Value> : IDictionary<Key, Value>
        {
            private readonly UnsupportedOperationException rejectWriteExc = new UnsupportedOperationException("Writing is not supported, it's a read-only map");
            private readonly Func<IDictionary<Key, Value>> origin;

            /// <summary>
            /// Simplified map building.
            /// </summary>
            public LiveMapEnvelope(Func<IDictionary<Key, Value>> origin)
            {
                this.origin = origin;
            }

            public Value this[Key key] { get => this.origin()[key]; set => throw this.rejectWriteExc; }

            public ICollection<Key> Keys => this.origin().Keys;

            public ICollection<Value> Values => this.origin().Values;

            public int Count => this.origin().Count;

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
                return this.origin().Contains(item);
            }

            public bool ContainsKey(Key key)
            {
                return this.origin().ContainsKey(key);
            }

            public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
            {
                this.origin().CopyTo(array, arrayIndex);
            }

            public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
            {
                return this.origin().GetEnumerator();
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
                value = default(Value);
                var result = this.origin().TryGetValue(key, out value);
                return result;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.origin().GetEnumerator();
            }
        }
    }
}