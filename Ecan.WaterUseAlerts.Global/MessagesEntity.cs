using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Ecan.WaterUseAlerts.Global
{
    [Serializable]
    [DataContract]
    public class MessagesEntity
    {
        public MessagesEntity() 
        {
            Debug = new List<string>();
            Info  = new List<string>();
            Warn  = new List<string>();
            Error = new List<string>();
        }

        [DataMember]
        public List<string> Debug { get; set; }

        [DataMember]
        public List<string> Info { get; set; }

        [DataMember]
        public List<string> Warn { get; set; }

        [DataMember]
        public List<string> Error { get; set; }

        public MessagesEntity Add(MessagesEntity msgs)
        {
            if (msgs != null)
            {
                this.Debug.AddRange(msgs.Debug);
                this.Info.AddRange(msgs.Info);
                this.Warn.AddRange(msgs.Warn);
                this.Error.AddRange(msgs.Error);

                //if (msgs.Debug.Count > 0) { this.Debug.AddRange(msgs.Debug); }
                //if (msgs.Info.Count > 0) { this.Debug.AddRange(msgs.Info); }
                //if (msgs.Warn.Count > 0) { this.Debug.AddRange(msgs.Warn); }
                //if (msgs.Error.Count > 0) { this.Debug.AddRange(msgs.Error); }
            }

            return this;
        }
    }
}
