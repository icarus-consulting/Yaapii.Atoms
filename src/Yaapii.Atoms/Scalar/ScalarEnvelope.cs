using System;
using System.Collections.Generic;
using System.Text;

namespace Yaapii.Atoms.Scalar
{
    public abstract class ScalarEnvelope<T> : IScalar<T>
    {
        private readonly ScalarOf<T> result;

        public ScalarEnvelope(Func<T> result)
            : this(new LiveScalar<T>(result))
        { }

        public ScalarEnvelope(IScalar<T> result)
        {
            this.result = new ScalarOf<T>(result);
        }

        /// <summary>
        /// Get the result.
        /// </summary>
        /// <returns>the result</returns>
        public T Value()
        {
            return result.Value();
        }
    }
}
