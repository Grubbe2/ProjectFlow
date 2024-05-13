using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.View;
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

namespace CharlotteSchou_Booking.Views
{
    /// <summary>
    /// Interaction logic for UpdateWorkProfilePage.xaml
    /// </summary>
    public partial class UpdateWorkProfilePage : Page
    {
        private ArtistController _ac;
        ArtistProfilePage _app;
        public UpdateWorkProfilePage(ArtistProfilePage app, ArtistController ac)
        {
            _ac = ac;
            _app = app;
            InitializeComponent();
            DataContext= _ac;
        }

        private void AddJobfunction_Click(object sender, RoutedEventArgs e)
        {
            _ac.AddJobfunction();
        }

        private void DeleteJobfunction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddAttribute_Click(object sender, RoutedEventArgs e)
        {
            _ac.AddAttribute();
        }

        private void DeleteAttribute_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Donebtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(_app);
        }
    }
}
