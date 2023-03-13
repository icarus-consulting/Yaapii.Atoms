// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// A map from string to string.
    /// </summary>
    public sealed class MapOf : MapEnvelope
    {
        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(KeyValuePair<string, string> entry, params KeyValuePair<string, string>[] more) : this(
            new Enumerable.Joined<KeyValuePair<string, string>>(
                new LiveMany<KeyValuePair<string, string>>(more),
                entry
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs and appends them to the given Dictionary.
        /// </summary>
        /// <param name="src">source dictionary</param>
        /// <param name="list">KeyValuePairs to append</param>
        public MapOf(IDictionary<string, string> src, params KeyValuePair<string, string>[] list) : this(
            src,
            new LiveMany<KeyValuePair<string, string>>(list))
        { }

        /// <summary>
        /// A map by merging the given KeyValuePairs to the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        public MapOf(IDictionary<string, string> src, IEnumerable<KeyValuePair<string, string>> list) : this(
            new Enumerable.Joined<KeyValuePair<string, string>>(
                src,
                list
            ))
        { }

        /// <summary>
        /// A map by taking the given entries.
        /// </summary>
        /// <param name="entries">enumerator of KeyValuePairs</param>
        public MapOf(IEnumerator<KeyValuePair<string, string>> entries) : this(
            new LiveMany<KeyValuePair<string, string>>(() => entries))
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        public MapOf(IKvp entry, params IKvp[] more) : base(() =>
            new LazyDict(
                new Enumerable.Joined<IKvp>(
                    new ManyOf<IKvp>(entry),
                    more
                )
            ),
            false
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// Rejects building of all values
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        public MapOf(IEnumerable<IKvp> entries) : this(
            entries, true
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        /// <param name="rejectBuildingAllValues">if you have KVPs with value functions, it is by default prevented to build all values by getting the enumerator. You can deactivate that here.</param>
        public MapOf(IEnumerable<IKvp> entries, bool rejectBuildingAllValues) : this(
            new LazyDict(entries, rejectBuildingAllValues)
        )
        { }

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public MapOf(IDictionary<string, string> entries) : base(() => entries, false)
        //caution: do not remove this ctor. It is a "proxy" ctor to prevent copying values. 
        //Because a map is also a IEnumerable of KeyValuePairs, the ctor accepting the enumerable would copy the map.
        { }

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public MapOf(IEnumerable<KeyValuePair<string, string>> entries) : this(
            () =>
            {
                var temp = new Dictionary<string, string>();
                foreach (var entry in entries)
                {
                    temp[entry.Key] = entry.Value;
                }
                return temp;
            }
        )
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        public MapOf(params string[] pairSequence) : this(
            new ManyOf(pairSequence)
        )
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        public MapOf(string key, string value, params string[] additional) : this(
            new Enumerable.Joined<string>(
                new ManyOf(key, value),
                additional
            )
        )
        { }

        /// <summary>
        /// A map from string to string.
        /// </summary>
        /// <param name="pairSequence">Pairs as a sequence, ordered like this: key-1, value-1, ... key-n, value-n</param>
        public MapOf(IEnumerable<string> pairSequence) : this(
            () =>
            {
                var idx = -1;
                var enumerator = pairSequence.GetEnumerator();
                var key = string.Empty;
                var result = new Dictionary<string, string>();
                while (enumerator.MoveNext())
                {
                    idx++;
                    if (idx % 2 == 0)
                    {
                        key = enumerator.Current;
                    }
                    else
                    {
                        result.Add(key, enumerator.Current);
                    }
                }

                if (idx % 2 != 1 && idx != -1)
                {
                    throw new ArgumentException($"Cannot build a map because an even number of strings is needed, and the provided ones count {idx + 1}");
                }
                return result;
            }
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public MapOf(params IMapInput[] inputs) : this(new ManyOf<IMapInput>(inputs))
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public MapOf(IEnumerable<IMapInput> inputs) : this(
            () =>
            {
                IDictionary<string, string> dict = new LazyDict();
                foreach (IMapInput input in inputs)
                {
                    dict = input.Apply(dict);
                }
                return dict;
            }
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public MapOf(Func<IDictionary<string, string>> input) : base(
            input, false
        )
        { }

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12,
            key13, value13
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            string key14, Value value14
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12,
            key13, value13,
            key14, value14
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            string key14, Value value14,
            string key15, Value value15
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12,
            key13, value13,
            key14, value14,
            key15, value15
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            string key14, Value value14,
            string key15, Value value15,
            string key16, Value value16
        )
        => new MapOf<Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12,
            key13, value13,
            key14, value14,
            key15, value15,
            key16, value16
        );

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public static IDictionary<string, Value> New<Value>(KeyValuePair<string, Value> entry, params KeyValuePair<string, Value>[] more)
            => new MapOf<Value>(entry, more);

        /// <summary>
        /// A map by taking the given entries.
        /// </summary>
        /// <param name="entries">enumerator of KeyValuePairs</param>
        public static IDictionary<string, Value> New<Value>(IEnumerator<KeyValuePair<string, Value>> entries)
            => new MapOf<Value>(entries);

        /// <summary>
        /// A map from the given IKvps.
        /// </summary>
        public static IDictionary<string, Value> New<Value>(IKvp<Value> entry, params IKvp<Value>[] more)
            => new MapOf<Value>(entry, more);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        /// <param name="rejectBuildingAllValues">if you have KVPs with value functions, it is by default prevented to build all values by getting the enumerator. You can deactivate that here.</param>
        public static IDictionary<string, Value> New<Value>(IEnumerable<IKvp<Value>> entries, bool rejectBuildingAllValues = true)
            => new MapOf<Value>(entries, rejectBuildingAllValues);

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public static IDictionary<string, Value> New<Value>(IDictionary<string, Value> entries)
            => new MapOf<Value>(entries);

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public static IDictionary<string, Value> New<Value>(IEnumerable<KeyValuePair<string, Value>> entries)
            => new MapOf<Value>(entries);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public static IDictionary<string, Value> New<Value>(params IMapInput<Value>[] inputs)
            => new MapOf<Value>(inputs);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public static IDictionary<string, Value> New<Value>(IEnumerable<IMapInput<Value>> inputs)
            => new MapOf<Value>(inputs);

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public static IDictionary<string, Value> New<Value>(Func<IDictionary<string, Value>> input)
            => new MapOf<Value>(input);

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10,
            Key key11, Value value11
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10,
            Key key11, Value value11,
            Key key12, Value value12
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10,
            Key key11, Value value11,
            Key key12, Value value12,
            Key key13, Value value13
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12,
            key13, value13
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10,
            Key key11, Value value11,
            Key key12, Value value12,
            Key key13, Value value13,
            Key key14, Value value14
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12,
            key13, value13,
            key14, value14
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10,
            Key key11, Value value11,
            Key key12, Value value12,
            Key key13, Value value13,
            Key key14, Value value14,
            Key key15, Value value15
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12,
            key13, value13,
            key14, value14,
            key15, value15
        );

        /// <summary>
        /// A map from the given keys and values.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(
            Key key1, Value value1,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value value10,
            Key key11, Value value11,
            Key key12, Value value12,
            Key key13, Value value13,
            Key key14, Value value14,
            Key key15, Value value15,
            Key key16, Value value16
        )
        => new MapOf<Key, Value>(
            key1, value1,
            key2, value2,
            key3, value3,
            key4, value4,
            key5, value5,
            key6, value6,
            key7, value7,
            key8, value8,
            key9, value9,
            key10, value10,
            key11, value11,
            key12, value12,
            key13, value13,
            key14, value14,
            key15, value15,
            key16, value16
        );

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(KeyValuePair<Key, Value> item, params KeyValuePair<Key, Value>[] more)
            => new MapOf<Key, Value>(item, more);

        /// <summary>
        /// A map from the given KeyValuePairs and appends them to the given Dictionary.
        /// </summary>
        /// <param name="src">source dictionary</param>
        /// <param name="list">KeyValuePairs to append</param>
        public static IDictionary<Key, Value> New<Key, Value>(IDictionary<Key, Value> src, params KeyValuePair<Key, Value>[] list)
            => new MapOf<Key, Value>(src, list);

        /// <summary>
        /// A map by merging the given KeyValuePairs to the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        public static IDictionary<Key, Value> New<Key, Value>(IDictionary<Key, Value> src, IEnumerable<KeyValuePair<Key, Value>> list)
            => new MapOf<Key, Value>(src, list);

        /// <summary>
        /// A map by taking the given entries.
        /// </summary>
        /// <param name="entries">enumerator of KeyValuePairs</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerator<KeyValuePair<Key, Value>> entries)
            => new MapOf<Key, Value>(entries);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        public static IDictionary<Key, Value> New<Key, Value>(IKvp<Key, Value> entry, params IKvp<Key, Value>[] more)
            => new MapOf<Key, Value>(entry, more);

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        /// <param name="rejectBuildingAllValues">if you have KVPs with value functions, it is by default prevented to build all values by getting the enumerator. You can deactivate that here.</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerable<IKvp<Key, Value>> entries, bool rejectBuildingAllValues = true)
            => new MapOf<Key, Value>(entries, rejectBuildingAllValues);

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public static IDictionary<Key, Value> New<Key, Value>(IDictionary<Key, Value> entries)
            => new MapOf<Key, Value>(entries);

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerable<KeyValuePair<Key, Value>> entries)
            => new MapOf<Key, Value>(entries);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public static IDictionary<Key, Value> New<Key, Value>(params IMapInput<Key, Value>[] inputs)
            => new MapOf<Key, Value>(inputs);

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public static IDictionary<Key, Value> New<Key, Value>(IEnumerable<IMapInput<Key, Value>> inputs)
            => new MapOf<Key, Value>(inputs);

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public static IDictionary<Key, Value> New<Key, Value>(Func<IDictionary<Key, Value>> input)
            => new MapOf<Key, Value>(input);
    }

    /// <summary>
    /// A map from string to typed value.
    /// </summary>
    public sealed class MapOf<Value> : MapEnvelope<Value>
    {
        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4),
                new KvpOf<Value>(key5, value5)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4),
                new KvpOf<Value>(key5, value5),
                new KvpOf<Value>(key6, value6)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4),
                new KvpOf<Value>(key5, value5),
                new KvpOf<Value>(key6, value6),
                new KvpOf<Value>(key7, value7)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4),
                new KvpOf<Value>(key5, value5),
                new KvpOf<Value>(key6, value6),
                new KvpOf<Value>(key7, value7),
                new KvpOf<Value>(key8, value8)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4),
                new KvpOf<Value>(key5, value5),
                new KvpOf<Value>(key6, value6),
                new KvpOf<Value>(key7, value7),
                new KvpOf<Value>(key8, value8),
                new KvpOf<Value>(key9, value9)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4),
                new KvpOf<Value>(key5, value5),
                new KvpOf<Value>(key6, value6),
                new KvpOf<Value>(key7, value7),
                new KvpOf<Value>(key8, value8),
                new KvpOf<Value>(key9, value9),
                new KvpOf<Value>(key10, value10)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4),
                new KvpOf<Value>(key5, value5),
                new KvpOf<Value>(key6, value6),
                new KvpOf<Value>(key7, value7),
                new KvpOf<Value>(key8, value8),
                new KvpOf<Value>(key9, value9),
                new KvpOf<Value>(key10, value10),
                new KvpOf<Value>(key11, value11)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4),
                new KvpOf<Value>(key5, value5),
                new KvpOf<Value>(key6, value6),
                new KvpOf<Value>(key7, value7),
                new KvpOf<Value>(key8, value8),
                new KvpOf<Value>(key9, value9),
                new KvpOf<Value>(key10, value10),
                new KvpOf<Value>(key11, value11),
                new KvpOf<Value>(key12, value12)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4),
                new KvpOf<Value>(key5, value5),
                new KvpOf<Value>(key6, value6),
                new KvpOf<Value>(key7, value7),
                new KvpOf<Value>(key8, value8),
                new KvpOf<Value>(key9, value9),
                new KvpOf<Value>(key10, value10),
                new KvpOf<Value>(key11, value11),
                new KvpOf<Value>(key12, value12),
                new KvpOf<Value>(key13, value13)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            string key14, Value value14,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4),
                new KvpOf<Value>(key5, value5),
                new KvpOf<Value>(key6, value6),
                new KvpOf<Value>(key7, value7),
                new KvpOf<Value>(key8, value8),
                new KvpOf<Value>(key9, value9),
                new KvpOf<Value>(key10, value10),
                new KvpOf<Value>(key11, value11),
                new KvpOf<Value>(key12, value12),
                new KvpOf<Value>(key13, value13),
                new KvpOf<Value>(key14, value14)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            string key14, Value value14,
            string key15, Value value15,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4),
                new KvpOf<Value>(key5, value5),
                new KvpOf<Value>(key6, value6),
                new KvpOf<Value>(key7, value7),
                new KvpOf<Value>(key8, value8),
                new KvpOf<Value>(key9, value9),
                new KvpOf<Value>(key10, value10),
                new KvpOf<Value>(key11, value11),
                new KvpOf<Value>(key12, value12),
                new KvpOf<Value>(key13, value13),
                new KvpOf<Value>(key14, value14),
                new KvpOf<Value>(key15, value15)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            string key1, Value value1,
            string key2, Value value2,
            string key3, Value value3,
            string key4, Value value4,
            string key5, Value value5,
            string key6, Value value6,
            string key7, Value value7,
            string key8, Value value8,
            string key9, Value value9,
            string key10, Value value10,
            string key11, Value value11,
            string key12, Value value12,
            string key13, Value value13,
            string key14, Value value14,
            string key15, Value value15,
            string key16, Value value16,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Value>>(
                new KvpOf<Value>(key1, value1),
                new KvpOf<Value>(key2, value2),
                new KvpOf<Value>(key3, value3),
                new KvpOf<Value>(key4, value4),
                new KvpOf<Value>(key5, value5),
                new KvpOf<Value>(key6, value6),
                new KvpOf<Value>(key7, value7),
                new KvpOf<Value>(key8, value8),
                new KvpOf<Value>(key9, value9),
                new KvpOf<Value>(key10, value10),
                new KvpOf<Value>(key11, value11),
                new KvpOf<Value>(key12, value12),
                new KvpOf<Value>(key13, value13),
                new KvpOf<Value>(key14, value14),
                new KvpOf<Value>(key15, value15),
                new KvpOf<Value>(key16, value16)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(KeyValuePair<string, Value> entry, params KeyValuePair<string, Value>[] more) : this(
            new Enumerable.Joined<KeyValuePair<string, Value>>(
                new LiveMany<KeyValuePair<string, Value>>(more),
                entry
            )
        )
        { }

        /// <summary>
        /// A map by taking the given entries.
        /// </summary>
        /// <param name="entries">enumerator of KeyValuePairs</param>
        public MapOf(IEnumerator<KeyValuePair<string, Value>> entries) : this(
            new LiveMany<KeyValuePair<string, Value>>(() => entries))
        { }

        /// <summary>
        /// A map from the given IKvps.
        /// </summary>
        public MapOf(IKvp<Value> entry, params IKvp<Value>[] more) : base(() =>
            new LazyDict<Value>(
                new Enumerable.Joined<IKvp<Value>>(
                    new ManyOf<IKvp<Value>>(entry),
                    more
                )
            ),
            false
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        /// <param name="rejectBuildingAllValues">if you have KVPs with value functions, it is by default prevented to build all values by getting the enumerator. You can deactivate that here.</param>
        public MapOf(IEnumerable<IKvp<Value>> entries, bool rejectBuildingAllValues = true) : this(
            new LazyDict<Value>(entries, rejectBuildingAllValues)
        )
        { }

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public MapOf(IDictionary<string, Value> entries) : base(() => entries, false)
        //caution: do not remove this ctor. It is a "proxy" ctor to prevent copying values. 
        //Because a map is also a IEnumerable of KeyValuePairs, the ctor accepting the enumerable would copy the map.
        { }

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public MapOf(IEnumerable<KeyValuePair<string, Value>> entries) : this(
            () =>
            {
                var temp = new Dictionary<string, Value>();
                foreach (var entry in entries)
                {
                    temp[entry.Key] = entry.Value;
                }
                return temp;
            }
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public MapOf(params IMapInput<Value>[] inputs) : this(
            new ManyOf<IMapInput<Value>>(inputs)
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public MapOf(IEnumerable<IMapInput<Value>> inputs) : this(
            () =>
            {
                IDictionary<string, Value> dict = new LazyDict<Value>();
                foreach (IMapInput<Value> input in inputs)
                {
                    dict = input.Apply(dict);
                }
                return dict;
            }
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public MapOf(Func<IDictionary<string, Value>> input) : base(
            input, false
        )
        { }

    }

    /// <summary>
    /// A map from string to typed value.
    /// </summary>
    public sealed class MapOf<Key, Value> : MapEnvelope<Key, Value>
    {
        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4),
                new KvpOf<Key, Value>(key5, value5)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4),
                new KvpOf<Key, Value>(key5, value5),
                new KvpOf<Key, Value>(key6, value6)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4),
                new KvpOf<Key, Value>(key5, value5),
                new KvpOf<Key, Value>(key6, value6),
                new KvpOf<Key, Value>(key7, value7)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4),
                new KvpOf<Key, Value>(key5, value5),
                new KvpOf<Key, Value>(key6, value6),
                new KvpOf<Key, Value>(key7, value7),
                new KvpOf<Key, Value>(key8, value8)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4),
                new KvpOf<Key, Value>(key5, value5),
                new KvpOf<Key, Value>(key6, value6),
                new KvpOf<Key, Value>(key7, value7),
                new KvpOf<Key, Value>(key8, value8),
                new KvpOf<Key, Value>(key9, value9)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4),
                new KvpOf<Key, Value>(key5, value5),
                new KvpOf<Key, Value>(key6, value6),
                new KvpOf<Key, Value>(key7, value7),
                new KvpOf<Key, Value>(key8, value8),
                new KvpOf<Key, Value>(key9, value9),
                new KvpOf<Key, Value>(key10, Key0)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            Key key11, Value Key1,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4),
                new KvpOf<Key, Value>(key5, value5),
                new KvpOf<Key, Value>(key6, value6),
                new KvpOf<Key, Value>(key7, value7),
                new KvpOf<Key, Value>(key8, value8),
                new KvpOf<Key, Value>(key9, value9),
                new KvpOf<Key, Value>(key10, Key0),
                new KvpOf<Key, Value>(key11, Key1)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            Key key11, Value Key1,
            Key key12, Value Key2,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4),
                new KvpOf<Key, Value>(key5, value5),
                new KvpOf<Key, Value>(key6, value6),
                new KvpOf<Key, Value>(key7, value7),
                new KvpOf<Key, Value>(key8, value8),
                new KvpOf<Key, Value>(key9, value9),
                new KvpOf<Key, Value>(key10, Key0),
                new KvpOf<Key, Value>(key11, Key1),
                new KvpOf<Key, Value>(key12, Key2)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            Key key11, Value Key1,
            Key key12, Value Key2,
            Key key13, Value Key3,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4),
                new KvpOf<Key, Value>(key5, value5),
                new KvpOf<Key, Value>(key6, value6),
                new KvpOf<Key, Value>(key7, value7),
                new KvpOf<Key, Value>(key8, value8),
                new KvpOf<Key, Value>(key9, value9),
                new KvpOf<Key, Value>(key10, Key0),
                new KvpOf<Key, Value>(key11, Key1),
                new KvpOf<Key, Value>(key12, Key2),
                new KvpOf<Key, Value>(key13, Key3)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            Key key11, Value Key1,
            Key key12, Value Key2,
            Key key13, Value Key3,
            Key key14, Value Key4,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4),
                new KvpOf<Key, Value>(key5, value5),
                new KvpOf<Key, Value>(key6, value6),
                new KvpOf<Key, Value>(key7, value7),
                new KvpOf<Key, Value>(key8, value8),
                new KvpOf<Key, Value>(key9, value9),
                new KvpOf<Key, Value>(key10, Key0),
                new KvpOf<Key, Value>(key11, Key1),
                new KvpOf<Key, Value>(key12, Key2),
                new KvpOf<Key, Value>(key13, Key3),
                new KvpOf<Key, Value>(key14, Key4)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            Key key11, Value Key1,
            Key key12, Value Key2,
            Key key13, Value Key3,
            Key key14, Value Key4,
            Key key15, Value Key5,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4),
                new KvpOf<Key, Value>(key5, value5),
                new KvpOf<Key, Value>(key6, value6),
                new KvpOf<Key, Value>(key7, value7),
                new KvpOf<Key, Value>(key8, value8),
                new KvpOf<Key, Value>(key9, value9),
                new KvpOf<Key, Value>(key10, Key0),
                new KvpOf<Key, Value>(key11, Key1),
                new KvpOf<Key, Value>(key12, Key2),
                new KvpOf<Key, Value>(key13, Key3),
                new KvpOf<Key, Value>(key14, Key4),
                new KvpOf<Key, Value>(key15, Key5)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(
            Key key1, Value Key,
            Key key2, Value value2,
            Key key3, Value value3,
            Key key4, Value value4,
            Key key5, Value value5,
            Key key6, Value value6,
            Key key7, Value value7,
            Key key8, Value value8,
            Key key9, Value value9,
            Key key10, Value Key0,
            Key key11, Value Key1,
            Key key12, Value Key2,
            Key key13, Value Key3,
            Key key14, Value Key4,
            Key key15, Value Key5,
            Key key16, Value Key6,
            bool rejectBuildingAllValues = true
        ) : this(
            new ManyOf<IKvp<Key, Value>>(
                new KvpOf<Key, Value>(key1, Key),
                new KvpOf<Key, Value>(key2, value2),
                new KvpOf<Key, Value>(key3, value3),
                new KvpOf<Key, Value>(key4, value4),
                new KvpOf<Key, Value>(key5, value5),
                new KvpOf<Key, Value>(key6, value6),
                new KvpOf<Key, Value>(key7, value7),
                new KvpOf<Key, Value>(key8, value8),
                new KvpOf<Key, Value>(key9, value9),
                new KvpOf<Key, Value>(key10, Key0),
                new KvpOf<Key, Value>(key11, Key1),
                new KvpOf<Key, Value>(key12, Key2),
                new KvpOf<Key, Value>(key13, Key3),
                new KvpOf<Key, Value>(key14, Key4),
                new KvpOf<Key, Value>(key15, Key5),
                new KvpOf<Key, Value>(key16, Key6)
            ),
            rejectBuildingAllValues
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs
        /// </summary>
        public MapOf(KeyValuePair<Key, Value> item, params KeyValuePair<Key, Value>[] more) : this(
            new Enumerable.Joined<KeyValuePair<Key, Value>>(
                new ManyOf<KeyValuePair<Key, Value>>(more),
                item
            )
        )
        { }

        /// <summary>
        /// A map from the given KeyValuePairs and appends them to the given Dictionary.
        /// </summary>
        /// <param name="src">source dictionary</param>
        /// <param name="list">KeyValuePairs to append</param>
        public MapOf(IDictionary<Key, Value> src, params KeyValuePair<Key, Value>[] list) : this(
            src,
            new ManyOf<KeyValuePair<Key, Value>>(list))
        { }

        /// <summary>
        /// A map by merging the given KeyValuePairs to the given Dictionary.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="list"></param>
        public MapOf(IDictionary<Key, Value> src, IEnumerable<KeyValuePair<Key, Value>> list) : this(
            new Enumerable.Joined<KeyValuePair<Key, Value>>(
                src,
                list
            ))
        { }

        /// <summary>
        /// A map by taking the given entries.
        /// </summary>
        /// <param name="entries">enumerator of KeyValuePairs</param>
        public MapOf(IEnumerator<KeyValuePair<Key, Value>> entries) : this(
            new LiveMany<KeyValuePair<Key, Value>>(() => entries))
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        public MapOf(IKvp<Key, Value> entry, params IKvp<Key, Value>[] more) : base(() =>
             new LazyDict<Key, Value>(
                 new Enumerable.Joined<IKvp<Key, Value>>(
                     new ManyOf<IKvp<Key, Value>>(entry),
                     more
                 )
             ),
            false
        )
        { }

        /// <summary>
        /// A map from the given key value pairs.
        /// </summary>
        /// <param name="entries">enumerable of kvps</param>
        /// <param name="rejectBuildingAllValues">if you have KVPs with value functions, it is by default prevented to build all values by getting the enumerator. You can deactivate that here.</param>
        public MapOf(IEnumerable<IKvp<Key, Value>> entries, bool rejectBuildingAllValues = true) : this(
            new LazyDict<Key, Value>(entries, rejectBuildingAllValues)
        )
        { }

        /// <summary>
        /// A map from another map.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public MapOf(IDictionary<Key, Value> entries) : base(() => entries, false)
        //caution: do not remove this ctor. It is a "proxy" ctor to prevent copying values. 
        //Because a map is also a IEnumerable of KeyValuePairs, the ctor accepting the enumerable would copy the map.
        { }

        /// <summary>
        /// A map from the given entries.
        /// </summary>
        /// <param name="entries">enumerable of entries</param>
        public MapOf(IEnumerable<KeyValuePair<Key, Value>> entries) : this(
            () =>
            {
                var temp = new Dictionary<Key, Value>();
                foreach (var entry in entries)
                {
                    temp[entry.Key] = entry.Value;
                }
                return temp;
            }
        )
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">inputs</param>
        public MapOf(params IMapInput<Key, Value>[] inputs) : this(new LiveMany<IMapInput<Key, Value>>(inputs))
        { }

        /// <summary>
        /// A map from the given inputs.
        /// </summary>
        /// <param name="inputs">enumerable of map inputs</param>
        public MapOf(IEnumerable<IMapInput<Key, Value>> inputs) : this(
            () =>
            {
                IDictionary<Key, Value> dict = new LazyDict<Key, Value>();
                foreach (IMapInput<Key, Value> input in inputs)
                {
                    dict = input.Apply(dict);
                }
                return dict;
            }
        )
        { }

        /// <summary>
        /// A map from the given dictionary.
        /// </summary>
        /// <param name="input">input dictionary</param>
        public MapOf(Func<IDictionary<Key, Value>> input) : base(
            input, false
        )
        { }
    }
}
