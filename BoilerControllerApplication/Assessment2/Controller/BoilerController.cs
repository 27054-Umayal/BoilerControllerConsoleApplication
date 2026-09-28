using BoilerControllerApplication.Core.Interfaces;
using BoilerControllerApplication.Core.Models;
using BoilerControllerApplication.Enums;
using BoilerControllerApplication.View;
namespace BoilerControllerApplication.Controller
{
    /// <summary>
    /// Serves as layer between the view and the service.
    /// </summary>
    public class BoilerController
    {
        public static CancellationTokenSource cts = new CancellationTokenSource();
        public SwitchStatusMenu switchStatus = SwitchStatusMenu.Open;
        public BoilerStatusMenu boilerStatus = BoilerStatusMenu.Lockout;
        public bool isInterrupted = false;
        private readonly IBoilerService _boilerService; 


        public BoilerController(IBoilerService boilerService)
        {
            this._boilerService = boilerService;
            this._boilerService.OnStatusUpdate += this.DisplayStatusUpdate;
            this._boilerService.OnTimerUpdate += this.DisplayTimerUpdate;
            this._boilerService.OnCancelUpdate += this.DisplayCancelUpdate;

        }

        /// <summary>
        /// Runs the main menu and the corresponding operations based on users choice.
        /// </summary>
        public void RunMainMenu()
        {
            try
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
                    CancellationToken token = cts.Token;
                    switch (choice)
                    {
                        case StartMenu.StartBoilerSequence:
                            _ = this.RunStartBoilerSequence(token);
                            break;
                        case StartMenu.StopBoilerSequence:
                            this.RunStopBoilerSequence(token);
                            break;
                        case StartMenu.SimulateBoilerSequence:
                            this.RunSimulateBoilerError(token);
                            break;
                        case StartMenu.ToggleRunInterLockSwitch:
                            this.RunToggleInterLockSwitch(token);
                            break;
                        case StartMenu.ResetLockout:
                            this.RunResetLockout();
                            break;
                        case StartMenu.ViewEventLog:
                            this.ViewEventLog();
                            break;
                        case StartMenu.Exit:
                            ApplicationConsole.DisplayMessage("Exiting the application, Thankyou!");
                            break;
                    }

                }
                while (choice != StartMenu.Exit);
            }
            catch(IOException ex)
            {
                ApplicationConsole.DisplayMessage($"IO error: {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                ApplicationConsole.DisplayMessage($"Unauthorized access error: {ex.Message}");
            }
            catch (Exception ex)
            {
                ApplicationConsole.DisplayMessage($"Error Occurred: {ex.Message}");
            }
            
        }

        private async Task RunStartBoilerSequence(CancellationToken token)
        {
            if(this._boilerService.IsSwitchStatusClosed(switchStatus) && this._boilerService.IsBoilerStatusReady(boilerStatus))
            {
                ApplicationConsole.DisplayMessage("Starting the boiler process...");
                boilerStatus = await this._boilerService.RunPrePurgeProcess(boilerStatus, token);
                boilerStatus = await this._boilerService.RunIgnitionProcess(boilerStatus, token);
                boilerStatus = this._boilerService.RunOperationalProcess(boilerStatus, token);
            }
            else
            {
                ApplicationConsole.DisplayMessage("The Boiler cannot be started when the switch is in closed state or the boiler is not in ready state.");
                EventLogModel log = new EventLogModel(DateTime.Now, "Error Detected", "Boiler Status cannot be started.");
                this._boilerService.AppendLogDetail(log);
            }
        }

        private void RunStopBoilerSequence(CancellationToken token)
        {
            this.boilerStatus = this._boilerService.SetBoilerStatus(BoilerStatusMenu.Lockout);
            this._boilerService.SetCancelled(cts);
            ApplicationConsole.DisplayMessage("Boiler Status changed to Lockout.");
            EventLogModel log = new EventLogModel(DateTime.Now, "Operation Status Updated", "Boiler Status changed to Lockout.");
            this._boilerService.AppendLogDetail(log);
        }

        private void RunSimulateBoilerError(CancellationToken token)
        {
            if(this._boilerService.IsBoilerStatusOperational(boilerStatus))
            {
                this.boilerStatus = this._boilerService.SetBoilerStatus(BoilerStatusMenu.Lockout);
                this._boilerService.SetCancelled(cts);
                ApplicationConsole.DisplayMessage("Boiler Status changed to Lockout.");
                EventLogModel log = new EventLogModel(DateTime.Now, "Operation Status Updated", "Boiler Status changed to Lockout.");
                this._boilerService.AppendLogDetail(log);
            }

            else
            {
                ApplicationConsole.DisplayMessage("The boiler is not in processing state can't do a lockout.");
                EventLogModel log = new EventLogModel(DateTime.Now, "Error Detected", "Boiler Status cannot be changed to Lockout.");
                this._boilerService.AppendLogDetail(log);

            }
        }

        private void RunResetLockout()
        {
            if(this._boilerService.IsSwitchStatusClosed(switchStatus))
            {
                this.boilerStatus = BoilerStatusMenu.Ready;
                ApplicationConsole.DisplayMessage("Boiler Status changed to Ready.");
                EventLogModel log = new EventLogModel(DateTime.Now, "Operation Status Updated", "Boiler Status changed to Ready.");
                this._boilerService.AppendLogDetail(log);
            }

            else
            {
                ApplicationConsole.DisplayMessage("Switch must be closed to perform the operation.");
                EventLogModel log = new EventLogModel(DateTime.Now, "Error Detected", "Switch must be closed to do the operation.");
                this._boilerService.AppendLogDetail(log);
            }
        }

        private void RunToggleInterLockSwitch(CancellationToken token)
        {
            if(this._boilerService.IsSwitchStatusOpen(switchStatus))
            {
                this.switchStatus = SwitchStatusMenu.Close;
                ApplicationConsole.DisplayMessage("Interlock switch toggled to close.");
                EventLogModel log = new EventLogModel(DateTime.Now, "Operation Status Updated", "Interlock Switch toggled to Close.");
                this._boilerService.AppendLogDetail(log);
            }

            else
            {
                if(this._boilerService.IsBoilerStatusReady(boilerStatus) || this._boilerService.IsBoilerStatusLockout(boilerStatus))
                {
                    this.switchStatus = SwitchStatusMenu.Open;
                    this._boilerService.SetCancelled(cts);
                    ApplicationConsole.DisplayMessage("Interlock Switch toggled to Open.");
                    EventLogModel log = new EventLogModel(DateTime.Now, "Operation Status Updated", "Interlock Switch toggled to Open.");
                    this._boilerService.AppendLogDetail(log);
                }

                else
                {
                    ApplicationConsole.DisplayMessage("Switch must be in Lockout/Ready state to perform the operation.");
                    EventLogModel log = new EventLogModel(DateTime.Now, "Error Detected", "Switch must be in Lockout/Ready state to do the operation.");
                    this._boilerService.AppendLogDetail(log);
                }
            }
        }

        private void ViewEventLog()
        {
            try
            {
                List<EventLogModel> historyOfLog = this._boilerService.LoadLogDetail();
                if (historyOfLog == null || !historyOfLog.Any())
                {
                    ApplicationConsole.DisplayMessage("No history of logs yet..");
                    return;
                }

                ApplicationConsole.DisplayLogs(historyOfLog);
            }
            catch(InvalidDataException ex)
            {
                ApplicationConsole.DisplayMessage($"Invalid data error:{ex.Message}");
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

        private void DisplayCancelUpdate(string message)
        {
            ApplicationConsole.DisplayMessage(message);
            this.isInterrupted = true;
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
