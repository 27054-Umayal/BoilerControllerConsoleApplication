namespace BoilerControllerApplication.Core.Models
{
    //TODO: Add xml comments
    public class EventLogModel
    {
        public DateTime TimeStamp { get; set; }
        public string Event { get; set; } = string.Empty;
        public string EventData {  get; set; } = string.Empty;
    }
}
