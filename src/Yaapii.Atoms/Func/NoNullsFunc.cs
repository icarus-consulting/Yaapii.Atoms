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
using System.IO;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that does not allow null as input.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class NoNullsFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// The function
        /// </summary>
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// Function that does not allow null as input.
        /// </summary>
        /// <param name="func">the function</param>
        public NoNullsFunc(IFunc<In, Out> func)
        {
            _func = func;
        }

        /// <summary>
        /// Apply it
        /// </summary>
        /// <param name="input">input</param>
        /// <returns>the output</returns>
        public Out Invoke(In input)
        {
            new FailNull(input, "got NULL instead of a valid function");

            Out result = _func.Invoke(input);
            if (result == null)
            {
                throw new IOException("got NULL instead of a valid result");
            }
            return result;
        }
    }

    /// <summary>
    /// Check whether a func returns null. React with Exception or fallback value / function.
    /// </summary>
    /// <typeparam name="Out">The type of output</typeparam>
    public class NoNullsFunc<Out> : IFunc<Out>
    {
        /// <summary>
        /// fnc to call
        /// </summary>
        private readonly IFunc<Out> _fnc;

        /// <summary>
        /// error to raise
        /// </summary>
        private readonly IFunc<Out> _fbk;

        /// <summary>
        /// Raises an <see cref="IOException"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        public NoNullsFunc(Func<Out> fnc) : this(new FuncOf<Out>(fnc), new IOException("Return value is null"))
        { }

        /// <summary>
        /// Raises an <see cref="IOException"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        public NoNullsFunc(IFunc<Out> fnc) : this(fnc, new IOException("Return value is null"))
        { }

        /// <summary>
        /// Raises given <see cref="Exception"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="ex">Exception to throw</param>
        public NoNullsFunc(Func<Out> fnc, Exception ex) : this(new FuncOf<Out>(fnc), new FuncOf<Out>(() => throw ex))
        { }

        /// <summary>
        /// Raises given <see cref="Exception"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="ex">Exception to throw</param>
        public NoNullsFunc(IFunc<Out> fnc, Exception ex) : this(fnc, new FuncOf<Out>(() => throw ex))
        { }

        /// <summary>
        /// Returns the fallback if the func returns null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="fallback">fallback value</param>
        public NoNullsFunc(Func<Out> fnc, Out fallback) : this(new FuncOf<Out>(fnc), new FuncOf<Out>(() => fallback))
        { }

        /// <summary>
        /// Returns the fallback if the func returns null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="fallback">fallback value</param>
        public NoNullsFunc(IFunc<Out> fnc, Out fallback) : this(fnc, new FuncOf<Out>(() => fallback))
        { }

        /// <summary>
        /// Calls the fallback function if the func return null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="fallback">fallback value</param>
        public NoNullsFunc(Func<Out> fnc, Func<Out> fallback) : this(new FuncOf<Out>(fnc), new FuncOf<Out>(fallback))
        { }

        /// <summary>
        /// Calls the fallback function if the func return null.
        /// </summary>
        /// <param name="fnc">func to check</param>
        /// <param name="fallback">fallback value</param>
        public NoNullsFunc(IFunc<Out> fnc, IFunc<Out> fallback)
        {
            _fnc = fnc;
            _fbk = fallback;
        }

        /// <summary>
        /// Call the function to get the value
        /// </summary>
        /// <returns>The value or the fallback value (if any)</returns>
        public Out Invoke()
        {
            Out ret = _fnc.Invoke();

            if (ret == null) ret = _fbk.Invoke();

            return ret;
        }
    }
}
