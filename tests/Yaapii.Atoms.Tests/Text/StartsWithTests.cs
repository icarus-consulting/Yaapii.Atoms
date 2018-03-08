using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class StartsWithTests
    {
        [Fact]
        public void FindsTextStart()
        {
            var x =
                new StartsWith(
                    new TextOf("Im an text with a realy good end!"),
                    new TextOf("Im a")
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void TakesStartAsString()
        {
            var x =
                new StartsWith(
                    new TextOf("Im an text with a realy good end!"),
                    "Im a"
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void FailsOnWrongStart()
        {
            var x =
                new StartsWith(
                    new TextOf("Im an text with a realy good end!"),
                    new TextOf("m an")
                );
            Assert.False(x.Value());
        }

        [Fact]
        public void FailsOnCaseMismatch()
        {
            var x =
                new StartsWith(
                    new TextOf("Im an text with a realy good end!"),
                    new TextOf("im an")
                );
            Assert.False(x.Value());
        }

        [Fact]
        public void FindsMultilineTextStart()
        {
            var x =
                new StartsWith(
                    new TextOf(
                        "Im an text with a realy good end!" + System.Environment.NewLine +
                        "But the end is here"),
                    new TextOf("Im a")
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void FailsOnWrongStartOfMultilineText()
        {
            var x =
                new StartsWith(
                    new TextOf(
                        "Im an text with a realy good end!" + System.Environment.NewLine +
                        "But the end is here"),
                    new TextOf("But t")
                );
            Assert.False(x.Value());
        }
    }
}
