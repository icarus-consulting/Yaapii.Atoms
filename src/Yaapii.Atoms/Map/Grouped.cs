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

using System.Collections.Generic;
using Yaapii.Atoms.List;

namespace Yaapii.Atoms.Lookup
{
    /// <summary>
    /// Groups a list to Keys and Lists of Values according to the given Functions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    public sealed class Grouped<T, Key, Value> : Map.Envelope<Key, IList<Value>>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">Source Enumerable</param>
        /// <param name="key">Function to convert Source Type to Key Type</param>
        /// <param name="value">Function to Convert Source Type to Key Týpe</param>
        public Grouped(IEnumerable<T> src, IFunc<T, Key> key, IFunc<T, Value> value) : this(
            new List.List.Live<T>(src),
            key,
            value
        )
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">Source Enumerable</param>
        /// <param name="key">Function to convert Source Type to Key Type</param>
        /// <param name="value">Function to Convert Source Type to Key Týpe</param>
        public Grouped(IList<T> src, IFunc<T, Key> key, IFunc<T, Value> value) : base(
            () =>
            {
                Dictionary<Key, IList<Value>> temp = new Dictionary<Key, IList<Value>>();
                foreach (var entry in src)
                {
                    temp[key.Invoke(entry)] = new Mapped<T, Value>(value, src);
                }
                return temp;
            },
            false
        )
        { }
    }
}
