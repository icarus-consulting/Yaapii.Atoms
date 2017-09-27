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
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Checks the equality of contents.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Equals<T> : IScalar<Boolean>
        where T : IComparable<T>
    {
        private readonly IScalar<T> _first;
        private readonly IScalar<T> _second;

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="frst">function to return first value to compare</param>
        /// <param name="scnd">function to return second value to compare</param>
        public Equals(Func<T> frst, Func<T> scnd) : this(new ScalarOf<T>(frst), new ScalarOf<T>(scnd))
        { }

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="frst">first value to compare</param>
        /// <param name="scnd">second value to compare</param>
        public Equals(T frst, T scnd) : this(new ScalarOf<T>(frst), new ScalarOf<T>(scnd))
        { }

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="frst">scalar of first value to compare</param>
        /// <param name="scnd">scalar of second value to compare</param>
        public Equals(IScalar<T> frst, IScalar<T> scnd)
        {
            this._first = frst;
            this._second = scnd;
        }

        /// <summary>
        /// true if equality.
        /// </summary>
        /// <returns>true or false</returns>
        public Boolean Value()
        {
            return this._first.Value().CompareTo(this._second.Value()) == 0;
        }
    }
}
