using System;
using System.Collections;
using System.Collections.Generic;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerator
{
    public class EnumeratorAsEnumerable<T> : IEnumerable<T>
    {
        private readonly IScalar<IEnumerator<T>> _origin;

        public EnumeratorAsEnumerable(IEnumerator<T> enumerator) : this(new ScalarOf<IEnumerator<T>>(enumerator))
        { }

        public EnumeratorAsEnumerable(IScalar<IEnumerator<T>> enumerator)
        {
            _origin = enumerator;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
