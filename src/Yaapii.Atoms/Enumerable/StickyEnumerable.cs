using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// The sticky represantation of an <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T">The type of the enumerable</typeparam>
    public class StickyEnumerable<T> : EnumerableEnvelope<T>
    {
        /// <summary>
        /// Makes a sticky enumerable
        /// </summary>
        /// <param name="items">The items</param>
        public StickyEnumerable(params T[] items) : this(new EnumerableOf<T>(items))
        { }
       
        /// <summary>
        /// Makes a sticky enumerable
        /// </summary>
        /// <param name="item">The enumerator</param>
        public StickyEnumerable(IEnumerator<T> item) : this(new EnumerableOf<T>(item))
        { }
       
        /// <summary>
        /// Makes a sticky enumerable
        /// </summary>
        /// <param name="src"></param>
        public StickyEnumerable(IEnumerable<T> src) : base(
            new StickyScalar<IEnumerable<T>>(() =>
            {
                List<T> lst = new List<T>();
                foreach (T item in src)
                {
                    lst.Add(item);
                }
                return lst;
            }
        ))
        { }
    }
}
