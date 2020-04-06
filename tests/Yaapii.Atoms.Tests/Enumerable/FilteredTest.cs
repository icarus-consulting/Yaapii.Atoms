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

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Lists;
using Yaapii.Atoms.Func;
using System.Linq;
using Yaapii.Atoms.Tests;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public sealed class FilteredTests
    {
        [Fact]
        public void Filters()
        {
            Assert.True(
                new LengthOf(
                    new Filtered<string>(
                       (input) => input != "B",
                       new List<string>() { "A", "B", "C" })).Value() == 2,
                "cannot filter items");
        }

        [Fact]
        public void FiltersEmptyList()
        {
            Assert.True(
                new LengthOf(
                    new Filtered<string>(

                        input => input.Length > 1,
                        new ManyOf<String>())
                    ).Value() == 0,
                "cannot filter empty enumerable");
        }

        [Fact]
        public void PerformanceMatchesLinQ()
        {
            Func<string, bool> filter = (input) => input != "B";

            var linq = new ElapsedTime(() => new List<string>() { "A", "B", "C" }.Where(filter)).AsTimeSpan();
            var atoms =
                new ElapsedTime(
                    () => new Filtered<string>(
                        filter,
                        new List<string>() { "A", "B", "C" }
                            )).AsTimeSpan();

            Assert.True((linq - atoms).Duration().Milliseconds < 10);
        }

        [Fact]
        public void FilterItemsGivenByParamsCtor()
        {
            Assert.True(
                new LengthOf(
                    new Filtered<string>(
                       (input) => input != "B",
                       "A", "B", "C")).Value() == 2,
                "cannot filter items");
        }
    }
}
