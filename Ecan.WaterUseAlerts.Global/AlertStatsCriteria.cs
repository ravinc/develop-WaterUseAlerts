using System;
using System.Runtime.Serialization;

namespace Ecan.WaterUseAlerts.Global
{
    public class AlertStatsCriteria
    {

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }
    }
}
