using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#pragma warning disable NoProperties // No Properties
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerator{T}"/> that starts from the beginning when ended.
    /// </summary>
    /// <typeparam name="T">type of the contents</typeparam>
    public sealed class CycledEnumerator<T> : IEnumerator<T>
    {
        /// <summary>
        /// enum to cycle
        /// </summary>
        private readonly IEnumerable<T> _enumerable;

        /// <summary>
        /// cache
        /// </summary>
        private readonly Queue<IEnumerator<T>> _buffer = new Queue<IEnumerator<T>>();

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that starts from the beginning when ended.
        /// </summary>
        /// <param name="enumerable">enum to cycle</param>
        public CycledEnumerator(IEnumerable<T> enumerable)
        {
            this._enumerable = enumerable;
        }

        public bool MoveNext()
        {
            if (this._buffer.Count == 0 || !this._buffer.Peek().MoveNext())
            {
                this._buffer.Clear();
                this._buffer.Enqueue(this._enumerable.GetEnumerator());
                return this._buffer.Peek().MoveNext();
            }
            return true;
        }

        public void Reset()
        {
            this._buffer.Clear();
        }

        public void Dispose()
        {
            
        }

        public T Current
        {
            get
            {
                return this._buffer.Peek().Current;
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
#pragma warning restore NoProperties // No Properties