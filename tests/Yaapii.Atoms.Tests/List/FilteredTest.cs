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
using Yaapii.Atoms.List;
using Yaapii.Atoms.Func;
using System.Linq;

namespace Yaapii.Atoms.Tests.List
{
    public sealed class FilteredTests
    {
        [Fact]
        public void Filters()
        {
            Assert.True(
                new LengthOf<string>(
                    new Filtered<string>(
                        new List<string>() { "A", "B", "C" },
                            (input) => input != "B")).Value() == 2,
                "cannot filter items");
        }

        [Fact]
        public void FiltersEmptyList()
        {
            Assert.True(
                new LengthOf<string>(
                    new Filtered<string>(
                        new EnumerableOf<String>(),
                        input => input.Length > 1)
                    ).Value() == 0,
                "cannot filter empty enumerable");
        }

        [Fact]
        public void PerformanceMatchesLinQ()
        {
            Func<string,bool> filter = (input) => input != "B";

            var linq = new ElapsedTime(() => new List<string>() { "A", "B", "C" }.Where(filter)).AsTimeSpan();
            var atoms =
                new ElapsedTime(
                    () => new Filtered<string>(
                        new List<string>() { "A", "B", "C" },
                            filter)).AsTimeSpan();

            Assert.True((linq - atoms).Duration().Milliseconds < 10);
        }
    }
}
