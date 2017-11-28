using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// A Action that executes a callback if it fails (= an <see cref="Exception"/> occurs).
    /// </summary>
    public sealed class ActionWithFallback : IAction
    {
        private readonly IAction _func;
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
        /// Invoke action
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
}
