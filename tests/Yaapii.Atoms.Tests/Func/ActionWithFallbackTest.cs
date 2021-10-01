// MIT License
//
// Copyright(c) 2021 ICARUS Consulting GmbH
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
using Xunit;

namespace Yaapii.Atoms.Func.Tests
{
    public sealed class ActionWithFallbackTest
    {
        [Fact]
        public void UsesParameterlessMainFunc()
        {
            int i = 0;
            new ActionWithFallback(
                    () => { i = 1; },
                    ex => { i = 2; }
                ).Invoke();

            Assert.True(
                i == 1,
                "Cannot use main action");
        }

        [Fact]
        public void UsesParameterlessFallbackFunc()
        {
            int i = 0;
            new ActionWithFallback(
                    () => { throw new Exception(); },
                    ex => { i = 2; }
                ).Invoke();

            Assert.True(
                i == 2,
                "Cannot use fallback action");
        }

        [Fact]
        public void UsesMainFunc()
        {
            int i = 0;
            new ActionWithFallback<int>(
                    (val) => { i = val; },
                    ex => { i = 2; }
                ).Invoke(1);

            Assert.True(
                i == 1,
                "Cannot use main action");
        }

        [Fact]
        public void UsesFallbackFunc()
        {
            int i = 0;
            new ActionWithFallback<int>(
                    (val) => { throw new Exception(); },
                    ex => { i = 2; }
                ).Invoke(1);

            Assert.True(
                i == 2,
                "Cannot use fallback action");
        }
    }
}
