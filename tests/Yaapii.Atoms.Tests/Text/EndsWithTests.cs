using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class EndsWithTests
    {
        [Fact]
        public void MatchesText()
        {
            var x =
                new EndsWith(
                    new TextOf("Im a text with a really good end!"),
                    new TextOf("od end!")
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void MatchesString()
        {
            var x =
                new EndsWith(
                    new TextOf("Im a text with a really good end!"),
                    "od end!"
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void DoesntMatch()
        {
            var x =
                new EndsWith(
                    new TextOf("Im a text with a really good end!"),
                    new TextOf("od end")
                );
            Assert.False(x.Value());
        }
    }
}
