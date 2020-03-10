using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Lookup.Tests
{
    public sealed class KvpOfTests
    {
        [Fact]
        public void BuildsValue()
        {
            Assert.Equal(
                "value",
                new Kvp.Of("key", () => "value").Value()
            );
        }

        [Fact]
        public void BuildsValueLazily()
        {
            Assert.Equal(
                "key",
                new Kvp.Of("key", () => throw new ApplicationException()).Key()
            );
        }

        [Fact]
        public void KnowsAboutBeingLazy()
        {
            Assert.True(new Kvp.Of("2", () => "4").IsLazy());
        }

        [Fact]
        public void KnowsAboutBeingNotLazy()
        {
            Assert.False(new Kvp.Of("2", "4").IsLazy());
        }

        [Fact]
        public void BuildsValueTyped()
        {
            Assert.Equal(
                1,
                new Kvp.Of<int>("key", () => 1).Value()
            );
        }

        [Fact]
        public void BuildsValueTypedLazily()
        {
            Assert.Equal(
                "key",
                new Kvp.Of<int>("key", () => throw new ApplicationException()).Key()
            );
        }

        [Fact]
        public void KnowsAboutBeingLazyValue()
        {
            Assert.True(new Kvp.Of<int>("2", () => 4).IsLazy());
        }

        [Fact]
        public void KnowsAboutBeingNotLazyValue()
        {
            Assert.False(new Kvp.Of<int>("2", 4).IsLazy());
        }

        [Fact]
        public void BuildsKeyValueTyped()
        {
            Assert.Equal(
                1,
                new Kvp.Of<int, int>(8, () => 1).Value()
            );
        }

        [Fact]
        public void BuildsKeyValueTypedLazily()
        {
            Assert.Equal(
                8,
                new Kvp.Of<int, int>(8, () => throw new ApplicationException()).Key()
            );
        }

        [Fact]
        public void KnowsAboutBeingLazyKeyValue()
        {
            Assert.True(new Kvp.Of<int, int>(2, () => 4).IsLazy());
        }

        [Fact]
        public void KnowsAboutBeingNotLazyKeyValue()
        {
            Assert.False(new Kvp.Of<int, int>(2, 4).IsLazy());
        }
    }
}
