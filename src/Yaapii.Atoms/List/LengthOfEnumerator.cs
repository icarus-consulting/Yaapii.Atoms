using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Length of an <see cref="IEnumerator{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class LengthOfEnumerator<T> : IScalar<Int32>
    {
        private readonly IEnumerator<T> _enumerator;

        /// <summary>
        /// Length of an <see cref="IEnumerator{T}"/>
        /// </summary>
        /// <param name="items">enumerator to count</param>
        public LengthOfEnumerator(IEnumerator<T> items)
        {
            this._enumerator = items;
        }

        public Int32 Value()
        {
            int size = 0;
            while (this._enumerator.MoveNext())
            {
                ++size;
            }
            return size;
        }

    }
}
