using System;
using Xunit;
using Yaapii.Atoms.Bytes;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Yaapii.Atoms.Scalar.Tests
{
    public sealed class BitAtTests
    {
        [Fact]
        public void DeliversFirstElement()
        {
            Assert.True(
                new BitAt(new BytesOf(1)).Value()
            );
        }

        [Fact]
        public void DeliversElementAtPosition()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(2),
                    1
                ).Value()
            );
        }

        [Fact]
        public void FailsWhenBytesEmpty()
        {
            Assert.Throws<ArgumentException>(() =>
                new BitAt(new BytesOf("")).Value()
            );
        }

        [Fact]
        public void DeliversFallback()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(""),
                    1,
                    true
                ).Value()
            );
        }

        [Fact]
        public void FallbackShowsError()
        {
            Assert.Throws<ArgumentException>(() =>
                new BitAt(
                    new BytesOf(""),
                    1,
                    (ex, bytes) => throw ex
                ).Value()
            );
        }

        [Fact]
        public void UsesInjectedExcption()
        {
            Assert.Throws<ApplicationException>(() =>
                new BitAt(
                    new BytesOf(""),
                    1,
                    new ApplicationException()
                ).Value()
            );
        }
    }
}
