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
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RetryScalar<T> : IScalar<T>
    {
        private readonly IScalar<T> _scalar;
        private readonly IFunc<Int32, bool> _exit;

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="slr">func to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public RetryScalar(Func<T> slr, int attempts = 3) : this(new ScalarOf<T>(() => slr.Invoke()), attempts)
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="slr">scalar to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public RetryScalar(IScalar<T> slr, int attempts = 3) :
            this(slr, new FuncOf<int, bool>(attempt => attempt >= attempts))
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry until the given condition <see cref="IFunc{In, Out}"/> matches before throwing an exception.
        /// </summary>
        /// <param name="slr">scalar to retry when needed</param>
        /// <param name="exit"></param>
        public RetryScalar(IScalar<T> slr, IFunc<Int32, Boolean> exit)
        {
            this._scalar = slr;
            this._exit = exit;
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public T Value()
        {
            return new RetryFunc<Boolean, T>(
                new FuncOf<Boolean, T>(input => this._scalar.Value()),
                this._exit).Invoke(true);
        }
    }
}
