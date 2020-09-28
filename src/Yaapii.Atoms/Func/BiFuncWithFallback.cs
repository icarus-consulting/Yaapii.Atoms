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

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
    /// </summary>
    /// <typeparam name="In1">First argument type</typeparam>
    /// <typeparam name="In2">Second argument type</typeparam>
    /// <typeparam name="Out">Return type</typeparam>
    public sealed class BiFuncWithFallback<In1, In2, Out> : IBiFunc<In1, In2, Out>
    {
        /// <summary>
        /// Func to call
        /// </summary>
        private readonly Func<In1, In2, Out> _func;

        /// <summary>
        /// Fallback to call wehen func fails
        /// </summary>
        private readonly IFunc<Exception, Out> _fallback;

        /// <summary>
        /// A follow function
        /// </summary>
        private readonly IFunc<Out, Out> _follow;

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        public BiFuncWithFallback(System.Func<In1, In2, Out> fnc, System.Func<Exception, Out> fbk) : this(
            fnc,
            new FuncOf<Exception, Out>(fbk),
            new FuncOf<Out, Out>((input) => input))
        { }

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        public BiFuncWithFallback(System.Func<In1, In2, Out> fnc, IFunc<Exception, Out> fbk) : this(
            fnc,
            fbk,
            new FuncOf<Out, Out>((input) => input))
        { }

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public BiFuncWithFallback(System.Func<In1, In2, Out> fnc, System.Func<Exception, Out> fbk, IFunc<Out, Out> flw) : this(
            fnc,
            new FuncOf<Exception, Out>(fbk),
            flw)
        { }

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public BiFuncWithFallback(System.Func<In1, In2, Out> fnc, IFunc<Exception, Out> fbk, System.Func<Out, Out> flw) : this(
            fnc,
            fbk,
            new FuncOf<Out, Out>(flw))
        { }

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public BiFuncWithFallback(System.Func<In1, In2, Out> fnc, System.Func<Exception, Out> fbk, System.Func<Out, Out> flw) : this(
            fnc,
            new FuncOf<Exception, Out>(fbk),
            new FuncOf<Out, Out>(flw))
        { }

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public BiFuncWithFallback(System.Func<In1, In2, Out> fnc, IFunc<Exception, Out> fbk, IFunc<Out, Out> flw)
        {
            this._func = fnc;
            this._fallback = fbk;
            this._follow = flw;
        }

        /// <summary>
        /// Invoke bi-function with input and retrieve output.
        /// </summary>
        /// <param name="first">First input argument</param>
        /// <param name="second">Second input argument</param>
        /// <returns>The reault</returns>
        public Out Invoke(In1 first, In2 second)
        {
            Out result;
            try
            {
                result = this._func.Invoke(first, second);
            }
            catch (Exception ex)
            {
                result = this._fallback.Invoke(ex);
            }
            return this._follow.Invoke(result);
        }
    }
}
