using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Func;

#pragma warning disable NoProperties // No Properties
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given function.
    /// </summary>
    /// <typeparam name="In">type of items</typeparam>
    /// <typeparam name="Out">type of mapped items</typeparam>
    public sealed class MappedEnumerator<In, Out> : IEnumerator<Out>
    {
        private readonly IEnumerator<In> _enumerator;
        private readonly Func<In, Out> _func;

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="IFunc{In, Out}"/> function.
        /// </summary>
        /// <param name="src">source enumerable</param>
        /// <param name="fnc">mapping function</param>
        public MappedEnumerator(IEnumerator<In> src, IFunc<In, Out> fnc) : this(src,
            input => fnc.Invoke(input))
        { }

        /// <summary>
        /// Mapped content of an <see cref="IEnumerable{T}"/> to another type using the given <see cref="Func{In, Out}"/> function.
        /// </summary>
        /// <param name="src">source enumerable</param>
        /// <param name="fnc">mapping function</param>
        public MappedEnumerator(IEnumerator<In> src, Func<In, Out> fnc)
        {
            this._enumerator = src;
            this._func = fnc;
        }

        public Boolean MoveNext()
        {
            return this._enumerator.MoveNext();
        }

        public Out Current
        {
            get
            {
                return new UncheckedFunc<In, Out>(this._func).Invoke(this._enumerator.Current);
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Reset()
        {
            this._enumerator.Reset();
        }

        public void Dispose()
        { }
    }
}
#pragma warning restore NoProperties // No Properties