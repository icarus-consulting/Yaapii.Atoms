using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Text.Tests
{
    public sealed class TextBase64Tests
    {
        [Theory]
        [InlineData("A fancy text")]
        [InlineData("A fancy text with \n line break")]
        [InlineData("A fancy text with € special character")]
        public void EncodesText(string text)
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");
            try
            {
                new LengthOf(
                    new TeeInput(
                        new TextOf(
                            new BytesBase64(
                                new BytesOf(
                                    new TextOf(text)
                                )
                            )
                        ).AsString(),
                        new OutputTo(new Uri(file))
                    )
                ).Value();

                Assert.True(
                    new TextOf(
                        new Uri(file)
                    ).Equals(
                    new TextBase64(
                        new TextOf(text)
                    )));
            }
            finally
            {
                // Cleanup
                if (File.Exists(file)) File.Delete(file);
            }
        }
    }
}
