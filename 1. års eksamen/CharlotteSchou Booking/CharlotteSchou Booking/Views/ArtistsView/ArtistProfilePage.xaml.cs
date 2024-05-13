using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Viewmodel;
using CharlotteSchou_Booking.Views;
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
    /// Interaction logic for ArtistProfilePage.xaml
    /// </summary>
    public partial class ArtistProfilePage : Page
    {

        private ArtistController _ac;
        private ArtistPage _ap; 
        public ArtistProfilePage(ArtistController ac, ArtistPage ap)
        {
            _ap = ap;
            _ac = ac;
            InitializeComponent();
            DataContext = this._ac;
            Statustxt.SelectedValue = _ac.SelectedArtistVM.Status.ToString();
            Aactivetxt.SelectedValue = _ac.SelectedArtistVM.Active.ToString();
        }


        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(_ap);
        }

        private void EditArtistBtn_Click(object sender, RoutedEventArgs e)
        {

            Func<string, int> checkforlength = x => x.Length;
            Status.TryParse(Statustxt.Text, out Status status);
            Active.TryParse(Aactivetxt.Text, out Active aactive);
            //if statement to confrim zipcode is 4 numbers, Phone is 8 numbers.   
            if (int.TryParse(Phonetxt.Text, out int phone) &&

                int.TryParse(ZipCodetxt.Text, out int zipCode) &&

                checkforlength(Phonetxt.Text) == 8 &&

                checkforlength(ZipCodetxt.Text) == 4)
            {
                _ac.Update(Nametxt.Text, Addresstxt.Text, Citytxt.Text, Emailtxt.Text, Aliastxt.Text, phone, zipCode, status, aactive);
                Errorlbl.Content = "Artist er blevet opdateret";

            }
            else { Errorlbl.Content = "Indtast gyldigt telefonnummer"; }
        }

        private void UpdateWorkprofileBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateWorkProfilePage uwpp = new UpdateWorkProfilePage(this, _ac);
            NavigationService.Navigate(uwpp);
        }

        //private void DeleteArtistBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    NavigationService.GoBack();
        //} 
    }
}
