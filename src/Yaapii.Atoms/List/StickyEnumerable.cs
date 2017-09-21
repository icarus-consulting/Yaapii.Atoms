using System;
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Scalar;

#pragma warning disable NoGetOrSet // No Statics
namespace Yaapii.Atoms.List
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> that returns content from cache always.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public sealed class StickyEnumerable<X> : IEnumerable<X>
    {

        /// <summary>
        /// Cache
        /// </summary>
        private readonly UncheckedScalar<IEnumerable<X>> _gate;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> that returns content from cache always.
        /// </summary>
        /// <param name="enumerable">the enum</param>
        public StickyEnumerable(IEnumerable<X> enumerable)
        {
            this._gate =
                new UncheckedScalar<IEnumerable<X>>(
                    new StickyScalar<IEnumerable<X>>(
                        () =>
                            {
                                var temp = new LinkedList<X>();
                                foreach (X item in enumerable)
                                {
                                    temp.AddLast(item);
                                }
                                return temp;
                            }));
        }

        /// <summary>
        /// Get the cached enumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<X> GetEnumerator()
        {
            return this._gate.Value().GetEnumerator();
        }

        /// <summary>
        /// Get the cached enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#pragma warning restore NoGetOrSet // No Statics
