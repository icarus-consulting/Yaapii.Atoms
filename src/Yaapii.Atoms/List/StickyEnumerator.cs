using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoProperties // No Properties
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerator{T}"/> that returns content from cache always.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public sealed class StickyEnumerator<X> : IEnumerator<X>
    {
        private readonly UncheckedScalar<IEnumerator<X>> _gate;

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that returns content from cache always.
        /// </summary>
        /// <param name="src">items</param>
        public StickyEnumerator(params X[] src) : this(
            () => new EnumerableOf<X>(src).GetEnumerator())
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">enumerator to put to cache</param>
        public StickyEnumerator(IEnumerator<X> src) : this(new ScalarOf<IEnumerator<X>>(src))
        { }

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> from the given that returns content from cache always.
        /// </summary>
        /// <param name="src">function to retrieve enumerator for cache</param>
        public StickyEnumerator(Func<IEnumerator<X>> src) : this(new CallableOf<IEnumerator<X>>(src))
        { }

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> from the given that returns content from cache always.
        /// </summary>
        /// <param name="src">function to retrieve enumerator for cache</param>
        public StickyEnumerator(ICallable<IEnumerator<X>> src) : this(new ScalarOf<IEnumerator<X>>(src))
        { }

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that returns content from cache always.
        /// </summary>
        /// <param name="src">scalar of enumerator to cache</param>
        public StickyEnumerator(IScalar<IEnumerator<X>> src)
        {
            this._gate =
                new UncheckedScalar<IEnumerator<X>>(
                    new StickyScalar<IEnumerator<X>>(
                        () =>
                        {
                            var temp = new LinkedList<X>();
                            var enumerator = src.Value();
                            while (enumerator.MoveNext())
                            {
                                temp.AddLast(enumerator.Current);
                            }
                            return temp.GetEnumerator();
                        }));
        }

        public Boolean MoveNext()
        {
            return this._gate.Value().MoveNext();
        }


        public X Current
        {
            get
            {
                return this._gate.Value().Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Reset()
        {
            this._gate.Value().Reset();
        }

        public void Dispose()
        {

        }
    }
}
#pragma warning restore NoProperties // No Properties