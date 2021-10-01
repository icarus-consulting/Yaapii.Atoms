// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that will retry if it fails.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class RetryFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// func to retry
        /// </summary>
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// exit condition
        /// </summary>
        private readonly IFunc<Int32, Boolean> _exit;

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        public RetryFunc(Func<In, Out> fnc) : this(new FuncOf<In, Out>((X) => fnc(X)), 3)
        { }

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        public RetryFunc(IFunc<In, Out> fnc) : this(fnc, 3)
        { }

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="attempts">how often to retry</param>
        public RetryFunc(System.Func<In, Out> fnc, int attempts = 3) :
            this(new FuncOf<In, Out>(fnc), attempts)
        { }

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="attempts">how often to retry</param>
        public RetryFunc(IFunc<In, Out> fnc, int attempts = 3) :
            this(fnc, new FuncOf<Int32, Boolean>(attempt => attempt >= attempts))
        { }

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="ext">exit condition</param>
        public RetryFunc(IFunc<In, Out> fnc, System.Func<Int32, Boolean> ext) :
            this(fnc, new FuncOf<Int32, Boolean>((i) => ext(i)))
        { }

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="ext">exit condition</param>
        public RetryFunc(System.Func<In, Out> fnc, System.Func<Int32, Boolean> ext) :
            this(new FuncOf<In, Out>((X) => fnc(X)), new FuncOf<Int32, Boolean>((i) => ext(i)))
        { }

        /// <summary>
        /// Function that will retry if it fails.
        /// </summary>
        /// <param name="fnc">func to retry</param>
        /// <param name="ext">exit condition</param>
        public RetryFunc(IFunc<In, Out> fnc, IFunc<Int32, Boolean> ext)
        {
            this._func = fnc;
            this._exit = ext;
        }

        /// <summary>
        /// Invoke the function and retrieve the output.
        /// </summary>
        /// <param name="input">the input argument</param>
        /// <returns>the output</returns>
        public Out Invoke(In input)
        {
            int attempt = 0;
            Exception error = new ArgumentException(
                "An immediate exit, didn't have a chance to try at least once");

            while (!this._exit.Invoke(attempt))
            {
                try
                {
                    return this._func.Invoke(input);
                }
                //catch (System.Threading.ThreadStateException ex)
                //{
                //    Thread.CurrentThread.Join();
                //    error = ex;
                //    break;
                //}
                catch (Exception ex)
                {
                    error = ex;
                }
                ++attempt;
            }
            throw error;
        }

    }
}
