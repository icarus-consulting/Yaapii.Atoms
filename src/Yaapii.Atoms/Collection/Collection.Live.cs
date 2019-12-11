using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Collection
{
    public partial class Collection
    {
        /// <summary>
        /// Envelope for collections. 
        /// It accepts a scalar and makes readonly Collection from it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public sealed class Live<T> : Envelope<T>
        {
            /// <summary>
            /// Makes a collection from an array
            /// </summary>
            /// <param name="array"></param>
            public Live(params T[] array) : this(new Many.Live<T>(array))
            { }

            /// <summary>
            /// Makes a collection from an <see cref="IEnumerator{T}"/>
            /// </summary>
            /// <param name="src"></param>
            public Live(IEnumerator<T> src) : this(new Many.Of<T>(src))
            { }

            /// <summary>
            /// Makes a collection from an <see cref="IEnumerable{T}"/>
            /// </summary>
            /// <param name="src"></param>
            public Live(IEnumerable<T> src) : base(
                () =>
                {
                    ICollection<T> list = new LinkedList<T>();
                    foreach (T item in src)
                    {
                        list.Add(item);
                    }
                    return list;
                },
                true
            )
            { }
        }
    }
}
