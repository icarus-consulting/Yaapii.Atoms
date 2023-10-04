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

namespace Yaapii.Atoms.Scalar
{
    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class LazyOf<T> : IScalar<T>
    {
        private readonly Lazy<T> origin;

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public LazyOf(Func<T> src, Func<T, bool> shouldReload)
        {
            this.origin = new Lazy<T>(src);
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public T Value()
        {
            return this.origin.Value;
        }
    }

    /// <summary>
    /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ScalarOf<T> : IScalar<T>
    {
        private readonly IScalar<T> origin;
        private readonly Func<T, bool> shouldReload;
        private readonly T[] cache;
        private readonly bool[] filled; //this not-readonly flag is a compromise due to performance issues when using StickyFunc.

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">func to cache result from</param>
        public ScalarOf(T src) : this(new Live<T>(src))
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">func to cache result from</param>
        public ScalarOf(Func<T> src) : this(new Live<T>(src))
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        public ScalarOf(IScalar<T> src) : this(src, input => false)
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="srcFunc">func to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public ScalarOf(Func<T> srcFunc, Func<T, bool> shouldReload) : this(new Live<T>(srcFunc), shouldReload)
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="srcFunc">func to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public ScalarOf(IFunc<T> srcFunc, Func<T, bool> shouldReload) : this(new Live<T>(srcFunc), shouldReload)
        { }

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public ScalarOf(IScalar<T> src, Func<T, bool> shouldReload)
        {
            this.origin = src;
            this.shouldReload = shouldReload;
            this.cache = new T[1];
            this.filled = new bool[1];
        }

        /// <summary>
        /// Get the value.
        /// </summary>
        /// <returns>the value</returns>
        public T Value()
        {
            if (this.filled[0] != true)
            {
                this.cache.SetValue(this.origin.Value(), 0);
                this.filled[0] = true;
            }
            else if (this.shouldReload(this.cache[0]))
            {
                this.cache[0] = this.origin.Value();
            }
            return this.cache[0];
        }
    }

    public static class ScalarOf
    {
        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">func to cache result from</param>
        public static IScalar<T> New<T>(T src) => new ScalarOf<T>(src);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">func to cache result from</param>
        public static IScalar<T> New<T>(Func<T> src) => new ScalarOf<T>(src);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache always.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        public static IScalar<T> New<T>(IScalar<T> src) => new ScalarOf<T>(src);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="srcFunc">func to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public static IScalar<T> New<T>(IFunc<T> srcFunc, Func<T, bool> shouldReload) => new ScalarOf<T>(srcFunc, shouldReload);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="srcFunc">func to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public static IScalar<T> New<T>(Func<T> srcFunc, Func<T, bool> shouldReload) => new ScalarOf<T>(srcFunc, shouldReload);

        /// <summary>
        /// A s<see cref="IScalar{T}"/> that will return the same value from a cache as long the reload condition is false.
        /// </summary>
        /// <param name="src">scalar to cache result from</param>
        /// <param name="shouldReload">reload condition func</param>
        public static IScalar<T> New<T>(IScalar<T> src, Func<T, bool> shouldReload) => new ScalarOf<T>(src, shouldReload);
    }
}
