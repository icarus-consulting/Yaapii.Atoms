using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Partitiones a given enumerable by a given size.
    /// <para>Is a IEnumerable</para>
    /// </summary>
    public sealed class Partitioned<T> : EnumerableEnvelope<IEnumerable<T>>
    {
        public Partitioned(int size, IEnumerable<T> list) : base(() =>
            new EnumerableOf<IEnumerable<T>>(
                new Enumerator.Partitioned<T>(
                    size, list.GetEnumerator()
                )
            )
        )
        { }
    }
}
