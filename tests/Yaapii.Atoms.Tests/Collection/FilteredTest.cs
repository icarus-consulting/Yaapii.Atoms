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

using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class FilteredTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            var col =
                new Filtered<int>(i => i < 2, 1, 2, 0, -1);
            Assert.True(col.Contains(1) && col.Contains(-1));
        }

        [Fact]
        public void FiltersList()
        {
            Assert.Equal(
                2,
                new LengthOf(
                    new Filtered<string>(
                        input => input.Length > 4,
                        new ManyOf<string>("hello", "world", "друг"))
                ).Value()
            );
        }

        [Fact]
        public void FiltersEmptyList()
        {
            var col =
                new Filtered<string>(
                    input => input.Length > 4,
                    new List<string>()
                );
            Assert.Empty(col);
        }

        [Fact]
        public void Size()
        {
            Assert.Equal(
                2,
                new Filtered<string>(
                    input => input.Length >= 4,
                    new ManyOf<string>("some", "text", "yes")
                ).Count
            );
        }

        [Fact]
        public void WithItemsNotEmpty()
        {
            Assert.NotEmpty(
                new Filtered<string>(
                    input => input.Length > 4,
                    new ManyOf<string>("first", "second")
                )
            );
        }

        [Fact]
        public void WithoutItemsIsEmpty()
        {
            Assert.Empty(
                new Filtered<string>(
                    input => input.Length > 16,
                    new ManyOf<string>("third", "fourth")
                )
            );
        }
    }
}
