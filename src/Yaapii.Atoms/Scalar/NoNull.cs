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
    public class NoNull<T> : ScalarEnvelope<T>
    {
        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        public NoNull(T origin) : this(
            origin,
            new IOException("got NULL instead of a valid value"))
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="ex">error to raise if null</param>
        public NoNull(T origin, Exception ex) : this(
            new LiveScalar<T>(origin),
            new FuncOf<T>(() => throw ex))
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="fallback">the fallback value</param>
        public NoNull(T origin, T fallback) : this(
            new LiveScalar<T>(origin),
            fallback)
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback value</param>
        public NoNull(IScalar<T> origin, T fallback) : this(
            origin,
            new FuncOf<T>(() => fallback))
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback</param>
        public NoNull(IScalar<T> origin, IFunc<T> fallback)
            : base(() =>
            {
                T ret = origin.Value();

                if (ret == null)
                {
                    ret = fallback.Invoke();
                }

                return ret;
            })
        { }
    }
}
