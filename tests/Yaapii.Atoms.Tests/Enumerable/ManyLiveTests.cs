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

using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Texts;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public sealed class ManyLiveTest
    {
        [Fact]
        public void ConvertsScalarsToEnumerable()
        {
            Assert.True(
                new LengthOf(
                    new Many.Live<string>(
                        "a", "b", "c"
                    )
                ).Value() == 3,
                "Can't convert scalars to iterable");
        }

        [Fact]
        public void ConvertsObjectsToEnumerable()
        {
            Assert.True(
                new LengthOf(
                    new Many.Live<IText>(
                        new Text.Live("a"), new Text.Live("b"), new Text.Live("c")
                    )
                ).Value() == 3,
            "Can't convert objects to enumerable");
        }

        [Fact]
        public void SensesChanges()
        {
            var lst = new List<string>();
            var length =
                new LengthOf(
                    new Many.Live<string>(() =>
                    {
                        lst.Add("something");
                        return lst;
                    })
                );
            Assert.NotEqual(length.Value(), length.Value());
        }
    }

}
