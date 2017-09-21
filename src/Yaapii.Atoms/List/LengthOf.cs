using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Length of an <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class LengthOf<T> : IScalar<Int32>
    {
        private readonly IEnumerable<T> _enumerable;

        /// <summary>
        /// Length of an <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">the enumerable</param>
        public LengthOf(IEnumerable<T> items)
        {
            this._enumerable = items;
        }

        public Int32 Value()
        {
            return new LengthOfEnumerator<T>(this._enumerable.GetEnumerator()).Value();
        }

    }
}
