using Xunit;
using Yaapii.Atoms.Text;

namespace Yaapii.Atoms.Tests.Text
{
    public sealed class EndsWithTests
    {
        [Fact]
        public void FindsTextEnd()
        {
            var x =
                new EndsWith(
                    new TextOf("Im an text with a realy good end!"),
                    new TextOf("od end!")
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void TakesTailAsString()
        {
            var x =
                new EndsWith(
                    new TextOf("Im an text with a realy good end!"),
                    "od end!"
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void FailsOnWrongTail()
        {
            var x =
                new EndsWith(
                    new TextOf("Im an text with a realy good end!"),
                    new TextOf("od end")
                );
            Assert.False(x.Value());
        }

        [Fact]
        public void FailsOnCaseMismatch()
        {
            var x =
                new EndsWith(
                    new TextOf("Im an text with a realy good end!"),
                    new TextOf("od enD!")
                );
            Assert.False(x.Value());
        }

        [Fact]
        public void FindsMultilineTextEnd()
        {
            var x =
                new EndsWith(
                    new TextOf(
                        "Im an text with a realy good end!" + System.Environment.NewLine +
                        "But the end is here"),
                    new TextOf("s here")
                );
            Assert.True(x.Value());
        }

        [Fact]
        public void FailsOnWrongTailOfMultilineText()
        {
            var x =
                new EndsWith(
                    new TextOf(
                        "Im an text with a realy good end!" + System.Environment.NewLine +
                        "But the end is here"),
                    new TextOf("od end!")
                );
            Assert.False(x.Value());
        }
    }
}
