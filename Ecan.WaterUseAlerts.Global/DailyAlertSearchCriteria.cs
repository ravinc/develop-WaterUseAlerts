using System;
using System.Runtime.Serialization;

namespace Ecan.WaterUseAlerts.Global
{
    public class DailyAlertSearchCriteria
    {

        [DataMember]
        public string IgnoreAll { get; set; }

        [DataMember]
        public string ConsentID { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string ActivityNumber { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }
    }
}
