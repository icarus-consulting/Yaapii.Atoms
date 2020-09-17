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
using System.Collections.Generic;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Union objects in two enumerables.
    /// </summary>
    public class Union<T> : ManyEnvelope<T>
    {
        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        /// <param name="compare">Condition to match</param>
        public Union(IEnumerable<T> a, IEnumerable<T> b, Func<T, T, bool> compare) : base(() =>
            {
                var result = new List<T>();
                foreach (var aItem in a)
                {
                    if (new Contains<T>(b, bItem => compare.Invoke(aItem, bItem)).Value())
                    {
                        result.Add(aItem);
                    }
                }
                return result;
            },
            false
        )
        { }

        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        public Union(IEnumerable<T> a, IEnumerable<T> b) : base(() =>
            {
                var result = new List<T>();
                foreach (var item in b)
                {
                    if (new Contains<T>(a, item).Value())
                    {
                        result.Add(item);
                    }
                }
                return result;
            },
            false
        )
        { }

        /// <summary>
        /// Union objects in two enumerables.
        /// </summary>
        private Union(Func<IEnumerable<T>> unite) : base(unite, false)
        { }
    }
}
