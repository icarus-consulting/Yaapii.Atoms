using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that is threadsafe.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class ThreadSafeFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// original func
        /// </summary>
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// threadsafe-lock
        /// </summary>
        private readonly Object _lck;

        /// <summary>
        /// Function that is threadsafe.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public ThreadSafeFunc(System.Func<In, Out> fnc) : this(new FuncOf<In, Out>((X) => fnc(X)))
        { }

        /// <summary>
        /// Function that is threadsafe.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public ThreadSafeFunc(IFunc<In, Out> fnc): this(fnc, fnc)
        { }

        /// <summary>
        /// Function that is threadsafe.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        /// <param name="lck">object that will be locked</param>
        public ThreadSafeFunc(IFunc<In, Out> fnc, object lck)
        {
            this._func = fnc;
            this._lck = lck;
        }

        public Out Invoke(In input)
        {
            lock (this._lck)
            {
                return this._func.Invoke(input);
            }
        }

    }
}
