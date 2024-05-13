using CharlotteSchou_Booking.Viewmodel;
using CharlotteSchou_Booking.Viewmodels;
using CharlotteSchou_Booking.Views.BookingView;
using System;
using System.Collections.Generic;
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

namespace CharlotteSchou_Booking.View
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        
        public MainPage()
        {
            InitializeComponent();
            
        }
        private void BookingBtn_Click(object sender, RoutedEventArgs e)
        {
            BookingList bl = new BookingList(this);
            NavigationService.Navigate(bl);
        }

        private void ArtistBtn_Click(object sender, RoutedEventArgs e)
        {
            ArtistPage ap = new ArtistPage();
            NavigationService.Navigate(ap);
        }
        private void BookingOverviewBtn_Click(object sender, RoutedEventArgs e)
        {
            BookingOverview bo = new BookingOverview();
            NavigationService.Navigate(bo);
        }
    }
}
