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
    /// The minimum of the given numbers
    /// </summary>
    public sealed class MinOf : NumberEnvelope
    {
        /// <summary>
        /// The minimum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MinOf(params int[] src) : this(
            new Many.Of<int>(src))
        { }

        /// <summary>
        /// The minimum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MinOf(IEnumerable<int> src) : base(
            new LiveScalar<double>(() =>
            {
                var min = double.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            }),
            new LiveScalar<int>(() =>
            {
                var min = int.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            }),
            new LiveScalar<long>(() =>
            {
                var min = long.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            }),
            new LiveScalar<float>(() =>
            {
                var min = float.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            })
            )
        { }

        /// <summary>
        /// The minimum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MinOf(params double[] src) : this(
            new Many.Of<double>(src))
        { }

        /// <summary>
        /// The minimum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MinOf(IEnumerable<double> src) : base(
            new LiveScalar<double>(() =>
            {
                var min = double.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            }),
            new LiveScalar<int>(() =>
            {
                var min = int.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (int)e.Current;
                }
                return min;
            }),
            new LiveScalar<long>(() =>
            {
                var min = long.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (long)e.Current;
                }
                return min;
            }),
            new LiveScalar<float>(() =>
            {
                var min = float.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (float)e.Current;
                }
                return min;
            })
        )
        { }

        /// <summary>
        /// The minimum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MinOf(params long[] src) : this(
            new Many.Of<long>(src))
        { }

        /// <summary>
        /// The minimum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MinOf(IEnumerable<long> src) : base(
            new LiveScalar<double>(() =>
            {
                var min = double.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            }),
            new LiveScalar<int>(() =>
            {
                var min = int.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (int)e.Current;
                }
                return min;
            }),
            new LiveScalar<long>(() =>
            {
                var min = long.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (long)e.Current;
                }
                return min;
            }),
            new LiveScalar<float>(() =>
            {
                var min = float.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (float)e.Current;
                }
                return min;
            })
        )
        { }

        /// <summary>
        /// The minimum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MinOf(params float[] src) : this(
            new Many.Of<float>(src))
        { }

        /// <summary>
        /// The minimum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MinOf(IEnumerable<float> src) : base(
            new LiveScalar<double>(() =>
            {
                var min = double.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            }),
            new LiveScalar<int>(() =>
            {
                var min = int.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (int)e.Current;
                }
                return min;
            }),
            new LiveScalar<long>(() =>
            {
                var min = long.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (long)e.Current;
                }
                return min;
            }),
            new LiveScalar<float>(() =>
            {
                var min = float.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (float)e.Current;
                }
                return min;
            })
        )
        { }
    }
}
