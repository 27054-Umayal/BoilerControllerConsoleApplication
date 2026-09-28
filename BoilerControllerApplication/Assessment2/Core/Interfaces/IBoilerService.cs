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
        /// Checks if the switch status is closed.
        /// </summary>
        /// <param name="switchStatus">The closed status.</param>
        /// <returns>True if closed; otherwise false.</returns>
        public bool IsSwitchStatusClosed(SwitchStatusMenu switchStatus);

        public bool IsSwitchStatusOpen(SwitchStatusMenu switchStatus);

        public bool IsBoilerStatusReady(BoilerStatusMenu boilerStatus);

        public bool IsBoilerStatusLockout(BoilerStatusMenu boilerStatus);

        public bool IsBoilerStatusOperational(BoilerStatusMenu boilerStatus);

        public BoilerStatusMenu SetBoilerStatus(BoilerStatusMenu updateToBoilerStatus);

        public Task<BoilerStatusMenu> RunPrePurgeProcess(BoilerStatusMenu boilerStatus, CancellationToken token);
        
        public Task<BoilerStatusMenu> RunIgnitionProcess(BoilerStatusMenu boilerStatus, CancellationToken token);
        
        public BoilerStatusMenu RunOperationalProcess(BoilerStatusMenu boilerStatus, CancellationToken token);

        public void SetCancelled(CancellationTokenSource cts);

        public Task RunTimer(string boilerStatus, CancellationToken token, int totalSeconds = 10);

        public List<EventLogModel> LoadLogDetail();
        public void AppendLogDetail(EventLogModel log);
    }
}
