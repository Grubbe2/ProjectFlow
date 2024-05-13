using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Models;
using CharlotteSchou_Booking.View;
using CharlotteSchou_Booking.Viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace CharlotteSchou_Booking.Views.BookingView
{
    /// <summary>
    /// Interaction logic for CreateBooking.xaml
    /// </summary>
    public partial class CreateBooking : Page
    {
        private BookingController _bc;
        private MainPage _mp;

        public CreateBooking(BookingController bc, MainPage mp)
        {
            _mp = mp;
            _bc = bc;
            InitializeComponent();
            DataContext = _bc;
        }

        private void CreateBookingBtn_Click(object sender, RoutedEventArgs e)
        {
         
            
            if (Addresstxt.Text.Length > 0 &&
               Towntxt.Text.Length > 0 &&
               Zipcodetxt.Text.Length > 0 &&
               Nametxt.Text.Length > 0 &&
               PhoneNumbertxt.Text.Length > 0)
            {
                if (PhoneNumbertxt.Text.Length != 8)
                {
                    Confirmlbl.Text = "Ugyldigt telefonnummer";
                }

                if (Zipcodetxt.Text.Length != 4)
                {
                    Confirmlbl.Text = "Ugyldigt postnummer";
                }
                if (ContactPersontxt.Text == "")
                {
                    ContactPersontxt.Text = PhoneNumbertxt.Text;
                }
                if (int.TryParse(PhoneNumbertxt.Text, out int phoneNumber) &&
                    int.TryParse(Zipcodetxt.Text, out int zipcode) &&
                    int.TryParse(ContactPersontxt.Text, out int contactPerson) &&
                    decimal.TryParse(Pricetxt.Text, out decimal price) &&
                    PhoneNumbertxt.Text.Length == 8 &&
                    Zipcodetxt.Text.Length == 4)
                {
                    Confirmlbl.Text = "";

                   
                    CreateBookingConfirmWindow confirmationWindow = new CreateBookingConfirmWindow();
                    bool? result = confirmationWindow.ShowDialog();

                    // Check the result of the confirmation dialog
                    if (result == true)
                    {
                        CustomerType.TryParse(DescriptionComboBox.Text, out CustomerType customerType);
                        // The user confirmed the booking, so create the booking
                        _bc.Create(Addresstxt.Text, zipcode, Towntxt.Text, Nametxt.Text, phoneNumber, 
                            Emailtxt.Text, contactPerson, price, Commenttxt.Text, customerType);
                        NavigationService.Navigate(_mp);
                    }
                }
            }
            else
            {
                Confirmlbl.Text = "Mangler informationer!";
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
           
            NavigationService.GoBack();
        }  
    }
}
