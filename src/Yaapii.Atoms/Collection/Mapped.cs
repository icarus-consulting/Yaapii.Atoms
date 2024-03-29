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
using System.Collections.Generic;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Collection
{
    /// <summary>
    /// A collection which is mapped to the output type.
    /// </summary>
    /// <typeparam name="In">source type</typeparam>
    /// <typeparam name="Out">target type</typeparam>
    public sealed class Mapped<In, Out> : CollectionEnvelope<Out>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source items</param>
        public Mapped(Func<In, Out> mapping, params In[] src) : this(mapping, new ManyOf<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerator</param>
        public Mapped(Func<In, Out> mapping, IEnumerator<In> src) : this(
            mapping, new ManyOf<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source enumerable</param>
        public Mapped(Func<In, Out> mapping, IEnumerable<In> src) : this(
            mapping, new LiveCollection<In>(src))
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mapping">mapping function</param>
        /// <param name="src">source collection</param>
        public Mapped(Func<In, Out> mapping, ICollection<In> src) : base(() =>
            new Enumerable.Mapped<In, Out>(mapping, src).GetEnumerator(),
            false
        )
        { }
    }

    // <summary>
    /// A collection which is mapped to the output type.
    /// </summary>
    /// <typeparam name="In">source type</typeparam>
    /// <typeparam name="Out">target type</typeparam>
    public static class Mapped
    {
        public static ICollection<Out> New<In, Out>(Func<In, Out> mapping, params In[] src) => new Mapped<In, Out>(mapping, src);

        public static ICollection<Out> New<In, Out>(Func<In, Out> mapping, IEnumerator<In> src) => new Mapped<In, Out>(mapping, src);

        public static ICollection<Out> New<In, Out>(Func<In, Out> mapping, IEnumerable<In> src) => new Mapped<In, Out>(mapping, src);

        public static ICollection<Out> New<In, Out>(Func<In, Out> mapping, ICollection<In> src) => new Mapped<In, Out>(mapping, src);
    }
}
