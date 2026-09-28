using BoilerControllerApplication.Controller;
using BoilerControllerApplication.Core.Interfaces;
using BoilerControllerApplication.Service;

namespace BoilerControllerApplication
{
    //TODO: Add xml comments
    public class Program
    {
        public static async Task Main()
        {
            IBoilerService boilerService = new BoilerService();
            BoilerController boilerController = new BoilerController(boilerService);
            await boilerController.RunMainMenu();
        }
    }
}
