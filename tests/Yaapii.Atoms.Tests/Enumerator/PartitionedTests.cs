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
using Xunit;

namespace Yaapii.Atoms.Enumerator
{
    public class PartitionedTests
    {
        [Fact]
        public void CountsPartititions()
        {
            IEnumerator<int> enumerator =
                new List<int>()
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10
                }.GetEnumerator();

            var partition =
                new Partitioned<int>(
                    3,
                    enumerator
                );

            partition.MoveNext();
            partition.MoveNext();
            partition.MoveNext();
            partition.MoveNext();

            Assert.Equal(
                new List<int>() { 10 },
                partition.Current
            );
        }


        [Fact]
        public void Partionate()
        {
            IEnumerator<string> enumerator =
                new List<string>()
                {
                    "Christian",
                    "Sauer"
                }.GetEnumerator();

            var partition =
                new Partitioned<string>(
                    1,
                    enumerator
                );

            partition.MoveNext();
            Assert.Equal(
                new List<string>() { "Christian" },
                partition.Current
            );
        }

        [Fact]
        public void ThrowsExceptionOnInvalidSize()
        {
            IEnumerator<string> enumerator = new List<string>() { }.GetEnumerator();

            Assert.Throws<ArgumentException>(() =>
                new Partitioned<string>(
                    0,
                    enumerator
                ).MoveNext()
            );
        }

        [Fact]
        public void CurrentThrowsException()
        {
            IEnumerator<string> enumerator = new List<string>() { }.GetEnumerator();

            Assert.Throws<InvalidOperationException>(() =>
                new Partitioned<string>(
                    2,
                    enumerator
                ).Current
            );
        }

        [Fact]
        public void FailsOnEnd()
        {
            IEnumerator<int> enumerator =
                new List<int>()
                {
                    1,
                    2,
                    3,
                    4,
                    5
                }.GetEnumerator();

            var partition =
                new Partitioned<int>(
                    5,
                    enumerator
                );

            partition.MoveNext();
            partition.MoveNext();

            Assert.Throws<InvalidOperationException>(() =>
            partition.Current
            );
        }
    }
}
