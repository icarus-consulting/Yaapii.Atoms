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

using System.Collections.Generic;
using Xunit;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public sealed class DivergencyTests
    {
        [Fact]
        public void Empty()
        {
            Assert.Empty(
                new Divergency<string>(
                    new ManyOf<string>("a", "b"),
                    new ManyOf<string>("a", "b")
                )
            );
        }

        [Theory]
        [InlineData(new string[] { "a", "b", "c" }, new string[] { "a", "b", "e" }, new string[] { "c", "e" })]
        [InlineData(new string[] { "a", "b" }, new string[] { "c", "d" }, new string[] { "a", "b", "c", "d" })]
        public void MatchesString(IEnumerable<string> a, IEnumerable<string> b, IEnumerable<string> expected)
        {
            Assert.Equal(
                expected,
                new Divergency<string>(
                   a, b
                )
            );
        }

        [Theory]
        [InlineData(new int[] { 5, 6 }, new int[] { 1, 2 }, new int[] { 5, 6, 1, 2 })]
        [InlineData(new int[] { 1, 2 }, new int[] { 1 }, new int[] { 2 })]
        public void MatchesInt(IEnumerable<int> a, IEnumerable<int> b, IEnumerable<int> expected)
        {
            Assert.Equal(
                expected,
                new Divergency<int>(a, b)
            );
        }
    }
}
