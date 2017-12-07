using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Collection.Tests
{
    public sealed class NotEmptyTest
    {
        [Fact]
        public void EmptyCollectionThrowsExeption()
        {
            Assert.Throws<Exception>(() =>
                new LengthOf(
                    new NotEmpty<bool>(
                        new CollectionOf<bool>()
                    )).Value());
        }

        [Fact]
        public void NotEmptyCollectionThrowsNoExeption()
        {
            Assert.True(
                new LengthOf(
                    new NotEmpty<bool>(
                        new CollectionOf<bool>(false)
                    )).Value() == 1);
        }
    }
}
