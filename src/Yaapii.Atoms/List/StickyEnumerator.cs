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
using System.Diagnostics;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoProperties // No Properties
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerator{T}"/> that returns content from cache always.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public sealed class StickyEnumerator<X> : IEnumerator<X>
    {
        private readonly IScalar<IEnumerator<X>> _gate;

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that returns content from cache always.
        /// </summary>
        /// <param name="src">items</param>
        public StickyEnumerator(params X[] src) : this(
            () => new EnumerableOf<X>(src).GetEnumerator())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">enumerator to put to cache</param>
        public StickyEnumerator(IEnumerator<X> src) : this(new ScalarOf<IEnumerator<X>>(src))
        { }

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> from the given that returns content from cache always.
        /// </summary>
        /// <param name="src">function to retrieve enumerator for cache</param>
        public StickyEnumerator(Func<IEnumerator<X>> src) : this(new CallableOf<IEnumerator<X>>(src))
        { }

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> from the given that returns content from cache always.
        /// </summary>
        /// <param name="src">function to retrieve enumerator for cache</param>
        public StickyEnumerator(ICallable<IEnumerator<X>> src) : this(new ScalarOf<IEnumerator<X>>(src))
        { }

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that returns content from cache always.
        /// </summary>
        /// <param name="src">scalar of enumerator to cache</param>
        public StickyEnumerator(IScalar<IEnumerator<X>> src)
        {
            this._gate =
                new StickyScalar<IEnumerator<X>>(
                    () =>
                    {
                        var temp = new LinkedList<X>();
                        var enumerator = src.Value();
                        while (enumerator.MoveNext())
                        {
                            temp.AddLast(enumerator.Current);
                        }
                        return temp.GetEnumerator();
                    });
        }

#pragma warning disable CS1591
        public Boolean MoveNext()
        {
            return this._gate.Value().MoveNext();
        }


        public X Current
        {
            get
            {
                return this._gate.Value().Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Reset()
        {
            this._gate.Value().Reset();
        }

        public void Dispose()
        {

        }
    }
}
#pragma warning restore NoProperties // No Properties