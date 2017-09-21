using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class RotatedTextTest
    {
        [Fact]
        public void RotateRightText()
        {
            Assert.True(
                new RotatedText(
                    new TextOf("Hello!"), 2
                ).AsString() == "o!Hell",
                "Can't rotate text to right");
        }

        [Fact]
        public void RotateLeftText()
        {
            Assert.True(
                new RotatedText(
                    new TextOf("Hi!"), -1
                ).AsString() == "i!H",
                "Can't rotate text to left");
        }

        [Fact]
        public void NoRotateWhenShiftZero()
        {
            var nonrotate = "Atoms!";
            Assert.True(
                new RotatedText(
                    new TextOf(nonrotate), 0
                ).AsString() == nonrotate,
                "Can't rotate text shift zero"
            );
        }

        [Fact]
        public void NoRotateWhenShiftModZero()
        {
            var nonrotate = "Rotate";
            Assert.True(
                new RotatedText(
                    new TextOf(nonrotate), nonrotate.Length
                ).AsString() == nonrotate,
                "Can't rotate text shift mod zero");
        }

        [Fact]
        public void NoRotateWhenEmpty()
        {
            Assert.True(
                new RotatedText(
                    new TextOf(""), 2
                ).AsString() == "",
            "Can't rotate text when empty");
        }
    }
}
