using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable NoProperties

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// Enumerator that only gives the distinct elements of multiple enumerators.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class DistinctEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerable<IEnumerator<T>> _originals;
        private readonly Queue<IEnumerator<T>> _buffer = new Queue<IEnumerator<T>>();
        private readonly List<T> _hits = new List<T>();

        /// <summary>
        /// Enumerator that only gives the distinct elements of multiple enumerators.
        /// </summary>
        /// <param name="enumerators"></param>
        public DistinctEnumerator(IEnumerable<IEnumerator<T>> enumerators)
        {
            _originals = enumerators;
            _buffer = new Queue<IEnumerator<T>>(enumerators);
        }

        /// <summary>
        /// Current element
        /// </summary>
        public T Current
        {
            get
            {
                return this._buffer.Peek().Current;
            }
        }

        object IEnumerator.Current => Current;

        /// <summary>
        /// Move to next element
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            //skip all entries that are already known
            SkipKnown();
            bool cnt = _buffer.Count > 0;
            if (!cnt)
            {
                this.Reset();
            }
            return cnt;
        }

        /// <summary>
        /// Reset this enumerator
        /// </summary>
        public void Reset()
        {
            this._buffer.Clear();
            foreach (var e in this._originals)
            {
                this._buffer.Enqueue(e);
            }
            this._hits.Clear();
        }

        private void SkipKnown()
        {
            while (this._buffer.Count > 0)
            {
                while (this._buffer.Count > 0 && !this._buffer.Peek().MoveNext())
                {
                    this._buffer.Dequeue();
                }

                if (_buffer.Count > 0 && !this._hits.Contains(this._buffer.Peek().Current))
                {
                    this._hits.Add(this._buffer.Peek().Current);
                    break;
                }
            }
        }

        public void Dispose()
        { }
    }
}

