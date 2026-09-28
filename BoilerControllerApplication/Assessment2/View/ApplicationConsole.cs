using BoilerControllerApplication.Core.Models;

namespace BoilerControllerApplication.View
{
    /// <summary>
    /// Handles the console input and output operations.
    /// </summary>
    public static class ApplicationConsole
    {
        /// <summary>
        /// Displays the specific message.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Displays the menu options.
        /// </summary>
        /// <param name="enumType">The type of menu to be displayed.</param>
        public static void ViewMenu(Type enumType)
        {
            foreach(var choice in Enum.GetValues(enumType))
            {
                DisplayMessage($"{(int)choice}.{choice}");
            }
        }

        /// <summary>
        /// Gets an integer input.
        /// </summary>
        /// <param name="promptMessage">The prompt message to be displayed.</param>
        /// <param name="intInput">The int input retrieved if a valid integer is entered.</param>
        /// <returns>An true if valid int input is entered; otherwise false.</returns>
        public static bool TryGetIntInput(string promptMessage, out int intInput)
        {
            DisplayMessage(promptMessage);
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            return int.TryParse(input, out intInput);
        }

        /// <summary>
        /// Displays the logs.
        /// </summary>
        /// <param name="historyOfLog">The history of logs to be displayed.</param>
        public static void DisplayLogs(List<EventLogModel> historyOfLog)
        {
            int SeparatorLineLength = 119;
            ApplicationConsole.DisplaySeparatorLine(SeparatorLineLength);
            ApplicationConsole.DisplayMessage($"| {"TimeStamp",-19} |" +
                                              $" {"Event Name",-30} |" +
                                              $" {"Event Data",-60} |");
            ApplicationConsole.DisplaySeparatorLine(SeparatorLineLength);
            for(int i = 0; i < historyOfLog.Count; i++)
            {
                ApplicationConsole.DisplayMessage($"| {historyOfLog[i].TimeStamp,-19} |" +
                                              $" {historyOfLog[i].Event,-30} |" +
                                              $" {historyOfLog[i].EventData,-60} |");
                ApplicationConsole.DisplaySeparatorLine(SeparatorLineLength);
            }

        }

        /// <summary>
        /// Displays the separator line.
        /// </summary>
        /// <param name="lineCharacterLength">The length of the separator line to be displayed.</param>
        public static void DisplaySeparatorLine(int lineCharacterLength = 20)
        {
            ApplicationConsole.DisplayMessage(new string('_', lineCharacterLength));
        }
    }
}
