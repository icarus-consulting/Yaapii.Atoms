using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoProperties // No Properties
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerator{T}"/> that repeats one element infinitely.
    /// </summary>
    /// <typeparam name="T">type of the contents</typeparam>
    public sealed class EndlessEnumerator<T> : IEnumerator<T>
    {
        /// <summary>
        /// element to repeat
        /// </summary>
        private readonly UncheckedScalar<T> element;

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that repeats one element infinitely.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        public EndlessEnumerator(T elm) : this(new ScalarOf<T>(elm))
        { }

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that repeats one element infinitely.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        public EndlessEnumerator(IScalar<T> elm) : this(new UncheckedScalar<T>(elm))
        { }

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that repeats one element infinitely.
        /// </summary>
        /// <param name="elm">scalar of element to repeat</param>
        public EndlessEnumerator(UncheckedScalar<T> elm)
        {
            this.element = elm;
        }

        public bool MoveNext()
        {
            return true;
        }

        public void Reset()
        { }

        public void Dispose()
        { }

        public T Current
        {
            get
            {
                return this.element.Value();
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();
    }
}
#pragma warning restore NoProperties // No Properties
