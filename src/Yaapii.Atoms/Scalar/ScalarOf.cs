using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Func;
using Yaapii.Atoms.Misc;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A <see cref="IScalar{T}"/> out of other objects
    /// </summary>
    /// <typeparam name="T">type of the value</typeparam>
    public sealed class ScalarOf<T> : IScalar<T>
    {
        private readonly ICallable<T> _func;

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of a object.
        /// </summary>
        /// <param name="org"></param>
        public ScalarOf(T org) : this((b) => org)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of the return value from a <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// <param name="func"></param>
        public ScalarOf(Func<T> func) : this(new CallableOf<T>(() => func.Invoke()))
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of the return value from a <see cref="Func{T, TResult}"/>.
        /// </summary>
        /// <param name="func"></param>
        public ScalarOf(Func<bool, T> func) : this(new CallableOf<T>(() => func.Invoke(true)))
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> out of the return value from a <see cref="IFunc{In, Out}"/>
        /// </summary>
        /// <param name="func"></param>
        public ScalarOf(IFunc<bool, T> func) : this(new CallableOf<T>(() => func.Invoke(true)))
        { }

        /// <summary>
        /// Primary ctor
        /// </summary>
        /// <param name="func"></param>
        public ScalarOf(ICallable<T> func)
        {
            _func = func;
        }

        /// <summary>
        /// Gives the value
        /// </summary>
        /// <returns></returns>
        public T Value()
        {
            return _func.Call();
        }
    }
}
