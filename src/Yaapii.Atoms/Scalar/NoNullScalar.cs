using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// Scalar that will raise error or return fallback if value is null.
    /// </summary>
    /// <typeparam name="T">type of return value</typeparam>
    public class NoNullScalar<T> : IScalar<T>
    {
        private readonly IScalar<T> _origin;
        private readonly IFunc<T> _fallback;

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        public NoNullScalar(T origin) : this(
            origin, 
            new IOException("got NULL instead of a valid value"))
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="ex">error to raise if null</param>
        public NoNullScalar(T origin, Exception ex) : this(
            new ScalarOf<T>(origin), 
            new FuncOf<T>(() => throw ex))
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="fallback">the fallback value</param>
        public NoNullScalar(T origin, T fallback) : this(
            new ScalarOf<T>(origin),
            fallback)
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback value</param>
        public NoNullScalar(IScalar<T> origin, T fallback) : this(
            origin, 
            new FuncOf<T>(() => fallback))
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback</param>
        public NoNullScalar(IScalar<T> origin, IFunc<T> fallback)
        {
            _origin = origin;
            _fallback = fallback;
        }

        /// <summary>
        /// get the value
        /// </summary>
        /// <returns>value or fallback value, if null</returns>
        public T Value()
        {
            T ret = _origin.Value();

            if (ret == null) ret = _fallback.Invoke();

            return ret;
        }
    }
}
