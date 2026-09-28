using BoilerControllerApplication.Core.Models;

namespace BoilerControllerApplication.View
{
    //TODO: Add xml comments
    public static class ApplicationConsole
    {
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static void ViewMenu(Type enumType)
        {
            foreach(var choice in Enum.GetValues(enumType))
            {
                DisplayMessage($"{(int)choice}.{choice}");
            }
        }

        public static bool TryGetIntInput(string promptMessage, out int intInput)
        {
            DisplayMessage(promptMessage);
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            return int.TryParse(input, out intInput);
        }

        public static void DisplayLogs(List<EventLogModel> historyOfLog)
        {
            int SeparatorLineLength = 104;
            ApplicationConsole.DisplaySeparatorLine(SeparatorLineLength);
            ApplicationConsole.DisplayMessage($"| {"TimeStamp",-19} |" +
                                              $" {"Event Name",-30} |" +
                                              $" {"Event Data",-45} |");
            ApplicationConsole.DisplaySeparatorLine(SeparatorLineLength);
            for(int i = 0; i < historyOfLog.Count; i++)
            {
                ApplicationConsole.DisplayMessage($"| {historyOfLog[i].TimeStamp,-19} |" +
                                              $" {historyOfLog[i].Event,-30} |" +
                                              $" {historyOfLog[i].EventData,-45} |");
                ApplicationConsole.DisplaySeparatorLine(SeparatorLineLength);
            }

        }

        public static void DisplaySeparatorLine(int lineCharacterLength = 20)
        {
            ApplicationConsole.DisplayMessage(new string('_', lineCharacterLength));
        }
    }
}
