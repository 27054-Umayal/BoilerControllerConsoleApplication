using BoilerControllerApplication.Enums;
using BoilerControllerApplication.Service;

namespace BoilerControllerApplication.Core.Interfaces
{
    //TODO: Add xml comments
    public delegate void StatusUpdate(string message);
    public delegate void TimerUpdate(string message);
    public interface IBoilerService
    {
        public event StatusUpdate? OnStatusUpdate;
        public event TimerUpdate? OnTimerUpdate;
        public bool IsSwitchStatusClosed(SwitchStatusMenu switchStatus);

        public bool IsSwitchStatusOpen(SwitchStatusMenu switchStatus);

        public bool IsBoilerStatusReady(BoilerStatusMenu boilerStatus);

        public bool IsBoilerStatusLockout(BoilerStatusMenu boilerStatus);

        public bool IsBoilerStatusOperational(BoilerStatusMenu boilerStatus);

        public BoilerStatusMenu SetBoilerStatus(BoilerStatusMenu updateToBoilerStatus);

        public Task RunPrePurgeProcess(BoilerStatusMenu boilerStatus);
        
        public Task RunIgnitionProcess(BoilerStatusMenu boilerStatus);
        
        public void RunOperationalProcess(BoilerStatusMenu boilerStatus);
    }
}
