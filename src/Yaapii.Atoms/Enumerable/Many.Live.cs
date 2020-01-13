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
using System.Linq;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerable
{
    public partial class Many
    {
        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of other objects.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public sealed class Live<T> : Many.Envelope<T>
        {
            /// <summary>
            /// A <see cref="IEnumerable{T}"/> out of an array.
            /// </summary>
            /// <param name="items"></param>
            public Live(params T[] items) : this(
                () => items.AsEnumerable<T>().GetEnumerator()
            )
            { }

            /// <summary>
            /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
            /// </summary>
            /// <param name="fnc">function which retrieves enumerator</param>
            public Live(Func<IEnumerator<T>> fnc) : this(new ScalarOf<IEnumerator<T>>(fnc))
            { }

            /// <summary>
            /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
            /// </summary>
            /// <param name="fnc">function which retrieves enumerator</param>
            public Live(Func<IEnumerable<T>> fnc) : this(new ScalarOf<IEnumerator<T>>(() => fnc.Invoke().GetEnumerator()))
            { }

            /// <summary>
            /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}"/>"/>.
            /// </summary>
            /// <param name="origin">scalar to return the IEnumerator</param>
            public Live(IScalar<IEnumerator<T>> origin) : base(
                new ScalarOf<IEnumerable<T>>(() => new LiveEnumeratorAsEnumerable<T>(origin.Value())),
                false
            )
            { }

            /// <summary>
            /// Makes enumerable from an enumerator
            /// </summary>
            /// <typeparam name="X"></typeparam>
            private class LiveEnumeratorAsEnumerable<X> : IEnumerable<X>
            {
                private readonly IEnumerator<X> origin;

                public LiveEnumeratorAsEnumerable(IEnumerator<X> enumerator)
                {
                    origin = enumerator;
                }

                public IEnumerator<X> GetEnumerator()
                {
                    return origin;
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return this.GetEnumerator();
                }
            }
        }
    }
}