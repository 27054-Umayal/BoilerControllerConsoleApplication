using BoilerControllerApplication.Core.Interfaces;
using BoilerControllerApplication.Enums;

namespace BoilerControllerApplication.Service
{
    //TODO: Add xml comments
    public class BoilerService : IBoilerService
    {
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
    }
}
