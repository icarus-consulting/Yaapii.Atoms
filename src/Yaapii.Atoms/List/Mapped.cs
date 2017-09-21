using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Func;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given function.
    /// </summary>
    /// <typeparam name="In">type of input elements</typeparam>
    /// <typeparam name="Out">type of mapped elements</typeparam>
    public sealed class Mapped<In, Out> : IEnumerable<Out>
    {
        private readonly IEnumerable<In> _enumerable;
        private readonly IFunc<In, Out> _func;

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IEnumerable<In> src, Func<In, Out> fnc) : this(
            src, 
            new FuncOf<In, Out>(fnc))
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">enumerable to map</param>
        /// <param name="fnc">function used to map</param>
        public Mapped(IEnumerable<In> src, IFunc<In, Out> fnc)
        {
            this._enumerable = src;
            this._func = fnc;
        }

        public IEnumerator<Out> GetEnumerator()
        {
            return new MappedEnumerator<In, Out>(
                this._enumerable.GetEnumerator(), this._func
            );
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics
