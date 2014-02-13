using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app 
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
// is first launched.

namespace CountTracker2
{
    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
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