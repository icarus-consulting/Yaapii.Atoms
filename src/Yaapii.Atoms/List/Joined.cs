using System;
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Func;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Multiple <see cref="IEnumerable{T}"/> joined together.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Joined<T> : IEnumerable<T>
    {
        /// <summary>
        /// enumerables to join
        /// </summary>
        private readonly IEnumerable<IEnumerable<T>> _list;

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> joined together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public Joined(params IEnumerable<T>[] items)
        {
            this._list = items;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new JoinedEnumerator<T>(
                new Mapped<IEnumerable<T>, IEnumerator<T>>(//Map the content of list: Get every enumerator out of it and build one whole enumerator from it
                    this._list, //List with enumerators
                    new StickyFunc<IEnumerable<T>, IEnumerator<T>>( //Sticky Gate
                        new FuncOf<IEnumerable<T>, IEnumerator<T>>(e => e.GetEnumerator()) //Get the Enumerator
                        )
                )
            );
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics