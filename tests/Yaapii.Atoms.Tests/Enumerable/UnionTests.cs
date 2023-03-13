// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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
using System.IO;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public sealed class UnionTests
    {
        [Fact]
        public void Empty()
        {
            Assert.Empty(
                new Union<string>(
                    new ManyOf<string>("a", "b"),
                    new ManyOf<string>("c")
                )
            );
        }

        [Theory]
        [InlineData(new string[] { "a", "b" }, new string[] { "a", "b" }, new string[] { "a", "b" })]
        [InlineData(new string[] { "a", "b" }, new string[] { "a" }, new string[] { "a" })]
        public void MatchesString(IEnumerable<string> a, IEnumerable<string> b, IEnumerable<string> expected)
        {
            Assert.Equal(
                expected,
                new Union<string>(
                   a, b
                )
            );
        }

        [Theory]
        [InlineData(new int[] { 1, 2 }, new int[] { 1, 2 }, new int[] { 1, 2 })]
        [InlineData(new int[] { 1, 2 }, new int[] { 1 }, new int[] { 1 })]
        public void MatchesInt(IEnumerable<int> a, IEnumerable<int> b, IEnumerable<int> expected)
        {
            Assert.Equal(
                expected,
                new Union<int>(
                   a, b
                )
            );
        }

        [Fact]
        public void UsesCompareFunction()
        {
            Assert.Equal(
                new ListOf<string>("c:/abraham/a.jpg", "c:/caesar/c.jpg"),
                new Union<string>(
                    new ListOf<string>("c:/abraham/a.jpg", "c:/bertram/b.jpg", "c:/caesar/c.jpg"),
                    new ListOf<string>("a", "c"),
                    (aItem, bItem) =>
                        new Equals<string>(
                            Path.GetFileNameWithoutExtension(aItem),
                            bItem
                        ).Value()
                )
            );
        }
    }
}
