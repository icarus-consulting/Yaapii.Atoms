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
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> that returns content from cache always.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public sealed class StickyEnumerable<X> : IEnumerable<X>
    {

        /// <summary>
        /// Cache
        /// </summary>
        private readonly IScalar<IEnumerable<X>> _gate;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> that returns content from cache always.
        /// </summary>
        /// <param name="enumerable">the enum</param>
        public StickyEnumerable(IEnumerable<X> enumerable)
        {
            this._gate =
                new StickyScalar<IEnumerable<X>>(
                    () =>
                        {
                            var temp = new LinkedList<X>();
                            foreach (X item in enumerable)
                            {
                                temp.AddLast(item);
                            }
                            return temp;
                        });
        }

        /// <summary>
        /// Get the cached enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<X> GetEnumerator()
        {
            return this._gate.Value().GetEnumerator();
        }

        /// <summary>
        /// Get the cached enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics
