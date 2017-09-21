using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Scalar that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IoCheckedScalar<T> : IScalar<T>
    {
        private readonly IScalar<T> _scalar;

        /// <summary>
        /// Scalar that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
        /// </summary>
        /// <param name="fnc">func to call</param>
        public IoCheckedScalar(Func<T> fnc) : this(new ScalarOf<T>(fnc))
        { }

        /// <summary>
        /// Scalar that does not throw <see cref="Exception"/> but throws <see cref="IOException"/> instead.
        /// </summary>
        /// <param name="scalar">scalar of the value to retrieve</param>
        public IoCheckedScalar(IScalar<T> scalar)
        {
            this._scalar = scalar;
        }

        public T Value()
        {
            return new IoCheckedFunc<IScalar<T>, T>
            (
                new IoCheckedFunc<IScalar<T>, T>(new FuncOf<IScalar<T>, T>(s => s.Value()))
            ).Invoke(this._scalar);
        }
    }
}