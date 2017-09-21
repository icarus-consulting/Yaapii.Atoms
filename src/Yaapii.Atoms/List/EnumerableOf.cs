using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> out of other objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EnumerableOf<T> : IEnumerable<T>
    {
        /// <summary>
        /// the enumerable
        /// </summary>
        private readonly UncheckedScalar<IEnumerator<T>> _origin;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public EnumerableOf(params T[] items) : this(
            () => items.AsEnumerable<T>().GetEnumerator())
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <param name="e">a enumerator</param>
        public EnumerableOf(IEnumerator<T> e) : this(new ScalarOf<IEnumerator<T>>(e))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}</see>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        private EnumerableOf(Func<IEnumerator<T>> fnc) : this(new ScalarOf<IEnumerator<T>>(fnc))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}</see>"/>.
        /// </summary>
        /// <param name="origin">scalar which returns enumerator</param>
        private EnumerableOf(IScalar<IEnumerator<T>> origin) : this(new UncheckedScalar<IEnumerator<T>>(origin))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}</see>"/>.
        /// </summary>
        /// <param name="origin">scalar to return the IEnumerator</param>
        private EnumerableOf(UncheckedScalar<IEnumerator<T>> origin)
        {
            _origin = origin;
        }

        public IEnumerator<T> GetEnumerator()

        {
            return _origin.Value();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _origin.Value();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics