using CharlotteSchou_Booking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml.Schema;

namespace CharlotteSchou_Booking.Models
{
 
    public enum CustomerType
    {
        Private,
        Business
    }

    public class Booking
    {
        private CustomerType _customerType;
        public List<Location> Locations {  get; set; }
        public Artist Artist { get; set; }
        public int BookingId { get; set;}
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public decimal Price { get; set; }
        public string EventType { get; set; }
        public CustomerType CustomerType { get; set; }
        public string Comment { get; set; }

        public Booking()
        {
            Locations = new();
        }

        public Booking(DateOnly date, TimeOnly time, decimal price, string eventType, CustomerType customerType, 
            string comment, Artist artist, Location location)
        {
            Locations = new(){ location };
            Artist = artist;
            Date = date;
            Time = time;
            Price = price;
            EventType = eventType;
            _customerType = customerType;
            Comment = comment;
        }
    }
}
