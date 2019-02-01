using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Ecan.WaterUseAlerts.Global
{
    public class ConsentIgnoreEntity
    {

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string ConsentID { get; set; }

        [DataMember]
        public string IgnoreReason { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public string Username{ get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

    }
}
