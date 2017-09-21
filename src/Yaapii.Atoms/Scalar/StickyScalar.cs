using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class StickyScalar<T> : IScalar<T>
    {
        private readonly IFunc<bool, T> _func;

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">func to cache result from</param>
        public StickyScalar(Func<T> src) : this(new ScalarOf<T>(src))
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        public StickyScalar(IScalar<T> src)
        {
            this._func = new StickyFunc<Boolean, T>(input => src.Value());
        }

        public T Value()
        {
            return this._func.Invoke(true);
        }
    }
}
