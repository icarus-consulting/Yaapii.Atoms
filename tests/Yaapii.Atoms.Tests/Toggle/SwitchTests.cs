using Xunit;

namespace Yaapii.Atoms.Toggle.Tests
{
    public sealed class SwitchTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasInitialState(bool initialState)
        {
            Assert.Equal(
                initialState,
                new Switch(initialState).IsOn()
            );
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Toggles(bool initialState)
        {
            var sw = new Switch(initialState);

            sw.Toggle();
            Assert.Equal(
                !initialState,
                sw.IsOn()
            );
        }
    }
}
