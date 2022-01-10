// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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
    /// A key to many strings.
    /// Since 1.9.2019
    /// </summary>
    public sealed class KvpOfMany : KvpEnvelope<IEnumerable<string>>
    {
        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KvpOfMany(string key, params Func<string>[] many) : this(key, () =>
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
        public KvpOfMany(string key, params string[] values) : this(key, () => new ManyOf(values))
        { }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public KvpOfMany(string key, IEnumerable<string> values) : this(key, () => values)
        { }

        /// <summary>
        /// A key to many strings.
        /// The function is executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KvpOfMany(string key, Func<IEnumerable<string>> values) : base(
            new KvpOf<IEnumerable<string>>(key, values)
        )
        { }

        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public static KvpOfMany<TValue> New<TValue>(string key, params Func<TValue>[] values)
            => new KvpOfMany<TValue>(key, values);

        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public static KvpOfMany<TValue> New<TValue>(string key, params TValue[] values)
            => new KvpOfMany<TValue>(key, values);

        /// <summary>
        /// A key to many strings.
        /// </summary>
        public static KvpOfMany<TValue> New<TValue>(string key, IEnumerable<TValue> values)
            => new KvpOfMany<TValue>(key, values);

        /// <summary>
        /// A key to many values.
        /// </summary>
        public static KvpOfMany<TValue> New<TValue>(string key, Func<IEnumerable<TValue>> values)
            => new KvpOfMany<TValue>(key, values);

        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public static KeyToValues<TKey, TValue> New<TKey, TValue>(TKey key, params Func<TValue>[] many)
            => new KeyToValues<TKey, TValue>(key, many);

        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public static KeyToValues<TKey, TValue> New<TKey, TValue>(TKey key, params TValue[] many)
            => new KeyToValues<TKey, TValue>(key, many);

        /// <summary>
        /// A key to many values.
        /// </summary>
        public static KeyToValues<TKey, TValue> New<TKey, TValue>(TKey key, IEnumerable<TValue> many)
            => new KeyToValues<TKey, TValue>(key, many);

        /// <summary>
        /// A key to many values.
        /// </summary>
        public static KeyToValues<TKey, TValue> New<TKey, TValue>(TKey key, Func<IEnumerable<TValue>> many)
            => new KeyToValues<TKey, TValue>(key, many);
    }

    /// <summary>
    /// A key to many values.
    /// </summary>
    public sealed class KvpOfMany<TValue> : KvpEnvelope<IEnumerable<TValue>>
    {
        /// <summary>
        /// A key to many strings.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KvpOfMany(string key, params Func<TValue>[] values) : this(key, () =>
            {
                var lst = new List<TValue>();
                for (var i = 0; i < values.Length; i++)
                {
                    lst.Add(values[i]());
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
        public KvpOfMany(string key, params TValue[] values) : this(key, () =>
            {
                var lst = new List<TValue>();
                for (var i = 0; i < values.Length; i++)
                {
                    lst.Add(values[i]);
                }
                return lst;
            }
        )
        { }

        /// <summary>
        /// A key to many strings.
        /// </summary>
        public KvpOfMany(string key, IEnumerable<TValue> values) : this(key, () => values)
        { }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public KvpOfMany(string key, Func<IEnumerable<TValue>> values) : base(
            new KvpOf<IEnumerable<TValue>>(key, values)
        )
        { }
    }

    /// <summary>
    /// A key to many values.
    /// </summary>
    public sealed class KeyToValues<TKey, TValue> : KvpEnvelope<TKey, IEnumerable<TValue>>
    {
        /// <summary>
        /// A key to many values.
        /// The functions are executed only when the value is requested.
        /// The result is sticky.
        /// </summary>
        public KeyToValues(TKey key, params Func<TValue>[] many) : this(key, () =>
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
        public KeyToValues(TKey key, params TValue[] many) : this(key, () =>
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
        public KeyToValues(TKey key, IEnumerable<TValue> many) : this(key, () => many)
        { }

        /// <summary>
        /// A key to many values.
        /// </summary>
        public KeyToValues(TKey key, Func<IEnumerable<TValue>> many) : base(
            new KvpOf<TKey, IEnumerable<TValue>>(key, many)
        )
        { }
    }
}
