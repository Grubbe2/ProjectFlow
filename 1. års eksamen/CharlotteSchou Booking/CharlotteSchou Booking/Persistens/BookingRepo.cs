using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Model.Persistens;
using CharlotteSchou_Booking.Models;
using CharlotteSchou_Booking.Viewmodel;
using CharlotteSchou_Booking.Viewmodels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace CharlotteSchou_Booking.Persistens
{
    public class BookingRepo : Repo<Booking>
    {
        private List <Booking> bookings = new List<Booking>();

        public BookingRepo()
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
                SqlCommand ReadAcmd = new SqlCommand("SELECT BookingId, Date, Time, Price, EventType, CustomerType, Comment from BOOKING", con);
                using (SqlDataReader dr = ReadAcmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CustomerType customerType;

                        if (bool.Parse(dr["CustomerType"].ToString()) == false) { customerType = CustomerType.Private; }
                        else customerType = CustomerType.Business;
                        DateTime date = DateTime.Parse(dr["Date"].ToString());
                        DateTime time = DateTime.Parse(dr["Time"].ToString());
                       Booking booking = new Booking()
                        {
                            BookingId = int.Parse(dr["BookingId"].ToString()),
                            Date = new DateOnly(date.Year, date.Month, date.Day),
                            Time = new TimeOnly(time.Hour, time.Minute),
                            Price = decimal.Parse(dr["Price"].ToString()),
                            EventType = dr["EventType"].ToString(),
                            CustomerType = customerType,
                            Comment = dr["Comment"].ToString(),
                            
                        };

                          bookings.Add(booking);
                    }
                }
                foreach (var booking in bookings)
                {
                    SqlCommand ReadACcmd = new SqlCommand("SELECT ARTIST.ArtistId, SocialSecurityNumber, Name, Address, City, Email, Alias, PhoneNumber, ZipCode, Status, Active " +
                        "FROM ARTIST " +
                        "JOIN BOOKING ON ARTIST.ArtistId = BOOKING.ArtistId " +
                        "WHERE BOOKING.BookingId = @BookingId;", con);
                    ReadACcmd.Parameters.Add("@BookingId", SqlDbType.Int).Value = booking.BookingId;
                    using (SqlDataReader dr = ReadACcmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Status statusresult;
                            if (bool.Parse(dr["Status"].ToString()) == true) { statusresult = Status.Primær; }
                            else statusresult = Status.Sekundær;

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
                                Active = Active.Aktiv
                            };
                            booking.Artist = artist;
                        }
                    }
                }

                foreach (Booking booking in bookings)
                {
                    SqlCommand ReadABcmd = new SqlCommand("SELECT LOCATION.LocationId, Address, City, Zipcode " +
                        "FROM LOCATION " +
                        "JOIN BOOKING_LOCATION ON LOCATION.LocationId = BOOKING_LOCATION.LocationId " +
                        "WHERE BOOKING_LOCATION.BookingId = @BookingId;", con);
                    ReadABcmd.Parameters.Add("@BookingId", System.Data.SqlDbType.Float).Value = booking.BookingId;
                    using (SqlDataReader dr = ReadABcmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Location location = new()
                            {
                                LocationId = int.Parse(dr["LocationId"].ToString()),
                                Address = dr["Address"].ToString(),
                                City = dr["City"].ToString(),
                                Zipcode = int.Parse(dr["Zipcode"].ToString()),


                            };
                            booking.Locations.Add(location);
                        }
                    }
                }
            }
        }

        public Booking Create(DateOnly date, TimeOnly time, decimal price, string eventType, CustomerType customerType, string comment, Artist artist, Location location)
        {
            Booking booking = new Booking(date, time, price, eventType, customerType, comment, artist, location); 

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open(); SqlCommand cmd = new SqlCommand("Insert INTO BOOKING (Date, Time, Price, EventType, CustomerType, Comment, ArtistId) " +
                    "VALUES(@Date, @Time, @Price, @EventType, @CustomerType, @Comment, @ArtistId)" + "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date;
                cmd.Parameters.Add("@Time", SqlDbType.Time).Value= time;
                cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = price;
                cmd.Parameters.Add("@EventType", SqlDbType.NVarChar).Value = eventType; 
                cmd.Parameters.Add("@CustomerType", SqlDbType.Bit).Value = customerType;
                cmd.Parameters.Add("@Comment", SqlDbType.NVarChar).Value= comment;
                cmd.Parameters.Add("@ArtistId", SqlDbType.Int).Value = artist.ArtistId;
                booking.BookingId = Convert.ToInt32(cmd.ExecuteScalar());
            }
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open(); SqlCommand cmd = new SqlCommand("Insert INTO BOOKING_LOCATION (BookingId, LocationId) " +
                    "VALUES(@BookingId, @LocationId)" + "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@BookingId", SqlDbType.Int).Value = booking.BookingId;
                cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = location.LocationId;
                cmd.ExecuteNonQuery();
            }
            bookings.Add(booking);
            return booking;
        }

        //loads a Booking object with a specific bookingId
        public override Booking Load(int bookingId)
        {
            Booking result = new();
            foreach(Booking b in bookings)
            {
                if (b.BookingId == bookingId)
                { result = b; 
                    break; 
                } else { result = null; }

            }return result;

        }

        //returns the list of all Booking objects
        public override List<Booking> LoadAll()
        {
            return bookings;
        }

        //removes a Booking object from the database
        public override void Remove(int bookingId)
        {
            Booking result = new();
            foreach (Booking b in bookings)
                if (b.BookingId == bookingId) 
                { result = b; 
                    break; 
                }

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                //We use BEGIN TRANSACTION to do more sql statements in one command.
                SqlCommand cmd = new SqlCommand("BEGIN TRANSACTION;" +
                    "DELETE FROM BOOKING_LOCATION " +
                    "WHERE BOOKING_LOCATION.BookingId = @BookingId; " +

                    "DELETE FROM LOCATION " +
                    "WHERE LocationId = @LocationId; " +

                    "DELETE FROM CUSTOMER " +
                    "WHERE CUSTOMER.BookingId = @BookingId " +

                    "DELETE FROM BOOKING WHERE BOOKING.BookingId = @BookingId;" +
                    "COMMIT; ", con);
                cmd.Parameters.Add("@BookingId", SqlDbType.Int).Value = result.BookingId;
                cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = result.Locations[0].LocationId;
                cmd.ExecuteNonQuery();
            }

            bookings.Remove(result);
        }

        public void Update(int bookingId, decimal price, string comment, CustomerType customerType)
        {
            foreach (Booking b in bookings)
            {
                if (b.BookingId == bookingId)
                {
                    if (b.Price != price) { b.Price = price; }
                    if (b.Comment != comment) { b.Comment = comment; }
                    if (b.CustomerType != customerType) { b.CustomerType = customerType; }


                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE booking SET Price = @Price, Comment = @Comment, CustomerType = @CustomerType " +
                            "WHERE BookingId = @BookingId", con);
                        cmd.Parameters.Add("@BookingId", SqlDbType.Int).Value = b.BookingId;
                        cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = b.Price;
                        cmd.Parameters.Add("@Comment", SqlDbType.NVarChar).Value = b.Comment;
                        cmd.Parameters.Add("@CustomerType", SqlDbType.Bit).Value = customerType;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<Booking> GetAllArtistBookings(int artistId)
        {
            List<Booking> result = new();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand sqlCommand = new SqlCommand("SELECT BookingId, Date, Time, Price, EventType, CustomerType, Comment from BOOKING " +
                    "Where ArtistId = @ArtistId;", con);
                sqlCommand.Parameters.Add("@ArtistId", SqlDbType.Float).Value = artistId;

                using (SqlDataReader dr = sqlCommand.ExecuteReader())
                    while (dr.Read())
                        
                        {
                        CustomerType customerType;

                        if (bool.Parse(dr["CustomerType"].ToString()) == true) { customerType = CustomerType.Private; }
                        else customerType = CustomerType.Business;
                        DateTime date = DateTime.Parse(dr["Date"].ToString());
                        DateTime time = DateTime.Parse(dr["Time"].ToString());

                        Booking booking = new Booking()
                        {
                            BookingId = int.Parse(dr["BookingId"].ToString()),
                            Date = new DateOnly(date.Year, date.Month, date.Day),
                            Time = new TimeOnly(time.Hour, time.Minute),
                            Price = decimal.Parse(dr["Price"].ToString()),
                            EventType = dr["EventType"].ToString(),
                            CustomerType = customerType,
                            Comment = dr["Comment"].ToString()
                        };
                        result.Add(booking);
                    }
                    return result;
            }
        }
    }
}
