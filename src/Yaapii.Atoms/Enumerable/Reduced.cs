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
using System.Collections.Generic;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> whose items are reduced to one item using the given function.
    /// </summary>
    /// <typeparam name="T">type of elements in a list to reduce</typeparam>
    public sealed class Reduced<T> : ScalarEnvelope<T>
    {
        private readonly ScalarOf<T> result;

        /// <summary>
        /// <see cref="IEnumerable{Element}"/> whose items are folded to one item using the given function.
        /// </summary>
        /// <param name="elements">enumerable to reduce</param>
        /// <param name="fnc">reducing function</param>
        public Reduced(IEnumerable<T> elements, IBiFunc<T, T, T> fnc) : this(
            elements,
            (arg1, arg2) => fnc.Invoke(arg1, arg2)
        )
        { }

        /// <summary>
        /// <see cref="IEnumerable{Element}"/> whose items are reduced to one item using the given function.
        /// </summary>
        /// <param name="elements">enumerable to reduce</param>
        /// <param name="fnc">reducing function</param>
        public Reduced(IEnumerable<T> elements, Func<T, T, T> fnc) : base(() =>
            {
                var enm = elements.GetEnumerator();

                if (!enm.MoveNext()) throw new ArgumentException($"Cannot reduce, at least one element is needed but the enumerable is empty.");
                T result = enm.Current;
                while (enm.MoveNext())
                {
                    result = fnc.Invoke(result, enm.Current);
                }
                return result;
            }
        )
        { }
    }

    /// <summary>
    /// <see cref="IEnumerable{T}"/> whose items are reduced to one item using the given function.
    /// </summary>
    public static class Reduced
    {
        /// <summary>
        /// <see cref="IEnumerable{Element}"/> whose items are folded to one item using the given function.
        /// </summary>
        /// <param name="elements">enumerable to reduce</param>
        /// <param name="fnc">reducing function</param>
        public static Reduced<T> New<T>(IEnumerable<T> elements, IBiFunc<T, T, T> fnc) => new Reduced<T>(elements, fnc);

        /// <summary>
        /// <see cref="IEnumerable{Element}"/> whose items are reduced to one item using the given function.
        /// </summary>
        /// <param name="elements">enumerable to reduce</param>
        /// <param name="fnc">reducing function</param>
        public static Reduced<T> New<T>(IEnumerable<T> elements, Func<T, T, T> fnc) => new Reduced<T>(elements, fnc);
    }
}
