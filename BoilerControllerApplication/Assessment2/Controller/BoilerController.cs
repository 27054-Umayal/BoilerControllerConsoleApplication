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
            this._boilerService.OnStatusUpdate += this.DisplayStatusUpdate;
            this._boilerService.OnTimerUpdate += this.DisplayTimerUpdate;
        }

        public async Task RunMainMenu()
        {
            ApplicationConsole.DisplayMessage("Welcome to Boiler Controller Application");
            StartMenu choice = default;
            do
            {
                ApplicationConsole.DisplayMessage("----Boiler Menu Options----");
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
                        await this.RunStartBoilerSequence();
                        break;
                    case StartMenu.StopBoilerSequence:
                        this.RunStopBoilerSequence();
                        break;
                    case StartMenu.SimulateBoilerSequence:
                        this.RunSimulateBoilerError();
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

        private async Task RunStartBoilerSequence()
        {
            if(this._boilerService.IsSwitchStatusClosed(switchStatus) && this._boilerService.IsBoilerStatusReady(boilerStatus))
            {
                ApplicationConsole.DisplayMessage("Starting the boiler process...");
                await this._boilerService.RunPrePurgeProcess(boilerStatus);
                await this._boilerService.RunIgnitionProcess(boilerStatus);
                this._boilerService.RunOperationalProcess(boilerStatus);
            }
            else
            {
                ApplicationConsole.DisplayMessage("The Boiler cannot be started when the switch is in closed state or the boiler is not in ready state.");
            }
        }

        private void RunStopBoilerSequence()
        {
            this.boilerStatus = this._boilerService.SetBoilerStatus(BoilerStatusMenu.Lockout);
            //TODO: Log
        }

        private void RunSimulateBoilerError()
        {
            if(this._boilerService.IsBoilerStatusOperational(boilerStatus))
            {
                this.boilerStatus = this._boilerService.SetBoilerStatus(BoilerStatusMenu.Lockout);
                Console.WriteLine(boilerStatus);
                //TODO: Log
            }

            else
            {
                ApplicationConsole.DisplayMessage("The boiler is in processing state can't do a lockout.");
                //TODO: Log

            }
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

        private void DisplayStatusUpdate(string message)
        {
            ApplicationConsole.DisplayMessage(message);
        }

        private void DisplayTimerUpdate(string message)
        {
            ApplicationConsole.DisplayMessage(message);
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
