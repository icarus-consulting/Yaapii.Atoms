using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Yaapii.Atoms.Texts.Tests
{
    public sealed class ParagraphTest
    {
        [Fact]
        public void ParamsStringWorks()
        {
            var p = new Paragraph("a", "b", "c");
            Assert.Equal("a\nb\nc".Replace("\n", Environment.NewLine), p.AsString());
        }

        [Fact]
        public void IEnumITextWorks()
        {
            var p = new Paragraph(
                new Many.Live<IText>(
                    new Text.Live("a"),
                    new Text.Live("b"),
                    new Text.Live("c")
                ));
            Assert.Equal("a\nb\nc".Replace("\n", Environment.NewLine), p.AsString());
        }

        [Fact]
        public void HeadArrayTailStrings()
        {
            var p = new Paragraph(
                "Hello", "World",
                new string[] { "I", "was", "here" },
                "foo", "bar"
            );
            Assert.Equal("Hello\nWorld\nI\nwas\nhere\nfoo\nbar".Replace("\n", Environment.NewLine), p.AsString());
        }

        [Fact]
        public void HeadsAndTailsMixedITextStrings()
        {
            var p = new Paragraph(
                new Text.Live("Hello"), new Text.Live("World"),
                new string[] { "I", "was", "here" },
                new Text.Live("foo"), new Text.Live("bar")
            );
            Assert.Equal("Hello\nWorld\nI\nwas\nhere\nfoo\nbar".Replace("\n", Environment.NewLine), p.AsString());
        }
    }
}
