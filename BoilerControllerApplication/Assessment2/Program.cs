using BoilerControllerApplication.Controller;
using BoilerControllerApplication.Core.Interfaces;
using BoilerControllerApplication.Service;

namespace BoilerControllerApplication
{
    //TODO: Add xml comments
    public class Program
    {
        public static void Main()
        {
            IBoilerService boilerService = new BoilerService();
            BoilerController boilerController = new BoilerController(boilerService);
            boilerController.RunMainMenu();
        }
    }
}
