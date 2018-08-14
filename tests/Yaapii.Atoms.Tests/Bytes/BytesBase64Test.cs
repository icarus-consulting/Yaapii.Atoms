using Xunit;
using Yaapii.Atoms.IO;

namespace Yaapii.Atoms.Bytes.Tests
{
    public sealed class BytesBase64Test
    {
        [Fact]
        public void EncodesBase64()
        {
            Assert.True(
                new BytesEqual(
                    new BytesBase64(
                        new BytesOf(
                            "Hello!")
                    ),
                    new BytesOf(
                        "SGVsbG8h")
                ).Value()
            );
        }
    }
}
