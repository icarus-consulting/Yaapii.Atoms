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
using Yaapii.Atoms.Func;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Multiple <see cref="IEnumerable{T}"/> joined together.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Joined<T> : Many.Envelope<T>
    {
        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(IEnumerable<T> lst, params T[] items) : this(
            new Many.Live<IEnumerable<T>>(lst, new Many.Live<T>(items)))
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public Joined(params IEnumerable<T>[] items) : this(
            new Many.Live<IEnumerable<T>>(items)
        )
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public Joined(IEnumerable<IEnumerable<T>> items) : base(() => 
            new Many.Live<T>(() =>
                new Enumerator.Joined<T>(
                    new Mapped<IEnumerable<T>, IEnumerator<T>>(//Map the content of list: Get every enumerator out of it and build one whole enumerator from it
                        new StickyFunc<IEnumerable<T>, IEnumerator<T>>( //Sticky Gate
                            new FuncOf<IEnumerable<T>, IEnumerator<T>>(
                                e => e.GetEnumerator() //Get the Enumerator
                            )
                        ),
                        items //List with enumerators
                    )
                )
            ),
            false
        )
        { }
    }
}