using Xunit;

namespace Yaapii.Atoms.Enumerable.Tests
{
    public class StickyTests
    {
        [Fact]
        public void ChangesOfSourceAreIgnored()
        {
            var content = 0;
            var items =
                new Sticky<string>(
                    new Repeated<string>(
                        () =>
                        {
                            content++;
                            return content.ToString();
                        },
                        1
                    )
                );

            Assert.Equal(
                new Text.Joined(" ", items).AsString(),
                new Text.Joined(" ", items).AsString()
            );

        }
    }
}

