using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Check whether a callable returns null. React with Exception or fallback value / function.
    /// </summary>
    /// <typeparam name="Out">The type of output</typeparam>
    public class NoNullsCallable<Out> : ICallable<Out>
    {
        /// <summary>
        /// fnc to call
        /// </summary>
        private readonly ICallable<Out> _fnc;

        /// <summary>
        /// error to raise
        /// </summary>
        private readonly ICallable<Out> _fbk;

        /// <summary>
        /// Raises an <see cref="IOException"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">callable to check</param>
        public NoNullsCallable(Func<Out> fnc) : this(new CallableOf<Out>(fnc), new IOException("Return value is null"))
        { }

        /// <summary>
        /// Raises an <see cref="IOException"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">callable to check</param>
        public NoNullsCallable(ICallable<Out> fnc) : this(fnc, new IOException("Return value is null"))
        { }

        /// <summary>
        /// Raises given <see cref="Exception"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">callable to check</param>
        /// <param name="ex">Exception to throw</param>
        public NoNullsCallable(Func<Out> fnc, Exception ex) : this(new CallableOf<Out>(fnc), new CallableOf<Out>(() => throw ex))
        { }

        /// <summary>
        /// Raises given <see cref="Exception"/> when the return value is null.
        /// </summary>
        /// <param name="fnc">callable to check</param>
        /// <param name="ex">Exception to throw</param>
        public NoNullsCallable(ICallable<Out> fnc, Exception ex) : this(fnc, new CallableOf<Out>(() => throw ex))
        { }

        /// <summary>
        /// Returns the fallback if the callable returns null.
        /// </summary>
        /// <param name="fnc">callable to check</param>
        /// <param name="fallback">fallback value</param>
        public NoNullsCallable(Func<Out> fnc, Out fallback) : this(new CallableOf<Out>(fnc), new CallableOf<Out>(() => fallback))
        { }

        /// <summary>
        /// Returns the fallback if the callable returns null.
        /// </summary>
        /// <param name="fnc">callable to check</param>
        /// <param name="fallback">fallback value</param>
        public NoNullsCallable(ICallable<Out> fnc, Out fallback) : this(fnc, new CallableOf<Out>(() => fallback))
        { }

        /// <summary>
        /// Calls the fallback function if the callable return null.
        /// </summary>
        /// <param name="fnc">callable to check</param>
        /// <param name="fallback">fallback value</param>
        public NoNullsCallable(Func<Out> fnc, Func<Out> fallback) : this(new CallableOf<Out>(fnc), new CallableOf<Out>(fallback))
        { }

        /// <summary>
        /// Calls the fallback function if the callable return null.
        /// </summary>
        /// <param name="fnc">callable to check</param>
        /// <param name="fallback">fallback value</param>
        public NoNullsCallable(ICallable<Out> fnc, ICallable<Out> fallback)
        {
            _fnc = fnc;
            _fbk = fallback;
        }

        /// <summary>
        /// Call the function to get the value
        /// </summary>
        /// <returns>The value or the fallback value (if any)</returns>
        public Out Call()
        {
            Out ret = _fnc.Call();

            if (ret == null) ret = _fbk.Call();

            return ret;
        }
    }
}
