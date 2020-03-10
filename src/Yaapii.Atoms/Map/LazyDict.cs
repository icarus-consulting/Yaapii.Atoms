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
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Lists;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Texts;

namespace Yaapii.Atoms.Lookup
{
    /// <summary>
    /// A dictionary whose values are retrieved only when accessing them.
    /// </summary>
    public sealed class LazyDict : IDictionary<string, string>
    {
        private readonly IDictionary<string, Sticky<string>> map;
        private readonly UnsupportedOperationException rejectReadException = new UnsupportedOperationException("Writing is not supported, it's a read-only map");
        private readonly bool rejectBuildingAllValues;
        private readonly Sticky<bool> anyValueIsLazy;

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(params IKvp[] kvps) : this(new Many.Live<IKvp>(kvps), true)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(bool rejectBuildingAllKeys, params IKvp[] kvps) : this(new Many.Live<IKvp>(kvps), rejectBuildingAllKeys)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(IEnumerable<IKvp> kvps, bool rejectBuildingAllValues = true)
        {
            this.rejectBuildingAllValues = rejectBuildingAllValues;
            this.map =
                new Map.Of<Sticky<string>>(() =>
                {
                    var dict = new Dictionary<string, Sticky<string>>();
                    foreach (var kvp in kvps)
                    {
                        dict[kvp.Key()] = new Sticky<string>(() => kvp.Value());
                    }
                    return dict;
                });
            this.anyValueIsLazy = new Sticky<bool>(() =>
            {
                return new Ternary<IFail, bool>(
                    new ScalarOf<Boolean>(() => new LengthOf(kvps).Value() == 0),
                    new False(),
                    new Reduced<bool>(
                        new Enumerable.Mapped<IKvp, bool>(kvp => kvp.IsLazy(), kvps),
                        (a, b) => a || b
                    )
                ).Value();
            });
        }

        /// <summary>
        /// Access a value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key] { get { return map[key].Value(); } set { throw this.rejectReadException; } }

        /// <summary>
        /// Access all keys
        /// </summary>
        public ICollection<string> Keys => map.Keys;

        /// <summary>
        /// Access all values
        /// </summary>
        public ICollection<string> Values
        {
            get
            {
                if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
                {
                    throw new InvalidOperationException(
                        "Cannot get values because this is a lazy dictionary."
                        + " Getting the values would build all keys."
                        + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
                }
                return
                    new List.Live<string>(
                       new Enumerable.Mapped<Sticky<string>, string>(
                           v => v.Value(),
                           map.Values
                       )
                   );
            }
        }


        /// <summary>
        /// Count entries
        /// </summary>
        public int Count => map.Count;

        /// <summary>
        /// Yes its readonly
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, string value)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<string, string> item)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        public void Clear()
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Test if map contains entry
        /// </summary>
        /// <param name="item">item to check</param>
        /// <returns>true if it contains</returns>
        public bool Contains(KeyValuePair<string, string> item)
        {
            return this.map.ContainsKey(item.Key) && this.map[item.Key].Value().Equals(item.Value);
        }

        /// <summary>
        /// Test if map contains key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return this.map.ContainsKey(key);
        }

        /// <summary>
        /// Copy this to an array
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">index to start</param>
        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException("Cannot copy entries because this is a lazy dictionary."
                    + " Copying the entries would build all values."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            if (arrayIndex > this.map.Count)
            {
                throw
                    new ArgumentOutOfRangeException(
                        new Formatted(
                            "arrayIndex {0} is higher than the item count in the map {1}.",
                            arrayIndex,
                            this.map.Count
                        ).AsString());
            }

            new List.Of<KeyValuePair<string, string>>(this).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// The enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException(
                    "Cannot get the enumerator because this is a lazy dictionary."
                    + " Enumerating the entries would build all values."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            return
                new Enumerable.Mapped<KeyValuePair<string, Sticky<string>>, KeyValuePair<string, string>>(
                    kvp => new KeyValuePair<string, string>(kvp.Key, kvp.Value.Value()),
                    this.map
                ).GetEnumerator();
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, string> item)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Tries to get value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">target to store value</param>
        /// <returns>true if success</returns>
        public bool TryGetValue(string key, out string value)
        {
            var result = this.map.ContainsKey(key);
            if (result)
            {
                value = this.map[key].Value();
                result = true;
            }
            else
            {
                value = string.Empty;
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// A dictionary whose values are retrieved only when accessing them.
    /// </summary>
    public sealed class LazyDict<Value> : IDictionary<string, Value>
    {
        private readonly IDictionary<string, Sticky<Value>> map;
        private readonly UnsupportedOperationException rejectReadException = new UnsupportedOperationException("Writing is not supported, it's a read-only map");
        private readonly bool rejectBuildingAllValues;
        private readonly Sticky<bool> anyValueIsLazy;

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(params IKvp<Value>[] kvps) : this(new Many.Live<IKvp<Value>>(kvps), true)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(bool rejectBuildingAllValues, params IKvp<Value>[] kvps) : this(new Many.Live<IKvp<Value>>(kvps), rejectBuildingAllValues)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(IEnumerable<IKvp<Value>> kvps, bool rejectBuildingAllValues = true)
        {
            this.rejectBuildingAllValues = rejectBuildingAllValues;
            this.map =
                new Map.Of<Sticky<Value>>(() =>
                {
                    var dict = new Dictionary<string, Sticky<Value>>();
                    foreach (var kvp in kvps)
                    {
                        dict[kvp.Key()] = new Sticky<Value>(() => kvp.Value());
                    }
                    return dict;
                });
            this.anyValueIsLazy = new Sticky<bool>(() =>
            {
                return new Ternary<IFail, bool>(
                    new ScalarOf<Boolean>(() => new LengthOf(kvps).Value() == 0),
                    new False(),
                    new Reduced<bool>(
                        new Enumerable.Mapped<IKvp<Value>, bool>(kvp => kvp.IsLazy(), kvps),
                        (a, b) => a || b
                    )
                ).Value();
            });
        }

        /// <summary>
        /// Access a value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Value this[string key] { get { return map[key].Value(); } set { throw this.rejectReadException; } }

        /// <summary>
        /// Access all keys
        /// </summary>
        public ICollection<string> Keys => map.Keys;

        /// <summary>
        /// Access all values
        /// </summary>
        public ICollection<Value> Values
        {
            get
            {
                if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
                {
                    throw new InvalidOperationException(
                        "Cannot get values because this is a lazy dictionary."
                        + " Getting the values would build all keys."
                        + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
                }
                return
                    new List.Live<Value>(
                       new Enumerable.Mapped<Sticky<Value>, Value>(
                           v => v.Value(),
                           map.Values
                       )
                   );
            }
        }

        /// <summary>
        /// Count entries
        /// </summary>
        public int Count => map.Count;

        /// <summary>
        /// Yes its readonly
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, Value value)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<string, Value> item)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        public void Clear()
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Test if map contains entry
        /// </summary>
        /// <param name="item">item to check</param>
        /// <returns>true if it contains</returns>
        public bool Contains(KeyValuePair<string, Value> item)
        {
            return this.map.ContainsKey(item.Key) && this.map[item.Key].Value().Equals(item.Value);
        }

        /// <summary>
        /// Test if map contains key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return this.map.ContainsKey(key);
        }

        /// <summary>
        /// Copy this to an array
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">index to start</param>
        public void CopyTo(KeyValuePair<string, Value>[] array, int arrayIndex)
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException("Cannot copy entries because this is a lazy dictionary."
                    + " Copying the entries would build all values."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            if (arrayIndex > this.map.Count)
            {
                throw
                    new ArgumentOutOfRangeException(
                        new Formatted(
                            "arrayIndex {0} is higher than the item count in the map {1}.",
                            arrayIndex,
                            this.map.Count
                        ).AsString());
            }

            new List.Of<KeyValuePair<string, Value>>(this).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// The enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<KeyValuePair<string, Value>> GetEnumerator()
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException(
                    "Cannot get the enumerator because this is a lazy dictionary."
                    + " Enumerating the entries would build all values."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            return
                new Enumerable.Mapped<KeyValuePair<string, Sticky<Value>>, KeyValuePair<string, Value>>(
                    kvp => new KeyValuePair<string, Value>(kvp.Key, kvp.Value.Value()),
                    this.map
                ).GetEnumerator();
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<string, Value> item)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Tries to get value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">target to store value</param>
        /// <returns>true if success</returns>
        public bool TryGetValue(string key, out Value value)
        {
            var result = this.map.ContainsKey(key);
            if (result)
            {
                value = this.map[key].Value();
                result = true;
            }
            else
            {
                value = default(Value);
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// A dictionary whose values are retrieved only when accessing them.
    /// </summary>
    public sealed class LazyDict<Key, Value> : IDictionary<Key, Value>
    {
        private readonly IDictionary<Key, Sticky<Value>> map;
        private readonly UnsupportedOperationException rejectReadException = new UnsupportedOperationException("Writing is not supported, it's a read-only map");
        private readonly bool rejectBuildingAllValues;
        private readonly Sticky<bool> anyValueIsLazy;

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(params IKvp<Key, Value>[] kvps) : this(new Many.Of<IKvp<Key, Value>>(kvps), true)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(bool rejectBuildingAllValues, params IKvp<Key, Value>[] kvps) : this(new Many.Live<IKvp<Key, Value>>(kvps), rejectBuildingAllValues)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        public LazyDict(IEnumerable<IKvp<Key, Value>> kvps, bool rejectBuildingAllValues = true)
        {
            this.rejectBuildingAllValues = rejectBuildingAllValues;
            this.map =
                new Map.Of<Key, Sticky<Value>>(() =>
                {
                    var dict = new Dictionary<Key, Sticky<Value>>();
                    foreach (var kvp in kvps)
                    {
                        dict[kvp.Key()] = new Sticky<Value>(() => kvp.Value());
                    }
                    return dict;
                });
            this.anyValueIsLazy = new Sticky<bool>(() =>
            {
                return new Ternary<IFail, bool>(
                    new ScalarOf<Boolean>(() => new LengthOf(kvps).Value() == 0),
                    new False(),
                    new Reduced<bool>(
                        new Enumerable.Mapped<IKvp<Key, Value>, bool>(kvp => kvp.IsLazy(), kvps),
                        (a, b) => a || b
                    )
                ).Value();
            });

        }

        /// <summary>
        /// Access a value by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Value this[Key key] { get { return map[key].Value(); } set { throw this.rejectReadException; } }

        /// <summary>
        /// Access all keys
        /// </summary>
        public ICollection<Key> Keys => map.Keys;

        /// <summary>
        /// Access all values
        /// </summary>
        public ICollection<Value> Values
        {
            get
            {
                if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
                {
                    throw new InvalidOperationException(
                        "Cannot get all values because this is a lazy dictionary."
                        + " Getting the values would build all keys."
                        + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
                }
                return
                    new List.Live<Value>(
                       new Enumerable.Mapped<Sticky<Value>, Value>(
                           v => v.Value(),
                           map.Values
                       )
                   );
            }
        }


        /// <summary>
        /// Count entries
        /// </summary>
        public int Count => map.Count;

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
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<Key, Value> item)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        public void Clear()
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Test if map contains entry
        /// </summary>
        /// <param name="item">item to check</param>
        /// <returns>true if it contains</returns>
        public bool Contains(KeyValuePair<Key, Value> item)
        {
            return this.map.ContainsKey(item.Key) && this.map[item.Key].Value().Equals(item.Value);
        }

        /// <summary>
        /// Test if map contains key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(Key key)
        {
            return this.map.ContainsKey(key);
        }

        /// <summary>
        /// Copy this to an array
        /// </summary>
        /// <param name="array">target array</param>
        /// <param name="arrayIndex">index to start</param>
        public void CopyTo(KeyValuePair<Key, Value>[] array, int arrayIndex)
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException(
                    "Cannot copy entries because this is a lazy dictionary."
                    + " Copying the entries would build all values."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            if (arrayIndex > this.map.Count)
            {
                throw
                    new ArgumentOutOfRangeException(
                        new Formatted(
                            "arrayIndex {0} is higher than the item count in the map {1}.",
                            arrayIndex,
                            this.map.Count
                        ).AsString());
            }

            new List.Of<KeyValuePair<Key, Value>>(this).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// The enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
        {
            if (this.rejectBuildingAllValues && this.anyValueIsLazy.Value())
            {
                throw new InvalidOperationException(
                    "Cannot get the enumerator because this is a lazy dictionary."
                    + " Enumerating the entries would build all values."
                    + " If you need this behaviour, set the ctor param 'rejectBuildingAllValues' to false.");
            }
            return
                new Enumerable.Mapped<KeyValuePair<Key, Sticky<Value>>, KeyValuePair<Key, Value>>(
                    kvp => new KeyValuePair<Key, Value>(kvp.Key, kvp.Value.Value()),
                    this.map
                ).GetEnumerator();
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(Key key)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Unsupported
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<Key, Value> item)
        {
            throw this.rejectReadException;
        }

        /// <summary>
        /// Tries to get value
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">target to store value</param>
        /// <returns>true if success</returns>
        public bool TryGetValue(Key key, out Value value)
        {
            var result = this.map.ContainsKey(key);
            if (result)
            {
                value = this.map[key].Value();
                result = true;
            }
            else
            {
                value = default(Value);
            }
            return result;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
