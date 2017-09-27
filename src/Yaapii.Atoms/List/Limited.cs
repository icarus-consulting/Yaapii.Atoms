﻿// MIT License
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
using Yaapii.Atoms.Scalar;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> limited to an item maximum.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class Limited<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _enumerable;
        private readonly IScalar<int> _limit;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        /// <param name="limit">maximum item count</param>
        public Limited(IEnumerable<T> enumerable, int limit) : this(enumerable, new ScalarOf<int>(limit))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> limited to an item maximum.
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        /// <param name="limit">maximum item count</param>
        public Limited(IEnumerable<T> enumerable, IScalar<int> limit)
        {
            this._enumerable = enumerable;
            this._limit = limit;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LimitedEnumerator<T>(this._enumerable.GetEnumerator(), this._limit.Value());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

#pragma warning restore NoGetOrSet // No Statics

