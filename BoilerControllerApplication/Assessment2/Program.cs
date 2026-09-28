using BoilerControllerApplication.Controller;
using BoilerControllerApplication.Core.Interfaces;
using BoilerControllerApplication.Repository;
using BoilerControllerApplication.Service;
using BoilerControllerApplication.View;

namespace BoilerControllerApplication
{
    /// <summary>
    /// Serves as the entry point for the Boiler Controller Application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Initializes the application components and starts the boiler system.
        /// </summary>
        public static void Main()
        {
            try
            {
                ILogRepo logRepo = new LogRepo();
                IBoilerService boilerService = new BoilerService(logRepo);
                BoilerController boilerController = new BoilerController(boilerService);
                boilerController.RunMainMenu();
            }

            catch (Exception ex)
            {
                ApplicationConsole.DisplayMessage($"Unexpected error occurred: {ex.Message}");
            }
            
        }
    }
}
