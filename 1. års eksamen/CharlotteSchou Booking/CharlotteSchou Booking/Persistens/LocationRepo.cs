using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Model.Persistens;
using CharlotteSchou_Booking.Models;
using CharlotteSchou_Booking.Viewmodels;
using CharlotteSchou_Booking.Views.BookingView;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharlotteSchou_Booking.Persistens
{
    public class LocationRepo : Repo<Location>
    {
        private List<Location> locations = new();

        public LocationRepo()
        {
            // refers to the connectionstring in the apsettings.json file
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");
            InitializeRepository();
        }

        //Establishes connection to the database
        public override void InitializeRepository()
        {
           using SqlConnection con = new SqlConnection(ConnectionString);
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT LocationId, Address, City, ZipCode FROM LOCATION", con );
                using (SqlDataReader dr =  cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Location location = new()
                        {
                            LocationId = int.Parse(dr["LocationId"].ToString()),
                            Address = dr["Address"].ToString(),
                            City = dr["City"].ToString(),
                            Zipcode = int.Parse(dr["ZipCode"].ToString()),
                            
                        };
                        locations.Add(location);

                    }
                }
               

            }
        }

        public Location Create(string address, string town, int zipcode)
        {
                Location location = new Location(address, town, zipcode);
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open(); SqlCommand cmd = new SqlCommand("INSERT INTO LOCATION (Address, City, Zipcode)"
                        + "VALUES(@Address, @City, @Zipcode)" + "SELECT @@IDENTITY", con);
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = address;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = town;
                    cmd.Parameters.Add("@ZipCode", SqlDbType.Int).Value = zipcode;
                    location.LocationId = Convert.ToInt32(cmd.ExecuteScalar());
                }
                locations.Add(location);
                return location;
        }


        public override Location Load(int LocationId)
        {
            Location result = new(); 
            foreach(Location l in locations)
            {
                if (l.LocationId == LocationId)
                {
                    result = l;
                    break;
                }
            }return result;
        }

        //returns the list of all locations
        public override List<Location> LoadAll()
        {
           return locations;
        }

        //removes a Location object from the database
        public override void Remove(int LocationId)
        {
            Location result = new(); 
            foreach(Location l in locations)
            {
                if (l.LocationId == LocationId)
                {
                    result = l;
                    break;
                }
            using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM LOCATION WHERE LOCATION = @LocationId", con);
                    cmd.Parameters.Add("LocationId", SqlDbType.Int).Value = LocationId; 
                    cmd.ExecuteNonQuery();
                }
            
            }locations.Remove(result);
        }

        public void Update(int locationId, string address, int zipCode, string city)
        {
            foreach (Location l in locations)
            {
                if (l.LocationId == locationId)
                {
                    if (l.Address != address) { l.Address = address; }
                    if (l.Zipcode != zipCode) { l.Zipcode = zipCode; }
                    if (l.City != city) { l.City = city; }


                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE location SET Address = @Address, Zipcode = @Zipcode, City = @City " +
                            "WHERE LocationId = @LocationId", con);
                        cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = l.LocationId;
                        cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = l.Address;
                        cmd.Parameters.Add("@Zipcode", SqlDbType.Int).Value = l.Zipcode;
                        cmd.Parameters.Add("@City", SqlDbType.NVarChar).Value = l.City;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
