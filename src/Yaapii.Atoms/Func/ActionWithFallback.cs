// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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
    /// A action that executes a fallback if it fails (= an <see cref="Exception"/> occurs).
    /// </summary>
    public sealed class ActionWithFallback : IAction
    {
        /// <summary>
        /// Action to call.
        /// </summary>
        private readonly IAction func;

        /// <summary>
        /// Fallback to call when the action fails.
        /// </summary>
        private readonly IAction<Exception> fallback;

        /// <summary>
        /// A Action that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Action to call</param>
        /// <param name="fbk">Fallback action</param>
        public ActionWithFallback(IAction fnc, System.Action<Exception> fbk) : this(
            fnc,
            new ActionOf<Exception>(fbk))
        { }

        /// <summary>
        /// A Action that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Action to call</param>
        /// <param name="fbk">Fallback action</param>
        public ActionWithFallback(System.Action fnc, IAction<Exception> fbk) : this(
            new ActionOf(fnc),
            fbk)
        { }

        /// <summary>
        /// A Action that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Action to call</param>
        /// <param name="fbk">Fallback action</param>
        public ActionWithFallback(System.Action fnc, System.Action<Exception> fbk) : this(
            new ActionOf(fnc),
            new ActionOf<Exception>(fbk))
        { }

        /// <summary>
        /// A Action that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Action to call</param>
        /// <param name="fbk">Fallback action</param>
        public ActionWithFallback(IAction fnc, IAction<Exception> fbk)
        {
            this.func = fnc;
            this.fallback = fbk;
        }

        /// <summary>
        /// Invoke action.
        /// </summary>
        public void Invoke()
        {
            try
            {
                func.Invoke();
            }
            catch (Exception ex)
            {
                fallback.Invoke(ex);
            }
        }
    }

    /// <summary>
    /// A Action with input that executes a callback if it fails (= an <see cref="Exception"/> occurs).
    /// </summary>
    public sealed class ActionWithFallback<In> : IAction<In>
    {
        /// <summary>
        /// Action to call.
        /// </summary>
        private readonly IAction<In> _func;

        /// <summary>
        /// Fallback to call when the action falis.
        /// </summary>
        private readonly IAction<Exception> _fallback;

        /// <summary>
        /// A Action with input that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Action to call</param>
        /// <param name="fbk">Fallback action</param>
        public ActionWithFallback(IAction<In> fnc, System.Action<Exception> fbk) : this(
            fnc,
            new ActionOf<Exception>(fbk))
        { }

        /// <summary>
        /// A Action with input that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Action to call</param>
        /// <param name="fbk">Fallback action</param>
        public ActionWithFallback(System.Action fnc, IAction<Exception> fbk) : this(
            new ActionOf<In>(fnc),
            fbk)
        { }

        /// <summary>
        /// A Action with input that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Action to call</param>
        /// <param name="fbk">Fallback action</param>
        public ActionWithFallback(System.Action<In> fnc, System.Action<Exception> fbk) : this(
            new ActionOf<In>(fnc),
            new ActionOf<Exception>(fbk))
        { }

        /// <summary>
        /// A Action with input that executes a callback if it fails (= an <see cref="Exception"/> occurs).
        /// </summary>
        /// <param name="fnc">Action to call</param>
        /// <param name="fbk">Fallback action</param>
        public ActionWithFallback(IAction<In> fnc, IAction<Exception> fbk)
        {
            this._func = fnc;
            this._fallback = fbk;
        }

        /// <summary>
        /// Invoke action.
        /// </summary>
        /// <param name="input">The input parameter</param>
        public void Invoke(In input)
        {
            try
            {
                this._func.Invoke(input);
            }
            catch (Exception ex)
            {
                this._fallback.Invoke(ex);
            }
        }
    }
}
