using BoilerControllerApplication.Enums;

namespace BoilerControllerApplication.Core.Interfaces
{
    //TODO: Add xml comments
    public interface IBoilerService
    {
        public bool IsSwitchStatusClosed(SwitchStatusMenu switchStatus);

        public bool IsSwitchStatusOpen(SwitchStatusMenu switchStatus);

        public bool IsBoilerStatusReady(BoilerStatusMenu boilerStatus);

        public bool IsBoilerStatusLockout(BoilerStatusMenu boilerStatus);
    }
}
