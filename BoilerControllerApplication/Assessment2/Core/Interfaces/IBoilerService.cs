using BoilerControllerApplication.Core.Models;
using BoilerControllerApplication.Enums;

namespace BoilerControllerApplication.Core.Interfaces
{
    //TODO: Add xml comments
    public delegate void StatusUpdate(string message);
    public delegate void TimerUpdate(string message);
    public delegate void CancelUpdate(string message);

    /// <summary>
    /// Handles the core boiler operations.
    /// </summary>
    public interface IBoilerService
    {
        public event StatusUpdate? OnStatusUpdate;
        public event TimerUpdate? OnTimerUpdate;
        public event CancelUpdate? OnCancelUpdate;
        /// <summary>
        /// Updates the switch status to closed.
        /// </summary>
        /// <param name="switchStatus"></param>
        /// <returns></returns>
        public bool IsSwitchStatusClosed(SwitchStatusMenu switchStatus);

        public bool IsSwitchStatusOpen(SwitchStatusMenu switchStatus);

        public bool IsBoilerStatusReady(BoilerStatusMenu boilerStatus);

        public bool IsBoilerStatusLockout(BoilerStatusMenu boilerStatus);

        public bool IsBoilerStatusOperational(BoilerStatusMenu boilerStatus);

        public BoilerStatusMenu SetBoilerStatus(BoilerStatusMenu updateToBoilerStatus);

        public Task<BoilerStatusMenu> RunPrePurgeProcess(BoilerStatusMenu boilerStatus);
        
        public Task<BoilerStatusMenu> RunIgnitionProcess(BoilerStatusMenu boilerStatus);
        
        public BoilerStatusMenu RunOperationalProcess(BoilerStatusMenu boilerStatus);

        public void SetCancelled();

        public List<EventLogModel> LoadLogDetail();
        public void AppendLogDetail(EventLogModel log);
    }
}
