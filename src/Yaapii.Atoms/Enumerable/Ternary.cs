// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.Enumerable
{
    /// <summary>
    /// Enumerable sourced depending on a given condition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Ternary<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> whenMatching;
        private readonly IEnumerable<T> whenNotMatching;
        private readonly Func<bool> condition;

        /// <summary>
        /// Enumerable sourced depending on a given condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public Ternary(IEnumerable<T> whenMatching, IEnumerable<T> whenNotMatching, bool condition) : this(
            whenMatching,
            whenNotMatching,
            () => condition
        )
        { }

        /// <summary>
        /// Enumerable sourced depending on a given condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public Ternary(IEnumerable<T> whenMatching, IEnumerable<T> whenNotMatching, Func<bool> condition)
        {
            this.whenMatching = whenMatching;
            this.whenNotMatching = whenNotMatching;
            this.condition = condition;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.condition())
                foreach (var item in this.whenMatching)
                    yield return item;
            else
                foreach (var item in this.whenNotMatching)
                    yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public static class Ternary
    {
        /// <summary>
        /// Enumerable sourced depending on a given condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static Ternary<T> New<T>(IEnumerable<T> whenMatching, IEnumerable<T> whenNotMatching, bool condition) =>
            new Ternary<T>(whenMatching, whenNotMatching, condition);

        /// <summary>
        /// Enumerable sourced depending on a given condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static Ternary<T> New<T>(IEnumerable<T> whenMatching, IEnumerable<T> whenNotMatching, Func<bool> condition) =>
            new Ternary<T>(whenMatching, whenNotMatching, condition);
    }
}

