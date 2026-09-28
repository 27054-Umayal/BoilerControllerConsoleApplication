using BoilerControllerApplication.Core.Interfaces;
using BoilerControllerApplication.Core.Models;
using BoilerControllerApplication.Enums;

namespace BoilerControllerApplication.Service
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>

    public class BoilerService : IBoilerService
    {
        public event StatusUpdate? OnStatusUpdate;
        public event TimerUpdate? OnTimerUpdate;
        public event CancelUpdate? OnCancelUpdate;

        private readonly ILogRepo _logRepo;

        public BoilerService(ILogRepo logRepo)
        {
            this._logRepo = logRepo;
        }
        public bool IsSwitchStatusClosed(SwitchStatusMenu switchStatus)
        {
            return SwitchStatusMenu.Close == switchStatus;
        }

        public bool IsSwitchStatusOpen(SwitchStatusMenu switchStatus)
        {
            return SwitchStatusMenu.Open == switchStatus;
        }

        public bool IsBoilerStatusReady(BoilerStatusMenu boilerStatus)
        {
            return BoilerStatusMenu.Ready == boilerStatus;
        }

        public bool IsBoilerStatusLockout(BoilerStatusMenu boilerStatus)
        {
            return BoilerStatusMenu.Lockout == boilerStatus;
        }

        public bool IsBoilerStatusOperational(BoilerStatusMenu boilerStatus)
        {
            return BoilerStatusMenu.Operational == boilerStatus;
        }

        public BoilerStatusMenu SetBoilerStatus(BoilerStatusMenu updateToBoilerStatus)
        { 
            return updateToBoilerStatus;
        }

        public void SetCancelled(CancellationTokenSource cts)
        {
            this.OnCancelUpdate?.Invoke("Operation interrupted...");
            cts.Cancel();
        }

        public async Task<BoilerStatusMenu> RunPrePurgeProcess(BoilerStatusMenu boilerStatus, CancellationToken token)
        {
            await this.RunTimer("Pre-Purge Processing", token);
            if(!token.IsCancellationRequested)
            {
                this.OnStatusUpdate?.Invoke($"Pre-Purge completed");
                EventLogModel log = new EventLogModel(DateTime.Now, "Operation Status Updated", "Pre-Purge completed.");
                this.AppendLogDetail(log);
                return BoilerStatusMenu.PrePurge;
            }
            else
            {
                return BoilerStatusMenu.Lockout;
            }
        }

        public async Task<BoilerStatusMenu> RunIgnitionProcess(BoilerStatusMenu boilerStatus, CancellationToken token)
        {
            await this.RunTimer("Ignition Processing", token);
            if(!token.IsCancellationRequested)
            {
                this.OnStatusUpdate?.Invoke($"Ignition completed");
                EventLogModel log = new EventLogModel(DateTime.Now, "Operation Status Updated", "Ignition completed.");
                this.AppendLogDetail(log);
                return BoilerStatusMenu.Ignition;
            }
            else
            {
                return BoilerStatusMenu.Lockout;
            }
        }

        public BoilerStatusMenu RunOperationalProcess(BoilerStatusMenu boilerStatus, CancellationToken token)
        {
            if (!token.IsCancellationRequested)
            {
                this.OnStatusUpdate?.Invoke("Operational");
                EventLogModel log = new EventLogModel(DateTime.Now, "Operation Status Updated", "Boiler now operational.");
                this.AppendLogDetail(log);
                return BoilerStatusMenu.Operational;
            }
            else
            {
                return BoilerStatusMenu.Lockout;
            }
        }

        public async Task RunTimer(string boilerStatus, CancellationToken token, int totalSeconds = 10)
        {
            for (int i  = totalSeconds ; i > 0; i--)
            {
               if(!token.IsCancellationRequested)
               {
                    this.OnTimerUpdate?.Invoke($"Status:{boilerStatus} | Remaining Time:{i} sec");
                    await Task.Delay(TimeSpan.FromSeconds(1));
               }
            }
        }

        public List<EventLogModel> LoadLogDetail()
        {
            return this._logRepo.LoadLogDetail();
        }

        public void AppendLogDetail(EventLogModel log)
        {
            this._logRepo.AppendLogDetail(log);
        }
    }
}
