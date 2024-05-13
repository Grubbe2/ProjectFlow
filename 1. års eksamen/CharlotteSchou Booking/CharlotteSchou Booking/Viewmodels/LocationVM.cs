using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharlotteSchou_Booking.Model.Persistens;
using CharlotteSchou_Booking.Models;
using CharlotteSchou_Booking.Viewmodels;

namespace CharlotteSchou_Booking.Viewmodels
{
    public class LocationVM
    {
        private Location _location;
        private int _locationId;
        private string _address;
        private string _city;
        private int _zipcode;
        
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string City
        {
            get { return _city; }
            set {  _city = value; }
        }


        public int LocationId
        {
            get { return _locationId; }
            set { _locationId = value; }
        }
        public int Zipcode
        {
            get { return _zipcode; }
            set { _zipcode = value; }
        }

        public LocationVM(Location location)
        {
            this._location = location;
            _locationId = location.LocationId;
            _address = location.Address;
            _city = location.City;
            _zipcode = location.Zipcode;
        }
    }
}
