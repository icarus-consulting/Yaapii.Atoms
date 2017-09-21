using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> that starts from the beginning when ended.
    /// </summary>
    /// <typeparam name="T">type of the contents</typeparam>
    public sealed class Cycled<T> : IEnumerable<T>
    {
        /// <summary>
        /// the source
        /// </summary>
        private readonly IEnumerable<T> _enumerable;

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that starts from the beginning when ended.
        /// </summary>
        /// <param name="enumerable">an enum to cycle</param>
        public Cycled(IEnumerable<T> enumerable)
        {
            this._enumerable = enumerable;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CycledEnumerator<T>(this._enumerable);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics