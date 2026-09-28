namespace BoilerControllerApplication.Constants
{
    /// <summary>
    /// Defines the file constants.
    /// </summary>
    public static class FileConstants
    {
        /// <summary>
        /// Defines the boiler log header detail.
        /// </summary>
        public const string BoilerLogHeaderDetail = "TimeStamp, Event, EventData";

        /// <summary>
        /// Defines the header line count.
        /// </summary>

        public static int HeaderLineCount = 1;

        /// <summary>
        /// Defines the boiler header detail length.
        /// </summary>

        public static int BoilerHeaderDetailLength = 3;

        /// <summary>
        /// Defines the file path of the Boiler log.
        /// </summary>

        public static readonly string BoilerLogFilePath = Path.Combine(AppContext.BaseDirectory, "BoilerLog.csv");
    }
}
