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
    }
}
