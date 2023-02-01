// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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
        private readonly Func<In1, In2, Out> func;

        /// <summary>
        /// Fallback to call wehen func fails
        /// </summary>
        private readonly IFunc<Exception, Out> fallback;

        /// <summary>
        /// A follow function
        /// </summary>
        private readonly IFunc<Out, Out> followUp;

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
            this.func = fnc;
            this.fallback = fbk;
            this.followUp = flw;
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
                result = this.func.Invoke(first, second);
            }
            catch (Exception ex)
            {
                result = this.fallback.Invoke(ex);
            }
            return this.followUp.Invoke(result);
        }
    }

    public abstract class BiFuncWithFallback
    {
        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        public static IBiFunc<In1, In2, Out> New<In1, In2, Out>(System.Func<In1, In2, Out> fnc, System.Func<Exception, Out> fbk)
            => new BiFuncWithFallback<In1, In2, Out>(fnc, fbk);

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        public static IBiFunc<In1, In2, Out> New<In1, In2, Out>(System.Func<In1, In2, Out> fnc, IFunc<Exception, Out> fbk) =>
            new BiFuncWithFallback<In1, In2, Out>(fnc, fbk);

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public static IBiFunc<In1, In2, Out> New<In1, In2, Out>(System.Func<In1, In2, Out> fnc, System.Func<Exception, Out> fbk, IFunc<Out, Out> flw) =>
            new BiFuncWithFallback<In1, In2, Out>(fnc, fbk, flw);

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public static IBiFunc<In1, In2, Out> New<In1, In2, Out>(System.Func<In1, In2, Out> fnc, IFunc<Exception, Out> fbk, System.Func<Out, Out> flw) =>
            new BiFuncWithFallback<In1, In2, Out>(fnc, fbk, flw);

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public static IBiFunc<In1, In2, Out> New<In1, In2, Out>(System.Func<In1, In2, Out> fnc, System.Func<Exception, Out> fbk, System.Func<Out, Out> flw) =>
            new BiFuncWithFallback<In1, In2, Out>(fnc, fbk, flw);

        /// <summary>
        /// A bi-function that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Func to call</param>
        /// <param name="fbk">Fallback func</param>
        /// <param name="flw">Func to call aferwards</param>
        public static BiFuncWithFallback<In1, In2, Out> New<In1, In2, Out>(System.Func<In1, In2, Out> fnc, IFunc<Exception, Out> fbk, IFunc<Out, Out> flw) =>
            new BiFuncWithFallback<In1, In2, Out>(fnc, fbk, flw);
    }
}
