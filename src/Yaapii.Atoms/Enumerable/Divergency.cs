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
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Items which do only exist in one enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Divergency<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> a;
        private readonly IEnumerable<T> b;
        private readonly Func<T, bool> match;

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public Divergency(IEnumerable<T> a, IEnumerable<T> b) : this(
            a, b, item => true
        )
        { }

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public Divergency(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> match)
        {
            this.a = new Filtered<T>(match, a);
            this.b = new Filtered<T>(match, b);
            this.match = match;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var all1 = new HashSet<T>(EqualityComparer<T>.Default);
            var all2 = new HashSet<T>(EqualityComparer<T>.Default);
            var notin1 = new HashSet<T>(EqualityComparer<T>.Default);
            var notin2 = new HashSet<T>(EqualityComparer<T>.Default);
            foreach (var element in this.a)
            {
                all1.Add(element);
            }

            foreach (var element in this.b)
            {
                all2.Add(element);
            }

            foreach (var element in this.b)
            {
                if (all1.Add(element))
                {
                    notin1.Add(element);
                }
            }

            foreach (var element in this.a)
            {
                if (all2.Add(element))
                {
                    notin2.Add(element);
                }
            }

            foreach(var item in new Joined<T>(notin2, notin1))
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// Items which do only exist in one enumerable.
    /// </summary>
    public static class Divergency
    {
        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public static IEnumerable<T> New<T>(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> match) => new Divergency<T>(a, b, match);

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public static IEnumerable<T> New<T>(IEnumerable<T> a, IEnumerable<T> b) => new Divergency<T>(a, b);

    }
}
