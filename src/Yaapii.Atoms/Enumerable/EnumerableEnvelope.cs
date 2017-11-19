using System;
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Enumerator;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    public class EnumerableEnvelope<T> : IEnumerable<T>
    {
        private readonly IScalar<IEnumerable<T>> _items;

        public EnumerableEnvelope(IScalar<IEnumerable<T>> sc)
        {
            this._items = sc;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this._items.Value().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
