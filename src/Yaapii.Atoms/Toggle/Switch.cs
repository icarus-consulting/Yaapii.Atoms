using System.Collections.Generic;

namespace Yaapii.Atoms.Toggle
{
    /// <summary>
    /// A toggable switch.
    /// </summary>
    public sealed class Switch : IToggle
    {
        private readonly List<bool> state;

        /// <summary>
        /// A toggable switch.
        /// </summary>
        /// <param name="isOn">The initial on state</param>
        public Switch(bool isOn) : this(
            new List<bool>() { isOn }
        )
        { }

        private Switch(List<bool> state)
        {
            this.state = state;
        }

        public bool IsOn()
        {
            return this.state[0];
        }

        public void Toggle()
        {
            this.state[0] = !this.state[0];
        }
    }
}
