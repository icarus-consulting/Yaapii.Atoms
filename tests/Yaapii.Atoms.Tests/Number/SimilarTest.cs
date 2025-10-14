// MIT License
//
// Copyright(c) 2025 ICARUS Consulting GmbH
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

using Xunit;

namespace Yaapii.Atoms.Number.Tests
{
    public sealed class SimilarTest
    {
        [Fact]
        public void IsSimilarSame()
        {
            INumber first =
                new NumberOf(13.37);
            INumber second =
                new NumberOf(13.37);

            Assert.True(
                new Similar(first, second).Value()
                );
        }

        [Fact]
        public void IsSimilarNoDecimal()
        {
            INumber first =
                new NumberOf(13);
            INumber second =
                new NumberOf(13);

            Assert.True(
                new Similar(first, second, 10).Value()
                );
        }

        [Fact]
        public void IsSimilarOneDecimal()
        {
            INumber first =
                new NumberOf(13.37);
            INumber second =
                new NumberOf(13.3);

            Assert.True(
                new Similar(first, second, 1).Value()
                );
        }

        [Fact]
        public void IsSimilarOnlyOneDecimal()
        {
            INumber first =
                new NumberOf(13.37777);
            INumber second =
                new NumberOf(13.33333);

            Assert.True(
                new Similar(first, second, 1).Value()
                );
        }

        [Fact]
        public void IsSimilarFiveDecimal()
        {
            INumber first =
                new NumberOf(13.333337);
            INumber second =
                new NumberOf(13.333339);

            Assert.True(
                new Similar(first, second, 5).Value()
                );
        }

        [Fact]
        public void IsSimilar10Decimal()
        {
            INumber first =
                new NumberOf(13.33333333337);
            INumber second =
                new NumberOf(13.33333333339);

            Assert.True(
                new Similar(first, second, 10).Value()
                );
        }

        [Fact]
        public void IsNotSimilar()
        {
            INumber first =
                new NumberOf(13.37);
            INumber second =
                new NumberOf(13.39);

            Assert.False(
                new Similar(first, second, 2).Value()
                );
        }
    }
}
