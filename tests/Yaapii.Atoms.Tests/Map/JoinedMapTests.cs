using Xunit;

namespace Yaapii.Atoms.Lookup.Tests
{
    public sealed class JoinedMapTests
    {
        [Fact]
        public void JoinsInputs()
        {
            var dict =
                new Joined (
                    new LazyDict(new Kvp.Of("A", "I am")),
                    new LazyDict(new Kvp.Of("B", "trapped in")),
                    new LazyDict(new Kvp.Of("C", "a dictionary"))
                );

            Assert.Equal(
                "I am trapped in a dictionary",
                $"{dict["A"]} {dict["B"]} {dict["C"]}"
            );
        }

        [Fact]
        public void ReplacesExisting()
        {
            var dict =
                new Joined(
                    new LazyDict(new Kvp.Of("A", "Hakuna")),
                    new LazyDict(new Kvp.Of("B", "Matata")),
                    new LazyDict(new Kvp.Of("B", "Banana"))
                );

            Assert.Equal(
                "Banana",
                dict["B"]
            );
        }

        [Fact]
        public void JoinsInputsTypedValue()
        {
            var dict =
                new Joined<int>(
                    new LazyDict<int>(new Kvp.Of<int>("A", 89)),
                    new LazyDict<int>(new Kvp.Of<int>("B", 17)),
                    new LazyDict<int>(new Kvp.Of<int>("C", 8))
                );

            Assert.Equal(
                "89 17 8",
                $"{dict["A"]} {dict["B"]} {dict["C"]}"
            );
        }

        [Fact]
        public void ReplacesExistingTypedValue()
        {
            var dict =
                new Joined<int>(
                    new LazyDict<int>(new Kvp.Of<int>("A", 1)),
                    new LazyDict<int>(new Kvp.Of<int>("B", 4)),
                    new LazyDict<int>(new Kvp.Of<int>("B", 19))
                );

            Assert.Equal(
                19,
                dict["B"]
            );
        }

        [Fact]
        public void JoinsInputsTypedKeyValue()
        {
            var dict =
                new Joined<int, int>(
                    new LazyDict<int, int>(new Kvp.Of<int, int>(0, 1)),
                    new LazyDict<int, int>(new Kvp.Of<int, int>(1, 3)),
                    new LazyDict<int, int>(new Kvp.Of<int, int>(2, 37))
                );

            Assert.Equal(
                "1337",
                $"{dict[0]}{dict[1]}{dict[2]}"
            );
        }

        [Fact]
        public void ReplacesExistingTypedKeyValue()
        {
            var dict =
                new Joined<int, int>(
                    new LazyDict<int, int>(new Kvp.Of<int, int>(0, 1)),
                    new LazyDict<int, int>(new Kvp.Of<int, int>(0, 4)),
                    new LazyDict<int, int>(new Kvp.Of<int, int>(0, 19))
                );

            Assert.Equal(
                19,
                dict[0]
            );
        }
    }
}
