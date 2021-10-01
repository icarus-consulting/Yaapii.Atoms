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

namespace Yaapii.Atoms.Map.Tests
{
    public sealed class KvpOfTests
    {
        [Fact]
        public void BuildsValue()
        {
            Assert.Equal(
                "value",
                new KvpOf("key", () => "value").Value()
            );
        }

        [Fact]
        public void BuildsValueLazily()
        {
            Assert.Equal(
                "key",
                new KvpOf("key", () => throw new ApplicationException()).Key()
            );
        }

        [Fact]
        public void KnowsAboutBeingLazy()
        {
            Assert.True(new KvpOf("2", () => "4").IsLazy());
        }

        [Fact]
        public void KnowsAboutBeingNotLazy()
        {
            Assert.False(new KvpOf("2", "4").IsLazy());
        }

        [Fact]
        public void BuildsValueTyped()
        {
            Assert.Equal(
                1,
                new KvpOf<int>("key", () => 1).Value()
            );
        }

        [Fact]
        public void BuildsValueTypedLazily()
        {
            Assert.Equal(
                "key",
                new KvpOf<int>("key", () => throw new ApplicationException()).Key()
            );
        }

        [Fact]
        public void KnowsAboutBeingLazyValue()
        {
            Assert.True(new KvpOf<int>("2", () => 4).IsLazy());
        }

        [Fact]
        public void KnowsAboutBeingNotLazyValue()
        {
            Assert.False(new KvpOf<int>("2", 4).IsLazy());
        }

        [Fact]
        public void BuildsKeyValueTyped()
        {
            Assert.Equal(
                1,
                new KvpOf<int, int>(8, () => 1).Value()
            );
        }

        [Fact]
        public void BuildsKeyValueTypedLazily()
        {
            Assert.Equal(
                8,
                new KvpOf<int, int>(8, () => throw new ApplicationException()).Key()
            );
        }

        [Fact]
        public void KnowsAboutBeingLazyKeyValue()
        {
            Assert.True(new KvpOf<int, int>(2, () => 4).IsLazy());
        }

        [Fact]
        public void KnowsAboutBeingNotLazyKeyValue()
        {
            Assert.False(new KvpOf<int, int>(2, 4).IsLazy());
        }
    }
}
