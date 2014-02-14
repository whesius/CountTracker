using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace CountTracker2
{
    public sealed partial class CountTrackerSettingsFlyout : SettingsFlyout
    {
        public CountTrackerSettingsFlyout()
        {
            this.InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {

            var listBoxItems = UnitsListBox.Items.Cast<ListBoxItem>().ToList();

            if (!String.IsNullOrWhiteSpace(TextBlockUnit.Text))
            {
                listBoxItems.Add(new ListBoxItem() { Content = TextBlockUnit.Text});
            }

            if (UnitsListBox.Items != null)
            {
                UnitsListBox.Items.Clear();    
            }
            
            foreach (var listBoxItem in listBoxItems)
            {
               UnitsListBox.Items.Add(listBoxItem);
            }
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (UnitsListBox.SelectedItem != null)
            {
                if (UnitsListBox.Items != null) UnitsListBox.Items.Remove(UnitsListBox.SelectedItem);
            }
        }
    }
}
