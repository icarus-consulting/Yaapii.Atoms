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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// A function with two inputs and one output which always throws <see cref="IOException"/> if it fails.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    /// <typeparam name="Y"></typeparam>
    /// <typeparam name="Z"></typeparam>
    public sealed class IoCheckedBiFunc<X, Y, Z> : IBiFunc<X, Y, Z>
    {
        private readonly IBiFunc<X, Y, Z> _func;

        /// <summary>
        /// A function with two inputs and one output which always throws <see cref="IOException"/> if it fails.
        /// </summary>
        /// <param name="fnc">function to call</param>
        public IoCheckedBiFunc(System.Func<X, Y, Z> fnc) : this(new BiFuncOf<X, Y, Z>(fnc))
        { }

        /// <summary>
        /// A function with two inputs and one output which always throws <see cref="IOException"/> if it fails.
        /// </summary>
        /// <param name="fnc">function to call</param>
        public IoCheckedBiFunc(IBiFunc<X, Y, Z> fnc)
        {
            this._func = fnc;
        }

        public Z Apply(X first, Y second)
        {
            try
            {
                return this._func.Apply(first, second);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (System.Threading.ThreadStateException ex)
            {
                Thread.CurrentThread.Join();
                throw new IOException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new IOException(ex.Message, ex);
            }
        }

    }
}
