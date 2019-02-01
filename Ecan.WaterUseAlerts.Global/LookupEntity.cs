using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Ecan.WaterUseAlerts.Global
{
    public class LookupEntity
    {

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Description{ get; set; }

    }
}
