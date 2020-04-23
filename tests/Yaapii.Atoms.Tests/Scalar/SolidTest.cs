// MIT License
//
// Copyright(c) 2020 ICARUS Consulting GmbH
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
using System.Threading.Tasks;
using Xunit;
using Yaapii.Atoms.Lists;

namespace Yaapii.Atoms.Scalar.Tests
{
    public sealed class SolidTest
    {
        [Fact]
        public void CachesResult()
        {
            var check = 0;
            var sc = new Solid<int>(() => check += 1);
            var max = Environment.ProcessorCount << 8;
            Parallel.For(0, max, (nr) => sc.Value());

            Assert.Equal(sc.Value(), sc.Value());
        }

        [Fact]
        public void WorksInMultipleThreads()
        {
            var sc = new Solid<IList<int>>(() => new ListOf<int>(1, 2));
            var max = Environment.ProcessorCount << 8;
            Parallel.For(0, max, (nr) => sc.Value());

            Assert.Equal(
                sc.Value(), sc.Value()
            );
        }
    }
}
