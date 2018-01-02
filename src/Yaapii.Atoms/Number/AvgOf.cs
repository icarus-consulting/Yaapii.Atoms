using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Number
{
    /// <summary>
    /// Average of numbers.
    ///
    /// <para>Here is how you can use it to fine mathematical average of numbers:</para>
    ///
    /// <code>
    /// int sum = new AvgOf(1, 2, 3, 4).intValue();
    /// long sum = new AvgOf(1L, 2L, 3L).longValue();
    /// int sum = new AvgOf(numbers.toArray(new Integer[numbers.size()])).intValue();
    /// </code>
    /// </summary>
    public sealed class AvgOf : NumberEnvelope
    {
        /// <summary>
        /// Average of doubles.
        ///
        /// <para>Here is how you can use it to fine mathematical average of numbers:</para>
        ///
        /// <code>
        /// int sum = new AvgOf(new EnumerableOf&lt;double&gt;(1D, 2D, 3D, 4D)).AsInt();
        /// </code>
        /// </summary>
        /// <param name="src">doubles</param>
        public AvgOf(params double[] src) : this(
            new EnumerableOf<double>(src))
        { }

        /// <summary>
        /// Average of integers.
        ///
        /// <para>Here is how you can use it to fine mathematical average of numbers:</para>
        ///
        /// <code>
        /// int sum = new AvgOf(new EnumerableOf&lt;int&gt;(1, 2, 3, 4)).AsInt();
        /// </code>
        /// </summary>
        /// <param name="src">integers</param>
        public AvgOf(params int[] src) : this(
            new EnumerableOf<int>(src))
        { }

        /// <summary>
        /// Average of longs.
        ///
        /// <para>Here is how you can use it to fine mathematical average of numbers:</para>
        ///
        /// <code>
        /// int sum = new AvgOf(new EnumerableOf&lt;long&gt;(1, 2, 3, 4)).AsInt();
        /// </code>
        /// </summary>
        /// <param name="src"></param>
        public AvgOf(params long[] src) : this(
            new EnumerableOf<long>(src))
        { }

        /// <summary>
        /// Average of floats.
        ///
        /// <para>Here is how you can use it to fine mathematical average of numbers:</para>
        ///
        /// <code>
        /// int sum = new AvgOf(new EnumerableOf&lt;float&gt;(1F, 2F, 3F, 4F)).AsInt();
        /// </code>
        /// </summary>
        /// <param name="src">floats</param>
        public AvgOf(params float[] src) : this(
            new EnumerableOf<float>(src))
        { }

        /// <summary>
        /// Average of doubles.
        ///
        /// <para>Here is how you can use it to fine mathematical average of numbers:</para>
        ///
        /// <code>
        /// int sum = new AvgOf(new EnumerableOf&lt;double&gt;(1D, 2D, 3D, 4D)).AsInt();
        /// </code>
        /// </summary>
        /// <param name="src"></param>
        public AvgOf(IEnumerable<double> src) : base(
            new ScalarOf<double>(() =>
            {
                double sum = 0D;
                double total = 0D;
                foreach (double val in src)
                {
                    sum += (double)val;
                    ++total;
                }
                if (total == 0D)
                {
                    total = 1D;
                }
                return sum / total;
            }),
            new ScalarOf<int>(() =>
            {
                int sum = 0;
                int total = 0;
                foreach (int val in src)
                {
                    sum += (int)val;
                    ++total;
                }
                if (total == 0)
                {
                    total = 1;
                }
                return sum / total;
            }),
            new ScalarOf<long>(() =>
            {
                long sum = 0L;
                long total = 0L;
                foreach (long val in src)
                {
                    sum += (long)val;
                    ++total;
                }
                if (total == 0)
                {
                    total = 1L;
                }
                return sum / total;
            }),
            new ScalarOf<float>(() =>
            {
                float sum = 0F;
                float total = 0F;
                foreach (float val in src)
                {
                    sum += (float)val;
                    ++total;
                }
                if (total == 0)
                {
                    total = 1F;
                }
                return sum / total;
            })
            )
        { }

        /// <summary>
        /// <summary>
        /// Average of integers.
        ///
        /// <para>Here is how you can use it to fine mathematical average of numbers:</para>
        ///
        /// <code>
        /// int sum = new AvgOf(new EnumerableOf&lt;int&gt;(1, 2, 3, 4)).AsInt();
        /// </code>
        /// </summary>
        /// <param name="src"></param>
        public AvgOf(IEnumerable<int> src) : base(
            new ScalarOf<double>(() =>
            {
                double sum = 0D;
                double total = 0D;
                foreach (double val in src)
                {
                    sum += (double)val;
                    ++total;
                }
                if (total == 0D)
                {
                    total = 1D;
                }
                return sum / total;
            }),
            new ScalarOf<int>(() =>
            {
                int sum = 0;
                int total = 0;
                foreach (int val in src)
                {
                    sum += (int)val;
                    ++total;
                }
                if (total == 0)
                {
                    total = 1;
                }
                return sum / total;
            }),
            new ScalarOf<long>(() =>
            {
                long sum = 0L;
                long total = 0L;
                foreach (long val in src)
                {
                    sum += (long)val;
                    ++total;
                }
                if (total == 0)
                {
                    total = 1L;
                }
                return sum / total;
            }),
            new ScalarOf<float>(() =>
            {
                float sum = 0F;
                float total = 0F;
                foreach (float val in src)
                {
                    sum += (float)val;
                    ++total;
                }
                if (total == 0)
                {
                    total = 1F;
                }
                return sum / total;
            })
            )
        { }

        /// <summary>
        /// <summary>
        /// Average of integers.
        ///
        /// <para>Here is how you can use it to fine mathematical average of numbers:</para>
        ///
        /// <code>
        /// int sum = new AvgOf(new EnumerableOf&lt;long&gt;(1L, 2L, 3L, 4L)).AsInt();
        /// </code>
        /// </summary>
        /// <param name="src"></param>
        public AvgOf(IEnumerable<long> src) : base(
            new ScalarOf<double>(() =>
            {
                double sum = 0D;
                double total = 0D;
                foreach (double val in src)
                {
                    sum += (double)val;
                    ++total;
                }
                if (total == 0D)
                {
                    total = 1D;
                }
                return sum / total;
            }),
            new ScalarOf<int>(() =>
            {
                int sum = 0;
                int total = 0;
                foreach (int val in src)
                {
                    sum += (int)val;
                    ++total;
                }
                if (total == 0)
                {
                    total = 1;
                }
                return sum / total;
            }),
            new ScalarOf<long>(() =>
            {
                long sum = 0L;
                long total = 0L;
                foreach (long val in src)
                {
                    sum += (long)val;
                    ++total;
                }
                if (total == 0)
                {
                    total = 1L;
                }
                return sum / total;
            }),
            new ScalarOf<float>(() =>
            {
                float sum = 0F;
                float total = 0F;
                foreach (float val in src)
                {
                    sum += (float)val;
                    ++total;
                }
                if (total == 0)
                {
                    total = 1F;
                }
                return sum / total;
            })
            )
        { }

        /// <summary>
        /// Average of integers.
        ///
        /// <para>Here is how you can use it to fine mathematical average of numbers:</para>
        ///
        /// <code>
        /// long sum = new AvgOf(new EnumerableOf&lt;float&gt;(1F, 2F, 3F, 4F)).AsLong();
        /// </code>
        /// </summary>
        /// <param name="src"></param>
        public AvgOf(IEnumerable<float> src) : base(
            new ScalarOf<double>(() =>
            {
                double sum = 0D;
                double total = 0D;
                foreach (double val in src)
                {
                    sum += (double)val;
                    ++total;
                }
                if (total == 0D)
                {
                    total = 1D;
                }
                return sum / total;
            }),
            new ScalarOf<int>(() =>
            {
                int sum = 0;
                int total = 0;
                foreach (int val in src)
                {
                    sum += (int)val;
                    ++total;
                }
                if (total == 0)
                {
                    total = 1;
                }
                return sum / total;
            }),
            new ScalarOf<long>(() =>
            {
                long sum = 0L;
                long total = 0L;
                foreach (long val in src)
                {
                    sum += (long)val;
                    ++total;
                }
                if (total == 0)
                {
                    total = 1L;
                }
                return sum / total;
            }),
            new ScalarOf<float>(() =>
            {
                float sum = 0F;
                float total = 0F;
                foreach (float val in src)
                {
                    sum += (float)val;
                    ++total;
                }
                if (total == 0)
                {
                    total = 1F;
                }
                return sum / total;
            })
            )
        { }
    }
}
