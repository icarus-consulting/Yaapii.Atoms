// MIT License
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
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Fail;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// A map from string to string.
    /// You must understand, that this map will build every time when any method is called.
    /// If you do not want this, use <see cref="MapOf"/>
    /// </summary>
    public sealed class LiveMap : IDictionary<string, string>
    {
        private readonly UnsupportedOperationException rejectWriteExc = new UnsupportedOperationException("Writing is not supported, it's a read-only map");
        private readonly UnsupportedOperationException rejectEnumerateExc = new UnsupportedOperationException("Enumerating the dictionary's values is not supported. Enumerating all values can have adverse side effects.");

        private readonly Func<IDictionary<string, Func<string>>> input;

        /// <summary>
        /// A map from the given Dictionary and the given kvps.
        /// </summary>
        /// <param name="src">source dictionary</param>
        /// <param name="list">KeyValuePairs to append</param>
        public LiveMap(LiveMap src, params KeyValuePair<string, string>[] list) : this(
            src,
            new LiveMany<KeyValuePair<string, string>>(list))
        { }

        /// <summary>
        /// A map by merging the given KeyValuePairs to the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        public LiveMap(LiveMap src, LiveMany<KeyValuePair<string, string>> list) : this(() =>
            new MapOf<Func<string>>(
                new Enumerable.Joined<IKvp<Func<string>>>(
                    new Mapped<string, IKvp<Func<string>>>(key =>
                        new KvpOf<Func<string>>(key, () => src[key]),
                        src.Keys
                    ),
                    new Mapped<KeyValuePair<string, string>, IKvp<Func<string>>>(kvp =>
                        new KvpOf<Func<string>>(kvp.Key, () => kvp.Value),
                        list
                    )
                )
            )
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        public LiveMap(IKvp entry, params IKvp[] more) : this(
            new LiveMany<IKvp>(() =>
                new Enumerable.Joined<IKvp>(
                    new ManyOf<IKvp>(entry),
                    more
                )
            )
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public LiveMap(LiveMany<IKvp> entries) : this(() =>
            new MapOf<Func<string>>(
                new Mapped<IKvp, IKvp<Func<string>>>(kvp =>
                    new KvpOf<Func<string>>(kvp.Key(), () => kvp.Value()),
                    entries
                )
            )
        )
        { }

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public LiveMap(LiveMany<KeyValuePair<string, string>> entries) : this(() =>
            new MapOf<Func<string>>(
                new Mapped<KeyValuePair<string, string>, IKvp<Func<string>>>(kvp =>
                    new KvpOf<Func<string>>(kvp.Key, () => kvp.Value),
                    entries
                )
            )
        )
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        /// <param name="pairSequence">Pairs as a sequence, ordered like this: key-1, value-1, ... key-n, value-n</param>
        public LiveMap(LiveMany<string> pairSequence) : this(
            () =>
            {
                var idx = -1;
                var enumerator = pairSequence.GetEnumerator();
                var key = string.Empty;
                var result = new Dictionary<string, Func<string>>();
                while (enumerator.MoveNext())
                {
                    idx++;
                    if (idx % 2 == 0)
                    {
                        key = enumerator.Current;
                    }
                    else
                    {
                        var copy = enumerator.Current;
                        result.Add(key, () => copy);
                    }
                }

                if (idx % 2 != 1)
                {
                    throw new ArgumentException($"Cannot build a map because an even number of strings is needed, and the provided ones count {idx}");
                }
                return result;
            }
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public LiveMap(Func<IDictionary<string, Func<string>>> input)
        {
            this.input = input;
        }

        public ICollection<string> Keys => this.input().Keys;

        public ICollection<string> Values => throw this.rejectEnumerateExc;

        public int Count => this.input().Count;

        public bool IsReadOnly => true;

        public string this[string key]
        {
            get
            {
                var val = this.input();
                try
                {
                    return val[key].Invoke();
                }
                catch (KeyNotFoundException)
                {
                    var keysString = new Texts.Joined(", ", val.Keys).AsString();
                    throw new ArgumentException($"The key '{key}' is not present in the map. The following keys are present in the map: {keysString}");
                }
            }
            set => throw this.rejectWriteExc;
        }

        public void Add(string key, string value)
        {
            throw this.rejectWriteExc;
        }

        public bool ContainsKey(string key)
        {
            return this.input().ContainsKey(key);
        }

        public bool Remove(string key)
        {
            throw this.rejectWriteExc;
        }

        public bool TryGetValue(string key, out string value)
        {
            var success = this.input().TryGetValue(key, out var result);
            value = result();
            return success;
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
            throw this.rejectEnumerateExc;
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            throw this.rejectEnumerateExc;
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            throw this.rejectWriteExc;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            throw this.rejectEnumerateExc;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw this.rejectEnumerateExc;
        }
    }

    /// <summary>
    /// A map from string to typed value.
    /// You must understand, that this map will build every time when any method is called.
    /// If you do not want this, use <see cref="MapOf"/>
    /// </summary>
    public sealed class LiveMap<Value> : IDictionary<string, Value>
    {
        private readonly UnsupportedOperationException rejectWriteExc = new UnsupportedOperationException("Writing is not supported, it's a read-only map");
        private readonly UnsupportedOperationException rejectEnumerateExc = new UnsupportedOperationException("Enumerating the dictionary's values is not supported. Enumerating all values can have adverse side effects.");

        private readonly Func<IDictionary<string, Func<Value>>> input;
        
        /// <summary>
        /// A map from the given KeyValuePairs and appends them to the given Dictionary.
        /// </summary>
        /// <param name="src">source dictionary</param>
        /// <param name="list">KeyValuePairs to append</param>
        public LiveMap(LiveMap<Value> src, params KeyValuePair<string, Value>[] list) : this(
            src,
            new LiveMany<KeyValuePair<string, Value>>(list))
        { }

        /// <summary>
        /// A map by merging the given KeyValuePairs to the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        public LiveMap(LiveMap<Value> src, LiveMany<KeyValuePair<string, Value>> list) : this(() =>
            new MapOf<Func<Value>>(
                new Enumerable.Joined<IKvp<Func<Value>>>(
                    new Mapped<string, IKvp<Func<Value>>>(key =>
                        new KvpOf<Func<Value>>(key, () => src[key]),
                        src.Keys
                    ),
                    new Mapped<KeyValuePair<string, Value>, IKvp<Func<Value>>>(kvp =>
                        new KvpOf<Func<Value>>(kvp.Key, () => kvp.Value),
                        list
                    )
                )
            )
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">more kvps</param>
        /// <param name="entry">A single entry</param>
        public LiveMap(IKvp<Value> entry, params IKvp<Value>[] entries) : this(
            new LiveMany<IKvp<Value>>(() =>
                new Enumerable.Joined<IKvp<Value>>(
                    new ManyOf<IKvp<Value>>(entry),
                    entries
                )
            )
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public LiveMap(LiveMany<IKvp<Value>> entries) : this(() =>
            new MapOf<Func<Value>>(
                new Mapped<IKvp<Value>, IKvp<Func<Value>>>(kvp =>
                    new KvpOf<Func<Value>>(kvp.Key(), () => kvp.Value()),
                    entries
                )
            )
        )
        { }

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public LiveMap(LiveMany<KeyValuePair<string, Value>> entries) : this(() =>
            new MapOf<Func<Value>>(
                new Mapped<KeyValuePair<string, Value>, IKvp<Func<Value>>>(kvp =>
                    new KvpOf<Func<Value>>(kvp.Key, () => kvp.Value),
                    entries
                )
            )
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public LiveMap(Func<IDictionary<string, Func<Value>>> input)
        {
            this.input = input;
        }

        public ICollection<string> Keys => this.input().Keys;

        public ICollection<Value> Values => throw this.rejectEnumerateExc;

        public int Count => this.input().Count;

        public bool IsReadOnly => true;

        public Value this[string key]
        {
            get
            {
                var val = this.input();
                try
                {
                    return val[key].Invoke();
                }
                catch (KeyNotFoundException)
                {
                    var keysString = new Texts.Joined(", ", val.Keys).AsString();
                    throw new ArgumentException($"The key '{key}' is not present in the map. The following keys are present in the map: {keysString}");
                }
            }
            set => throw this.rejectWriteExc;
        }

        public void Add(string key, Value value)
        {
            throw this.rejectWriteExc;
        }

        public bool ContainsKey(string key)
        {
            return this.input().ContainsKey(key);
        }

        public bool Remove(string key)
        {
            throw this.rejectWriteExc;
        }

        public bool TryGetValue(string key, out Value value)
        {
            var success = this.input().TryGetValue(key, out var result);
            value = result();
            return success;
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
            throw this.rejectEnumerateExc;
        }

        public void CopyTo(KeyValuePair<string, Value>[] array, int arrayIndex)
        {
            throw this.rejectEnumerateExc;
        }

        public bool Remove(KeyValuePair<string, Value> item)
        {
            throw this.rejectWriteExc;
        }

        public IEnumerator<KeyValuePair<string, Value>> GetEnumerator()
        {
            throw this.rejectEnumerateExc;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw this.rejectEnumerateExc;
        }
    }

    /// <summary>
    /// A map from one type to another.
    /// You must understand, that this map will build every time when any method is called.
    /// If you do not want this, use <see cref="MapOf"/>
    /// </summary>
    public sealed class LiveMap<Key, Value> : IDictionary<Key, Value>
    {
        private readonly UnsupportedOperationException rejectWriteExc = new UnsupportedOperationException("Writing is not supported, it's a read-only map");
        private readonly UnsupportedOperationException rejectEnumerateExc = new UnsupportedOperationException("Enumerating the dictionary's values is not supported. Enumerating all values can have adverse side effects.");

        private readonly Func<IDictionary<Key, Func<Value>>> input;

        /// <summary>
        /// A map from the given KeyValuePairs and appends them to the given Dictionary.
        /// </summary>
        /// <param name="src">source dictionary</param>
        /// <param name="list">KeyValuePairs to append</param>
        public LiveMap(LiveMap<Key, Value> src, params KeyValuePair<Key, Value>[] list) : this(
            src,
            new LiveMany<KeyValuePair<Key, Value>>(list))
        { }

        /// <summary>
        /// A map by merging the given KeyValuePairs to the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        public LiveMap(LiveMap<Key, Value> src, LiveMany<KeyValuePair<Key, Value>> list) : this(() =>
            new MapOf<Key, Func<Value>>(
                new Enumerable.Joined<IKvp<Key, Func<Value>>>(
                    new Mapped<Key, IKvp<Key, Func<Value>>>(key =>
                        new KvpOf<Key, Func<Value>>(key, () => src[key]),
                        src.Keys
                    ),
                    new Mapped<KeyValuePair<Key, Value>, IKvp<Key, Func<Value>>>(kvp =>
                        new KvpOf<Key, Func<Value>>(kvp.Key, () => kvp.Value),
                        list
                    )
                )
            )
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        public LiveMap(IKvp<Key, Value> entry, params IKvp<Key, Value>[] more) : this(
            new LiveMany<IKvp<Key, Value>>(() =>
                new Enumerable.Joined<IKvp<Key, Value>>(
                    new ManyOf<IKvp<Key, Value>>(entry),
                    more
                )
            )
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public LiveMap(LiveMany<IKvp<Key, Value>> entries) : this(() =>
            new MapOf<Key, Func<Value>>(
                new Mapped<IKvp<Key, Value>, IKvp<Key, Func<Value>>>(kvp =>
                    new KvpOf<Key, Func<Value>>(kvp.Key(), () => kvp.Value()),
                    entries
                )
            )
        )
        { }

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public LiveMap(LiveMany<KeyValuePair<Key, Value>> entries) : this(() =>
            new MapOf<Key, Func<Value>>(
                new Mapped<KeyValuePair<Key, Value>, IKvp<Key, Func<Value>>>(kvp =>
                    new KvpOf<Key, Func<Value>>(kvp.Key, () => kvp.Value),
                    entries
                )
            )
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public LiveMap(Func<IDictionary<Key, Func<Value>>> input)
        {
            this.input = input;
        }

        public ICollection<Key> Keys => this.input().Keys;

        public ICollection<Value> Values => throw this.rejectEnumerateExc;

        public int Count => this.input().Count;

        public bool IsReadOnly => true;

        public Value this[Key key]
        {
            get
            {
                var val = this.input();
                try
                {
                    return val[key].Invoke();
                }
                catch (KeyNotFoundException)
                {
                    throw new ArgumentException("The requested key is not present in the map.");
                }
            }
            set => throw this.rejectWriteExc;
        }

        public void Add(Key key, Value value)
        {
            throw this.rejectWriteExc;
        }

        public bool ContainsKey(Key key)
        {
            return this.input().ContainsKey(key);
        }

        public bool Remove(Key key)
        {
            throw this.rejectWriteExc;
        }

        public bool TryGetValue(Key key, out Value value)
        {
            var success = this.input().TryGetValue(key, out var result);
            value = result();
            return success;
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
            throw this.rejectEnumerateExc;
        }

        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            throw this.rejectEnumerateExc;
        }

        public bool Remove(KeyValuePair<Key, Value> item)
        {
            throw this.rejectWriteExc;
        }

        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            throw this.rejectEnumerateExc;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw this.rejectEnumerateExc;
        }
    }
}
