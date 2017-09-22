﻿/// MIT License
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

using System.IO;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A <see cref="IScalar{T}"/> that doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class UncheckedScalar<T> : IScalar<T>
    {
        private readonly IScalar<T> _origin;

        /// <summary>
        /// A <see cref="IScalar{T}"/> that doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead
        /// </summary>
        /// <param name="fnc">value retrieving function to uncheck</param>
        public UncheckedScalar(System.Func<T> fnc) : this(new ScalarOf<T>(() => fnc.Invoke()))
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that doesn't throw <see cref="IOException"/> but throws <see cref="UncheckedIOException"/> instead
        /// </summary>
        /// <param name="origin">scalar to call</param>
        public UncheckedScalar(IScalar<T> origin)
        {
            this._origin = origin;
        }

        public T Value()
        {
            try
            {
                return new IoCheckedScalar<T>(this._origin).Value();
            }
            catch (IOException ex)
            {
                throw new UncheckedIOException(ex);
            }
        }
    }
}
