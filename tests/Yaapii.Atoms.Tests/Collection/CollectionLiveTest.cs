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

using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class LiveTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            var col = new Collection.Live<int>(1, 2, 0, -1);

            Assert.True(col.Contains(1) && col.Contains(2) && col.Contains(0) && col.Contains(-1));
        }

        [Fact]
        public void BuildsCollection()
        {
            Assert.Contains(
                -1,
                new Collection.Live<int>(1, 2, 0, -1)
            );
        }

        [Fact]
        public void BuildsCollectionFromIterator()
        {
            Assert.Contains(
                -1,
                new Collection.Live<int>(
                    new Many.Of<int>(1, 2, 0, -1).GetEnumerator())
            );
        }

        [Fact]
        public void SensesChanges()
        {
            var count = 1;
            var col =
                new Collection.Live<int>(
                    new Many.Live<int>(() =>
                        new Repeated<int>(
                            () =>
                            {
                                count++;
                                return 0;
                            },
                            count
                        )
                    )
                );
            Assert.NotEqual(col.Count, col.Count);
        }
    }
}
