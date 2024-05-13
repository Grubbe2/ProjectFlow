using CharlotteSchou_Booking.Persistens;
using CharlotteSchou_Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CharlotteSchou_Booking.Model.Persistens;
using CharlotteSchou_Booking.Viewmodel;
using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Viewmodels;

namespace UnitTest
{
    [TestClass]
    public class UnitTestBookingRepo
    {
        BookingRepo BookingRepo;
        ArtistRepo ArtistRepo;
        LocationRepo LocationRepo;
        ArtistController ArtistController;
        Booking booking1;
        Artist artist1;
        Location location1;
        DateOnly dateOnly;
        TimeOnly timeOnly;
        CustomerType customerType;

        [TestInitialize]
        public void Init()
        {
            BookingRepo = new BookingRepo();
            ArtistRepo = new ArtistRepo();
            LocationRepo = new LocationRepo();
            artist1 = new Artist();
            location1 = new Location();

        }

        [TestMethod]
        public void TestCreateBooking()
        {
            // Arange
            artist1 = ArtistRepo.Create("Jonas Grubbe", "Nørregade 1", "Odense", "jonasgrubbe@gmail.com", "Slemme Jonas", 12345678, 0101000101, 4220, Status.Sekundær, Active.Aktiv);
            location1 = LocationRepo.Create("midtergade 12", "Odense", 5000);
            booking1 = BookingRepo.Create(dateOnly, timeOnly, 5000, "MandeStrip", CustomerType.Private, "han skal være med på den værste", artist1, location1);
            

            //Act
            Booking bookingCreated = BookingRepo.Load(booking1.BookingId);

            // Assert
            Assert.IsNotNull(bookingCreated);
            Assert.AreEqual(dateOnly, bookingCreated.Date);
            Assert.AreEqual(timeOnly, bookingCreated.Time);
            Assert.AreEqual(5000, bookingCreated.Price);
            Assert.AreEqual("MandeStrip", bookingCreated.EventType);
            Assert.AreEqual(customerType, bookingCreated.CustomerType);
            Assert.AreEqual("han skal være med på den værste", bookingCreated.Comment);
            Assert.AreEqual(artist1, bookingCreated.Artist);
            Assert.AreEqual(location1, bookingCreated.Locations[0]);

            // Delete to avoid duplication in database
            BookingRepo.Remove(booking1.BookingId);

            // Delete to avoid duplication in database
            ArtistRepo.Remove(artist1.ArtistId);
        }

        [TestMethod]
        public void TestDeleteBooking()
        {
            // Arrange
            artist1 = ArtistRepo.Create("Jonas Grubbe", "Nørregade 1", "Odense", "jonasgrubbe@gmail.com", "Slemme Jonas", 12345678, 0101000101, 4220, Status.Sekundær, Active.Aktiv);
            location1 = LocationRepo.Create("midtergade 12", "Odense", 5000);
            booking1 = BookingRepo.Create(dateOnly, timeOnly, 5000, "MandeStrip", CustomerType.Private, "han skal være med på den værste", artist1, location1);
            Booking BookingCreated = booking1;

            // Act
            BookingRepo.Remove(booking1.BookingId);
            booking1 = BookingRepo.Load(BookingCreated.BookingId);

            // Assert
            Assert.IsNull(booking1);

            // Delete to avoid duplication in database
            ArtistRepo.Remove(artist1.ArtistId);
        }

        [TestMethod]
        public void TestUpdateBooking()
        {
            // Arrange
            artist1 = ArtistRepo.Create("Jonas Grubbe", "Nørregade 1", "Odense", "jonasgrubbe@gmail.com", "Slemme Jonas", 12345678, 0101000101, 4220, Status.Sekundær, Active.Aktiv);
            location1 = LocationRepo.Create("midtergade 12", "Odense", 5000);
            booking1 = BookingRepo.Create(dateOnly, timeOnly, 5000, "MandeStrip", CustomerType.Private, "han skal være med på den værste", artist1, location1);

            // Act
            BookingRepo.Update(booking1.BookingId, 10000, "Han skal have smukke blå øjne", CustomerType.Business);

            // Assert
            var updatedBooking = BookingRepo.Load(booking1.BookingId);

            Assert.IsNotNull(updatedBooking);
            Assert.AreEqual(10000, updatedBooking.Price);
            Assert.AreEqual("Han skal have smukke blå øjne", updatedBooking.Comment);
            Assert.AreEqual(CustomerType.Business, updatedBooking.CustomerType);

            // Delete to avoid duplication in database
            BookingRepo.Remove(booking1.BookingId);

            // Delete to avoid duplication in database
            ArtistRepo.Remove(artist1.ArtistId);
        }
    }
}
