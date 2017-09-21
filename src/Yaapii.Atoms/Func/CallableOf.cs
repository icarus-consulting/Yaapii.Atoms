using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Misc;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function that has only output.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class CallableOf<Out> : ICallable<Out>
    {
        /// <summary>
        /// func that will be called
        /// </summary>
        private readonly IFunc<bool, Out> _func;

        /// <summary>
        /// Function that has only output.
        /// </summary>
        /// <param name="fnc">func to call</param>
        public CallableOf(System.Func<Out> fnc)
        {
            this._func = new FuncOf<bool, Out>(() => fnc.Invoke());
        }

        public Out Call()
        {
            return this._func.Invoke(true);
        }
    }
}
