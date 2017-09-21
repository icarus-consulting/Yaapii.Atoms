using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// Sum of all given numbers.
    /// </summary>
    public sealed class SumOfInts : IScalar<long>
    {
        private readonly IEnumerable<IScalar<Int32>> src;

        /// <summary>
        /// Sum of all given numbers.
        /// </summary>
        /// <param name="src"></param>
        public SumOfInts(params Int32[] src) : this(
            new Mapped<Int32, IScalar<Int32>>(src, i => new ScalarOf<Int32>(i)))
        { }

        /// <summary>
        /// Sum of all given numbers.
        /// </summary>
        /// <param name="src">list of numbers to sum</param>
        public SumOfInts(IEnumerable<Int32> src) : this(
            new Mapped<Int32, IScalar<Int32>>(src, i => new ScalarOf<Int32>(i)))
        { }

        /// <summary>
        /// Sum of all given numbers.
        /// </summary>
        /// <param name="src">scalar of numbers to sum</param>
        public SumOfInts(IEnumerable<IScalar<Int32>> src)
        {
            this.src = src;
        }

        public long Value()
        {
            IEnumerator<IScalar<Int32>> numbers = this.src.GetEnumerator();
            long result = 0L;
            while (numbers.MoveNext())
            {
                result += numbers.Current.Value();
            }
            return result;
        }
    }
}
