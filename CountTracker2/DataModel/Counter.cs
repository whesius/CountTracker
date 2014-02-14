using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CountTracker2.DataModel
{
    /// <summary>
    /// Counter
    /// </summary>
    public class Counter
    {
        public Counter(String uniqueId, String title, String unit, DateTime lastMeasurement, String imagePath)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Unit = unit;
            this.LastMeasurement = lastMeasurement;
            this.ImagePath = imagePath;
            this.Measurements = new List<Measurement>();
        }

        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }
        public string ImagePath { get; set; }
        public DateTime LastMeasurement { get; set; }
        public List<Measurement> Measurements { get; set; }

        public string LastInfo
        {
            get
            {
                var lastMeasurement = Measurements.OrderBy(ms => ms.MeasuredDate).LastOrDefault();
                if (lastMeasurement != null)
                {
                    return string.Format("{0} {1} on {2}", lastMeasurement.MeasuredValue, Unit, lastMeasurement.MeasuredDate.ToString(CultureInfo.CurrentUICulture));
                }
                return "No measurements";

            }
        }
        public override string ToString()
        {
            return this.Title;
        }
    }
}