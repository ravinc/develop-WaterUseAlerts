using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Ecan.WaterUseAlerts.Global
{
    public class DailyAlertEntity
    {

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string ServiceProvider { get; set; }

        [DataMember]
        public string ConsentID { get; set; }

        [DataMember]
        public string ActivityNumber { get; set; }

        [DataMember]
        public string WaterUseType { get; set; }

        [DataMember]
        public string Limit { get; set; }

        [DataMember]
        public string MaxUse { get; set; }

        [DataMember]
        public double MaxUseNumeric
        {
            get
            {
                double value = 0;
                if (!string.IsNullOrEmpty(this.MaxUse))
                {
                    bool success = double.TryParse(new string(this.MaxUse
                                         .SkipWhile(x => !char.IsDigit(x))
                                         .TakeWhile(x => char.IsDigit(x) || x.Equals('.'))
                                         .ToArray()), out value);
                }
                return value;
            }
        }

        [DataMember]
        public string Meters { get; set; }

        [DataMember]
        public string Type { get; set; }

        public string DisplayableType
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Type))
                {
                    if (this.Type.Equals("Water Year Volume"))
                        return "Year Volume";
                    if (this.Type.Equals("Take Rate - Low Flow Ban"))
                        return "Low Flow Ban";
                }
                return this.Type;
            }
        }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public int Ignore { get; set; }

        [DataMember]
        public string IgnoreAsText
        {
            get
            {
                return (this.Ignore == 0) ? "No" : "Yes";
            }
        }

        [DataMember]
        public string MetersTruncated
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Meters) && this.Meters.Length > 35)
                    return this.Meters.Substring(0, 32) + " ..";
                return this.Meters;
            }
        }

        [DataMember]
        public string IgnoreReason { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public string Username{ get; set; }

        [DataMember]
        public string PercentOverLimit { get; set; }

        [DataMember]
        public DateTime MaxAvDate { get; set; }

        [DataMember]
        public DateTime? FirstNCDate { get; set; }

        [DataMember]
        public DateTime? LastNCDate { get; set; }

        [DataMember]
        public DateTime RunDate { get; set; }

        [DataMember]
        public DateTime LastModifiedDate { get; set; }


        [DataMember]
        public bool IsRmoaStatus
        {
            get
            {
                return string.IsNullOrEmpty(this.Status) || this.Status.Contains("Not Set") || this.Status.Contains("Investigate");
            }
        }

        /**
         * All sort needs to happen in decsending order to get the highest values first
         **/
        [DataMember]
        public int SortIndex
        {
            get
            {
                if (this.Type.Contains("Low Flow Ban"))
                    return 9;
                if (this.Type.Contains("Water Year Volume"))
                    return 8;
                return 0;
            }
        }

        public bool IsForRmoa()
        {
            return this.IsRmoaStatus
                && this.Ignore.Equals(0)
                && this.IsRmoaStatus;
        }



    }
}
