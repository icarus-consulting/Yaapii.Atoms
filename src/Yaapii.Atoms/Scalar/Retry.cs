// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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
    public sealed class Retry<T> : ScalarEnvelope<T>
    {
        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="scalar">func to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public Retry(Func<T> scalar, int attempts = 3)
            : this(new Live<T>(() => scalar.Invoke()), attempts)
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public Retry(IScalar<T> scalar, int attempts = 3) :
            this(scalar, new FuncOf<int, bool>(attempt => attempt >= attempts))
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry until the given condition <see cref="IFunc{In, Out}"/> matches before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="exit"></param>
        public Retry(IScalar<T> scalar, IFunc<Int32, Boolean> exit)
            : base(() =>
                new RetryFunc<Boolean, T>(
                    new FuncOf<Boolean, T>(input => scalar.Value()),
                    exit).Invoke(true)
            )
        { }
    }
}
