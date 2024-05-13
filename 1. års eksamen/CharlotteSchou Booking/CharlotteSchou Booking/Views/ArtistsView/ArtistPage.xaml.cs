using CharlotteSchou_Booking.Viewmodel;
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
    /// Interaction logic for ArtistPage.xaml
    /// </summary>
    public partial class ArtistPage : Page
    {
        private ArtistController ac;

        public ArtistPage()
        {
            ac = new();
            InitializeComponent();
            DataContext = ac;
        }

        private void CreateNewArtistBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateNewArtistPage cnap = new CreateNewArtistPage(ac);
            NavigationService.Navigate(cnap);
        }

        private void ViewArtistBtn_Click(object sender, RoutedEventArgs e)
        {
            ArtistProfilePage app = new ArtistProfilePage(ac, this);
            NavigationService.Navigate(app);
        }

        private void ViewArtistBtnChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewArtistBtn.IsEnabled = true;
        }

        private void BckBtn_Click(object sender, RoutedEventArgs e)
        {
            MainPage mp = new MainPage();
            
            NavigationService.Navigate(mp);
        }

        private void tb_SearchTermMW_GotFocus(object sender, RoutedEventArgs e)
        {
            tb_SearchTermMW.Text = String.Empty;
        }


    }
}
