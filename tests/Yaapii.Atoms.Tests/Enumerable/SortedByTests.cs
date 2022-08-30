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

using Xunit;
using Yaapii.Atoms.Misc;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public sealed class SortedByTests
    {
        [Fact]
        public void SortsAnArrayByTextNumber()
        {
            Assert.True(
                new Text.Joined(", ",
                    new SortedBy<string, int>(
                        s => new IntOf(s.Substring(2)).Value(),
                        new ManyOf("nr3", "nr2", "nr10", "nr44", "nr-6", "nr0")
                    )
                ).AsString() == "nr-6, nr0, nr2, nr3, nr10, nr44",
            "Can't sort an enumerable");
        }

        [Fact]
        public void SortsAnArrayWithComparator()
        {
            Assert.True(
                new Text.Joined(", ",
                    new SortedBy<string, int>(
                        s => new IntOf(s.Substring(s.Length-1)).Value(),
                        IReverseCompare<int>.Default,
                        new ManyOf<string>(
                            "a2", "c3", "hello9", "dude6", "Friend7"
                        )
                    )).AsString() == "hello9, Friend7, dude6, c3, a2",
                "Can't sort an enumerable with a custom comparator");
        }

        [Fact]
        public void SortsAnEmptyArray()
        {
            Assert.Empty(
                new SortedBy<int, int>(
                    i => i,
                    new ManyOf<int>()
                ));
        }
    }
}
