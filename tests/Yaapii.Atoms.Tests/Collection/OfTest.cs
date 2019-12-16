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

using System.Threading;
using Xunit;
using Yaapii.Atoms.Fail;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class OfTest
    {
        [Fact]
        public void BehavesAsCollection()
        {
            Assert.Contains(
                -1,
                new Collection.Of<int>(
                    new ListOf<int>(1, 2, 0, -1)));
        }

        [Fact]
        public void IgnoresChangesInIterable()
        {
            int size = 2;
            var list =
                new Collection.Of<int>(
                    new ListOf<int>(
                        new Enumerable.Repeated<int>(
                            new ScalarOf<int>(() => 0),
                            new ScalarOf<int>(() =>
                            {
                                Interlocked.Increment(ref size);
                                return size;
                            })
                        )));

        }

        [Fact]
        public void DecoratesArray()
        {
            Assert.Equal(
                2,
                new Collection.Of<int>(-1, 0).Count);
        }

        [Fact]
        public void Empty()
        {
            Assert.Empty(
                new Collection.Of<int>());
        }

        [Fact]
        public void Contains()
        {
            Assert.Contains(
                2,
                new Collection.Of<int>(1, 2)
            );
        }

        [Fact]
        public void RejectsAdd()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Collection.Of<int>(1, 2).Add(1));
        }

        [Fact]
        public void RejectsRemove()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Collection.Of<int>(1, 2).Remove(1));
        }


        [Fact]
        public void RejectsClear()
        {
            Assert.Throws<UnsupportedOperationException>(() =>
                new Collection.Of<int>(1, 2).Clear());
        }

    }
}
