using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Error;

namespace Yaapii.Atoms.Func
{
    /// <summary>
    /// BiFunc that does not throw <see cref="IOException" /> but throws <see cref="UncheckedIOException"/> instead.
    /// </summary>
    /// <typeparam name="In1">type of first argument</typeparam>
    /// <typeparam name="In2">type of second argument</typeparam>
    /// <typeparam name="Out">type of output</typeparam>
    public sealed class UncheckedBiFunc<In1, In2, Out> : IBiFunc<In1, In2, Out>
    {
        /// <summary>
        /// original func
        /// </summary>
        private readonly IBiFunc<In1, In2, Out> func;

        /// <summary>
        /// BiFunc that does not throw <see cref="IOException" /> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="fnc">func to call</param>
        public UncheckedBiFunc(System.Func<In1, In2, Out> fnc) : this(new BiFuncOf<In1, In2, Out>(fnc))
        { }

        /// <summary>
        /// BiFunc that does not throw <see cref="IOException" /> but throws <see cref="UncheckedIOException"/> instead.
        /// </summary>
        /// <param name="fnc">func to call</param>
        public UncheckedBiFunc(IBiFunc<In1, In2, Out> fnc)
        {
            this.func = fnc;
        }

        public Out Apply(In1 first, In2 second)
        {
            try
            {
                return new IoCheckedBiFunc<In1, In2, Out>(this.func).Apply(first, second);
            }
            catch (IOException ex)
            {
                throw new UncheckedIOException(ex);
            }
        }
    }
}
