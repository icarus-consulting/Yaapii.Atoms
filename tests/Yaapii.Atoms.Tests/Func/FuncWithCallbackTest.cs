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
using Xunit;

namespace Yaapii.Atoms.Func.Tests
{
    public sealed class FuncWithCallbackTest
    {
        [Fact]
        public void UsesMainFunc()
        {
            Assert.True(
                new FuncWithFallback<bool, string>(
                    input => "It's success",
                    ex => "In case of failure..."
                ).Invoke(true).Contains("success"),
                "cannot use main function");
        }

        [Fact]
        public void UsesFallback()
        {
            Assert.True(
                new FuncWithFallback<bool, string>(
                    input =>
                    {
                        throw new Exception("Failure");
                    },
                    ex => "Never mind"
                ).Invoke(true) == "Never mind"
            );
        }

        [Fact]
        public void UsesFollowUp()
        {
            Assert.True(
            new FuncWithFallback<bool, string>(
                input => "works fine",
                ex => "won't happen",
                input => "follow up"
            ).Invoke(true) == "follow up");
        }

        [Fact]
        public void UsesParameterlessMainFunc()
        {
            Assert.True(
            new FuncWithFallback<string>(
                () => "It's success",
                ex => "In case of failure..."
            ).Invoke().Contains("success"),
            "cannot use main function");
        }

        [Fact]
        public void UsesParameterlessCallback()
        {
            Assert.True(
                new FuncWithFallback<string>(
                    () =>
                    {
                        throw new Exception("Failure");
                    },
                    ex => "Never mind"
                ).Invoke() == "Never mind"
            );
        }

        [Fact]
        public void ParameterlessUsesFollowUp()
        {
            Assert.True(
            new FuncWithFallback<string>(
                () => "works fine",
                ex => "won't happen",
                input => "follow up"
            ).Invoke() == "follow up");
        }
    }
}
