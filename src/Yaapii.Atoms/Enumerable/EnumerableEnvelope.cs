using System;
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Envelope for Enumerable.
    /// It bundles the methods offered by IEnumerable and enables scalar based ctors.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumerableEnvelope<T> : IEnumerable<T>
    {
        /// <summary>
        /// The source enumerable.
        /// </summary>
        private readonly IScalar<IEnumerable<T>> _origin;

        internal EnumerableEnvelope(Func<IEnumerable<T>> fnc) : this(
            new ScalarOf<IEnumerable<T>>(fnc))
        { }

        /// <summary>
        /// Makes envelope for IEnumerable scalars.
        /// </summary>
        /// <param name="sc"></param>
        internal EnumerableEnvelope(IScalar<IEnumerable<T>> sc)
        {
            this._origin = sc;
        }

        /// <summary>
        /// Enumerator for this envelope.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this._origin.Value().GetEnumerator();
        }

        /// <summary>
        /// Enumerator for this envelope.
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
