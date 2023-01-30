// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.IO;
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
            new Live<T>(origin),
            new FuncOf<T>(() => throw ex))
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="fallback">the fallback value</param>
        public NoNull(T origin, T fallback) : this(
            new Live<T>(origin),
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

    public static class NoNull
    {
        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        public static IScalar<T> New<T>(T origin)
            => new NoNull<T>(origin);

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="ex">error to raise if null</param>
        public static IScalar<T> New<T>(T origin, Exception ex)
            => new NoNull<T>(origin, ex);

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="fallback">the fallback value</param>
        public static IScalar<T> New<T>(T origin, T fallback)
            => new NoNull<T>(origin, fallback);

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback value</param>
        public static IScalar<T> New<T>(IScalar<T> origin, T fallback)
            => new NoNull<T>(origin, fallback);

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback</param>
        public static IScalar<T> New<T>(IScalar<T> origin, IFunc<T> fallback)
            => new NoNull<T>(origin, fallback);
    }
}
