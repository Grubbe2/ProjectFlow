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
    public partial class UpdateBooking : Page
    {
        private BookingController _bc;
        

        public UpdateBooking(BookingController bc)
        {
            _bc = bc;
            InitializeComponent();
            DataContext = _bc;
            DescriptionComboBox.SelectedValue = _bc.SelectedCustomerVM.BookingVM.CustomerType.ToString();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
           
            NavigationService.GoBack();
        }

        private void UpdateBookingBtn_Click(object sender, RoutedEventArgs e)
        {
            Func<string, int> checkforlength = x => x.Length;
            if (checkforlength(Addresstxt.Text) > 0 &&
               checkforlength(Towntxt.Text) > 0 &&
               checkforlength(Zipcodetxt.Text) > 0 &&
               checkforlength(Nametxt.Text) > 0 &&
               checkforlength(PhoneNumbertxt.Text) > 0)
            {
                if (checkforlength(PhoneNumbertxt.Text) != 8)
                {
                    Confirmlbl.Text = "Ugyldigt telefonnummer";
                }

                if (checkforlength(Zipcodetxt.Text) != 4)
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
                    checkforlength(PhoneNumbertxt.Text) == 8 &&
                    checkforlength(Zipcodetxt.Text) == 4)
                {
                    Confirmlbl.Text = "";

                    // Check the result of the confirmation dialog
                    CustomerType.TryParse(DescriptionComboBox.Text, out CustomerType customerType);
                    //The user confirmed the booking, so create the booking
                    _bc.Update(Addresstxt.Text, zipcode, Towntxt.Text, Nametxt.Text, phoneNumber, Emailtxt.Text, contactPerson, price, Commenttxt.Text, customerType);
                    NavigationService.GoBack();

                }
            }
            else
            {
                Confirmlbl.Text = "Mangler informationer!";
            }
        }
    }
}
