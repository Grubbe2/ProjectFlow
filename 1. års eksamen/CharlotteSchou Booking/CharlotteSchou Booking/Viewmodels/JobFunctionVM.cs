using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Model.Persistens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharlotteSchou_Booking.Viewmodels
{
    public class JobfunctionVM
    {
        private Jobfunction _jobfunction;
        private string _name;
        private string _description;

        private int _jobfunctionId;

        public int JobfunctionId
        {
            get { return _jobfunctionId; }
            set { _jobfunctionId = value; }
        }

        public string Name 
        { 
        get
            { return _name; } 
        set 
            { _name = value; } 
        }
        public string Description 
        {
        get 
            { return _description; } 
        set { _description = value; } 
        }
        public JobfunctionVM(Jobfunction jobfunction)
        {
            _jobfunction = jobfunction;
            _jobfunctionId = jobfunction.JobfunctionId;
            _name = jobfunction.Name;
            _description = jobfunction.Description;

        } 
        
        

    }
}
