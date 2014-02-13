using System;

namespace CountTracker2
{
    /// <summary>
    /// Measurement
    /// </summary>
    public class Measurement
    {
        public Measurement(String uniqueId, String value, DateTime measuredDate)
        {
            this.UniqueId = uniqueId;
            this.MeasuredValue = value;
            this.MeasuredDate = measuredDate;         
        }

        public string UniqueId { get; set; }
        public string MeasuredValue { get; set; }      
        public DateTime MeasuredDate { get; set; }
      
        public override string ToString()
        {
            return string.Format("{0} on {1}", this.MeasuredValue, this.MeasuredDate);
        }
    }
}