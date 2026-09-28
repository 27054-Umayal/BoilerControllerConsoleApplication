using BoilerControllerApplication.Core.Interfaces;
using BoilerControllerApplication.Core.Models;
using BoilerControllerApplication.Constants;
using System.Globalization;

namespace BoilerControllerApplication.Repository
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public class LogRepo : ILogRepo
    {
        public LogRepo()
        {
            if (!File.Exists(FileConstants.BoilerLogFilePath))
            {
                File.WriteAllText(FileConstants.BoilerLogFilePath, FileConstants.BoilerLogHeaderDetail + Environment.NewLine);
            }

        }
        public List<EventLogModel> LoadLogDetail()
        {
            string[] lines = File.ReadAllLines(FileConstants.BoilerLogFilePath);
            if(lines.Length == FileConstants.HeaderLineCount) 
            {
                throw new InvalidDataException("File is empty");
            }
            string[] header = lines[0].Split(',');
            if(header.Length != FileConstants.BoilerHeaderDetailLength)
            {
                throw new InvalidDataException("Invalid Header data");
            }
            List<EventLogModel> historyOfLog = new List<EventLogModel>();
            for(int i = 1; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                if(values.Length != FileConstants.BoilerHeaderDetailLength)
                {
                    throw new InvalidDataException("Invalid data");
                }

                EventLogModel eventLog = new EventLogModel(DateTime.Parse(values[0], CultureInfo.InvariantCulture), values[1], values[2]);
                historyOfLog.Add(eventLog);
            }
            return historyOfLog;
        }

        public void AppendLogDetail(EventLogModel log)
        {
            string line = $"{log.TimeStamp.ToString(CultureInfo.InvariantCulture)}," +
                $"{log.Event}," +
                $"{log.EventData}" +
                Environment.NewLine;
            File.AppendAllText(FileConstants.BoilerLogFilePath, line);
        }
    }
}
