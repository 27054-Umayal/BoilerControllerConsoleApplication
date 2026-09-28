namespace BoilerControllerApplication.Core.Models
{
    //TODO: Add xml comments
    public class EventLogModel
    {
        public EventLogModel(DateTime timeStamp, string eventName, string eventData)
        {
            TimeStamp = timeStamp;
            Event = eventName;
            EventData = eventData;
        }

        public DateTime TimeStamp { get; set; }
        public string Event { get; set; } = string.Empty;
        public string EventData {  get; set; } = string.Empty;
    }
}
