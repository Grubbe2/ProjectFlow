using CharlotteSchou_Booking.View;
using CharlotteSchou_Booking.Viewmodels;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;
using System.Windows.Controls;


namespace CharlotteSchou_Booking.Views.BookingView
{
    /// <summary>
    /// Interaction logic for BookingOverview.xaml
    /// </summary>

    public partial class BookingOverview : Page
    {
    
        private BookingController _bc;
        public BookingOverview()
        {
            _bc = new();
            InitializeComponent();
            DataContext = _bc;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ShowBookingBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateBooking UBooking = new(_bc);
            NavigationService.Navigate(UBooking);
        }

        private void ShowBookingBtnChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookingOverviewListBox.SelectedItem != null)
            {
                ShowBookingBtn.IsEnabled = true;
            }
            else
            {
                ShowBookingBtn.IsEnabled = false;
            }
        }
    }
}
