using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharlotteSchou_Booking.Models
{
    public class Customer
    {
        public Booking Booking { get; set; }
        public int CustomerId { get; set; } 
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ContactPerson { get; set; }
         
    
        public Customer(string name, int phoneNumber, string email, int contactPerson, Booking booking)    
        {
            Booking = booking;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            ContactPerson = contactPerson;
        }

        public Customer()
        {
            
        }
    }
}
