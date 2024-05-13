
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharlotteSchou_Booking.Model
{
    public enum Status
    {
        Primær,
        Sekundær
    }

    public enum Active
    {
        Aktiv,
        Inaktiv
    }

    public class Artist
    {   

        public List<Attribute> Attributes;
        public List<Jobfunction> Jobfunctions;
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public double SSN { get; set; }
        public string Alias { get; set; }
        public int Phone { get; set; }
        public int ZipCode { get; set; }
        public Status Status { get; set; }
        public Active Active { get; set; }
       




        public Artist(string name, string address, string city, string email, string alias, int phone, double ssn, int zipCode, Status status, Active aactive )
        {
            Name = name;
            Address = address;
            City = city;
            Email = email; 
            Alias = alias;
            Phone = phone;
            SSN = ssn;
            ZipCode = zipCode;
            Status = status;
            Active = aactive;
            Jobfunctions = new();
            Attributes = new();
        }

        public Artist()
        {
            Jobfunctions = new();
            Attributes = new();
        }


        public override string ToString()
        {
            return $"{ArtistId},{Name},{City},{Email},{SSN},{Alias},{Phone}, {ZipCode}, {Status}, {Active}";
        }

    }
}
