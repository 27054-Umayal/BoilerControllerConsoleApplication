using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BoilerControllerApplication.Core.Models;
using BoilerControllerApplication.Core.Interfaces;
using BoilerControllerApplication.Enums;
using BoilerControllerApplication.Repository;

namespace BoilerControllerApplication.Service
{
    //TODO: Add xml comments

    public class BoilerService : IBoilerService
    {
        public event StatusUpdate? OnStatusUpdate;
        public event TimerUpdate? OnTimerUpdate;

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

        public async Task<BoilerStatusMenu> RunPrePurgeProcess(BoilerStatusMenu boilerStatus)
        {
            await this.RunTimer("Pre-Purge Processing");
            this.OnStatusUpdate?.Invoke($"Pre-Purge completed");
            EventLogModel log = new EventLogModel(DateTime.Now, "Operation Status Updated", "Pre-Purge completed.");
            this.AppendLogDetail(log);
            return BoilerStatusMenu.PrePurge;
        }

        public async Task<BoilerStatusMenu> RunIgnitionProcess(BoilerStatusMenu boilerStatus)
        {
            await this.RunTimer("Ignition Processing");
            this.OnStatusUpdate?.Invoke($"Ignition completed");
            EventLogModel log = new EventLogModel(DateTime.Now, "Operation Status Updated", "Ignition completed.");
            this.AppendLogDetail(log);
            return BoilerStatusMenu.Ignition;
        }

        public BoilerStatusMenu RunOperationalProcess(BoilerStatusMenu boilerStatus)
        {
            this.OnStatusUpdate?.Invoke("Operational");
            EventLogModel log = new EventLogModel(DateTime.Now, "Operation Status Updated", "Boiler now operational.");
            this.AppendLogDetail(log);
            return BoilerStatusMenu.Operational;
        }

        public async Task RunTimer(string boilerStatus, int totalSeconds = 10)
        {
            for (int i  = totalSeconds ; i > 0; i--)
            {
                this.OnTimerUpdate?.Invoke($"Status:{boilerStatus} | Remaining Time:{i} sec");
                await Task.Delay(TimeSpan.FromSeconds(1));
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
