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
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// A filtered <see cref="IEnumerable{T}"/>.
    /// Pass a filter function which will applied to all items, similar to List{T}.Where(...) in LinQ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Filtered<T> : LiveEnumerableEnvelope<T>
    {
        /// <summary>
        /// the enumerable to filter
        /// </summary>
        private readonly IEnumerable<T> enumerable;

        /// <summary>
        /// filter function
        /// </summary>
        private readonly Func<T, Boolean> func;

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="fnc">filter function</param>
        /// <param name="item1">first item to filter</param>
        /// <param name="item2">secound item to filter</param>
        /// <param name="items">other items to filter</param>
        public Filtered(Func<T, Boolean> fnc, T item1, T item2, params T[] items) : this(
            fnc,
            new LiveEnumerable<T>(() => 
                new Joined<T>(
                    new LiveEnumerable<T>(
                        item1,
                        item2
                    ),
                    items
                ).GetEnumerator()
            )
        )
        { }

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public Filtered(Func<T, Boolean> fnc, IEnumerable<T> src) : base(() =>
            new LiveEnumerable<T>(() =>
                new Enumerator.Filtered<T>(
                    src.GetEnumerator(),
                    fnc
                )
            )
        )
        {
            this.enumerable = src;
            this.func = fnc;
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics
