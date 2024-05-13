using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Viewmodel;
using CharlotteSchou_Booking.Viewmodels;
using Microsoft.Identity.Client;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Interaction logic for CreateNewArtistPage.xaml
    /// </summary>
    public partial class CreateNewArtistPage : Page
    {
        private ArtistController _ac;
        

        public CreateNewArtistPage(ArtistController ac)
        {
            this._ac = ac;
            InitializeComponent();
            DataContext = _ac;
            Statustxt.SelectedValue = Status.Primær.ToString();
            Aactivetxt.SelectedValue = Active.Aktiv.ToString();
        }

        private void CreateArtistBtn_Click(object sender, RoutedEventArgs e)
        {
            //if statement to confrim zipcode is 4 numbers, Phone is 8 numbers, and SSN is 10 numbers.
            Func<string, int> checkforlength = x => x.Length;
            Status.TryParse(Statustxt.Text, out Status status);
            Active.TryParse(Aactivetxt.Text, out Active aactive);
            if (int.TryParse(Phonetxt.Text, out int phone) && 
                float.TryParse(SSNtxt.Text, out float ssn) && 
                int.TryParse(ZipCodetxt.Text, out int zipCode) && 
                 
                checkforlength(Phonetxt.Text) == 8 && 
                checkforlength(SSNtxt.Text) == 10 &&
                checkforlength(ZipCodetxt.Text) == 4)
            {
                _ac.Create(Nametxt.Text, Adresstxt.Text, Citytxt.Text, Emailtxt.Text, Aliastxt.Text, phone, ssn, zipCode, status, aactive);
                Confirmlbl.Content = "Artist er oprettet korrekt"; 
                NavigationService.GoBack();
            }
            else { Confirmlbl.Content = "CPR Nr, Telefon Nr eller Post Nr er ikke indtastet korrekt"; }

        
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


        //Name
        private string nameTxt = "Navn";
        private void Nametxt_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == nameTxt)
            {
                tb.Text = string.Empty;
                tb.Foreground = Brushes.Black;
            }
        }

        private void Nametxt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;   
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = nameTxt;
                tb.Foreground = Brushes.Gray;
            }
        }

        //Alias
        private string aliasTxt = "Kaldenavn";
        private void Aliastxt_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == aliasTxt)
            {
                tb.Text = string.Empty;
                tb.Foreground = Brushes.Black;
            }
        }

        private void Aliastxt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = aliasTxt;
                tb.Foreground = Brushes.Gray;
            }
        }

        //Address
        private string addressTxt = "Adresse";
        private void Addresstxt_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == addressTxt)
            {
                tb.Text = string.Empty;
                tb.Foreground = Brushes.Black;
            }
        }

        private void Addresstxt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = addressTxt;
                tb.Foreground = Brushes.Gray;
            }
        }

        //City
        private string townTxt = "By";
        private void Towntxt_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == townTxt)
            {
                tb.Text = string.Empty;
                tb.Foreground = Brushes.Black;
            }
        }

        private void Towntxt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = townTxt;
                tb.Foreground = Brushes.Gray;
            }
        }

        //Email
        private string emailTxt = "Email";
        private void Emailtxt_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == emailTxt)
            {
                tb.Text = string.Empty;
                tb.Foreground = Brushes.Black;
            }
        }

        private void Emailtxt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = emailTxt;
                tb.Foreground = Brushes.Gray;
            }
        }

        //Phonenummer
        private string phoneNummerTxt = "Telefonnummer";
        private void PhoneNumbertxt_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == phoneNummerTxt)
            {
                tb.Text = string.Empty;
                tb.Foreground = Brushes.Black;
            }
        }

        private void PhoneNumbertxt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = phoneNummerTxt;
                tb.Foreground = Brushes.Gray;
            }
        }

        //SSN
        private string ssnTxt = "CPR-NR";
        private void SSNtxt_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == ssnTxt)
            {
                tb.Text = string.Empty;
                tb.Foreground = Brushes.Black;
            }
        }

        private void SSNtxt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = ssnTxt;
                tb.Foreground = Brushes.Gray;
            }
        }

        //Zipcode
        private string zipCodeTxt = "Postnummer";
        private void ZipCodetxt_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == zipCodeTxt)
            {
                tb.Text = string.Empty;
                tb.Foreground = Brushes.Black;
            }
        }

        private void ZipCodetxt_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Text = zipCodeTxt;
                tb.Foreground = Brushes.Gray;
            }
        }
    }
}
