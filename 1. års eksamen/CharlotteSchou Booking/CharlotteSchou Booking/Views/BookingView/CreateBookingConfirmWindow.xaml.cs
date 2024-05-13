using System.Windows;


namespace CharlotteSchou_Booking.Views.BookingView
{
    public partial class CreateBookingConfirmWindow : Window
    {
        public CreateBookingConfirmWindow()
        {
            InitializeComponent();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}