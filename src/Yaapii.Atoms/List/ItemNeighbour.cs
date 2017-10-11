using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Element before another element in a <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">type of element</typeparam>
    public sealed class ItemNeighbour<T> : IScalar<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// source enum
        /// </summary>
        private readonly IEnumerable<T> _src;

        /// <summary>
        /// fallback func
        /// </summary>
        private readonly IFunc<IEnumerable<T>, T> _fbk;

        /// <summary>
        /// needle
        /// </summary>
        private readonly T _needle;

        /// <summary>
        /// requested relative position to the given item
        /// </summary>
        private readonly int _pos;

        /// <summary>
        /// Next neighbour element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="item">item to start</param>
        public ItemNeighbour(T item, IEnumerable<T> source) : this(
                item,
                source,
                new FuncOf<IEnumerable<T>, T>(itr => { throw new IOException("Can't get right neighbour from iterable"); }))
        { }

        /// <summary>
        /// Next neighbour in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="item">item to start</param>
        public ItemNeighbour(T item, IEnumerable<T> source, T fallback) : this(item, source, 1, new FuncOf<IEnumerable<T>, T>(b => fallback))
        { }

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        /// <param name="item">item to start</param>
        public ItemNeighbour(T item, IEnumerable<T> source, int relativeposition) : this(item, source, relativeposition, new FuncOf<IEnumerable<T>, T>(itr => { throw new IOException("Can't get neighbour at position from iterable"); }))
        { }

        /// <summary>
        /// Element at a position in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="item">item to start</param>
        public ItemNeighbour(T item, IEnumerable<T> source, int relativeposition, T fallback) : this(item, source, relativeposition, new FuncOf<IEnumerable<T>, T>(b => fallback))
        { }

        /// <summary>
        /// Next neighbour of an item in a <see cref="IEnumerable{T}"/> with a fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">soruce enum</param>
        /// <param name="fallback">fallback value</param>
        /// <param name="item">item to start</param>
        public ItemNeighbour(T item, IEnumerable<T> source, IFunc<IEnumerable<T>, T> fallback) : this(item, source, 1, fallback)
        { }

        /// <summary>
        /// Element that comes before another element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="item">item to start</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="relativeposition">requested position relative to the given item</param>
        public ItemNeighbour(T item, IEnumerable<T> source, int relativeposition, IFunc<IEnumerable<T>, T> fallback)
        {
            this._needle = item;
            this._src = source;
            this._fbk = fallback;
            this._pos = relativeposition;
        }

        /// <summary>
        /// Get the item.
        /// </summary>
        /// <returns>the item</returns>
        public T Value()
        {
            return new ItemNeighbourEnumerator<T>(
                this._src.GetEnumerator(), this._needle, this._pos, this._fbk
            ).Value();
        }
    }
}