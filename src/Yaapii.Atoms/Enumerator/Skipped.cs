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
using System.Collections;
using System.Collections.Generic;

#pragma warning disable Immutability // Fields are readonly or constant
#pragma warning disable NoProperties // No Properties
#pragma warning disable CS1591

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// A <see cref="IEnumerator{Tests}"/> which skips a given count of items.
    /// </summary>
    /// <typeparam name="T">type of items in enumerable</typeparam>
    public sealed class Skipped<T> : IEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;
        private readonly int skip;
        private int left;

        /// <summary>
        /// A <see cref="IEnumerator{Tests}"/> which skips a given count of items.
        /// </summary>
        /// <param name="enumerator"><see cref="IEnumerator{T}"/> to skip items in</param>
        /// <param name="skip">how many to skip</param>
        public Skipped(IEnumerator<T> enumerator, int skip)
        {
            this.enumerator = enumerator;
            this.skip = skip;
            this.left = this.skip;
        }

        public Boolean MoveNext()
        {
            while (this.left > 0 && this.enumerator.MoveNext())
            {
                --this.left;
            }
            return this.enumerator.MoveNext();
        }

        public void Reset()
        {
            this.left = this.skip;
            this.enumerator.Reset();
        }

        public void Dispose()
        { }

        public T Current
        {
            get
            {
                return this.enumerator.Current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
    }

    /// <summary>
    /// A <see cref="IEnumerator{Tests}"/> which skips a given count of items.
    /// </summary>
    public static class Skipped
    {
        /// <summary>
        /// A <see cref="IEnumerator{Tests}"/> which skips a given count of items.
        /// </summary>
        /// <param name="enumerator"><see cref="IEnumerator{T}"/> to skip items in</param>
        /// <param name="skip">how many to skip</param>
        public static Skipped<T> New<T>(IEnumerator<T> enumerator, int skip) => new Skipped<T>(enumerator, skip);
    }
}
