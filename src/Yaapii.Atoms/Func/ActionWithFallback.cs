using System;
using System.Collections.Generic;
using System.Text;

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
        private readonly IAction _func;

        /// <summary>
        /// Fallback to call when the action fails.
        /// </summary>
        private readonly IAction<Exception> _fallback;

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
            this._func = fnc;
            this._fallback = fbk;
        }

        /// <summary>
        /// Invoke action.
        /// </summary>
        public void Invoke()
        {
            try
            {
                _func.Invoke();
            }
            catch (Exception ex)
            {
                _fallback.Invoke(ex);
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
