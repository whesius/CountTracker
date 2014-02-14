using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace CountTracker2
{
    public sealed partial class CountTrackerSettingsFlyout 
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

                if (UnitsListBox.Items != null)
                {
                    UnitsListBox.Items.Clear();
                }

                foreach (var listBoxItem in listBoxItems)
                {
                    UnitsListBox.Items.Add(listBoxItem);
                }
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
