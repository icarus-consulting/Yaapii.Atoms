using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// A collection which is mapped to the output type.
    /// </summary>
    /// <typeparam name="In">source type</typeparam>
    /// <typeparam name="Out">target type</typeparam>
    public sealed class Mapped<In, Out> : CollectionEnvelope<Out>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">mapping function</param>
        /// <param name="src">source items</param>
        public Mapped(Func<In, Out> fnc, params In[] src) : this(fnc, new EnumerableOf<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(Func<In, Out> fnc, IEnumerator<In> src) : this(
            fnc, new EnumerableOf<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">mapping function</param>
        /// <param name="src">source enumerable</param>
        public Mapped(Func<In, Out> fnc, IEnumerable<In> src) : this(
            fnc, new CollectionOf<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">mapping function</param>
        /// <param name="src">source collection</param>
        public Mapped(Func<In, Out> fnc, ICollection<In> src) : base(
            () => new CollectionOf<Out>(
                new Enumerable.Mapped<In, Out>(fnc, src)
            ))
        { }
    }
}