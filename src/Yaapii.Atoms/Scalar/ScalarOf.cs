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
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Misc;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A <see cref="IScalar{T}"/> out of other objects
    /// </summary>
    /// <typeparam name="T">type of the value</typeparam>
    public sealed class ScalarOf<T> : IScalar<T>
    {
        private readonly Func<T> func;

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of an object.
        /// </summary>
        /// <param name="org"></param>
        public ScalarOf(T org) : this((b) => org)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of the return value from a <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// <param name="func"></param>
        public ScalarOf(IFunc<T> func) : this(() => func.Invoke())
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of the return value from a <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// <param name="func"></param>
        public ScalarOf(IFunc<bool, T> func) : this(() => func.Invoke(true))
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of the return value from an <see cref="IFunc{In, Out}"/>
        /// </summary>
        /// <param name="func"></param>
        public ScalarOf(Func<bool, T> func) : this(() => func.Invoke(true))
        { }

        /// <summary>
        /// Primary ctor
        /// </summary>
        /// <param name="func"></param>
        public ScalarOf(Func<T> func)
        {
            this.func = func;
        }

        /// <summary>
        /// Gives the value
        /// </summary>
        /// <returns></returns>
        public T Value()
        {
            return func.Invoke();
        }
    }
}
