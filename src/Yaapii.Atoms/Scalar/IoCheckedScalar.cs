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
using System.IO;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Scalar that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IoCheckedScalar<T> : IScalar<T>
    {
        private readonly IScalar<T> _scalar;

        /// <summary>
        /// Scalar that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
        /// </summary>
        /// <param name="fnc">func to call</param>
        public IoCheckedScalar(Func<T> fnc) : this(new ScalarOf<T>(fnc))
        { }

        /// <summary>
        /// Scalar that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
        /// </summary>
        /// <param name="scalar">scalar of the value to retrieve</param>
        public IoCheckedScalar(IScalar<T> scalar)
        {
            this._scalar = scalar;
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public T Value()
        {
            return new IoCheckedFunc<IScalar<T>, T>
            (
                new IoCheckedFunc<IScalar<T>, T>(new FuncOf<IScalar<T>, T>(s => s.Value()))
            ).Invoke(this._scalar);
        }
    }
}