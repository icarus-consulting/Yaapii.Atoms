/// MIT License
///
/// Copyright(c) 2017 ICARUS Consulting GmbH
///
/// Permission is hereby granted, free of charge, to any person obtaining a copy
/// of this software and associated documentation files (the "Software"), to deal
/// in the Software without restriction, including without limitation the rights
/// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
/// copies of the Software, and to permit persons to whom the Software is
/// furnished to do so, subject to the following conditions:
///
/// The above copyright notice and this permission notice shall be included in all
/// copies or substantial portions of the Software.
///
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
/// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
/// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
/// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
/// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
/// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Fail;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class SkippedEnumeratorTest
    {
        [Fact]
        public void SkipEnumerator()
        {
            var skipped =
                new List<string>(
                    new EnumerableOf<string>(
                        new SkippedEnumerator<string>(
                            new EnumerableOf<string>(
                                "one", "two", "three", "four"
                            ).GetEnumerator(),
                        2)));

            Assert.True(new LengthOfEnumerator<string>(skipped.GetEnumerator()).Value() == 2, "cannot skip elements");
            Assert.False(skipped.Contains("one"), "cannot skip elements");
            Assert.False(skipped.Contains("two"), "cannot skip elements");
            Assert.True(skipped.Contains("three"), "cannot skip elements");
            Assert.True(skipped.Contains("four"), "cannot skip elements");
        }

        [Fact]
        public void CannotSkippedMoreThanExists()
        {
            Assert.False(
                new SkippedEnumerator<string>(
                    new EnumerableOf<string>(
                        "one", "two"
                    ).GetEnumerator(),
                    2
                ).MoveNext(),
                "enumerates more elements than exist");
        }
    }
}
