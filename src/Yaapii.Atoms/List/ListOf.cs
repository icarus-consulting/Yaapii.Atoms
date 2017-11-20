// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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
using Yaapii.Atoms.List;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Enumerable;
using System.Collections.ObjectModel;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Makes a readonly list.
    /// </summary>
    /// <typeparam name="T">type of items</typeparam>
    public sealed class ListOf<T> : ListEnvelope<T>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="array">source array</param>
        public ListOf(params T[] array) : this(new EnumerableOf<T>(array))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerator</param>
        public ListOf(IEnumerator<T> src) : this(new EnumerableOf<T>(() => src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">source enumerable</param>
        public ListOf(IEnumerable<T> src) : base(() =>
             {
                 var temp = new List<T>();
                 foreach (T item in src)
                 {
                     temp.Add(item);
                 }

                 return temp;
             })
        { }

    }
}