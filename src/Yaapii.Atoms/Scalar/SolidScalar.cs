using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A Scalar that is both threadsafe and sticky.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class SolidScalar<T> : IScalar<T>
    {
        private readonly IScalar<T> src;
        private readonly object lck;
        private volatile object cache;

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public SolidScalar(Func<T> src) : this(src, src)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public SolidScalar(Func<T> src, object lck) : this(new ScalarOf<T>(src), lck)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public SolidScalar(IScalar<T> src) : this(src, src)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">object to lock while using scalar</param>
        public SolidScalar(IScalar<T> src, Object lck)
        {
            this.src = src;
            this.lck = lck;
        }

        public T Value()
        {
            if(this.cache == null)
            {
                lock (this.lck)
                {
                    if (this.cache == null)
                    {
                        this.cache = this.src.Value();
                    }
                }
            }
            return (T)this.cache;
        }
    }
}
