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
    /// Enumerable which memoizes already visited items.
    /// </summary>
    public class Sticky<T> : IEnumerable<T>
    {
        private readonly object exclusive;
        private readonly Lazy<IEnumerator<T>> source;
        private readonly bool[] ended;
        private readonly List<T> buffer;

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public Sticky(IEnumerable<T> source) : this(() => source)
        { }

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public Sticky(Func<IEnumerable<T>> source) : this(() => source().GetEnumerator())
        { }

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public Sticky(IEnumerator<T> source) : this(() => source)
        { }

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public Sticky(Func<IEnumerator<T>> source)
        {
            this.exclusive = new object();
            this.buffer = new List<T>();
            this.source = new Lazy<IEnumerator<T>>(() => source());
            this.ended = new bool[1] { false };
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (!ended[0])
            {
                foreach (var item in Partial())
                {
                    yield return item;
                }
            }
            else
            {
                foreach (var item in Full())
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<T> Partial()
        {
            var i = 0;
            var enumerator = this.source.Value;
            while (true)
            {
                var hasValue = default(bool);

                lock (exclusive)
                {
                    if (i >= buffer.Count)
                    {
                        hasValue = enumerator.MoveNext();
                        if (hasValue)
                            buffer.Add(enumerator.Current);
                    }
                    else
                    {
                        hasValue = true;
                    }
                }

                if (hasValue)
                    yield return buffer[i];
                else
                {
                    this.ended[0] = true;
                    break;
                }

                i++;
            }
        }

        private IEnumerable<T> Full()
        {
            foreach (var item in this.buffer)
            {
                yield return item;
            }
        }
    }

    public static class Sticky
    {
        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static Sticky<T> New<T>(IEnumerable<T> source) =>
            new Sticky<T>(source);

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static Sticky<T> New<T>(Func<IEnumerable<T>> source) =>
            new Sticky<T>(source);

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static Sticky<T> New<T>(IEnumerator<T> source) =>
            new Sticky<T>(source);


        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static Sticky<T> New<T>(Func<IEnumerator<T>> source) =>
            new Sticky<T>(source);
    }
}

