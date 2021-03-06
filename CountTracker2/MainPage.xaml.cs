﻿using System;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using CountTracker2.Common;

// The Grouped Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234231
using CountTracker2.DataModel;
using CountTracker2.ViewModel;

namespace CountTracker2
{
    /// <summary>
    ///     A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        private readonly NavigationHelper navigationHelper;
        
        public MainPage()
        {
            InitializeComponent();
            navigationHelper = new NavigationHelper(this);
            navigationHelper.LoadState += navigationHelper_LoadState;

            SettingsPane.GetForCurrentView().CommandsRequested += GroupedItemsPage_CommandsRequested;
        }

        /// <summary>
        ///     NavigationHelper is used on each page to aid in navigation and
        ///     process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return navigationHelper; }
        }

        /// <summary>
        ///     This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return defaultViewModel; }
        }

        private void GroupedItemsPage_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            var unitSetting =
                new SettingsCommand("CountTrackerSettingsFlyout", "Units", handler =>
                {
                    var settingsFlyout = new CountTrackerSettingsFlyout();
                    settingsFlyout.Show();
                });

                args.Request.ApplicationCommands.Clear();
                args.Request.ApplicationCommands.Add(unitSetting);
            
        }

        /// <summary>
        ///     Populates the page with content passed during navigation.  Any saved state is also
        ///     provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        ///     The source of the event; typically <see cref="NavigationHelper" />
        /// </param>
        /// <param name="e">
        ///     Event data that provides both the navigation parameter passed to
        ///     <see cref="Frame.Navigate(Type, Object)" /> when this page was initially requested and
        ///     a dictionary of state preserved by this page during an earlier
        ///     session.  The state will be null the first time a page is visited.
        /// </param>
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        ///     Invoked when a group header is clicked.
        /// </summary>
        /// <param name="sender">The Button used as a group header for the selected group.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        private void Header_Click(object sender, RoutedEventArgs e)
        {
            // Determine what group the Button instance represents
            object counter = (sender as FrameworkElement).DataContext;

            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            Frame.Navigate(typeof (CounterPage), ((Counter) counter).UniqueId);
        }

        /// <summary>
        ///     Invoked when an item within a group is clicked.
        /// </summary>
        /// <param name="sender">
        ///     The GridView (or ListView when the application is snapped)
        ///     displaying the item clicked.
        /// </param>
        /// <param name="e">Event data that describes the item clicked.</param>
        private void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            string itemId = ((Counter) e.ClickedItem).UniqueId;

            Frame.Navigate(typeof (CounterPage), itemId);
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the
        /// <see cref="GridCS.Common.NavigationHelper.LoadState" />
        /// and
        /// <see cref="GridCS.Common.NavigationHelper.SaveState" />
        /// .
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void AppBarButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (CountersGridView.SelectedItem != null)
            {
                var selectedItem = (Counter)CountersGridView.SelectedItem;
                var dataContext = (MainViewModel)CountersGridView.DataContext;
                dataContext.Counters.Remove((selectedItem));
            }
        }
    }
}