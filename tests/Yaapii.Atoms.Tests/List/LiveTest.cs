﻿// MIT License
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
using System.Threading;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Yaapii.Atoms.Lists.Tests
{
    public sealed class LiveTest
    {
        [Fact]
        public void BehavesAsList()
        {
            Assert.True(
                new List.Live<int>(1, 2).Contains(2),
                "Can't behave as a list");
        }

        [Fact]
        public void ElementAtIndexTest()
        {
            int num = 345;

            Assert.True(
                new List.Live<int>(-1, num, 0, 1)[1] == num,
                "Can't convert an iterable to a list");
        }

        [Fact]
        public void EmptyTest()
        {
            Assert.True(
            new List.Live<int>(
                new List<int>()).Count == 0,
            "Can't convert an empty iterable to an empty list");
        }

        [Fact]
        public void LowBoundTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => 
                new List.Live<int>(
                    new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
                    [-1]);
        }

        [Fact]
        public void HighBoundTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () =>
                new List.Live<int>(
                    new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 })
                        [11]);
        }

        [Fact]
        public void SensesChangesInIterable()
        {
            int size = 2;
            var list =
                new List.Live<int>(
                    new Yaapii.Atoms.Enumerable.HeadOf<int>(
                        new Yaapii.Atoms.Enumerable.Endless<int>(1),
                        new ScalarOf<int>(() => Interlocked.Increment(ref size))
                ));

            Assert.Equal(3, new Enumerable.LengthOf(list).Value());
            Assert.Equal(4, new Enumerable.LengthOf(list).Value());
        }

    }
}
