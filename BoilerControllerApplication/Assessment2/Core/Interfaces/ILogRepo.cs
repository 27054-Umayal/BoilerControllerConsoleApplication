using BoilerControllerApplication.Core.Models;

namespace BoilerControllerApplication.Core.Interfaces
{
    /// <summary>
    /// Deals with the logger details.
    /// </summary>
    public interface ILogRepo
    {
        /// <summary>
        /// Loads the log detail.
        /// </summary>
        /// <returns>The events logged.</returns>
        public List<EventLogModel> LoadLogDetail();

        /// <summary>
        /// Appends the log detail.
        /// </summary>
        /// <param name="log">The event detail to log.</param>
        public void AppendLogDetail(EventLogModel log);
    }
}
