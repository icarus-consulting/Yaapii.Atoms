using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#pragma warning disable NoProperties // No Properties
#pragma warning disable Immutability // Fields are readonly or constant
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerator{T}"/> limited to an item maximum.
    /// </summary>
    /// <typeparam name="T">type of the enumerator content</typeparam>
    public sealed class LimitedEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly int _limit;
        private int _consumed;

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> limited to an item maximum.
        /// </summary>
        /// <param name="enumerator">enumerator to limit</param>
        /// <param name="limit">maximum item count</param>
        public LimitedEnumerator(IEnumerator<T> enumerator, int limit)
        {
            this._enumerator = enumerator;
            this._limit = limit;
            this._consumed = 0;
        }

        public Boolean MoveNext()
        {
            return this._consumed++ < this._limit && this._enumerator.MoveNext();
        }

        public void Reset()
        {
            this._enumerator.Reset();
            this._consumed = 0;
        }

        public void Dispose()
        { }

        public T Current
        {
            get
            {
                return this._enumerator.Current;
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();
    }
}
#pragma warning restore NoProperties // No Properties
#pragma warning restore Immutability // Fields are readonly or constant