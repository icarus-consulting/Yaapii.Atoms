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
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// The sticky represantation of an <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T">The type of the enumerable</typeparam>
    public class Sticky<T> : EnumerableEnvelope<T>
    {
        /// <summary>
        /// Makes a sticky enumerable
        /// </summary>
        /// <param name="items">The items</param>
        public Sticky(params T[] items) : this(new EnumerableOf<T>(items))
        { }
       
        /// <summary>
        /// Makes a sticky enumerable
        /// </summary>
        /// <param name="item">The enumerator</param>
        public Sticky(IEnumerator<T> item) : this(new EnumerableOf<T>(item))
        { }
       
        /// <summary>
        /// Makes a sticky enumerable
        /// </summary>
        /// <param name="src"></param>
        public Sticky(IEnumerable<T> src) : base(
            new StickyScalar<IEnumerable<T>>(() =>
            {
                List<T> lst = new List<T>();
                foreach (T item in src)
                {
                    lst.Add(item);
                }
                return lst;
            }
        ))
        { }
    }
}
