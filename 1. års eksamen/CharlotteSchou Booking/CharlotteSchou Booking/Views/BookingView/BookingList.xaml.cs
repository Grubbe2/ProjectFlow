using CharlotteSchou_Booking.View;
using CharlotteSchou_Booking.Viewmodels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace CharlotteSchou_Booking.Views.BookingView
{
    /// <summary>
    /// Interaction logic for BookingList.xaml
    /// </summary>
    public partial class BookingList : Page
    {
       
        private BookingController _bc;
        
        private MainPage _mp;
        

        public BookingList(MainPage mp)
        {
            _mp = mp;
            _bc = new BookingController();
            InitializeComponent();
            DataContext = _bc;
            string[] comboboxDataSource = Enumerable.Range(0, 2 * 24).Select(min => DateTime.Today.AddMinutes(30 * min).ToString("H:mm", CultureInfo.InvariantCulture)).ToArray();
            for (int i = 0; i < comboboxDataSource.Length; i++)
            {
                TimeCBox.Items.Add(comboboxDataSource[i]);
            }
            PopUpCalendar.PlacementTarget = CalendarBtn;
        }

        

    
        private void CalendarBtn_Click(object sender, RoutedEventArgs e)
        {
            PopUpCalendar.IsOpen = true;
            PopUpCalendar.StaysOpen = false;
        }



        private void OKBooking_Click(object sender, RoutedEventArgs e)
        {
            CreateBooking cb = new CreateBooking(_bc, _mp);
            NavigationService.Navigate(cb);
            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void ViewOKBookingBtnChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Listboxbooking.SelectedItem != null &&
                EventTypeComboBox.SelectedItem != null &&
                AttributeComboBox.SelectedItem != null &&
                PopUpCalendar != null &&
                TimeCBox.SelectedItem != null)   
            {
                OKBookingBtn.IsEnabled = true;
            }
            else
            {
                OKBookingBtn.IsEnabled = false;
            }
        }
    }
}
