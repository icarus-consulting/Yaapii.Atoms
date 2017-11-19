// MIT License
//
// Copyright(c) 2017 ICARUS Consulting GmbH
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

namespace Yaapii.Atoms.List
{
    /// <summary>
    /// sums all given reals
    /// </summary>
    public sealed class SumOfReals : IScalar<Double>
    {
        private readonly IEnumerable<IScalar<Double>> src;

        /// <summary>
        /// Sum of all given numbers.
        /// </summary>
        /// <param name="src">doubles to sum</param>
        public SumOfReals(params Double[] src) : this(
            new Mapped<Double, IScalar<Double>>(
                src,
                d => new ScalarOf<Double>(d)
                ))
        { }

        /// <summary>
        /// Sum of all given numbers.
        /// </summary>
        /// <param name="src">double to sum</param>
        public SumOfReals(IEnumerable<IScalar<Double>> src)
        {
            this.src = src;
        }

        /// <summary>
        /// Get the sum.
        /// </summary>
        /// <returns>the sum</returns>
        public Double Value()
        {
            IEnumerator<IScalar<Double>> numbers = this.src.GetEnumerator();
            Double result = 0.0;
            while (numbers.MoveNext())
            {
                result += numbers.Current.Value();
            }
            return result;
        }
    }
}
