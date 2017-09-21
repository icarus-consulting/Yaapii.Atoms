using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A <see cref="IScalar{T}"/> that is threadsafe.
    /// </summary>
    /// <typeparam name="T">type of value</typeparam>
    public sealed class ThreadSafeScalar<T> : IScalar<T>
    {
        private readonly IScalar<T> _source;
        private readonly Object _lock;

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">object to lock while using scalar</param>
        public ThreadSafeScalar(IScalar<T> src) : this(src, src)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">object to lock while using scalar</param>
        public ThreadSafeScalar(IScalar<T> src, Object lck)
        {
            this._source = src;
            this._lock = lck;
        }

        public T Value()
        {
            lock (this._source)
            {
                return this._source.Value();
            }
        }
    }
}
