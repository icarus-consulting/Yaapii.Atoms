using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.IO.Tests
{
    public sealed class HeadOfTest
    {
        [Fact]
        void ReadsHeadOfLongerInput()
        {
            Assert.Contains(
                "reads",
                new TextOf(
                    new HeadOf(
                        new InputOf("readsHeadOfLongInput"),
                        5
                    )
                ).AsString()
            );
        }

        [Fact]
        void ReadsOnlyLength()
        {
            var res =
                new TextOf(
                    new HeadOf(
                        new InputOf("readsHeadOfLongInput"),
                        5
                    )
                ).AsString();

            Assert.Equal(
                5,
                new LengthOf(
                    new InputOf(
                        res
                    )
                ).Value()
            );
        }

        [Fact]
        void ReadsEmptyHeadOfInput()
        {
            Assert.Contains(
                "",
                new TextOf(
                    new HeadOf(
                        new InputOf("readsEmptyHeadOfInput"),
                        0
                    )
                ).AsString()
            );
        }

        [Fact]
        void ReadsHeadOfShorterInput()
        {
            var input = "readsHeadOfShorterInput";
            Assert.Contains(
                input,
                new TextOf(
                    new HeadOf(
                        new InputOf(input),
                        35
                    )
                ).AsString()
            );
        }
    }
}
