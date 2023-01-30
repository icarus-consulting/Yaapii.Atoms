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

using System;
using System.Collections.Generic;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Func.Tests
{
    public sealed class BowFuncTests
    {
        [Fact]
        public void WaitsForTrigger()
        {
            var actions = new List<string>();
            var count = 0;
            new BowFunc<string>(
                () => { actions.Add("ask trigger"); return count++ > 0; },
                (str) => actions.Add("shoot")
            ).Invoke("test");

            Assert.Equal(
                "ask trigger, ask trigger, shoot",
                new Joined(", ", actions).AsString()
            );
        }

        [Fact]
        public void Prepares()
        {
            var actions = new List<string>();
            var count = 0;
            new BowFunc<string>(
                () => { actions.Add("ask trigger"); return count++ > 0; },
                () => actions.Add("prepare"),
                (str) => actions.Add("shoot"),
                new TimeSpan(0, 0, 10)
            ).Invoke("test");

            Assert.Equal(
                "prepare, ask trigger, ask trigger, shoot",
                new Joined(", ", actions).AsString()
            );
        }

        [Fact]
        public void InvokesShoot()
        {
            var actions = new List<string>();
            var count = 0;
            new BowFunc<string>(
                () => { actions.Add("ask trigger"); return count++ > 0; },
                () => actions.Add("prepare"),
                (str) => actions.Add(str),
                new TimeSpan(0, 0, 10)
            ).Invoke("test");

            Assert.Equal(
                "prepare, ask trigger, ask trigger, test",
                new Joined(", ", actions).AsString()
            );
        }

        [Fact]
        public void CancelsAfterTimeout()
        {
            Assert.Throws<ApplicationException>(
                () =>
                    new BowFunc<string>(
                        () => false,
                        () => { },
                        (str) => { },
                        new TimeSpan(0, 0, 0, 0, 100)
                    ).Invoke("test")
            );
        }

        [Fact]
        public void RejectsOnException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new BowFunc<string>(
                    () => throw new InvalidOperationException("fail"),
                    (str) => { }
                ).Invoke("test")
            );
        }
    }
}
