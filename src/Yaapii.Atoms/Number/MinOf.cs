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
            new EnumerableOf<int>(src))
        { }

        /// <summary>
        /// The minimum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MinOf(IEnumerable<int> src) : base(
            new ScalarOf<double>(() =>
            {
                var min = double.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            }),
            new ScalarOf<int>(() =>
            {
                var min = int.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            }),
            new ScalarOf<long>(() =>
            {
                var min = long.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            }),
            new ScalarOf<float>(() =>
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
            new EnumerableOf<double>(src))
        { }

        /// <summary>
        /// The minimum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MinOf(IEnumerable<double> src) : base(
            new ScalarOf<double>(() =>
            {
                var min = double.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            }),
            new ScalarOf<int>(() =>
            {
                var min = int.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (int)e.Current;
                }
                return min;
            }),
            new ScalarOf<long>(() =>
            {
                var min = long.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (long)e.Current;
                }
                return min;
            }),
            new ScalarOf<float>(() =>
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
            new EnumerableOf<long>(src))
        { }

        /// <summary>
        /// The minimum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MinOf(IEnumerable<long> src) : base(
            new ScalarOf<double>(() =>
            {
                var min = double.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            }),
            new ScalarOf<int>(() =>
            {
                var min = int.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (int)e.Current;
                }
                return min;
            }),
            new ScalarOf<long>(() =>
            {
                var min = long.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (long)e.Current;
                }
                return min;
            }),
            new ScalarOf<float>(() =>
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
            new EnumerableOf<float>(src))
        { }

        /// <summary>
        /// The minimum of the source integers
        /// </summary>
        /// <param name="src">integers to find max in</param>
        public MinOf(IEnumerable<float> src) : base(
            new ScalarOf<double>(() =>
            {
                var min = double.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = e.Current;
                }
                return min;
            }),
            new ScalarOf<int>(() =>
            {
                var min = int.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (int)e.Current;
                }
                return min;
            }),
            new ScalarOf<long>(() =>
            {
                var min = long.MaxValue;
                var e = src.GetEnumerator();
                while (e.MoveNext())
                {
                    if (e.Current < min) min = (long)e.Current;
                }
                return min;
            }),
            new ScalarOf<float>(() =>
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
