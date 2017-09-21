using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> limited to an item maximum.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class Limited<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _enumerable;
        private readonly IScalar<int> _limit;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        /// <param name="limit">maximum item count</param>
        public Limited(IEnumerable<T> enumerable, int limit) : this(enumerable, new ScalarOf<int>(limit))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> limited to an item maximum.
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        /// <param name="limit">maximum item count</param>
        public Limited(IEnumerable<T> enumerable, IScalar<int> limit)
        {
            this._enumerable = enumerable;
            this._limit = limit;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new LimitedEnumerator<T>(this._enumerable.GetEnumerator(), this._limit.Value());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

#pragma warning restore NoGetOrSet // No Statics

