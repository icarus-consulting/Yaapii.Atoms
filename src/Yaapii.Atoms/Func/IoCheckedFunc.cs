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
using System.IO;
using System.Threading;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// func that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class IoCheckedFunc<In, Out> : IFunc<In, Out>
    {
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// func that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
        /// </summary>
        /// <param name="func">func to call</param>
        public IoCheckedFunc(System.Func<In, Out> func) : this(new FuncOf<In, Out>((X) => func(X)))
        { }

        /// <summary>
        /// func that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
        /// </summary>
        /// <param name="func">func to call</param>
        public IoCheckedFunc(IFunc<In, Out> func)
        {
            _func = func;
        }

        /// <summary>
        /// Invoke the function and retrieve the output.
        /// </summary>
        /// <param name="input">the input argument</param>
        /// <returns>the output</returns>
        public Out Invoke(In input)
        {
            try
            {
                return this._func.Invoke(input);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            //catch (System.Threading.ThreadStateException ex)
            //{
            //    Thread.CurrentThread.Join();
            //    throw new IOException(ex.Message, ex);
            //}
            catch (Exception ex)
            {
                throw new IOException(ex.Message, ex);
            }
        }

    }
}
