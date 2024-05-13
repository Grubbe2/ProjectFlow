using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Models;
using CharlotteSchou_Booking.Persistens;
using CharlotteSchou_Booking.Viewmodels;


namespace CharlotteSchou_Booking.Viewmodels
{
    public class BookingVM
    {
        private int _bookingId;
        private DateOnly _date;
        private TimeOnly _time;
        private decimal _price;
        private string _eventType;
        private string _comment;
        private CustomerType _customerType;
        private List<LocationVM> _locations;
        private ArtistVM _artistVM;

        public ArtistVM ArtistVM { get => _artistVM; set => _artistVM = value; }
        public List<LocationVM> LocationsVM { get => _locations; set => _locations = value; }

        public int BookingId { get => _bookingId; set => _bookingId = value; }
        public DateOnly Date { get => _date; set => _date = value; }
        public TimeOnly Time { get => _time; set => _time = value; }
        public decimal Price { get => _price; set => _price = value; }
        public string EventType { get => _eventType; set => _eventType = value; }
        public CustomerType CustomerType { get => _customerType; set => _customerType = value; }
        public string Comment { get => _comment; set => _comment = value; }

        public BookingVM(Booking booking)
        {
            LocationsVM = new();
            this._artistVM = new(booking.Artist);
            LocationsVM = new();
            _bookingId = booking.BookingId;
            _date = booking.Date;
            _time = booking.Time;
            _price = booking.Price;
            _eventType = booking.EventType;
            _customerType = booking.CustomerType;
            _comment = booking.Comment;
        }
    }
}
