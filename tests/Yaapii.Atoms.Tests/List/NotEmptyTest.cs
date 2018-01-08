using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.List.Tests
{
    public sealed class NotEmptyTest
    {
        [Fact]
        public void EmptyCollectionThrowsExeption()
        {
            Assert.Throws<Exception>(() =>
                new LengthOf(
                    new NotEmpty<bool>(
                        new ListOf<bool>()
                    )).Value());
        }

        [Fact]
        public void NotEmptyCollectionThrowsNoExeption()
        {
            Assert.True(
                new LengthOf(
                    new NotEmpty<bool>(
                        new ListOf<bool>(false)
                    )).Value() == 1);
        }

        [Fact]
        public void EmptyCollectionThrowsCustomExeption()
        {
            Assert.Throws<OperationCanceledException>(() =>
                new LengthOf(
                    new NotEmpty<bool>(
                        new ListOf<bool>(),
                        new OperationCanceledException()
                    )).Value());
        }
    }
}
