using BoilerControllerApplication.Core.Interfaces;
using BoilerControllerApplication.Enums;
using BoilerControllerApplication.View;
namespace BoilerControllerApplication.Controller
{
    //TODO: Add xml comments
    public class BoilerController
    {
        public SwitchStatusMenu switchStatus = SwitchStatusMenu.Open;
        public BoilerStatusMenu boilerStatus = BoilerStatusMenu.Lockout;
        private readonly IBoilerService _boilerService;

        public BoilerController(IBoilerService boilerService)
        {
            this._boilerService = boilerService;
        }

        public void RunMainMenu()
        {
            StartMenu choice = default;
            do
            {
                ApplicationConsole.ViewMenu(typeof(StartMenu));
                int choiceInput = this.ReadIntInput("Enter the choice", out int intInput);
                if (!Enum.IsDefined(typeof(StartMenu), intInput))
                {
                    ApplicationConsole.DisplayMessage("Invalid choice entered.");
                    continue;
                }

                choice = (StartMenu)intInput;
                switch(choice)
                {
                    case StartMenu.StartBoilerSequence:
                        break;
                    case StartMenu.StopBoilerSequence:
                        break;
                    case StartMenu.SimulateBoilerSequence:
                        break;
                    case StartMenu.ToggleRunInterLockSwitch:
                        this.RunToggleInterLockSwitch();
                        break;
                    case StartMenu.ResetLockout:
                        this.RunResetLockout();
                        break;
                    case StartMenu.ViewEventLog:
                        break;
                    case StartMenu.Exit:
                        ApplicationConsole.DisplayMessage("Exiting the application, Thankyou!");
                        break;
                }

            }
            while (choice != StartMenu.Exit);
        }

        private void RunResetLockout()
        {
            if(this._boilerService.IsSwitchStatusClosed(switchStatus))
            {
                this.boilerStatus = BoilerStatusMenu.Ready;
                //TODO: Log 
            }

            else
            {
                ApplicationConsole.DisplayMessage("Switch must be closed to perform the operation.");
            }
        }

        private void RunToggleInterLockSwitch()
        {
            if(this._boilerService.IsSwitchStatusOpen(switchStatus))
            {
                this.switchStatus = SwitchStatusMenu.Close;
                //TODO: Log
            }

            else
            {
                if(this._boilerService.IsBoilerStatusReady(boilerStatus) || this._boilerService.IsBoilerStatusLockout(boilerStatus))
                {
                    this.switchStatus = SwitchStatusMenu.Open;
                }

                else
                {
                    ApplicationConsole.DisplayMessage("Switch must be in Lockout/Ready state to perform the operation.");
                }

                //TODO: Log
            }
        }

        private int ReadIntInput(string promptMessage,  out int input)
        {
            while(true)
            {
                bool isValidIntInput = ApplicationConsole.TryGetIntInput(promptMessage, out input);
                if(isValidIntInput)
                {
                    return input;
                }
                ApplicationConsole.DisplayMessage("Invalid input is entered. A number is expected.");
            }
        }
    }
}
