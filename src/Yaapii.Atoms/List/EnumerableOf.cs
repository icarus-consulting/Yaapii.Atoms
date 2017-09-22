/// MIT License
///
/// Copyright(c) 2017 ICARUS Consulting GmbH
///
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> out of other objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EnumerableOf<T> : IEnumerable<T>
    {
        /// <summary>
        /// the enumerable
        /// </summary>
        private readonly UncheckedScalar<IEnumerator<T>> _origin;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public EnumerableOf(params T[] items) : this(
            () => items.AsEnumerable<T>().GetEnumerator())
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <param name="e">a enumerator</param>
        public EnumerableOf(IEnumerator<T> e) : this(new ScalarOf<IEnumerator<T>>(e))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}</see>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        private EnumerableOf(Func<IEnumerator<T>> fnc) : this(new ScalarOf<IEnumerator<T>>(fnc))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}</see>"/>.
        /// </summary>
        /// <param name="origin">scalar which returns enumerator</param>
        private EnumerableOf(IScalar<IEnumerator<T>> origin) : this(new UncheckedScalar<IEnumerator<T>>(origin))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}</see>"/>.
        /// </summary>
        /// <param name="origin">scalar to return the IEnumerator</param>
        private EnumerableOf(UncheckedScalar<IEnumerator<T>> origin)
        {
            _origin = origin;
        }

        public IEnumerator<T> GetEnumerator()

        {
            return _origin.Value();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _origin.Value();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics