using CharlotteSchou_Booking.Models;
using CharlotteSchou_Booking.Persistens;
using CharlotteSchou_Booking.Viewmodel;
using CharlotteSchou_Booking.Viewmodels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;

namespace CharlotteSchou_Booking.Model.Persistens
{
    public class ArtistRepo : Repo<Artist>
    {
        private List<Artist> artists = new List<Artist>();
     


        public ArtistRepo()
        {
            // refers to the connectionstring in the apsettings.json file
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");
            InitializeRepository();

        }

        //Establishes connection to the database
        public override void InitializeRepository()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand ReadAcmd = new SqlCommand("SELECT ArtistId, SocialSecurityNumber, Name, Address, City, Email, Alias, PhoneNumber, ZipCode, Status, Active from ARTIST", con);
                using (SqlDataReader dr = ReadAcmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Status statusresult;
                        if (bool.Parse(dr["Status"].ToString()) != true) { statusresult = Status.Primær; }
                        else statusresult = Status.Sekundær;

                        Active aactiveresult;
                        if (bool.Parse(dr["Active"].ToString()) != true) { aactiveresult = Active.Aktiv; }
                        else aactiveresult = Active.Inaktiv;
                        
                        Artist artist = new Artist()
                        {
                            ArtistId = int.Parse(dr["ArtistId"].ToString()),
                            SSN = float.Parse(dr["SocialSecurityNumber"].ToString()),
                            Name = dr["Name"].ToString(),
                            Address = dr["Address"].ToString(),
                            City = dr["City"].ToString(),
                            Email = dr["Email"].ToString(),
                            Alias = dr["Alias"].ToString(),
                            Phone = int.Parse(dr["PhoneNumber"].ToString()),
                            ZipCode = int.Parse(dr["Zipcode"].ToString()),
                            Status = statusresult,
                            Active = aactiveresult
                        };
                        artists.Add(artist);
                    }
                }
                foreach (Artist artist in artists)
                {
                    SqlCommand ReadAAcmd = new SqlCommand("SELECT ATTRIBUTE.AttributeId, Characteristics, Services FROM ATTRIBUTE " +
                        "JOIN ARTIST_ATTRIBUTE ON ATTRIBUTE.AttributeId = ARTIST_ATTRIBUTE.AttributeId " +
                        "WHERE ARTIST_ATTRIBUTE.ArtistId = @ArtistId;", con);
                    ReadAAcmd.Parameters.Add("@ArtistId", SqlDbType.Float).Value = artist.ArtistId;
                    using (SqlDataReader dr = ReadAAcmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Attribute attribute = new()
                            {
                                AttributeId = int.Parse(dr["AttributeId"].ToString()),
                                Characteristics = dr["Characteristics"].ToString(),
                                Service = dr["Services"].ToString(),
                            };
                            artist.Attributes.Add(attribute);
                        }
                    }
                    SqlCommand ReadAJcmd = new SqlCommand("SELECT JOBFUNCTION.JobfunctionId, Name, Description FROM JOBFUNCTION " +
                        "JOIN ARTIST_JOBFUNCTION ON JOBFUNCTION.JobfunctionId = ARTIST_JOBFUNCTION.JobfunctionId " +
                        "WHERE ARTIST_JOBFUNCTION.ArtistID = @ArtistId;", con);
                    ReadAJcmd.Parameters.Add("@ArtistId", SqlDbType.Float).Value = artist.ArtistId;
                    using (SqlDataReader dr = ReadAJcmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                          
                            Jobfunction jobfunction = new Jobfunction()
                            {
                                JobfunctionId = int.Parse(dr["JobfunctionId"].ToString()),
                                Name = dr["Name"].ToString(),
                                Description = dr["Description"].ToString(),
                            };
                            artist.Jobfunctions.Add(jobfunction);
                        }
                    }
                }
            }
        }

        //creates a new Artist object in the database
        public Artist Create(string name, string address, string city, string email, string alias, int phone, double ssn, int zipCode, Status status, Active aactive)
        {
            Artist artist = new Artist(name, address, city, email, alias, phone, ssn, zipCode, status, aactive);
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open(); SqlCommand cmd = new SqlCommand("INSERT INTO ARTIST (SocialSecurityNumber, Name, Address, City, Email, Alias, PhoneNumber, ZipCode, Status, Active)"
                    + "VALUES(@SocialSecurityNumber, @Name, @Address, @City, @Email, @Alias, @PhoneNumber, @ZipCode, @Status, @Active)" + "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@SocialSecurityNumber", SqlDbType.Float).Value = ssn;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = address;
                cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = city;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                cmd.Parameters.Add("@Alias", SqlDbType.NVarChar).Value = alias;
                cmd.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = phone;
                cmd.Parameters.Add("@ZipCode", SqlDbType.Int).Value = zipCode;
                cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = status;
                cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = aactive;

                artist.ArtistId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            
            artists.Add(artist);
            return artist;
        }

        //returns the list of all artists
        public override List<Artist> LoadAll()
        {
            return artists;
        }

        //returns artist with a specific artistId
        public override Artist Load(int artistId)
        {
            Artist result = new();
            foreach(Artist a in artists)
            {
                if (a.ArtistId == artistId)
                {
                    result = a;
                    break;
                }else { result = null; }
            }
            return result;
        }

        //deletes an Artist object from the database
        public override void Remove(int artistiId)
        {
            Artist result = new();
            foreach (Artist a in artists) 
            { 
            if (a.ArtistId == artistiId)
                {
                    result = a;
                    break;
                }
            
            }

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                //We use BEGIN TRANSACTION to do more sql statements in one command.
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" +
                    "DELETE FROM BOOKING_LOCATION " +
                    "WHERE BookingId IN (" +
                    "SELECT BOOKING_LOCATION.BookingId " +
                    "FROM BOOKING_LOCATION " +
                    "JOIN BOOKING ON BOOKING.BookingId = BOOKING_LOCATION.BookingId " +
                    "WHERE BOOKING.ArtistId = @ArtistId); " +

                    "DELETE FROM CUSTOMER " +
                    "WHERE BookingId IN (" +
                    "SELECT CUSTOMER.BookingId " +
                    "FROM CUSTOMER " +
                    "JOIN BOOKING ON BOOKING.BookingId = CUSTOMER.BookingId " +
                    "WHERE BOOKING.ArtistId = @ArtistId); " +

                    "DELETE FROM BOOKING WHERE BOOKING.ArtistId = @ArtistId; " +
                    "DELETE FROM ARTIST_JOBFUNCTION " +
                    "WHERE ARTIST_JOBFUNCTION.ArtistId = @ArtistId; " +
                    "DELETE FROM ARTIST_ATTRIBUTE " +
                    "WHERE ArtistId = @ArtistId; " +
                    "DELETE FROM ARTIST " +
                    "WHERE ArtistId = @ArtistId;" +
                    "COMMIT", con);


                cmd.Parameters.Add("@ArtistId", SqlDbType.Float).Value = result.ArtistId;
                cmd.ExecuteNonQuery();
            }
            
            artists.Remove(result);

            
        }


        //updates one or more attributes for an Artist object in the database
        public void Update(int artistId, string name, string address, string city, string email, string alias, int phoneNumber, int zipCode, Status status, Active active)
        {
            foreach (Artist a in artists)
            {
                if (a.ArtistId == artistId)
                {
                    if (a.Name != name) { a.Name = name; }
                    if (a.Address != address) { a.Address = address; }
                    if (a.City != city) { a.City = city; }
                    if (a.Email != email) { a.Email = email; }
                    if (a.Alias != alias) { a.Alias = alias; }
                    if (a.Phone != phoneNumber) { a.Phone = phoneNumber; }
                    if (a.ZipCode != zipCode) { a.ZipCode = zipCode; }
                    if (a.Status != status) { a.Status = status; }
                    if (a.Active != active) { a.Active = active; }

                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("UPDATE artist SET Name = @Name, Address = @Address, City = @City, " +
                                "Email = @Email, Alias = @Alias, PhoneNumber = @PhoneNumber, ZipCode = @ZipCode, Status = @Status, " +
                                "Active = @Active WHERE ArtistId = @ArtistId",  con);
                            cmd.Parameters.Add("@ArtistId", SqlDbType.Int).Value = artistId;
                            cmd.Parameters.Add("@SocialSecurityNumber", SqlDbType.Float).Value = a.SSN;
                            cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = address;
                            cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = city;
                            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                            cmd.Parameters.Add("@Alias", SqlDbType.NVarChar).Value = alias;
                            cmd.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = phoneNumber;
                            cmd.Parameters.Add("@ZipCode", SqlDbType.Int).Value = zipCode;
                            cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = status;
                            cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = active;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        //updates a Jobfunction for an Artist object in the database
        public void Update(Artist artist, Jobfunction jobfunction)
        {
      
            if (!artist.Jobfunctions.Any(jf => jf.JobfunctionId == jobfunction.JobfunctionId) )
            {
                artist.Jobfunctions.Add(jobfunction);
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open(); SqlCommand cmd = new SqlCommand("INSERT INTO ARTIST_JOBFUNCTION (ArtistId, JobfunctionId)"
                        + "VALUES(@ArtistId, @JobfunctionId)" + "SELECT @@IDENTITY", con);
                    cmd.Parameters.Add("@artistId", SqlDbType.Int).Value = artist.ArtistId;
                    cmd.Parameters.Add("@JobfunctionId", SqlDbType.Int).Value = jobfunction.JobfunctionId;
                    cmd.ExecuteNonQuery();
                }
            }
               
        }
        //updates an attribute for an Artist object
        public void Update(Artist artist, Attribute attribute)
        {
    
         
            if(!artist.Attributes.Any(at => at.AttributeId == attribute.AttributeId) )
            {
                artist.Attributes.Add(attribute);
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open(); SqlCommand cmd = new SqlCommand("INSERT INTO ARTIST_ATTRIBUTE (ArtistId, AttributeId)"
                        + "VALUES(@ArtistId, @AttributeId)" + "SELECT @@IDENTITY", con);
                    cmd.Parameters.Add("@artistId", SqlDbType.Int).Value = artist.ArtistId;
                    cmd.Parameters.Add("@AttributeId", SqlDbType.Int).Value = attribute.AttributeId;
                    cmd.ExecuteNonQuery();
                }
            }
            
        }
    }
}
