// MIT License
//
// Copyright(c) 2022 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A Scalar that is both threadsafe and sticky.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Solid<T> : IScalar<T>
    {
        private readonly IScalar<T> src;
        private readonly object lck;
        private volatile object cache;

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public Solid(Func<T> src) : this(src, src)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">the object to lock</param>
        public Solid(Func<T> src, object lck) : this(new Live<T>(src), lck)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public Solid(IScalar<T> src) : this(src, src)
        { }

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">object to lock while using scalar</param>
        public Solid(IScalar<T> src, Object lck)
        {
            this.src = src;
            this.lck = lck;
        }

        public T Value()
        {
            if (this.cache == null)
            {
                lock (this.lck)
                {
                    if (this.cache == null)
                    {
                        this.cache = this.src.Value();
                    }
                }
            }
            return (T)this.cache;
        }
    }

    public static class Solid
    {
        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public static Solid<T> New<T>(Func<T> src)
            => new Solid<T>(src);

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">the object to lock</param>
        public static Solid<T> New<T>(Func<T> src, object lck)
            => new Solid<T>(src, lck);

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        public static Solid<T> New<T>(IScalar<T> src)
            => new Solid<T>(src);

        /// <summary>
        /// A <see cref="IScalar{T}"/> that is threadsafe and sticky.
        /// </summary>
        /// <param name="src">the scalar to make operate threadsafe</param>
        /// <param name="lck">object to lock while using scalar</param>
        public static Solid<T> New<T>(IScalar<T> src, Object lck)
            => new Solid<T>(src, lck);
    }
}
