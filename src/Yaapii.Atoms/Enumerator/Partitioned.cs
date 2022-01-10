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

namespace Yaapii.Atoms.Enumerator
{
    /// <summary>
    /// Partitiones an Enumerator by a given size
    /// <para>Is a IEnumerator</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Partitioned<T> : IEnumerator<IEnumerable<T>>
    {
        private readonly int size;
        private readonly IEnumerator<T> enumerator;
        private readonly List<T> buffer;

        /// <summary>
        /// Partitiones an Enumerator by a given size
        /// </summary>
        /// <param name="size"></param>
        /// <param name="enumerator"></param>
        public Partitioned(int size, IEnumerator<T> enumerator) : this(
            size,
            enumerator,
            new List<T>()
        )
        { }

        private Partitioned(int size, IEnumerator<T> enumerator, List<T> buffer)
        {
            this.size = size;
            this.enumerator = enumerator;
            this.buffer = buffer;
        }

        /// <summary>
        /// Returns the current buffer value.
        /// </summary>
        public IEnumerable<T> Current
        {
            get
            {
                if (buffer.Count < 1)
                {
                    throw new InvalidOperationException();
                }
                return buffer;
            }
        }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            this.enumerator.Dispose();
        }

        /// <summary>
        /// Moves to the next object in the Enumerator
        /// </summary>
        /// <returns></returns>ma
        public bool MoveNext()
        {
            if (this.size < 1)
            {
                throw new ArgumentException("Partition size < 1");
            }
            return Partitionate();
        }

        /// <summary>
        /// Resets the enumerator as well as the control buffer.
        /// </summary>
        public void Reset()
        {
            this.enumerator.Reset();
            this.buffer.Clear();
        }

        private bool Partitionate()
        {
            bool result = true;
            this.buffer.Clear();

            if (!this.enumerator.MoveNext())
            {
                result = false;
            }
            else
            {
                this.buffer.Add(this.enumerator.Current);
                for (int i = 0; i < this.size - 1 && this.enumerator.MoveNext(); ++i)
                {
                    this.buffer.Add(this.enumerator.Current);
                }
            }
            return result;
        }
    }

    /// <summary>
    /// Partitiones an Enumerator by a given size
    /// <para>Is a IEnumerator</para>
    /// </summary>
    public static class Partitioned
    {
        /// <summary>
        /// Partitiones an Enumerator by a given size
        /// </summary>
        /// <param name="size"></param>
        /// <param name="enumerator"></param>
        public static Partitioned<T> New<T>(int size, IEnumerator<T> enumerator) =>
            new Partitioned<T>(size, enumerator);
    }
}
