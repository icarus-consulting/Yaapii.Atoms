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
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Number
{
    /// <summary>
    /// Checks if two Numbers are similar with a given accuracy
    /// </summary>
    public sealed class Similar : IScalar<Boolean>
    {
        private readonly IScalar<Boolean> isSimilar;

        /// <summary>
        /// Checks if two Numbers are similar with a given accuracy
        /// </summary>
        /// <param name="first">First Number</param>
        /// <param name="second">Second Number</param>
        public Similar(INumber first, INumber second) : this(first, second, 0)
        { }


        /// <summary>
        /// Checks if two Numbers are similar with a given accuracy
        /// </summary>
        /// <param name="first">First Number</param>
        /// <param name="second">Second Number</param>
        /// <param name="accuracy">Number of equal decimal places</param>
        public Similar(INumber first, INumber second, int accuracy)
        {
            this.isSimilar =
                new ScalarOf<Boolean>(() =>
                {
                    bool isSimilar;
                    if (accuracy > 0)
                    {
                        isSimilar =
                            Math.Abs(
                                first.AsDouble() - second.AsDouble()
                            ) <
                            Math.Pow(
                                10, -1 * accuracy
                            );
                    }
                    else
                    {
                        isSimilar =
                            Math.Abs(
                                    first.AsDouble() - second.AsDouble()
                                ) == 0;
                    }


                    return isSimilar;

                });
        }

        public bool Value()
        {
            return this.isSimilar.Value();
        }
    }
}
