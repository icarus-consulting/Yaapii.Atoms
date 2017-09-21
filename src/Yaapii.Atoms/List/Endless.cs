using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

#pragma warning disable NoProperties // No Properties
#pragma warning disable Immutability // Fields are readonly or constant
#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> that repeats one element infinitely.
    /// </summary>
    /// <typeparam name="T">type of the elements</typeparam>
    public sealed class Endless<T> : IEnumerable<T>
    {

        /// <summary>
        /// repeated element
        /// </summary>
        private readonly T _element;

        /// <summary>
        /// A <see cref="IEnumerable"/> that repeats one element infinitely.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        public Endless(T elm)
        {
            this._element = elm;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EndlessEnumerator<T>(this._element);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoProperties // No Properties
#pragma warning restore Immutability // Fields are readonly or constant
#pragma warning restore NoGetOrSet // No Statics