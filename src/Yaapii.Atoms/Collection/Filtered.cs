using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection
{
    public sealed class Filtered<T> : CollectionEnvelope<T>
    {

        public Filtered(Func<T, Boolean> func, params T[] src) : this(func, new EnumerableOf<T>(src))
        { }

        /**
         * Ctor.
         * @param src Source collection
         * @param func Filter function
         * @since 0.23
         */
        public Filtered(Func<T, Boolean> func, IEnumerator<T> src) : this(func, new EnumerableOf<T>(src))
        { }

        /**
         * Ctor.
         * @param src Source collection
         * @param func Filter function
         */
        public Filtered(Func<T, Boolean> func, IEnumerable<T> src) : base(new ScalarOf<ICollection<T>>(() => new CollectionOf<T>(
                 new Enumerable.Filtered<T>(
                     func, src
                 ))))
        { }

    }
}