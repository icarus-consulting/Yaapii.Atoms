using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Text.Tests
{
    public sealed class StartsWithTests
    {
        [Fact]
        public void MatchesText()
        {
            var x =
                new StartsWith(
                    new TextOf("Im an text with a really good end!"),
                    new TextOf("Im a")
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void MatchesString()
        {
            var x =
                new StartsWith(
                    new TextOf("Im a text with a really good end!"),
                    "Im a"
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void DoesntMatch()
        {
            var x =
                new StartsWith(
                    new TextOf("Im a text with a really good end!"),
                    new TextOf("m an")
                );
            Assert.False(x.Value());
        }
    }
}
