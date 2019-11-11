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
using Yaapii.Atoms.Texts;

namespace Yaapii.Atoms.Lookup
{
    public partial class Kvp
    {
        /// <summary>
        /// Key-value pair made of strings.
        /// Since 9.9.2019
        /// </summary>
        public sealed class Of : IKvp
        {
            private readonly Lazy<KeyValuePair<string, Func<string>>> entry;
            private readonly Lazy<string> value;

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(string key, string value) : this(
                new TextOf(key),
                value
            )
            { }

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(string key, Func<string> value) : this(
                () => new KeyValuePair<string, Func<string>>(key, value)
            )
            { }

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(IText key, Func<string> value) : this(
                () => new KeyValuePair<string, Func<string>>(
                    key.AsString(), value
                )
            )
            { }

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(IText key, string value) : this(
                () => new KeyValuePair<string, Func<string>>(
                    key.AsString(),
                    () => value
                )
            )
            { }

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(IScalar<KeyValuePair<string, string>> kvp)
            { }

            private Of(Func<KeyValuePair<string, Func<string>>> kvp)
            {
                this.entry =
                    new Lazy<KeyValuePair<string, Func<string>>>(
                        () => kvp.Invoke()
                    );
                this.value = new Lazy<string>(() => this.entry.Value.Value.Invoke());
            }

            public string Key()
            {
                return this.entry.Value.Key;
            }

            public string Value()
            {
                return this.value.Value;
            }
        }

        /// <summary>
        /// Key-value pair matching a string to specified type value.
        /// </summary>
        public sealed class Of<TValue> : IKvp<TValue>
        {
            private readonly Lazy<KeyValuePair<string, Func<TValue>>> entry;
            private readonly Lazy<TValue> value;

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(string key, TValue value) : this(
                new TextOf(key),
                value
            )
            { }

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(string key, Func<TValue> value) : this(
                () => new KeyValuePair<string, Func<TValue>>(key, value)
            )
            { }

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(IText key, Func<TValue> value) : this(
                () => new KeyValuePair<string, Func<TValue>>(
                    key.AsString(), value
                )
            )
            { }

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(IText key, TValue value) : this(
                () => new KeyValuePair<string, Func<TValue>>(
                    key.AsString(),
                    () => value
                )
            )
            { }

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(IScalar<KeyValuePair<string, TValue>> kvp)
            { }

            private Of(Func<KeyValuePair<string, Func<TValue>>> kvp)
            {
                this.entry =
                    new Lazy<KeyValuePair<string, Func<TValue>>>(
                        () => kvp.Invoke()
                    );
                this.value = new Lazy<TValue>(() => this.entry.Value.Value.Invoke());
            }

            public string Key()
            {
                return this.entry.Value.Key;
            }

            public TValue Value()
            {
                return this.value.Value;
            }
        }

        /// <summary>
        /// Key-value pair matching a key type to specified type value.
        /// </summary>
        public sealed class Of<TKey, TValue> : IKvp<TKey, TValue>
        {
            private readonly Lazy<KeyValuePair<TKey, Func<TValue>>> entry;
            private readonly Lazy<TValue> value;

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(TKey key, Func<TValue> value) : this(
                () => new KeyValuePair<TKey, Func<TValue>>(key, value)
            )
            { }

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(TKey key, TValue value) : this(
                () => new KeyValuePair<TKey, Func<TValue>>(key, () => value)
            )
            { }

            /// <summary>
            /// Key-value pair matching a string to specified type value.
            /// </summary>
            public Of(IScalar<KeyValuePair<TKey, TValue>> kvp)
            { }

            private Of(Func<KeyValuePair<TKey, Func<TValue>>> kvp)
            {
                this.entry =
                    new Lazy<KeyValuePair<TKey, Func<TValue>>>(
                        () => kvp.Invoke()
                    );
                this.value = new Lazy<TValue>(() => this.entry.Value.Value.Invoke());
            }

            public TKey Key()
            {
                return this.entry.Value.Key;
            }

            public TValue Value()
            {
                return this.value.Value;
            }
        }
    }
}
