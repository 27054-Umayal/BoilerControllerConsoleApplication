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
        /// <param name="switchStatus">The current status to check.</param>
        /// <returns>True if closed; otherwise false.</returns>
        public bool IsSwitchStatusClosed(SwitchStatusMenu switchStatus);

        /// <summary>
        /// Checks if the switch status is open.
        /// </summary>
        /// <param name="switchStatus">The current status to check.</param>
        /// <returns>True if closed; otherwise false.</returns>
        public bool IsSwitchStatusOpen(SwitchStatusMenu switchStatus);

        /// <summary>
        /// Checks if the boiler status is ready.
        /// </summary>
        /// <param name="boilerStatus">The current status to check.</param>
        /// <returns>True if closed; otherwise false.</returns>
        public bool IsBoilerStatusReady(BoilerStatusMenu boilerStatus);

        /// <summary>
        /// Checks if the boiler status is lockout.
        /// </summary>
        /// <param name="boilerStatus">The current status to check.</param>
        /// <returns>True if closed; otherwise false.</returns>
        public bool IsBoilerStatusLockout(BoilerStatusMenu boilerStatus);

        /// <summary>
        /// Checks if the boiler status is operational.
        /// </summary>
        /// <param name="boilerStatus">The current status to check.</param>
        /// <returns>True if closed; otherwise false.</returns>
        public bool IsBoilerStatusOperational(BoilerStatusMenu boilerStatus);

        /// <summary>
        /// Updates the boiler status.
        /// </summary>
        /// <param name="updateToBoilerStatus">The boiler status to be updated.</param>
        /// <returns>The updated boiler status menu.</returns>
        public BoilerStatusMenu SetBoilerStatus(BoilerStatusMenu updateToBoilerStatus);

        /// <summary>
        /// Executes the pre purge process.
        /// </summary>
        /// <param name="boilerStatus">The boiler status to be updated.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The async task that represents the boiler status.</returns>
        public Task<BoilerStatusMenu> RunPrePurgeProcess(BoilerStatusMenu boilerStatus, CancellationToken token);

        /// <summary>
        /// Executes the ignition process.
        /// </summary>
        /// <param name="boilerStatus">The boiler status to be updated.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The async task that represents the boiler status.</returns>
        public Task<BoilerStatusMenu> RunIgnitionProcess(BoilerStatusMenu boilerStatus, CancellationToken token);

        /// <summary>
        /// Executes the operational process.
        /// </summary>
        /// <param name="boilerStatus">The boiler status to be updated.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The updated boiler status menu.</returns>
        public BoilerStatusMenu RunOperationalProcess(BoilerStatusMenu boilerStatus, CancellationToken token);

        /// <summary>
        /// Updates of cancellation.
        /// </summary>
        /// <param name="cts">The Cancellation Token Source.</param>
        public void SetCancelled(CancellationTokenSource cts);

        /// <summary>
        /// Executes the run timer.
        /// </summary>
        /// <param name="boilerStatus">The boiler status.</param>
        /// <param name="token">The cancellation token.</param>
        /// <param name="totalSeconds">The total seconds.</param>
        /// <returns></returns>
        public Task RunTimer(string boilerStatus, CancellationToken token, int totalSeconds = 10);

        /// <summary>
        /// The load log detail method.
        /// </summary>
        /// <returns>Event log history.</returns>
        public List<EventLogModel> LoadLogDetail();

        /// <summary>
        /// The appending of the log detail.
        /// </summary>
        /// <param name="log">Event log detail.</param>
        public void AppendLogDetail(EventLogModel log);
    }
}
