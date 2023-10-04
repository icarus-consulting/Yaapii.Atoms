using System;
using System.Collections;
using System.Collections.Generic;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// A given enumerator as enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EnumeratorAsEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerator<T> enumerator;

        /// <summary>
        /// A given enumerator as enumerable.
        /// </summary>
        public EnumeratorAsEnumerable(IEnumerator<T> enumerator) : this(() => enumerator)
        { }

        /// <summary>
        /// A given enumerator as enumerable.
        /// </summary>
        public EnumeratorAsEnumerable(Func<IEnumerator<T>> enumerator)
        {
            this.enumerator = new Enumerator.Sticky<T>(enumerator);
        }

        public IEnumerator<T> GetEnumerator()
        {
            this.enumerator.Reset();
            while (this.enumerator.MoveNext())
            {
                yield return this.enumerator.Current;
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

