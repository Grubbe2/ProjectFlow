using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Model.Persistens;
using CharlotteSchou_Booking.Models;
using CharlotteSchou_Booking.Viewmodels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace CharlotteSchou_Booking.Persistens
{
    public class CustomerRepo : Repo<Customer>
    {
        private List<Customer> customers = new();

        public CustomerRepo() 
        {
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
                SqlCommand cmd = new SqlCommand("SELECT CustomerId, Name, PhoneNumber, Email, ContactPerson from CUSTOMER", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Customer customer = new Customer()
                        {
                            CustomerId = int.Parse(dr["CustomerId"].ToString()),
                            Name = dr["Name"].ToString(),
                            PhoneNumber = int.Parse(dr["PhoneNumber"].ToString()),
                            Email = dr["Email"].ToString(),
                            ContactPerson = int.Parse(dr["ContactPerson"].ToString())
                            
                        };
                        customers.Add(customer);
                    }
                }
                foreach (var customer in customers)
                {
                    SqlCommand ReadABcmd = new SqlCommand("SELECT BOOKING.BookingId, Date, Time, Price, EventType, CustomerType, Comment " +
                        "FROM BOOKING " +
                        "JOIN CUSTOMER ON CUSTOMER.BookingId = BOOKING.BookingId " +
                        "WHERE CUSTOMER.CustomerId = @CustomerId;", con);
                    ReadABcmd.Parameters.Add("@CustomerId", System.Data.SqlDbType.Float).Value = customer.CustomerId;
                    using (SqlDataReader dr = ReadABcmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            CustomerType privateOrBusiness;

                            if (bool.Parse(dr["CustomerType"].ToString()) == true) { privateOrBusiness = CustomerType.Private; }
                            else privateOrBusiness = CustomerType.Business;
                            DateTime date = DateTime.Parse(dr["Date"].ToString());
                            DateTime time = DateTime.Parse(dr["Time"].ToString());
                            Booking booking = new Booking()
                            {
                                BookingId = int.Parse(dr["BookingId"].ToString()),
                                Date = new DateOnly(date.Year, date.Month, date.Day),
                                Time = new TimeOnly(time.Hour, time.Minute),
                                Price = decimal.Parse(dr["Price"].ToString()),
                                EventType = dr["EventType"].ToString(),
                                CustomerType = privateOrBusiness,
                                Comment = dr["Comment"].ToString(),

                            };
                            customer.Booking = booking;
                        }
                    }
                }
            }
        }

        public Customer Create(string name, int phoneNumber, string email, int contantPerson, Booking booking)
        {
            Customer customer = new Customer(name, phoneNumber, email, contantPerson, booking);
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open(); SqlCommand cmd = new SqlCommand("INSERT INTO CUSTOMER (Name, PhoneNumber, Email, ContactPerson, BookingId) "
                    + "VALUES(@Name, @PhoneNumber, @Email, @ContactPerson, @BookingId)" + "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = name;
                cmd.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = phoneNumber;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                cmd.Parameters.Add("@ContactPerson", SqlDbType.Int).Value = contantPerson;
                cmd.Parameters.Add("@BookingId", SqlDbType.Int).Value = booking.BookingId;
                customer.CustomerId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            customers.Add(customer);
            return customer;
        }

        public override Customer Load(int CustomerId)
        {
            Customer result = new();
            foreach(Customer c in customers)
            {
                if (result.CustomerId == CustomerId)
                {
                    result = c;
                    break;
                }
            }return result;
        }

        //returns a list of all customers
        public override List<Customer> LoadAll()
        {
            return customers;
        }

        //removes a customer with a specific CustomerId from the database
        public override void Remove(int CustomerId)
        {
            Customer result = new();
            foreach (Customer c in customers)
            {
                if (result.CustomerId == CustomerId)
                {
                    result = c;
                    break;
                }
            }
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM CUSTOMER WHERE CUSTOMERID = @CUSTOMERID", con);
                    cmd.Parameters.Add("CustomerId", SqlDbType.Int).Value = result.CustomerId;
                    cmd.ExecuteNonQuery();

                }
            }customers.Remove(result);
        }

        public void Update(int customerId, string name, int phoneNumber, string email, int contactPerson)
        {
            foreach (Customer c in customers)
            {
                if (c.CustomerId == customerId)
                {
                    if (c.Name != name) { c.Name = name; }
                    if (c.PhoneNumber != phoneNumber) { c.PhoneNumber = phoneNumber; }
                    if (c.Email != email) { c.Email = email; }
                    if (c.ContactPerson != contactPerson) { c.ContactPerson = contactPerson; }


                    using (SqlConnection con = new SqlConnection(ConnectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE customer SET Name = @Name, PhoneNumber = @PhoneNumber, Email = @Email, ContactPerson = @ContactPerson " +
                            "WHERE CustomerId = @CustomerId", con);
                        cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = c.CustomerId;
                        cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = c.Name;
                        cmd.Parameters.Add("@PhoneNumber", SqlDbType.Int).Value = c.PhoneNumber;
                        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = c.Email;
                        cmd.Parameters.Add("@ContactPerson", SqlDbType.Int).Value = c.ContactPerson;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
