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

using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Bytes
{
    /// <summary>
    /// Equality for <see cref="IBytes"/>
    /// </summary>
    public sealed class BytesEqual : IScalar<bool>
    {
        private readonly IScalar<bool> equals;

        /// <summary>
        /// Makes a truth about <see cref="IBytes"/> are equal or not.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public BytesEqual(IBytes left, IBytes right)
        {
            equals = new ScalarOf<bool>(() =>
            {
                var leftBytes = left.AsBytes();
                var rightBytes = right.AsBytes();
                var equal = leftBytes.Length == rightBytes.Length;

                for (var i = 0; i < leftBytes.Length && equal; i++)
                {
                    if (leftBytes[i] != rightBytes[i])
                    {
                        equal = false;
                        break;
                    }
                }

                return equal;
            });
        }

        /// <summary>
        /// Equal or not
        /// </summary>
        /// <returns></returns>
        public bool Value()
        {
            return this.equals.Value();
        }
    }
}
