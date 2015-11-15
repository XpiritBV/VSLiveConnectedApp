using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VSLiveConnectedApp.Data
{
    public class Slot
    {
        public DateTime StartTime { get; set; }

        public TimeSpan Duration { get; set; }

        public string Title { get; set; }

        [JsonIgnore]
        public string DayFormatted
        {
            get { return StartTime.Date.ToString("D"); }
        }

        [JsonIgnore]
        public string StartTimeFormatted
        {
            get { return StartTime.ToString("HH:mm"); }
        }

        [JsonIgnore]
        public string EndTimeFormatted
        {
            get { return StartTime.Add(Duration).ToString("HH:mm"); }
        }

    }
}
