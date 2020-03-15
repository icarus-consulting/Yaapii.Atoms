// MIT License
//
// Copyright(c) 2019 ICARUS Consulting GmbH
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
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Number
{
    /// <summary>
    /// Calculated sum of numbers.
    /// </summary>
    public sealed class SumOf : NumberEnvelope
    {
        /// <summary>
        /// A sum of floats
        /// </summary>
        /// <param name="src">source floats</param>
        public SumOf(params float[] src) : this(
            new Many.Of<float>(src))
        { }

        /// <summary>
        /// A sum of longs
        /// </summary>
        /// <param name="src">source longs</param>
        public SumOf(params long[] src) : this(
            new Many.Of<long>(src))
        { }

        /// <summary>
        /// A sum of ints
        /// </summary>
        /// <param name="src">source ints</param>
        public SumOf(params int[] src) : this(
            new Many.Of<int>(src))
        { }

        /// <summary>
        /// A sum of doubles
        /// </summary>
        /// <param name="src">source doubles</param>
        public SumOf(params double[] src) : this(
            new Many.Of<double>(src))
        { }

        /// <summary>
        /// A sum of doubles
        /// </summary>
        /// <param name="src">source doubles</param>
        public SumOf(IEnumerable<double> src) : base(
            new LiveScalar<double>(() =>
            {
                double sum = 0D;
                foreach (int val in src)
                {
                    sum += (double)val;
                }
                return sum;
            }),
            new LiveScalar<int>(() =>
            {
                int sum = 0;
                foreach (int val in src)
                {
                    sum += (int)val;
                }
                return sum;
            }),
            new LiveScalar<long>(() =>
            {
                long sum = 0L;
                foreach (long val in src)
                {
                    sum += (long)val;
                }
                return sum;
            }),
            new LiveScalar<float>(() =>
            {
                float sum = 0F;
                foreach (float val in src)
                {
                    sum += (float)val;
                }
                return sum;
            }))
        { }

        /// <summary>
        /// A sum of integers
        /// </summary>
        /// <param name="src">source integers</param>
        public SumOf(IEnumerable<int> src) : base(
            new LiveScalar<double>(() =>
            {
                double sum = 0D;
                foreach (int val in src)
                {
                    sum += (double)val;
                }
                return sum;
            }),
            new LiveScalar<int>(() =>
            {
                int sum = 0;
                foreach (int val in src)
                {
                    sum += (int)val;
                }
                return sum;
            }),
            new LiveScalar<long>(() =>
            {
                long sum = 0L;
                foreach (long val in src)
                {
                    sum += (long)val;
                }
                return sum;
            }),
            new LiveScalar<float>(() =>
            {
                float sum = 0F;
                foreach (float val in src)
                {
                    sum += (float)val;
                }
                return sum;
            }))
        { }

        /// <summary>
        /// A sum of longs
        /// </summary>
        /// <param name="src">source longs</param>
        public SumOf(IEnumerable<long> src) : base(
            new LiveScalar<double>(() =>
            {
                double sum = 0D;
                foreach (int val in src)
                {
                    sum += (double)val;
                }
                return sum;
            }),
            new LiveScalar<int>(() =>
            {
                int sum = 0;
                foreach (int val in src)
                {
                    sum += (int)val;
                }
                return sum;
            }),
            new LiveScalar<long>(() =>
            {
                long sum = 0L;
                foreach (long val in src)
                {
                    sum += (long)val;
                }
                return sum;
            }),
            new LiveScalar<float>(() =>
            {
                float sum = 0F;
                foreach (float val in src)
                {
                    sum += (float)val;
                }
                return sum;
            }))
        { }

        /// <summary>
        /// A sum of floats
        /// </summary>
        /// <param name="src">source floats</param>
        public SumOf(IEnumerable<float> src) : base(
            new LiveScalar<double>(() =>
            {
                double sum = 0D;
                foreach (int val in src)
                {
                    sum += (double)val;
                }
                return sum;
            }),
            new LiveScalar<int>(() =>
            {
                int sum = 0;
                foreach (int val in src)
                {
                    sum += (int)val;
                }
                return sum;
            }),
            new LiveScalar<long>(() =>
            {
                long sum = 0L;
                foreach (long val in src)
                {
                    sum += (long)val;
                }
                return sum;
            }),
            new LiveScalar<float>(() =>
            {
                float sum = 0F;
                foreach (float val in src)
                {
                    sum += (float)val;
                }
                return sum;
            }))
        { }
    }
}
