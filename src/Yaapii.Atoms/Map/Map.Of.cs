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
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Lookup
{
    public partial class Map
    {
        /// <summary>
        /// A map from string to string.
        /// </summary>
        public sealed class Of : Map.Envelope
        {
            /// <summary>
            /// A map from the given KeyValuePairs
            /// </summary>
            public Of(KeyValuePair<string, string> entry, params KeyValuePair<string, string>[] more) : this(
                new Enumerable.Joined<KeyValuePair<string, string>>(
                    new Many.Live<KeyValuePair<string, string>>(more),
                    entry
                )
            )
            { }

            /// <summary>
            /// A map from the given KeyValuePairs and appends them to the given Dictionary.
            /// </summary>
            /// <param name="src">source dictionary</param>
            /// <param name="list">KeyValuePairs to append</param>
            public Of(IDictionary<string, string> src, params KeyValuePair<string, string>[] list) : this(
                src,
                new Many.Live<KeyValuePair<string, string>>(list))
            { }

            /// <summary>
            /// A map by merging the given KeyValuePairs to the given Dictionary.
            /// </summary>
            /// <param name="src"></param>
            /// <param name="list"></param>
            public Of(IDictionary<string, string> src, IEnumerable<KeyValuePair<string, string>> list) : this(
                new Enumerable.Joined<KeyValuePair<string, string>>(
                    src,
                    list
                ))
            { }

            /// <summary>
            /// A map by taking the given entries.
            /// </summary>
            /// <param name="entries">enumerator of KeyValuePairs</param>
            public Of(IEnumerator<KeyValuePair<string, string>> entries) : this(
                new Many.Live<KeyValuePair<string, string>>(() => entries))
            { }

            /// <summary>
            /// A map from the given function result
            /// </summary>
            /// <param name="fnc"></param>
            public Of(Func<IEnumerable<KeyValuePair<string, string>>> fnc) : this(
                new Many.Live<KeyValuePair<string, string>>(fnc))
            { }

            /// <summary>
            /// A map from the given key value pairs.
            /// </summary>
            public Of(IKvp entry, params IKvp[] more) : this(
                new Many.Live<IMapInput>(
                    new MapInput.Of(entry),
                    new MapInput.Of(more)
                )
            )
            { }

            /// <summary>
            /// A map from the given key value pairs.
            /// </summary>
            /// <param name="entries">enumerable of kvps</param>
            public Of(IEnumerable<IKvp> entries) : this(
                new Many.Live<IMapInput>(
                    new MapInput.Of(entries)
                )
            )
            { }

            /// <summary>
            /// A map from the given entries.
            /// </summary>
            /// <param name="entries">enumerable of entries</param>
            public Of(IEnumerable<KeyValuePair<string, string>> entries) : this(
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
            /// <param name="pairSequence">Pairs as a sequence, ordered like this: key-1, value-1, ... key-n, value-n</param>
            public Of(params string[] pairSequence) : this(new Many.Of<string>(pairSequence))
            { }

            /// <summary>
            /// A map from string to string.
            /// </summary>
            /// <param name="pairSequence">Pairs as a sequence, ordered like this: key-1, value-1, ... key-n, value-n</param>
            public Of(IEnumerable<string> pairSequence) : this(
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

                    if (idx % 2 != 1)
                    {
                        throw new ArgumentException($"Cannot build a map because an even number of strings is needed, and the provided ones count {idx}");
                    }
                    return result;
                }
            )
            { }

            /// <summary>
            /// A map from the given inputs.
            /// </summary>
            /// <param name="inputs">inputs</param>
            public Of(params IMapInput[] inputs) : this(new Many.Of<IMapInput>(inputs))
            { }

            /// <summary>
            /// A map from the given inputs.
            /// </summary>
            /// <param name="inputs">enumerable of map inputs</param>
            public Of(IEnumerable<IMapInput> inputs) : this(
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
            public Of(Func<IDictionary<string, string>> input) : base(
                input, false
            )
            { }
        }

        /// <summary>
        /// A map from string to typed value.
        /// </summary>
        public sealed class Of<Value> : Envelope<Value>
        {
            /// <summary>
            /// A map from the given KeyValuePairs
            /// </summary>
            public Of(KeyValuePair<string, Value> entry, params KeyValuePair<string, Value>[] more) : this(
                new Enumerable.Joined<KeyValuePair<string, Value>>(
                    new Many.Live<KeyValuePair<string, Value>>(more),
                    entry
                )
            )
            { }

            /// <summary>
            /// A map from the given KeyValuePairs and appends them to the given Dictionary.
            /// </summary>
            /// <param name="src">source dictionary</param>
            /// <param name="list">KeyValuePairs to append</param>
            public Of(IDictionary<string, Value> src, params KeyValuePair<string, Value>[] list) : this(
                src,
                new Many.Live<KeyValuePair<string, Value>>(list))
            { }

            /// <summary>
            /// A map by merging the given KeyValuePairs to the given Dictionary.
            /// </summary>
            /// <param name="src"></param>
            /// <param name="list"></param>
            public Of(IDictionary<string, Value> src, IEnumerable<KeyValuePair<string, Value>> list) : this(
                new Enumerable.Joined<KeyValuePair<string, Value>>(
                    src,
                    list
                ))
            { }

            /// <summary>
            /// A map by taking the given entries.
            /// </summary>
            /// <param name="entries">enumerator of KeyValuePairs</param>
            public Of(IEnumerator<KeyValuePair<string, Value>> entries) : this(
                new Many.Live<KeyValuePair<string, Value>>(() => entries))
            { }

            /// <summary>
            /// A map from the given function result
            /// </summary>
            /// <param name="fnc"></param>
            public Of(Func<IEnumerable<KeyValuePair<string, Value>>> fnc) : this(
                new Many.Live<KeyValuePair<string, Value>>(fnc))
            { }

            /// <summary>
            /// A map from the given key value pairs.
            /// </summary>
            /// <param name="entries">enumerable of kvps</param>
            public Of(IKvp<Value> entry, params IKvp<Value>[] entries) : this(
                new Many.Live<IMapInput<Value>>(
                    new MapInput.Of<Value>(entry),
                    new MapInput.Of<Value>(entries)
                )
            )
            { }

            /// <summary>
            /// A map from the given key value pairs.
            /// </summary>
            /// <param name="entries">enumerable of kvps</param>
            public Of(IEnumerable<IKvp<Value>> entries) : this(
                new Many.Live<IMapInput<Value>>(
                    new MapInput.Of<Value>(entries)
                )
            )
            { }

            /// <summary>
            /// A map from the given entries.
            /// </summary>
            /// <param name="entries">enumerable of entries</param>
            public Of(IEnumerable<KeyValuePair<string, Value>> entries) : this(
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
            public Of(params IMapInput<Value>[] inputs) : this(
                new Many.Live<IMapInput<Value>>(inputs)
            )
            { }

            /// <summary>
            /// A map from the given inputs.
            /// </summary>
            /// <param name="inputs">enumerable of map inputs</param>
            public Of(IEnumerable<IMapInput<Value>> inputs) : this(
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
            public Of(Func<IDictionary<string, Value>> input) : base(
                input, false
            )
            { }
        }

        /// <summary>
        /// A map from string to typed value.
        /// </summary>
        public sealed class Of<Key, Value> : Envelope<Key, Value>
        {
            /// <summary>
            /// A map from the given KeyValuePairs
            /// </summary>
            public Of(KeyValuePair<Key, Value> item, params KeyValuePair<Key, Value>[] more) : this(
                new Enumerable.Joined<KeyValuePair<Key, Value>>(
                    new Many.Live<KeyValuePair<Key, Value>>(more),
                    item
                )
            )
            { }

            /// <summary>
            /// A map from the given KeyValuePairs and appends them to the given Dictionary.
            /// </summary>
            /// <param name="src">source dictionary</param>
            /// <param name="list">KeyValuePairs to append</param>
            public Of(IDictionary<Key, Value> src, params KeyValuePair<Key, Value>[] list) : this(
                src,
                new Many.Live<KeyValuePair<Key, Value>>(list))
            { }

            /// <summary>
            /// A map by merging the given KeyValuePairs to the given Dictionary.
            /// </summary>
            /// <param name="src"></param>
            /// <param name="list"></param>
            public Of(IDictionary<Key, Value> src, IEnumerable<KeyValuePair<Key, Value>> list) : this(
                new Enumerable.Joined<KeyValuePair<Key, Value>>(
                    src,
                    list
                ))
            { }

            /// <summary>
            /// A map by taking the given entries.
            /// </summary>
            /// <param name="entries">enumerator of KeyValuePairs</param>
            public Of(IEnumerator<KeyValuePair<Key, Value>> entries) : this(
                new Many.Live<KeyValuePair<Key, Value>>(() => entries))
            { }

            /// <summary>
            /// A map from the given function result
            /// </summary>
            /// <param name="fnc"></param>
            public Of(Func<IEnumerable<KeyValuePair<Key, Value>>> fnc) : this(
                new Many.Live<KeyValuePair<Key, Value>>(fnc))
            { }

            /// <summary>
            /// A map from the given key value pairs.
            /// </summary>
            public Of(IKvp<Key, Value> entry, params IKvp<Key, Value>[] more) : this(
                new Many.Live<IMapInput<Key, Value>>(
                    new MapInput.Of<Key, Value>(entry),
                    new MapInput.Of<Key, Value>(more)
                )
            )
            { }

            /// <summary>
            /// A map from the given key value pairs.
            /// </summary>
            /// <param name="entries">enumerable of kvps</param>
            public Of(IEnumerable<IKvp<Key, Value>> entries) : this(
                new Many.Live<IMapInput<Key, Value>>(
                    new MapInput.Of<Key, Value>(entries)
                )
            )
            { }

            /// <summary>
            /// A map from the given entries.
            /// </summary>
            /// <param name="entries">enumerable of entries</param>
            public Of(IEnumerable<KeyValuePair<Key, Value>> entries) : this(
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
            public Of(params IMapInput<Key, Value>[] inputs) : this(new Many.Of<IMapInput<Key, Value>>(inputs))
            { }

            /// <summary>
            /// A map from the given inputs.
            /// </summary>
            /// <param name="inputs">enumerable of map inputs</param>
            public Of(IEnumerable<IMapInput<Key, Value>> inputs) : this(
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
            public Of(Func<IDictionary<Key, Value>> input) : base(
                input, false
            )
            { }
        }
    }
}
