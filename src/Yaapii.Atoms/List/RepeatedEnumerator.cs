using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

#pragma warning disable Immutability // Fields are readonly or constant
#pragma warning disable NoProperties // No Properties
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// <see cref="IEnumerator{T}"/> which repeats one value multiple times.
    /// </summary>
    /// <typeparam name="T">type of element to repeat</typeparam>
    public sealed class RepeatedEnumerator<T> : IEnumerator<T>
    {
        private readonly UncheckedScalar<T> _element;
        private int _left;
        private int _max;

        /// <summary>
        /// <see cref="IEnumerator{T}"/> which repeats one value multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="max">how often to repeat</param>
        public RepeatedEnumerator(T elm, int max) : this(new ScalarOf<T>(elm), max)
        { }

        /// <summary>
        /// <see cref="IEnumerator{T}"/> which repeats one value multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="max">how often to repeat</param>
        public RepeatedEnumerator(IScalar<T> elm, int max): this(new UncheckedScalar<T>(elm), max)
        { }

        /// <summary>
        /// <see cref="IEnumerator{T}"/> which repeats one value multiple times.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        /// <param name="max">how often to repeat</param>
        public RepeatedEnumerator(UncheckedScalar<T> elm, int max)
        {
            this._element = elm;
            this._max = max;
            this._left = max;
        }

        public Boolean MoveNext()
        {
            if (this._left == 0) return false;
            --this._left;
            return true;
        }

        public void Reset()
        {
            this._left = this._max; 
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public T Current
        {
            get
            {
                return this._element.Value();
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();
    }
}
#pragma warning restore NoProperties // No Properties
#pragma warning restore Immutability // Fields are readonly or constant