using CharlotteSchou_Booking.Commands;
using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Model.Persistens;
using CharlotteSchou_Booking.Models;
using CharlotteSchou_Booking.Persistens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Attribute = CharlotteSchou_Booking.Model.Attribute;

namespace CharlotteSchou_Booking.Viewmodels
{
    public class BookingController : INotifyPropertyChanged
    {
        private CustomerVM _customerVM;
        private ArtistVM _artistVM;
        private AttributeVM _attributeVM;
        private JobfunctionVM _jobfunctionVM;
        private JobfunctionRepo _jobfunctionRepo;
        private AttributeRepo _attributeRepo;
        private BookingRepo _bookingRepo;
        private LocationRepo _locationRepo;
        private CustomerRepo _customerRepo;
        private ArtistRepo _artistRepo;
        private DateTime _date;
        private TimeOnly _timeOnly;
        public event PropertyChangedEventHandler? PropertyChanged;


        public ICommand DeleteBookingCommand { get; } = new DeleteBookingCommand();
        public ObservableCollection<BookingVM> BookingsVM { get; set; }
        public ObservableCollection<LocationVM> LocationsVM { get; set; }
        public ObservableCollection<CustomerVM> CustomersVM { get; set; }
        public ObservableCollection<ArtistVM> ArtistsVM { get; set; }
        public ObservableCollection <JobfunctionVM> JobfunctionsVM { get; set; }  
        public ObservableCollection <AttributeVM> AttributesVM { get; set; }
        public List<string> CustomerTypeList { get; set; }
       

        public CustomerVM SelectedCustomerVM
        {
            get { return _customerVM; }
            set { _customerVM = value;
                OnPropertyChanged("SelectedBookingVM");
            }
        }
        public TimeOnly SelectedTime
        {
            get { return _timeOnly; }
            set { 
                _timeOnly = value;
                PerformSearch();
                OnPropertyChanged("SelectedTime");
            }
        }
        public DateTime SelectedDate
        {
            get { return _date; }
            set
            {
                _date = value;
                PerformSearch();
                OnPropertyChanged("SelectedDate");
            }
        }
        public AttributeVM SelectedAttributeVM
        {
            get { return _attributeVM; }
            set { _attributeVM = value;
                PerformSearch();
                OnPropertyChanged("SelectedAttributeVM");
            }
        }
        public JobfunctionVM SelectedJobfunctionVM
        {
            get { return _jobfunctionVM; }
            set
            {
                _jobfunctionVM = value;
                PerformSearch();
                OnPropertyChanged("SelectedJobfunctionVM");
            }
        }
        public ArtistVM SelectedArtistVM
        {
            get { return _artistVM; }
            set
            {
                _artistVM = value;
            }
        }


        public BookingController()
        {
            //PropertyChanged += BookingPropertyChanged;
            _bookingRepo = new BookingRepo();
            _locationRepo = new LocationRepo();
            _customerRepo = new CustomerRepo();
            _attributeRepo = new AttributeRepo();
            _jobfunctionRepo = new JobfunctionRepo();
            _artistRepo = new ArtistRepo();

            CustomerTypeList = new()
            {
                CustomerType.Private.ToString(),
                CustomerType.Business.ToString()
            };

            BookingsVM = new();
            foreach (Booking b in _bookingRepo.LoadAll())
            {
                BookingVM bVM = new(b);
                BookingsVM.Add(bVM);
            }

            CustomersVM = new();
            foreach (var customer in _customerRepo.LoadAll())
            {
                customer.Booking = _bookingRepo.Load(customer.Booking.BookingId);
                CustomerVM cVM = new(customer);
                foreach (var l in _bookingRepo.Load(customer.Booking.BookingId).Locations)
                {
                    LocationVM lVM = new(l);
                    cVM.BookingVM.LocationsVM.Add(lVM);
                }
                CustomersVM.Add(cVM);
            }

            ArtistsVM = new();
            foreach (var a in _artistRepo.LoadAll())
            {
                ArtistVM aVM = new(a);
                ArtistsVM.Add(aVM);
                foreach (var job in a.Jobfunctions)
                {
                    JobfunctionVM jVM = new(job);
                    aVM.JobfunctionsVM.Add(jVM);
                }
                foreach (var att in a.Attributes)
                {
                    AttributeVM attVM = new(att);
                    aVM.AttributesVM.Add(attVM);
                }
            }

            JobfunctionsVM = new();
            foreach (Jobfunction a in _jobfunctionRepo.LoadAll())
            {
                JobfunctionVM jVM = new(a);
                JobfunctionsVM.Add(jVM);
            }


            AttributesVM = new();
            foreach (Attribute a in _attributeRepo.LoadAll())

            {
                AttributeVM aVM = new(a);
                AttributesVM.Add(aVM);
            }
        }


        public void Create(string address, int zipCode, string city, string name, int phoneNumber, 
            string email, int contactPerson, decimal price, string comment, CustomerType customerType)
        {
            DateOnly dateOnly = new DateOnly(_date.Year, _date.Month, _date.Day);
            Location location = _locationRepo.Create(address, city, zipCode);
            Booking booking = _bookingRepo.Create(dateOnly, _timeOnly, price, SelectedJobfunctionVM.Name, 
                customerType, comment, _artistRepo.Load(SelectedArtistVM.ArtistId), location);
            Customer customer = _customerRepo.Create(name, phoneNumber, email, contactPerson, booking);
            LocationVM locationVM = new(location);
            CustomerVM customerVM = new(customer);
            BookingVM bVM = new(booking);
            bVM.LocationsVM.Add(locationVM);
            bVM.ArtistVM = SelectedArtistVM;
            customerVM.BookingVM = bVM;
            CustomersVM.Add(customerVM);
            BookingsVM.Add(bVM);
        }
        public void Remove(BookingVM bVM)
        {
            _bookingRepo.Remove(bVM.BookingId);
            BookingsVM.Remove(bVM);
            CustomersVM.Remove(SelectedCustomerVM);
        }
        public void Update(string address, int zipCode, string city, string name, int phoneNumber, string email, int contactPerson, decimal price, string comment, CustomerType customerType)
        {
            if (SelectedCustomerVM.Name != name) { SelectedCustomerVM.Name = name; }
            if (SelectedCustomerVM.PhoneNumber != phoneNumber) { SelectedCustomerVM.PhoneNumber = phoneNumber; }
            if (SelectedCustomerVM.Email != email) { SelectedCustomerVM.Email = email; }
            if (SelectedCustomerVM.ContactPerson != contactPerson) { SelectedCustomerVM.ContactPerson = contactPerson; }
            if (SelectedCustomerVM.BookingVM.Price != price) { SelectedCustomerVM.BookingVM.Price = price; }
            if (SelectedCustomerVM.BookingVM.CustomerType != customerType) { SelectedCustomerVM.BookingVM.CustomerType = customerType; }
            if (SelectedCustomerVM.BookingVM.Comment != comment) { SelectedCustomerVM.BookingVM.Comment = comment; }
            if (SelectedCustomerVM.BookingVM.LocationsVM[0].City != city) { SelectedCustomerVM.BookingVM.LocationsVM[0].City = city; }
            if (SelectedCustomerVM.BookingVM.LocationsVM[0].Address != address) { SelectedCustomerVM.BookingVM.LocationsVM[0].Address = address; }
            if (SelectedCustomerVM.BookingVM.LocationsVM[0].Zipcode != zipCode) { SelectedCustomerVM.BookingVM.LocationsVM[0].Zipcode = zipCode; }
            
            _bookingRepo.Update(SelectedCustomerVM.BookingVM.BookingId, price, comment, customerType);
            _locationRepo.Update(SelectedCustomerVM.BookingVM.LocationsVM[0].LocationId, address, zipCode, city);
            _customerRepo.Update(SelectedCustomerVM.CustomerId, name, phoneNumber, email, contactPerson);
        }
        private void PerformSearch()
        {
            ArtistsVM.Clear();
            List<ArtistVM> tempArtistsList = new List<ArtistVM>();
            List<DateTime> notAvailableDateTime = new();
            DateTime bookingDateTime;
            DateTime dateTime = new DateTime(_date.Year, _date.Month, _date.Day, _timeOnly.Hour, _timeOnly.Minute, _timeOnly.Second);
            foreach (Artist artist in _artistRepo.LoadAll())
            {
                notAvailableDateTime.Clear();
                //Used to get all booking that the specific artist has, and check if the dates overlap
                foreach (var booking in _bookingRepo.GetAllArtistBookings(artist.ArtistId))
                {
                    bookingDateTime = new(booking.Date.Year, booking.Date.Month, booking.Date.Day,
                                 booking.Time.Hour, booking.Time.Minute, booking.Time.Second);

                    notAvailableDateTime.Add(bookingDateTime);
                }

                // If an item in ItemRepository fits the search term add them to a temporary list
                if ((_jobfunctionVM == null || artist.Jobfunctions.Any(jf => jf.JobfunctionId == _jobfunctionVM.JobfunctionId)) &&
                    (_attributeVM == null || _attributeVM.AttributeId == 12 || artist.Attributes.Any(att => att.AttributeId == _attributeVM.AttributeId)) &&
                    (_date == null || (!notAvailableDateTime.Any(date => date == dateTime))))
                {
                    tempArtistsList.Add(new ArtistVM(artist));
                }

            }

            // Populate ItemsVM with the alphabetically sorted list of ItemViewModels containing only ItemViewModels that fit the search term
            foreach (ArtistVM artistVM in tempArtistsList)
            {
                ArtistsVM.Add(artistVM);
            }
            tempArtistsList.Clear();

        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
                
            }
        }
    }
}
