using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using CharlotteSchou_Booking.Model.Persistens;
using CharlotteSchou_Booking.Models;
using CharlotteSchou_Booking.Viewmodels;

namespace CharlotteSchou_Booking.Viewmodels
{
    public class CustomerVM
    {
        private BookingVM _bookingVM;
        private int _customerId;
        private string _name;
        private int _phoneNumber;
        private string _email;
        private int _contactPerson;
        

        public BookingVM BookingVM
        {
            get { return _bookingVM; }
            set { _bookingVM = value; }
        }
        public int CustomerId
        {
            get { return _customerId; }
            set { _customerId = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public int ContactPerson
        {
            get { return _contactPerson; }
            set { _contactPerson = value; }
        }

        

        public CustomerVM(Customer customer)
        {
            _bookingVM = new(customer.Booking);
            _customerId = customer.CustomerId;
            _name = customer.Name;
            _phoneNumber = customer.PhoneNumber;
            _email = customer.Email;
            _contactPerson = customer.ContactPerson;
        }
        
    }
}
