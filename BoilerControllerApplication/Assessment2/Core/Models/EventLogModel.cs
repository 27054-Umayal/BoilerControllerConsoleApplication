namespace BoilerControllerApplication.Core.Models
{
    /// <summary>
    /// Demonstrates the event log model components.
    /// </summary>
    public class EventLogModel
    {
        /// <summary>
        /// Initializes the event log model components.
        /// </summary>
        /// <param name="timeStamp">The timestamp of the log.</param>
        /// <param name="eventName">The event name of the log.</param>
        /// <param name="eventData">The event data of the log.</param>
        public EventLogModel(DateTime timeStamp, string eventName, string eventData)
        {
            TimeStamp = timeStamp;
            Event = eventName;
            EventData = eventData;
        }

        /// <summary>
        /// Gets or sets the timestamp value.
        /// </summary>
        /// <value>The timestamp value to be set.</value>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the event value.
        /// </summary>
        /// <value>The event name value to be set.</value>
        public string Event { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the event value.
        /// </summary>
        /// <value>The event data to be set.</value>
        public string EventData {  get; set; } = string.Empty;
    }
}
