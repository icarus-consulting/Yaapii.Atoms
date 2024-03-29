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

using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Multiple lists joined together as one.
    /// </summary>
    /// <typeparam name="T">type of items in list</typeparam>
    public sealed class Joined<T> : ListEnvelope<T>
    {
        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="origin">a list to join</param>
        /// <param name="src">lists to join</param>
        public Joined(IList<T> origin, params IList<T>[] src) : this(
            new Enumerable.Joined<IList<T>>(
                new LiveMany<IList<T>>(origin), src
            )
        )
        { }

        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="src">The lists to join together</param>
        /// <param name="origin">a list to join</param>
        public Joined(IList<T> origin, params T[] src) : this(
            new Enumerable.Joined<IList<T>>(
                new LiveMany<IList<T>>(origin), src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public Joined(params IList<T>[] src) : this(new LiveMany<IList<T>>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public Joined(IEnumerable<IList<T>> src) : base(() =>
            {
                return
                    new ListOf<T>(
                        new Atoms.Enumerable.Joined<T>(src)
                    );
            },
            false
        )
        { }
    }
    public static class Joined
    {
        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="origin">a list to join</param>
        /// <param name="src">lists to join</param>
        public static IList<T> New<T>(IList<T> origin, params IList<T>[] src)
            => new Joined<T>(origin, src);

        /// <summary>
        /// Multiple <see cref="IList{T}"/> joined together
        /// </summary>
        /// <param name="src">The lists to join together</param>
        /// <param name="origin">a list to join</param>
        public static IList<T> New<T>(IList<T> origin, params T[] src)
            => new Joined<T>(origin, src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public static IList<T> New<T>(params IList<T>[] src)
            => new Joined<T>(src);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">The lists to join together</param>
        public static IList<T> New<T>(IEnumerable<IList<T>> src)
            => new Joined<T>(src);
    }
}
