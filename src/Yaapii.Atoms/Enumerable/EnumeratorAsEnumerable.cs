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
        private readonly Lazy<IEnumerator<T>> enumerator;

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
            this.enumerator = new Lazy<IEnumerator<T>>(enumerator);
        }

        public IEnumerator<T> GetEnumerator()
        {
            while(this.enumerator.Value.MoveNext())
            {
                yield return this.enumerator.Value.Current;
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

