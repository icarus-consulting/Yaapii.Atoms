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

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Items which do only exist in one enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Divergency<T> : ManyEnvelope<T>
    {
        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public Divergency(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> match) : base(() =>
            {
                var result = new List<T>();
                foreach (var item in b)
                {
                    if (new Contains<T>(a, match).Value() != new Contains<T>(b, match).Value())
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
        /// Items which do only exist in one enumerable.
        /// </summary>
        public Divergency(IEnumerable<T> a, IEnumerable<T> b) : base(() =>
            {
                var result = new List<T>();
                foreach (var item in new Joined<T>(a, b))
                {
                    if (new Contains<T>(a, item).Value() != new Contains<T>(b, item).Value())
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
        /// Items which do only exist in one enumerable.
        /// </summary>
        private Divergency(Func<IEnumerable<T>> unite) : base(unite, false)
        { }
    }

    /// <summary>
    /// Items which do only exist in one enumerable.
    /// </summary>
    public static class Divergency
    {
        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public static Divergency<T> New<T>(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> match) => new Divergency<T>(a, b, match);

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public static Divergency<T> New<T>(IEnumerable<T> a, IEnumerable<T> b) => new Divergency<T>(a, b);

    }
}
