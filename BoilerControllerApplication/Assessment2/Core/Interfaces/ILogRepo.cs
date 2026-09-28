using BoilerControllerApplication.Core.Models;

namespace BoilerControllerApplication.Core.Interfaces
{
    //TODO: Add xml comments
    public interface ILogRepo
    {
        public List<EventLogModel> LoadLogDetail();
        public void AppendLogDetail(EventLogModel log);
    }
}
