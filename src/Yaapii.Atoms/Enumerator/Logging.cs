using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#pragma warning disable NoProperties // No Properties
namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// An enumerator that logs the object T when it is moved.
    /// </summary>
    public sealed class LoggingEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> origin;
        private readonly Action<T> log;

        /// <summary>
        /// An enumerator that logs the object T when it is moved.
        /// </summary>
        public LoggingEnumerator(IEnumerator<T> origin, Action<T> log)
        {
            this.origin = origin;
            this.log = log;
        }

        public T Current => this.origin.Current;

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            this.origin.Dispose();
        }

        public bool MoveNext()
        {
            var moved = this.origin.MoveNext();
            if (moved)
            {
                this.log(Current);
            }
            return moved;
        }

        public void Reset()
        {
            this.origin.Reset();
        }
    }
}
