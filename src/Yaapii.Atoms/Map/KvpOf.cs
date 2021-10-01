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

using System;
using System.Collections.Generic;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Map
{
    /// <summary>
    /// Key-value pair made of strings.
    /// Since 9.9.2019
    /// </summary>
    public sealed class KvpOf : IKvp
    {
        private readonly ScalarOf<KeyValuePair<string, Func<string>>> entry;
        private readonly ScalarOf<string> value;
        private readonly bool isLazy;

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public KvpOf(IText key, Func<string> value)
            : this(key.AsString(), value)
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public KvpOf(IText key, string value)
            : this(key.AsString(), value)
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public KvpOf(string key, Func<string> value) : this(
            () => new KeyValuePair<string, Func<string>>(key, value),
            true
        )
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public KvpOf(string key, string value) : this(
            () => new KeyValuePair<string, Func<string>>(key, () => value),
            false
        )
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public KvpOf(IScalar<KeyValuePair<string, string>> kvp)
            : this(() => kvp.Value())
        { }

        /// <summary>
        /// Key-value pair made of strings.
        /// </summary>
        public KvpOf(Func<KeyValuePair<string, string>> kvp)
            : this(() =>
            {
                var simple = kvp.Invoke();
                return new KeyValuePair<string, Func<string>>(simple.Key, () => simple.Value);
            }, true)
        { }

        private KvpOf(Func<KeyValuePair<string, Func<string>>> kvp, bool isLazy)
        {
            this.entry =
                new ScalarOf<KeyValuePair<string, Func<string>>>(
                    () => kvp.Invoke()
                );
            this.value = new ScalarOf<string>(() => this.entry.Value().Value.Invoke());
            this.isLazy = isLazy;
        }

        public string Key()
        {
            return this.entry.Value().Key;
        }

        public string Value()
        {
            return this.value.Value();
        }

        public bool IsLazy()
        {
            return this.isLazy;
        }
    }

    /// <summary>
    /// Key-value pair matching a string to specified type value.
    /// </summary>
    public sealed class KvpOf<TValue> : IKvp<TValue>
    {
        private readonly ScalarOf<KeyValuePair<string, Func<TValue>>> entry;
        private readonly ScalarOf<TValue> value;
        private readonly bool isLazy;

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IText key, Func<TValue> value)
            : this(key.AsString(), value)
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IText key, TValue value)
            : this(key.AsString(), value)
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(string key, Func<TValue> value) : this(
            () => new KeyValuePair<string, Func<TValue>>(key, value),
            true
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(string key, TValue value) : this(
            () => new KeyValuePair<string, Func<TValue>>(key, () => value),
            false
        )
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(IScalar<KeyValuePair<string, TValue>> kvp)
            : this(() => kvp.Value())
        { }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public KvpOf(Func<KeyValuePair<string, TValue>> kvp)
            : this(() =>
            {
                var simple = kvp.Invoke();
                return new KeyValuePair<string, Func<TValue>>(simple.Key, () => simple.Value);
            }, true)
        { }

        private KvpOf(Func<KeyValuePair<string, Func<TValue>>> kvp, bool isLazy)
        {
            this.entry =
                new ScalarOf<KeyValuePair<string, Func<TValue>>>(
                    () => kvp.Invoke()
                );
            this.value = new ScalarOf<TValue>(() => this.entry.Value().Value.Invoke());
            this.isLazy = isLazy;
        }

        public string Key()
        {
            return this.entry.Value().Key;
        }

        public TValue Value()
        {
            return this.value.Value();
        }

        public bool IsLazy()
        {
            return this.isLazy;
        }
    }

    /// <summary>
    /// Key-value pair matching a key type to specified type value.
    /// </summary>
    public sealed class KvpOf<TKey, TValue> : IKvp<TKey, TValue>
    {
        private readonly ScalarOf<KeyValuePair<TKey, Func<TValue>>> entry;
        private readonly ScalarOf<TValue> value;
        private readonly bool isLazy;

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public KvpOf(TKey key, Func<TValue> value) : this(
            () => new KeyValuePair<TKey, Func<TValue>>(key, value),
            true
        )
        { }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public KvpOf(TKey key, TValue value) : this(
            () => new KeyValuePair<TKey, Func<TValue>>(key, () => value),
            false
        )
        { }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public KvpOf(IScalar<KeyValuePair<TKey, TValue>> kvp)
            : this(() => kvp.Value())
        { }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public KvpOf(Func<KeyValuePair<TKey, TValue>> kvp)
            : this(() =>
            {
                var simple = kvp.Invoke();
                return new KeyValuePair<TKey, Func<TValue>>(simple.Key, () => simple.Value);
            }, true)
        { }

        private KvpOf(Func<KeyValuePair<TKey, Func<TValue>>> kvp, bool isLazy)
        {
            this.entry =
                new ScalarOf<KeyValuePair<TKey, Func<TValue>>>(
                    () => kvp.Invoke()
                );
            this.value = new ScalarOf<TValue>(() => this.entry.Value().Value.Invoke());
            this.isLazy = isLazy;
        }

        public TKey Key()
        {
            return this.entry.Value().Key;
        }

        public TValue Value()
        {
            return this.value.Value();
        }

        public bool IsLazy()
        {
            return this.isLazy;
        }
    }
}
