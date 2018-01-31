using System.Collections.Generic;
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Multiple enumerables merged together, so that every entry is unique.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Distinct<T> : EnumerableEnvelope<T>
    {
        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public Distinct(params IEnumerable<T>[] enumerables) : this(new EnumerableOf<IEnumerable<T>>(enumerables))
        { }

        /// <summary>
        /// The distinct elements of one or multiple Enumerables.
        /// </summary>
        /// <param name="enumerables">enumerables to get distinct elements from</param>
        public Distinct(IEnumerable<IEnumerable<T>> enumerables) : base(
            new ScalarOf<IEnumerable<T>>(
                new EnumerableOf<T>(
                    new DistinctEnumerator<T>(
                        new Mapped<IEnumerable<T>, IEnumerator<T>>(
                            (e) => e.GetEnumerator(),
                            enumerables
                            )
                        )
                    )
                )
            )
        { }
    }
}
