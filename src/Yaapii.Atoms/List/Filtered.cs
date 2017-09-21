using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Func;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A filtered <see cref="IEnumerable{T}"/>.
    /// Pass a filter function which will applied to all items, similar to List<T>.Where(...) in LinQ
    /// </summary>
    /// <typeparam name="T">type of the elements</typeparam>
    public sealed class Filtered<T> : IEnumerable<T>
    {
        /// <summary>
        /// the enumerable to filter
        /// </summary>
        private readonly IEnumerable<T> _enumerable;

        /// <summary>
        /// filter function
        /// </summary>
        private readonly Func<T, Boolean> _func;

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public Filtered(IEnumerable<T> src, IFunc<T, Boolean> fnc)
        {
            this._enumerable = src;
            this._func = (input) => fnc.Invoke(input);
        }

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public Filtered(IEnumerable<T> src, Func<T, Boolean> fnc)
        {
            this._enumerable = src;
            this._func = fnc;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new FilteredEnumerator<T>(
                this._enumerable.GetEnumerator(),
                this._func
            );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics
