using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharlotteSchou_Booking.Models
{
    public class Location
    {
        public int LocationId { get; set; } 
        
        public string Address { get; set; }
        public string City { get; set; }
        
        public int Zipcode { get; set; }
          



        public Location(string address, string town, int zipcode) 
        {
            Address = address;
            City = town;
            Zipcode = zipcode;
        }

        public Location()
        {
        }
    }
}
