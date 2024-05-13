using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Model.Persistens;
using CharlotteSchou_Booking.Viewmodels;

namespace CharlotteSchou_Booking.Viewmodels
{
    public class ArtistVM 
    {
        private int _artistId;
        private string _alias;
        private string _name;
        private int _phone;
        private string _email;
        private string _address;
        private string _city;
        private int _zipCode;
        private Status _status;
        private Active _active;
        

        public ArtistRepo ArtistRepo { get; } 
        public List<JobfunctionVM> JobfunctionsVM { get; set; }
        public List<AttributeVM> AttributesVM { get; set; }

       
        public int ArtistId
        {
            get { return _artistId; }
            set { _artistId = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }            
        }

        public int Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _city = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        
        public int ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; }
        }

        public Status Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public Active Active
        {
            get { return _active; }
            set { _active = value; }
        }

       

        public ArtistVM(Artist artist)
        {
            JobfunctionsVM = new();
            AttributesVM = new();
            _artistId = artist.ArtistId;
            _alias = artist.Alias;
            _name = artist.Name;
            _phone = artist.Phone;
            _email = artist.Email;
            _address = artist.Address;
            _city = artist.City;
            _zipCode = artist.ZipCode;
            _status = artist.Status;
            _active = artist.Active;
        }


        public ArtistVM(string alias, string name, int phone, Status status)
        {
            Alias = alias;
            Name = name;
            Phone = phone;
            Status = status;
        }
    }
}
