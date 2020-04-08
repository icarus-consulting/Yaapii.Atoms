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
using System.Linq;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> out of strings.
    /// </summary>

    public sealed class ManyOf : ManyEnvelope<string>
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public ManyOf(params string[] items) : this(() =>
            {
                var lst = new List<string>();
                for(int i=0;i<items.Length;i++)
                {
                    lst.Add(items[i]);
                };
                return lst;
            }
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <param name="e">a enumerator</param>
        public ManyOf(IEnumerator<string> e) : this(new Live<IEnumerator<string>>(e))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        public ManyOf(IScalar<IEnumerator<string>> sc) : this(() => sc.Value())
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public ManyOf(Func<IEnumerable<string>> fnc) : this(() => fnc().GetEnumerator())
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}"/>"/>.
        /// </summary>
        /// <param name="origin">scalar to return the IEnumerator</param>
        public ManyOf(Func<IEnumerator<string>> origin) : base(
            () =>
            {
                var enm = origin();
                var lst = new List<string>();
                while (enm.MoveNext())
                {
                    lst.Add(enm.Current);
                };
                return lst;
            },
            false
        )
        { }
    }

    /// <summary>
    /// A <see cref="IEnumerable{T}"/> out of other objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public sealed class ManyOf<T> : ManyEnvelope<T>
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public ManyOf(params T[] items) : this(
            () => items.AsEnumerable<T>().GetEnumerator()
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <param name="e">a enumerator</param>
        public ManyOf(IEnumerator<T> e) : this(new Live<IEnumerator<T>>(e))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        public ManyOf(IScalar<IEnumerator<T>> sc) : this(() => sc.Value())
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public ManyOf(Func<IEnumerable<T>> fnc) : this(() => fnc().GetEnumerator())
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}"/>"/>.
        /// </summary>
        /// <param name="origin">scalar to return the IEnumerator</param>
        public ManyOf(Func<IEnumerator<T>> origin) : base(
            () =>
            {
                var enm = origin();
                var lst = new List<T>();
                while (enm.MoveNext())
                {
                    lst.Add(enm.Current);
                };
                return lst;
            },
            false
        )
        { }
    }
}