namespace Yaapii.Atoms
{
    /// <summary>
    /// A toggable switch.
    /// </summary>
    public interface IToggle
    {
        /// <summary>
        /// The on state.
        /// </summary>
        bool IsOn();

        /// <summary>
        /// Toggles the switch state.
        /// </summary>
        void Toggle();
    }
}
