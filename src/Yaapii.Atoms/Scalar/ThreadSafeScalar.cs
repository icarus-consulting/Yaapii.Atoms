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
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A <see cref="IScalar{T}"/> that is threadsafe.
    /// </summary>
    /// <typeparam name="T">type of value</typeparam>
    public sealed class ThreadSafeScalar<T> : IScalar<T>
    {
        private readonly IScalar<T> _source;
        private readonly Object _lock;

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public ThreadSafeScalar(IScalar<T> src) : this(src, src)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">object to lock while using scalar</param>
        public ThreadSafeScalar(IScalar<T> src, Object lck)
        {
            this._source = src;
            this._lock = lck;
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public T Value()
        {
            lock (this._source)
            {
                return this._source.Value();
            }
        }
    }
}
