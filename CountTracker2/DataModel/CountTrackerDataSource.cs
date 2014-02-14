using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace CountTracker2.DataModel
{
    public sealed class CountTrackerDataSource
    {
        private static CountTrackerDataSource _countTrackerDataSource = new CountTrackerDataSource();

        private List<Counter> _counters = new List<Counter>();
        public List<Counter> Counters
        {
            get { return this._counters; }
        }

        public static async Task<List<Counter>> GetCountersAsync()
        {
            await _countTrackerDataSource.GetCountTrackerDataAsync();

            return _countTrackerDataSource.Counters;
        }

        public static async Task<Counter> GetCounterAsync(string uniqueId)
        {
            await _countTrackerDataSource.GetCountTrackerDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _countTrackerDataSource.Counters.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<Measurement> GetMeasurementAsync(string uniqueId)
        {
            await _countTrackerDataSource.GetCountTrackerDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _countTrackerDataSource.Counters.SelectMany(counter => counter.Measurements).Where((measurement) => measurement.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        private async Task GetCountTrackerDataAsync()
        {
            if (this._counters.Count != 0)
                return;

            Uri dataUri = new Uri("ms-appx:///DataModel/SampleData/CountTrackerData.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Counters"].GetArray();

            foreach (JsonValue counterValue in jsonArray)
            {
                JsonObject counterObject = counterValue.GetObject();
                Counter counter = new Counter(counterObject["UniqueId"].GetString(),
                                              counterObject["Title"].GetString(),
                                              counterObject["Unit"].GetString(),
                                              DateTime.Parse(counterObject["LastMeasurement"].GetString()),
                                              counterObject["ImagePath"].GetString());

                foreach (JsonValue measurementValue in counterObject["Measurements"].GetArray())
                {
                    JsonObject itemObject = measurementValue.GetObject();

                    var measurement =  new Measurement(  itemObject["UniqueId"].GetString(),
                                                         itemObject["MeasuredValue"].GetString(),
                                                         DateTime.Parse(itemObject["MeasuredDate"].GetString())
                                                       );
                    counter.Measurements.Add(measurement);
                }
                this.Counters.Add(counter);
            }
        }
    }
}