using BoilerControllerApplication.Controller;
using BoilerControllerApplication.Core.Interfaces;
using BoilerControllerApplication.Repository;
using BoilerControllerApplication.Service;
using BoilerControllerApplication.View;

namespace BoilerControllerApplication
{
    //TODO: Add xml comments
    public class Program
    {
        public static async Task Main()
        {
            try
            {
                ILogRepo logRepo = new LogRepo();
                IBoilerService boilerService = new BoilerService(logRepo);
                BoilerController boilerController = new BoilerController(boilerService);
                await boilerController.RunMainMenu();
            }

            catch (Exception ex)
            {
                ApplicationConsole.DisplayMessage($"Unexpected error occurred: {ex.Message}");
            }
            
        }
    }
}
