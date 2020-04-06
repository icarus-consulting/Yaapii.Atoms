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
using System.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Checks the equality of contents.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Equals<T> : ScalarEnvelope<Boolean>
        where T : IComparable<T>
    {
        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">function to return first value to compare</param>
        /// <param name="second">function to return second value to compare</param>
        public Equals(Func<T> first, Func<T> second) : this(new LiveScalar<T>(first), new LiveScalar<T>(second))
        { }

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">first value to compare</param>
        /// <param name="second">second value to compare</param>
        public Equals(T first, T second) : this(new LiveScalar<T>(first), new LiveScalar<T>(second))
        { }

        /// <summary>
        /// Checks the equality of contents.
        /// </summary>
        /// <param name="first">scalar of first value to compare</param>
        /// <param name="second">scalar of second value to compare</param>
        public Equals(IScalar<T> first, IScalar<T> second)
            : base(() => first.Value().CompareTo(second.Value()) == 0)
        { }
    }
}
