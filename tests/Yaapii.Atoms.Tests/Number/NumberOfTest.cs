using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;
using Yaapii.Atoms.Number;

namespace Yaapii.Atoms.Number.Tests
{
    public sealed class NumberOfTest
    {
        [Fact]
        public void ParsesFloat()
        {
            Assert.True(
                new NumberOf("4673.4534", NumberFormatInfo.InvariantInfo).AsFloat() == 4673.4534F
            );
        }

        [Fact]
        public void RejectsNoFloatText()
        {
            Assert.Throws<ArgumentException>(() =>
                new NumberOf("ghki").AsFloat()
            );
        }

        [Fact]
        public void ParsesInt()
        {
            Assert.True(
                new NumberOf("1337").AsInt() == 1337
            );
        }

        [Fact]
        public void RejectsNoIntText()
        {
            Assert.Throws<ArgumentException>(() =>
                new NumberOf("ghki").AsInt()
            );
        }

        [Fact]
        public void ParsesDouble()
        {
            Assert.True(
                new NumberOf("843.23969274001", NumberFormatInfo.InvariantInfo).AsDouble() == 843.23969274001D
            );
        }

        [Fact]
        public void RejectsNoDoubleText()
        {
            Assert.Throws<ArgumentException>(() =>
                new NumberOf("ghki").AsDouble()
            );
        }

        [Fact]
        public void ParsesLong()
        {
            Assert.True(
                new NumberOf("139807814253711").AsLong() == 139807814253711L
            );
        }

        [Fact]
        public void RejectsNoLongText()
        {
            Assert.Throws<ArgumentException>(() =>
                new NumberOf("ghki").AsLong()
            );
        }
    }
}
