using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Ecan.WaterUseAlerts.Global
{
    public class AlertStatsEntity
    {

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public string StatsType{ get; set; }

    }
}
