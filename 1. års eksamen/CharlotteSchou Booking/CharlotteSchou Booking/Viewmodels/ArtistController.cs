using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CharlotteSchou_Booking.Commands;
//using CharlotteSchou_Booking.Commands;
using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Model.Persistens;
using CharlotteSchou_Booking.Models;
using CharlotteSchou_Booking.Persistens;
using CharlotteSchou_Booking.Viewmodels;


namespace CharlotteSchou_Booking.Viewmodel
{
    public class ArtistController : INotifyPropertyChanged
    {

        private ArtistVM _artistVM;
        private JobfunctionVM _jobFunctionVM;
        private AttributeVM _attributeVM;
        private ArtistRepo _artistRepo;
        private JobfunctionRepo _jobfunctionRepo;
        private AttributeRepo _attributeRepo;
        

        public ObservableCollection<ArtistVM> ArtistsVM { get; set; }
        public List<AttributeVM> AttributesVM { get; set; }
        public List<JobfunctionVM> JobfunctionsVM { get; set; }
        public ObservableCollection<BookingVM> BookingsVM { get; set; }
        public List<string> StatusList { get; set; }
        public List<string> ActiveList { get; set; } 


        public ArtistController()
        {

            StatusList = new()
            {
                Status.Primær.ToString(),
                Status.Sekundær.ToString()
            };
            ActiveList = new()
            {
                Active.Aktiv.ToString(),
                Active.Inaktiv.ToString()
            };
            _artistRepo = new ArtistRepo();
            _jobfunctionRepo = new JobfunctionRepo();
            _attributeRepo = new AttributeRepo();

            
            ArtistsVM = new();
            foreach (var a in _artistRepo.LoadAll())
            {
                ArtistVM aVM = new(a);
                ArtistsVM.Add(aVM);
                foreach(var job in a.Jobfunctions)
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
            foreach (var a in _jobfunctionRepo.LoadAll())
            {
                JobfunctionVM jVM = new(a);
                JobfunctionsVM.Add(jVM);
            }

            AttributesVM = new();
            foreach (var a in _attributeRepo.LoadAll())
            {
                AttributeVM aVM = new(a);
                AttributesVM.Add(aVM);
            }
            
        }

        public ArtistVM SelectedArtistVM
        {
            get { return _artistVM; }
            set
            {
                if (_artistVM != value)
                {

                    _artistVM = value;
                    OnPropertyChanged("SelectedArtistVM");
                }
            }
        }

        public JobfunctionVM SelectedJobfunctionVM
        {
            get
            { return _jobFunctionVM; }
            set
            {
                _jobFunctionVM = value;
                
            }
        }

        public AttributeVM SelectedAttributeVM
        {
            get
            { return _attributeVM; }
            set
            {
                _attributeVM = value;
                
            }
        }

        //creates an Artist object locally and in the database
        public void Create(string name, string address, string city, string email, string alias, int phone, double ssn, int zipCode, Status status, Active aactive)
        {
            Artist artist = _artistRepo.Create(name, address, city, email, alias, phone, ssn, zipCode, status, aactive);
            ArtistVM aVM = new(artist);
            ArtistsVM.Add(aVM);
        }
        public ICommand DeleteArtistCommand { get; } = new DeleteArtistCommand();
        //removes an Artist locally and from the database
        public void Remove(ArtistVM avm)
        {
            _artistRepo.Remove(avm.ArtistId);
            ArtistsVM.Remove(avm);
        }

        //updates the selected Artist object in the database
        public void Update(string name, string address, string city, string email, string alias, int phone, int zipCode, Status status, Active aactive)
        {
            if (SelectedArtistVM.Name != name) { SelectedArtistVM.Name = name; }
            if (SelectedArtistVM.Address != address) { SelectedArtistVM.Address = address; }
            if (SelectedArtistVM.City != city) { SelectedArtistVM.City = city; }
            if (SelectedArtistVM.Email != email) { SelectedArtistVM.Email = email; }
            if (SelectedArtistVM.Alias != alias) { SelectedArtistVM.Alias = alias; }
            if (SelectedArtistVM.Phone != phone) { SelectedArtistVM.Phone = phone; }
            if (SelectedArtistVM.ZipCode != zipCode) { SelectedArtistVM.ZipCode = zipCode; }
            if (SelectedArtistVM.Status != status) { SelectedArtistVM.Status = status; }
            if (SelectedArtistVM.Active != aactive) { SelectedArtistVM.Active = aactive; }
            _artistRepo.Update(SelectedArtistVM.ArtistId, name, address, city, email, alias, phone, zipCode, status, aactive);
        }

      

        public void AddJobfunction()
        {
            if (SelectedArtistVM != null && SelectedJobfunctionVM != null)
            {
                SelectedArtistVM.JobfunctionsVM.Add(SelectedJobfunctionVM);
                _artistRepo.Update(_artistRepo.Load(SelectedArtistVM.ArtistId), _jobfunctionRepo.Load(SelectedJobfunctionVM.JobfunctionId));

            }
        }

        public void AddAttribute()
        {
            if (SelectedArtistVM != null && SelectedAttributeVM != null)
            {
                SelectedArtistVM.AttributesVM.Add(SelectedAttributeVM);
                _artistRepo.Update(_artistRepo.Load(SelectedArtistVM.ArtistId), _attributeRepo.Load(SelectedAttributeVM.AttributeId));

            }
        }

        // Private field for searchTermMW and SearchTermMW property with a normal getter and a setter to dynamically update the list as a search term is written
        private string searchTermMW = "Søg...";
        public string SearchTermMW
        {
            get { return searchTermMW; }
            set
            {
                searchTermMW = value;
                ArtistsVM.Clear();
                List<ArtistVM> tempArtistsList = new List<ArtistVM>();
                foreach (Artist artist in _artistRepo.LoadAll())
                {
                    // If an item in ItemRepository fits the search term add them to a temporary list
                    if (artist.Alias.ToLower().Contains(SearchTermMW.ToLower()))
                    {
                        tempArtistsList.Add(new ArtistVM(artist.Alias, artist.Name, artist.Phone, artist.Status));
                    }
                }


                 //Order the temporary list alphabetically
                tempArtistsList = tempArtistsList.OrderBy(artist => artist.Alias).ToList();

                // Populate ItemsVM with the alphabetically sorted list of ItemViewModels containing only ItemViewModels that fit the search term
                foreach (ArtistVM artistVM in tempArtistsList)
                {

                    ArtistsVM.Add(artistVM);
                }
                tempArtistsList.Clear();

                OnPropertyChanged("SearchTermMW");

            }
        }

        //event and method for INotfiyPropertyChanged interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
