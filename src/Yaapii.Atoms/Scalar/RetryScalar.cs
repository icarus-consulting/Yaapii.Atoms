using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RetryScalar<T> : IScalar<T>
    {
        private readonly IScalar<T> _scalar;
        private readonly IFunc<Int32, bool> _exit;

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="slr">func to retry when needed</param>
        public RetryScalar(Func<T> slr, int attempts = 3) : this(new ScalarOf<T>(() => slr.Invoke()), attempts)
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="slr">scalar to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public RetryScalar(IScalar<T> slr, int attempts = 3) :
            this(slr, new FuncOf<int, bool>(attempt => attempt >= attempts))
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry until the given condition <see cref="IFunc{In, Out}<"/> matches before throwing an exception.
        /// </summary>
        /// <param name="slr">scalar to retry when needed</param>
        /// <param name="exit"></param>
        public RetryScalar(IScalar<T> slr, IFunc<Int32, Boolean> exit)
        {
            this._scalar = slr;
            this._exit = exit;
        }

        public T Value()
        {
            return new RetryFunc<Boolean, T>(
                new FuncOf<Boolean, T>(input => this._scalar.Value()),
                this._exit).Invoke(true);
        }
    }
}
