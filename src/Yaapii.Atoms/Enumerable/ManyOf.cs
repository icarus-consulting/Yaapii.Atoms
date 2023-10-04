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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> out of strings.
    /// </summary>

    public sealed class ManyOf : IEnumerable<string>
    {
        private readonly IEnumerable<string> items;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public ManyOf(params string[] items) : this(() => new Params<string>(items))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <param name="e">a enumerator</param>
        public ManyOf(IEnumerator<string> e) : this(new Live<IEnumerator<string>>(e))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        public ManyOf(IScalar<IEnumerator<string>> sc) : this(() => sc.Value())
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public ManyOf(Func<IEnumerable<string>> fnc) : this(() => fnc().GetEnumerator())
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}"/>"/>.
        /// </summary>
        /// <param name="origin">scalar to return the IEnumerator</param>
        public ManyOf(Func<IEnumerator<string>> origin, bool live = false)
        {
            this.items =
                Ternary.New(
                    Sticky.New(Produced),
                    LiveMany.New(Produced),
                    live
                );
        }

        public IEnumerator<string> GetEnumerator() => this.items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private IEnumerable<string> Produced()
        {
            foreach (var item in this.items)
            {
                yield return item;
            }
        }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public static IEnumerable<T> New<T>(params T[] items) => new ManyOf<T>(items);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <param name="e">a enumerator</param>
        public static IEnumerable<T> New<T>(IEnumerator<T> e) => new ManyOf<T>(e);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        public static IEnumerable<T> New<T>(IScalar<IEnumerator<T>> sc) => new ManyOf<T>(sc);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        /// <param name="fnc">function which retrieves enumerator</param>
        public static IEnumerable<T> New<T>(Func<IEnumerable<T>> fnc) => new ManyOf<T>(() => fnc().GetEnumerator());

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}"/>"/>.
        /// </summary>
        public static IEnumerable<T> New<T>(Func<IEnumerator<T>> origin) => new ManyOf<T>(origin);
    }

    /// <summary>
    /// A <see cref="IEnumerable{T}"/> out of other objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public sealed class ManyOf<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> result;
        private readonly IEnumerable<T> origin;

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of an array.
        /// </summary>
        /// <param name="items"></param>
        public ManyOf(params T[] items) : this(
            new Params<T>(items)
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/>.
        /// </summary>
        /// <param name="e">a enumerator</param>
        public ManyOf(IEnumerator<T> e) : this(new EnumeratorAsEnumerable<T>(e))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        public ManyOf(IScalar<IEnumerator<T>> sc) : this(new EnumeratorAsEnumerable<T>(sc.Value()))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> returned by a <see cref="Func{T}"/>"/>.
        /// </summary>
        public ManyOf(Func<IEnumerator<T>> sc) : this(new EnumeratorAsEnumerable<T>(sc))
        { }

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> out of a <see cref="IEnumerator{T}"/> encapsulated in a <see cref="IScalar{T}"/>"/>.
        /// </summary>
        /// <param name="origin">scalar to return the IEnumerator</param>
        public ManyOf(IEnumerable<T> origin, bool live = false)
        {
            this.result =
                Ternary.New(
                    Sticky.New(Produced),
                    LiveMany.New(Produced),
                    live
                );
            this.origin = origin;
        }

        public IEnumerator<T> GetEnumerator() => this.result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<T> Produced()
        {
            foreach (var item in this.origin)
            {
                yield return item;
            }
        }
    }
}
