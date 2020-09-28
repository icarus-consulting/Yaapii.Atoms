// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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
    /// The maximum of the given numbers
    /// </summary>
    public sealed class MaxOf : NumberEnvelope
    {
        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(params int[] src) : this(
            new ManyOf<int>(src))
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(IEnumerable<int> src) : base(
            new ScalarOf<double>(() =>
            {
                var max = double.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            }),
            new ScalarOf<int>(() =>
            {
                var max = int.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            }),
            new ScalarOf<long>(() =>
            {
                var max = long.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            }),
            new ScalarOf<float>(() =>
            {
                var max = float.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            })
            )
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(params double[] src) : this(
            new ManyOf<double>(src))
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(IEnumerable<double> src) : base(
            new ScalarOf<double>(() =>
            {
                var max = double.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            }),
            new ScalarOf<int>(() =>
            {
                var max = int.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (int)e.Current;
                }
                return max;
            }),
            new ScalarOf<long>(() =>
            {
                var max = long.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (long)e.Current;
                }
                return max;
            }),
            new ScalarOf<float>(() =>
            {
                var max = float.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (float)e.Current;
                }
                return max;
            })
        )
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(params long[] src) : this(
            new ManyOf<long>(src))
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(IEnumerable<long> src) : base(
            new ScalarOf<double>(() =>
            {
                var max = double.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            }),
            new ScalarOf<int>(() =>
            {
                var max = int.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (int)e.Current;
                }
                return max;
            }),
            new ScalarOf<long>(() =>
            {
                var max = long.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (long)e.Current;
                }
                return max;
            }),
            new ScalarOf<float>(() =>
            {
                var max = float.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (float)e.Current;
                }
                return max;
            })
        )
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(params float[] src) : this(
            new ManyOf<float>(src))
        { }

        /// <summary>
        /// The maximum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MaxOf(IEnumerable<float> src) : base(
            new ScalarOf<double>(() =>
            {
                var max = double.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = e.Current;
                }
                return max;
            }),
            new ScalarOf<int>(() =>
            {
                var max = int.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (int)e.Current;
                }
                return max;
            }),
            new ScalarOf<long>(() =>
            {
                var max = long.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (long)e.Current;
                }
                return max;
            }),
            new ScalarOf<float>(() =>
            {
                var max = float.MinValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current > max) max = (float)e.Current;
                }
                return max;
            })
        )
        { }
    }
}
