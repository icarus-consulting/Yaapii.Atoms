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

#pragma warning disable NoProperties // No Properties
#pragma warning disable CS1591
namespace Yaapii.Atoms.Enumerator
{

    /// <summary>
    /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition.
    /// </summary>
    /// <typeparam name="X"></typeparam>
    public sealed class Filtered<X> : IEnumerator<X>
    {
        /// <summary>
        /// enumerator to filter
        /// </summary>
        private readonly IEnumerator<X> enumerator;

        /// <summary>
        /// filter function
        /// </summary>
        private readonly Func<X, Boolean> func;

        /// <summary>
        /// buffer to store filtered elements
        /// </summary>
        private readonly Queue<X> buffer;

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public Filtered(IEnumerator<X> src, IFunc<X, Boolean> fnc) : this(src, (ipt) => fnc.Invoke(ipt))
        { }

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public Filtered(IEnumerator<X> src, Func<X, Boolean> fnc)
        {
            this.enumerator = src;
            this.func = fnc;
            this.buffer = new Queue<X>();
        }

        public X Current
        {
            get
            {
                return this.buffer.Peek();
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.buffer.Peek();
            }
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (this.buffer.Count > 0) this.buffer.Dequeue();

            if (this.buffer.Count == 0)
            {
                while (this.enumerator.MoveNext())
                {
                    X obj = this.enumerator.Current;
                    if (func.Invoke(obj))
                    {
                        this.buffer.Enqueue(obj);
                        break;
                    }
                }
            }
            else
            {
                this.buffer.Dequeue();
            }

            return this.buffer.Count > 0;
        }

        public void Reset()
        {
            this.enumerator.Reset();
        }
    }

    /// <summary>
    /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition.
    /// </summary>
    public static class Filtered
    {
        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public static Filtered<T> New<T>(IEnumerator<T> src, IFunc<T, Boolean> fnc) =>
            new Filtered<T>(src, fnc);


        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public static Filtered<T> New<T>(IEnumerator<T> src, Func<T, Boolean> fnc) =>
            new Filtered<T>(src, fnc);
    }
}

