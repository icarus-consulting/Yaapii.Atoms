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
using Xunit;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Misc;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.List.Tests
{
    public sealed class SortedTest
    {
        [Fact]
        public void SortsAnArray()
        {
            Assert.True(
                new JoinedText(", ",
                    new Enumerable.Mapped<int, string>(
                        i => i.ToString(),
                        new Sorted<int>(
                            new EnumerableOf<int>(3, 2, 10, 44, -6, 0)
                        )
                    )
                ).AsString() == "-6, 0, 2, 3, 10, 44",
            "Can't sort an enumerable");
        }

        [Fact]
        public void SortsAnArrayWithComparator()
        {
            Assert.True(
                new JoinedText(", ",
                    new Sorted<string>(
                        IReverseComparer<string>.Default,
                        new EnumerableOf<string>(
                            "a", "c", "hello", "dude", "Friend"
                        )
                    )).AsString() == "hello, Friend, dude, c, a",
                "Can't sort an enumerable with a custom comparator");
        }

        [Fact]
        public void SortsAnEmptyArray()
        {
            Assert.Empty(
                new Sorted<int>(
                    new EnumerableOf<int>()
                ));
        }
    }
}
