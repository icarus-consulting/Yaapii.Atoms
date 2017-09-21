using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#pragma warning disable Immutability // Fields are readonly or constant
#pragma warning disable NoProperties // No Properties
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerator{Tests}"/> which skips a given count of items.
    /// </summary>
    /// <typeparam name="T">type of items in enumerable</typeparam>
    public sealed class SkippedEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;
        private readonly int _skip;
        private int _left;

        /// <summary>
        /// A <see cref="IEnumerator{Tests}"/> which skips a given count of items.
        /// </summary>
        /// <param name="enumerator"><see cref="IEnumerator{T}"/> to skip items in</param>
        /// <param name="skip">how many to skip</param>
        public SkippedEnumerator(IEnumerator<T> enumerator, int skip)
        {
            this._enumerator = enumerator;
            this._skip = skip;
            this._left = this._skip;
        }

        public Boolean MoveNext()
        {
            while (this._left > 0 && this._enumerator.MoveNext())
            {
                --this._left;
            }
            return this._enumerator.MoveNext();
        }

        public void Reset()
        {
            this._left = this._skip;
            this._enumerator.Reset();
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

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
    }
}
#pragma warning restore Immutability // Fields are readonly or constant
#pragma warning restore NoProperties // No Properties