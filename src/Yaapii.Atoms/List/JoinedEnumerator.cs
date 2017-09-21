using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.List;

#pragma warning disable NoProperties // No Properties
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Multiple <see cref="IEnumerator{T}"/> joined together.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class JoinedEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerable<IEnumerator<T>> _list;
        private readonly Queue<IEnumerator<T>> _buffer;

        /// <summary>
        /// Multiple <see cref="IEnumerator{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join together</param>
        public JoinedEnumerator(params IEnumerator<T>[] items) : this(new List<IEnumerator<T>>(items))
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerator{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join together</param>
        public JoinedEnumerator(IEnumerable<IEnumerator<T>> items)
        {
            this._list = items;
            this._buffer = new Queue<IEnumerator<T>>(items);
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

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            while(this._buffer.Count > 0 && !this._buffer.Peek().MoveNext())
            {
                this._buffer.Dequeue();
            }

            return this._buffer.Count > 0;
        }

        public void Reset()
        {
            this._buffer.Clear();
            var e = this._list.GetEnumerator();
            while (e.MoveNext())
            {
                this._buffer.Enqueue(e.Current);
            }
            
        }
    }
}
#pragma warning restore NoProperties // No Properties