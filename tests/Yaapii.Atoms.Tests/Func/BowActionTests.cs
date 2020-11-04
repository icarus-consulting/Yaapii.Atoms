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
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Func.Tests
{
    public sealed class BowActionTests
    {
        [Fact]
        public void WaitsForTrigger()
        {
            var actions = new List<string>();
            var count = 0;
            new BowAction(
                () => { actions.Add("ask trigger"); return count++ > 0; },
                () => actions.Add("shoot")
            ).Invoke();

            Assert.Equal(
                "ask trigger, ask trigger, shoot",
                new Joined(", ", actions).ToString()
            );
        }

        [Fact]
        public void Prepares()
        {
            var actions = new List<string>();
            var count = 0;
            new BowAction(
                () => { actions.Add("ask trigger"); return count++ > 0; },
                () => actions.Add("prepare"),
                () => actions.Add("shoot")
            ).Invoke();

            Assert.Equal(
                "prepare, ask trigger, ask trigger, shoot",
                new Joined(", ", actions).ToString()
            );
        }

        [Fact]
        public void CancelsAfterTimeout()
        {
            Assert.Throws<ApplicationException>(
                () =>
                    new BowAction(
                        () => false,
                        () => { },
                        new TimeSpan(0, 0, 0, 0, 100)
                    ).Invoke()
            );
        }

        [Fact]
        public void RejectsOnException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new BowAction(
                    () => throw new InvalidOperationException("fail"),
                    () => { }
                ).Invoke()
            );
        }
    }
}
