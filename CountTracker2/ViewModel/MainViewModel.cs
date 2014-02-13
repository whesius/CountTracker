using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace CountTracker2.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        private List<Counter> _counters;

        public List<Counter> Counters {
            get {
                    return _counters;
            }
            set {
                _counters = value;
            }
        }

        public Counter Counter { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
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

        public async void LoadCounters()
        {
            Counters = await CountTrackerDataSource.GetCountersAsync();
        }

    }
}