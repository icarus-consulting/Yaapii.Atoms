using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Lookup;

namespace Yaapii.Atoms.Dict.Tests
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
    }
}
