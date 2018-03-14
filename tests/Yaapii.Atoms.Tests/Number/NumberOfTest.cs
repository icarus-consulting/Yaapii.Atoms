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
                new NumberOf(4673.453F).AsFloat() == 4673.453F
            );
        }
        [Fact]
        public void ParsesCultureFloat()
        {
            Assert.True(
                new NumberOf(4673.453F, CultureInfo.GetCultureInfo("de-DE")).AsFloat().ToString() == "4673,453"
            );
        }

        [Fact]
        public void RejectsNoFloatText()
        {
            Assert.Throws<FormatException>(() =>
                new NumberOf("ghki").AsFloat()
            );
        }

        [Fact]
        public void ParsesInt()
        {
            Assert.True(
                new NumberOf(1337).AsInt() == 1337
            );
        }

        [Fact]
        public void ParsesCultureInt()
        {
            Assert.True(
                new NumberOf(1337, CultureInfo.GetCultureInfo("de-DE")).AsInt() == 1337
            );
        }

        [Fact]
        public void RejectsNoIntText()
        {
            Assert.Throws<FormatException>(() =>
                new NumberOf("ghki").AsInt()
            );
        }

        [Fact]
        public void ParsesDouble()
        {
            Assert.True(
                new NumberOf(843.23969274001D).AsDouble() == 843.23969274001D
            );
        }

        [Fact]
        public void ParsesCultureDouble()
        {
            Assert.True(
                new NumberOf(843.23969274001D, CultureInfo.GetCultureInfo("de-DE")).AsDouble().ToString() == "843,23969274001"
            );
        }

        [Fact]
        public void RejectsNoDoubleText()
        {
            Assert.Throws<FormatException>(() =>
                new NumberOf("ghki").AsDouble()
            );
        }

        [Fact]
        public void ParsesLong()
        {
            Assert.True(
                new NumberOf(139807814253711).AsLong() == 139807814253711L
            );
        }

        [Fact]
        public void ParsesCultureLong()
        {
            Assert.True(
                new NumberOf(139807814253711, CultureInfo.GetCultureInfo("de-DE")).AsLong() == 139807814253711L
            );
        }

        [Fact]
        public void RejectsNoLongText()
        {
            Assert.Throws<FormatException>(() =>
                new NumberOf("ghki").AsLong()
            );
        }

        [Fact]
        public void IntToDouble()
        {
            Assert.True(
                new NumberOf(
                    5,
                    NumberFormatInfo.InvariantInfo
                ).AsDouble() == 5d
            );
        }

        [Fact]
        public void DoubleToFloat()
        {
            Assert.True(
                new NumberOf(
                    (551515155.451d),
                    NumberFormatInfo.InvariantInfo
                ).AsFloat() == 551515155.451f
            );
        }

        [Fact]
        public void FloatAsDouble()
        {
            Assert.True(
                new NumberOf(
                    (5.243),
                    NumberFormatInfo.InvariantInfo
                ).AsDouble() == 5.243d
            );
        }

        [Fact]
        public void LongAsInt()
        {
            Assert.True(
                new NumberOf(
                    (50l),
                    NumberFormatInfo.InvariantInfo
                ).AsInt() == 50
            );
        }


        [Fact]
        public void IntAsLong()
        {
            Assert.True(
                new NumberOf(
                    (50),
                    NumberFormatInfo.InvariantInfo
                ).AsLong() == 50l
            );
        }
    }
}
