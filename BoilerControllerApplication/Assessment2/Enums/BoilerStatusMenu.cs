namespace BoilerControllerApplication.Enums
{
    /// <summary>
    /// Represents the boiler status menu.
    /// </summary>
    public enum BoilerStatusMenu
    {
        /// <summary>
        /// Represents the lockout state.
        /// </summary>
        Lockout = 1,

        /// <summary>
        /// Represents the ready state.
        /// </summary>
        Ready,

        /// <summary>
        /// Represents the pre purge state.
        /// </summary>
        PrePurge,

        /// <summary>
        /// Represents the ignition state.
        /// </summary>
        Ignition,

        /// <summary>
        /// Represents the operational state.
        /// </summary>
        Operational,
    }
}
