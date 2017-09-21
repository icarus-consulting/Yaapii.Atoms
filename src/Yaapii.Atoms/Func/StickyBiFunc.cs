using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// Function with two inputs which returns the output from cache.
    /// </summary>
    /// <typeparam name="In1">type of first argument</typeparam>
    /// <typeparam name="In2">type of second argument</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class StickyBiFunc<In1, In2, Out>
    {
        /// <summary>
        /// original func
        /// </summary>
        private readonly IBiFunc<In1, In2, Out> func;

        /// <summary>
        /// cache
        /// </summary>
        private readonly Dictionary<Dictionary<In1, In2>, Out> cache;

        /// <summary>
        /// Function with two inputs which returns the output from cache.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        public StickyBiFunc(System.Func<In1, In2, Out> fnc) : this(new BiFuncOf<In1, In2, Out>(fnc))
        { }

        /// <summary>
        /// Function with two inputs which returns the output from cache.
        /// </summary>
        /// <param name="fnc">func to cache result from</param>
        public StickyBiFunc(IBiFunc<In1, In2, Out> fnc)
        {
            this.func = fnc;
            this.cache = new Dictionary<Dictionary<In1, In2>, Out>(0);
        }

        public Out Apply(In1 first, In2 second)
        {
            var keymap = new Dictionary<In1, In2>();
            keymap[first] = second;
            if (!this.cache.ContainsKey(keymap))
            {
                this.cache.Add(keymap, this.func.Apply(first, second));
            }
            return this.cache[keymap];
        }

    }
}
