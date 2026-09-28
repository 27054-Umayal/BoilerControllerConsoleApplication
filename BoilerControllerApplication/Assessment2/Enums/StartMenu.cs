namespace BoilerControllerApplication.Enums
{
    /// <summary>
    /// Represents the start menu.
    /// </summary>
    public enum StartMenu
    {
        /// <summary>
        /// Represents the starting of the boiler sequence operation.
        /// </summary>
        StartBoilerSequence = 1,

        /// <summary>
        /// Represents the stopping of the boiler sequence operation.
        /// </summary>
        StopBoilerSequence,

        /// <summary>
        /// Represents the simulation of the boiler sequence operation.
        /// </summary>
        SimulateBoilerSequence,

        /// <summary>
        /// Represents the toggling of switch operation.
        /// </summary>
        ToggleRunInterLockSwitch,

        /// <summary>
        /// Represents the resetting lockout operation.
        /// </summary>
        ResetLockout,

        /// <summary>
        /// Represents the view event logs operation.
        /// </summary>
        ViewEventLog,

        /// <summary>
        /// Represents the exit operation.
        /// </summary>
        Exit,
    }
}
