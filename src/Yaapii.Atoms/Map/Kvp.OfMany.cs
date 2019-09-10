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
    public partial class Kvp
    {
        /// <summary>
        /// A key to many strings.
        /// Since 1.9.2019
        /// </summary>
        public sealed class OfMany : Kvp.Envelope<IEnumerable<string>>
        {
            /// <summary>
            /// A key to many values.
            /// The functions are executed only when the value is requested.
            /// The result is sticky.
            /// </summary>
            public OfMany(string key, params Func<string>[] many) : this(key, () =>
                {
                    var lst = new List<string>();
                    for (var i = 0; i < many.Length; i++)
                    {
                        lst.Add(many[i]());
                    }
                    return lst;
                })
            { }

            /// <summary>
            /// A key to many values.
            /// The functions are executed only when the value is requested.
            /// The result is sticky.
            /// </summary>
            public OfMany(string key, params string[] many) : this(key, () => new Strings(many))
            { }

            /// <summary>
            /// A key to many values.
            /// </summary>
            public OfMany(string key, IEnumerable<string> many) : this(key, () => many)
            { }

            /// <summary>
            /// A key to many strings.
            /// The function is executed only when the value is requested.
            /// The result is sticky.
            /// </summary>
            public OfMany(string key, Func<IEnumerable<string>> many) : base(
                new Kvp.Of<IEnumerable<string>>(key, many)
            )
            { }
        }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public sealed class OfMany<TValue> : Kvp.Envelope<IEnumerable<TValue>>
        {
            /// <summary>
            /// A key to many strings.
            /// The functions are executed only when the value is requested.
            /// The result is sticky.
            /// </summary>
            public OfMany(string key, params Func<TValue>[] many) : this(key, () =>
                {
                    var lst = new List<TValue>();
                    for (var i = 0; i < many.Length; i++)
                    {
                        lst.Add(many[i]());
                    }
                    return lst;
                }
            )
            { }

            /// <summary>
            /// A key to many strings.
            /// The functions are executed only when the value is requested.
            /// The result is sticky.
            /// </summary>
            public OfMany(string key, params TValue[] many) : this(key, () =>
                {
                    var lst = new List<TValue>();
                    for (var i = 0; i < many.Length; i++)
                    {
                        lst.Add(many[i]);
                    }
                    return lst;
                }
            )
            { }

            /// <summary>
            /// A key to many strings.
            /// </summary>
            public OfMany(string key, IEnumerable<TValue> many) : this(key, () => many)
            { }

            /// <summary>
            /// A key to many values.
            /// </summary>
            public OfMany(string key, Func<IEnumerable<TValue>> many) : base(
                new Kvp.Of<IEnumerable<TValue>>(key, many)
            )
            { }
        }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public sealed class Values<TKey, TValue> : Kvp.Envelope<TKey, IEnumerable<TValue>>
        {
            /// <summary>
            /// A key to many values.
            /// The functions are executed only when the value is requested.
            /// The result is sticky.
            /// </summary>
            public Values(TKey key, params Func<TValue>[] many) : this(key, () =>
            {
                var lst = new List<TValue>();
                for (var i = 0; i < many.Length; i++)
                {
                    lst.Add(many[i]());
                }
                return lst;
            }
            )
            { }

            /// <summary>
            /// A key to many values.
            /// The functions are executed only when the value is requested.
            /// The result is sticky.
            /// </summary>
            public Values(TKey key, params TValue[] many) : this(key, () =>
            {
                var lst = new List<TValue>();
                for (var i = 0; i < many.Length; i++)
                {
                    lst.Add(many[i]);
                }
                return lst;
            }
            )
            { }

            /// <summary>
            /// A key to many values.
            /// </summary>
            public Values(TKey key, IEnumerable<TValue> many) : this(key, () => many)
            { }

            /// <summary>
            /// A key to many values.
            /// </summary>
            public Values(TKey key, Func<IEnumerable<TValue>> many) : base(
                new Kvp.Of<TKey, IEnumerable<TValue>>(key, many)
            )
            { }
        }
    }
}
