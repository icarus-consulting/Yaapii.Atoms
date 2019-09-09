using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Map;

namespace Yaapii.Atoms.Dict.Tests
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
    }
}
