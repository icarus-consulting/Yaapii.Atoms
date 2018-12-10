using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Mapped list
    /// </summary>
    /// <typeparam name="In">Type of source items</typeparam>
    /// <typeparam name="Out">Type of target items</typeparam>
    public sealed class Mapped<In, Out> : ListEnvelope<Out>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(IFunc<In, Out> fnc, IEnumerable<In> src) : this((input)=>fnc.Invoke(input), src)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(Func<In, Out> fnc, IEnumerator<In> src) : this(fnc, new ListOf<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(Func<In, Out> fnc, IEnumerable<In> src) : this(fnc, new ListOf<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="fnc">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(Func<In, Out> fnc, ICollection<In> src) : base(() => new ListOf<Out>(
                  new Collection.Mapped<In, Out>(fnc, src)))
        { }

}
}
