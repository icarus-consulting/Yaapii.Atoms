using System;
using System.Collections.Generic;
using System.Text;
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
