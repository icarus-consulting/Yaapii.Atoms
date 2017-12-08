using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public sealed class NotEmptyTest
    {
        [Fact]
        public void EmptyEnumerableThrowsExeption()
        {
            Assert.Throws<Exception>(() =>
                new LengthOf(
                    new NotEmpty<bool>(
                        new EnumerableOf<bool>()
                    )).Value());
        }

        [Fact]
        public void NotEmptyEnumerableThrowsNoExeption()
        {
            Assert.True(
                new LengthOf(
                    new NotEmpty<bool>(
                        new EnumerableOf<bool>(false)
                    )).Value() == 1);
        }

        [Fact]
        public void EmptyCollectionThrowsCustomExeption()
        {
            Assert.Throws<OperationCanceledException>(() =>
                new LengthOf(
                    new NotEmpty<bool>(
                        new EnumerableOf<bool>(),
                        new OperationCanceledException()
                    )).Value());
        }
    }
}
