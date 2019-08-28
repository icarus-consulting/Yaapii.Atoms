using System;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.Func;

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
        public void FailsWithCustomExceptionOfFirstPosition()
        {
            Assert.Throws<ApplicationException>(() =>
                new BitAt(new BytesOf(""), new ApplicationException("Custom exception message")).Value()
            );
        }

        [Fact]
        public void DeliversFallback()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(""),
                    2,
                    true
                ).Value()
            );
        }

        [Fact]
        public void DeliversFallbackOnFirstPosition()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(""),
                    true
                ).Value()
            );
        }

        [Fact]
        public void ExecutesFallbackFunc()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(""),
                    2,
                    bytes => true
                ).Value()
            );
        }

        [Fact]
        public void ExecutesFallbackIFunc()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(""),
                    2,
                    new FuncOf<IBytes, bool>(bytes => true)
                ).Value()
            );
        }

        [Fact]
        public void ExecutesFallbackFuncOnFirstPosition()
        {
            Assert.True(
                new BitAt(
                    new BytesOf(""),
                    bytes => true
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
