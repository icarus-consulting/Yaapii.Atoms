// MIT License
//
// Copyright(c) 2023 ICARUS Consulting GmbH
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
using Yaapii.Atoms.Func;

namespace Yaapii.Atoms.Func.Tests
{
    public sealed class EachTests
    {
        [Fact]
        public void IncreasesOne()
        {
            List<int> lst = new List<int>() { 2, 1, 0 };

            new Each<int>(
                (i) => lst[i] = i,
                0, 1, 2
            ).Invoke();

            Assert.True(
                lst[0] == 0 &&
                lst[2] == 2
            );
        }

        [Fact]
        public void TestProc()
        {
            var list = new LinkedList<int>();
            new Each<int>(
                new ActionOf<int>(i => list.AddLast(i)),
                1, 1
            ).Invoke();

            Assert.True(
                list.Count == 2);
        }
    }
}
