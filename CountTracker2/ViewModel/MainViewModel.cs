using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using CountTracker2.DataModel;
using GalaSoft.MvvmLight;

namespace CountTracker2.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    ///     <para>
    ///         You can also use Blend to data bind with the tool's support.
    ///     </para>
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private List<Counter> _counters;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            LoadCounters();
        }

        public List<Counter> Counters
        {
            get { return _counters; }
            set
            {
                _counters = value;
                UpdateTile();
            }
        }

        public Counter Counter { get; set; }

        private void UpdateTile()
        {
            // Update 150x150
            XmlDocument xmlDoc = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text01);
            XmlNodeList nodeText = xmlDoc.GetElementsByTagName("text");

            // Show the number of days since the last update
            Counter lastUpdated = _counters.OrderByDescending(c => c.LastMeasurement).FirstOrDefault();
            if (lastUpdated != null)
            {
                nodeText[0].InnerText = string.Format("{0} days ago",
                    ((int) (lastUpdated.LastMeasurement - DateTime.Now).TotalDays));

                var tileNotification = new TileNotification(xmlDoc);

                TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
            }

            // Update 310x150
            xmlDoc = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text01);
            nodeText = xmlDoc.GetElementsByTagName("text");

            if (lastUpdated != null)
            {
                nodeText[0].InnerText = string.Format("{0} days ago",
                    ((int) (lastUpdated.LastMeasurement - DateTime.Now).TotalDays));

                var tileNotification = new TileNotification(xmlDoc);

                TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
            }
        }

        public async void LoadCounters()
        {
            Counters = await CountTrackerDataSource.GetCountersAsync();
        }
    }
}