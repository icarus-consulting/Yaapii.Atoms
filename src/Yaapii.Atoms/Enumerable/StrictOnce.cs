using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// An enumerable which rejects being created more than one time.
    /// This object is about to be used to ensure proper code design, preventing unnecessary waste of resources.
    /// Wrap it around live objects if you want to be sure that your code stays fast.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class StrictOnce<T> : IEnumerable<T>
    {
        private readonly Lazy<IEnumerable<T>> many;

        /// <summary>
        /// An enumerable which rejects being created more than one time.
        /// This object is about to be used to ensure proper code design, preventing unnecessary waste of resources.
        /// Wrap it around live objects if you want to be sure that your code stays fast.
        /// </summary>
        public StrictOnce(Func<IEnumerable<T>> many)
        {
            this.many = new Lazy<IEnumerable<T>>(many);
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.many.IsValueCreated)
            {
                throw new InvalidOperationException($"The enumerable is restricted to be created only once, but this is the second attempt to create it. Check the code design.");
            }
            return this.many.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
