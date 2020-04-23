// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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
    /// Simplified map building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapEnvelope : IDictionary<string, string>
    {
        private readonly UnsupportedOperationException rejectWriteExc = new UnsupportedOperationException("Writing is not supported, it's a read-only map");

        private readonly Func<IDictionary<string, string>> origin;
        private readonly ScalarOf<IDictionary<string, string>> fixedOrigin;
        private readonly bool live;

        /// <summary>
        /// Simplified map building.
        /// </summary>
        public MapEnvelope(Func<IDictionary<string, string>> origin, bool live)
        {
            this.origin = origin;
            this.live = live;
            this.fixedOrigin = new ScalarOf<IDictionary<string, string>>(origin);
        }

        public string this[string key]
        {
            get
            {
                var val = Val();
                try
                {
                    return val[key];
                }
                catch (KeyNotFoundException)
                {
                    var keysString = new Text.Joined(", ", val.Keys).AsString();
                    throw new ArgumentException($"The key '{key}' is not present in the map. The following keys are present in the map: {keysString}");
                }
            }
            set => throw this.rejectWriteExc;
        }

        public ICollection<string> Keys => Val().Keys;

        public ICollection<string> Values => Val().Values;

        public int Count => Val().Count;

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
            return Val().Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return Val().ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            Val().CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return Val().GetEnumerator();
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
            return Val().TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Val().GetEnumerator();
        }

        private IDictionary<string, string> Val()
        {
            IDictionary<string, string> result;
            if (this.live)
            {
                result = this.origin();
            }
            else
            {
                result = this.fixedOrigin.Value();
            }
            return result;
        }
    }

    /// <summary>
    /// Simplified map building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapEnvelope<Value> : IDictionary<string, Value>
    {
        private readonly UnsupportedOperationException rejectWriteExc = new UnsupportedOperationException("Writing is not supported, it's a read-only map");

        private readonly Func<IDictionary<string, Value>> origin;
        private readonly ScalarOf<IDictionary<string, Value>> fixedOrigin;
        private readonly bool live;

        /// <summary>
        /// Simplified map building.
        /// </summary>
        public MapEnvelope(Func<IDictionary<string, Value>> origin, bool live)
        {
            this.origin = origin;
            this.live = live;
            this.fixedOrigin = new ScalarOf<IDictionary<string, Value>>(origin);
        }

        public Value this[string key]
        {
            get
            {
                var val = Val();
                try
                {
                    return val[key];
                }
                catch (KeyNotFoundException)
                {
                    var keysString = new Text.Joined(", ", val.Keys).AsString();
                    throw new ArgumentException($"The key '{key}' is not present in the map. The following keys are present in the map: {keysString}");
                }
            }
            set => throw this.rejectWriteExc;
        }

        public ICollection<string> Keys => Val().Keys;

        public ICollection<Value> Values => Val().Values;

        public int Count => Val().Count;

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
            return Val().Contains(item);
        }

        public bool ContainsKey(string key)
        {
            return Val().ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, Value>[] array, int arrayIndex)
        {
            Val().CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, Value>> GetEnumerator()
        {
            return Val().GetEnumerator();
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
            return Val().TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Val().GetEnumerator();
        }

        private IDictionary<string, Value> Val()
        {
            IDictionary<string, Value> result;
            if (this.live)
            {
                result = this.origin();
            }
            else
            {
                result = this.fixedOrigin.Value();
            }
            return result;
        }
    }

    /// <summary>
    /// Simplified map building.
    /// Since 9.9.2019
    /// </summary>
    public abstract class MapEnvelope<Key, Value> : IDictionary<Key, Value>
    {
        private readonly UnsupportedOperationException rejectWriteExc = new UnsupportedOperationException("Writing is not supported, it's a read-only map");

        private readonly Func<IDictionary<Key, Value>> origin;
        private readonly ScalarOf<IDictionary<Key, Value>> fixedOrigin;
        private readonly bool live;

        /// <summary>
        /// Simplified map building.
        /// </summary>
        public MapEnvelope(Func<IDictionary<Key, Value>> origin, bool live)
        {
            this.origin = origin;
            this.live = live;
            this.fixedOrigin = new ScalarOf<IDictionary<Key, Value>>(origin);
        }

        public Value this[Key key]
        {
            get
            {
                try
                {
                    return Val()[key];
                }
                catch (KeyNotFoundException)
                {
                    throw new ArgumentException("The requested key is not present in the map.");
                }
            }
            set => throw this.rejectWriteExc;
        }

        public ICollection<Key> Keys => Val().Keys;

        public ICollection<Value> Values => Val().Values;

        public int Count => Val().Count;

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
            return Val().Contains(item);
        }

        public bool ContainsKey(Key key)
        {
            return Val().ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            Val().CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            return Val().GetEnumerator();
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
            return Val().TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Val().GetEnumerator();
        }

        private IDictionary<Key, Value> Val()
        {
            IDictionary<Key, Value> result;
            if (this.live)
            {
                result = this.origin();
            }
            else
            {
                result = this.fixedOrigin.Value();
            }
            return result;
        }
    }
}
