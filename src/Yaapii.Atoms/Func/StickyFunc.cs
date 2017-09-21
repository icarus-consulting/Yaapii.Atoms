using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Func that caches the result and returns from cache.
    /// </summary>
    /// <typeparam name="In">type of input</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class StickyFunc<In, Out> : IFunc<In, Out>
    {
        /// <summary>
        /// original func
        /// </summary>
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// cache
        /// </summary>
        private readonly Dictionary<In, Out> _cache;

        /// <summary>
        /// Func that caches the result and returns from cache.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public StickyFunc(System.Func<In, Out> fnc) : 
            this(new FuncOf<In, Out>((X) => fnc(X)))
        { }

        /// <summary>
        /// Func that caches the result and returns from cache.
        /// </summary>
        /// <param name="fnc">func to cache output from</param>
        public StickyFunc(IFunc<In, Out> fnc)
        {
            this._func = fnc;
            this._cache = new Dictionary<In, Out>();
        }

        public Out Invoke(In input)
        {
            if (!this._cache.ContainsKey(input))
            {
                this._cache.Add(input, this._func.Invoke(input));
            }
            return this._cache[input];
        }

    }
}
