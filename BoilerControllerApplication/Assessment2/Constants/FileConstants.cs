namespace BoilerControllerApplication.Constants
{
    public static class FileConstants
    {
        public const string BoilerLogHeaderDetail = "TimeStamp, Event, EventData";

        public static int HeaderLineCount = 1;

        public static int BoilerHeaderDetailLength = 3;

        public static readonly string BoilerLogFilePath = Path.Combine(AppContext.BaseDirectory, "BoilerLog.csv");
    }
}
