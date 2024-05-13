


using CharlotteSchou_Booking.Model;
using CharlotteSchou_Booking.Model.Persistens;
using CharlotteSchou_Booking.Persistens;
using System;
using System.Security.Cryptography;
using Attribute = CharlotteSchou_Booking.Model.Attribute;

namespace UnitTest
{
    [TestClass]
    public class UnitTestArtistRepo
    {
        ArtistRepo artistRepo;
        JobfunctionRepo jobfunctionRepo;
        AttributeRepo attributeRepo;
        Artist artist1;   

        [TestInitialize]
        public void Init()
        {
            // ARRANGE
            artistRepo = new ArtistRepo();
            jobfunctionRepo = new JobfunctionRepo();
            attributeRepo = new AttributeRepo();
            
        }



        [TestMethod]
        public void TestCreateArtist()
        {
            // Arrange
            artist1 = artistRepo.Create("Jonas Grubbe", "Nørregade 1", "Odense", "jonasgrubbe@gmail.com", "Slemme Jonas", 12345678, 0101000101, 4220, Status.Sekundær, Active.Aktiv);


            //Act
            Artist artistCreated = artistRepo.Load(artist1.ArtistId);

            // Assert 
            Assert.IsNotNull(artistCreated);
            Assert.AreEqual("Jonas Grubbe", artistCreated.Name);
            Assert.AreEqual("Nørregade 1", artistCreated.Address);
            Assert.AreEqual("Odense", artistCreated.City);
            Assert.AreEqual("jonasgrubbe@gmail.com", artistCreated.Email);
            Assert.AreEqual("Slemme Jonas", artistCreated.Alias);
            Assert.AreEqual(12345678, artistCreated.Phone);

            // Delete to avoid duplication in database
            artistRepo.Remove(artist1.ArtistId);
        }


        [TestMethod]
        public void TestDeleteArtist()
        {
            // ARRANGE
            artist1 = artistRepo.Create("Jonas Grubbe", "Nørregade 1", "Odense", "jonasgrubbe@gmail.com", "Slemme Jonas", 12345678, 0101000101, 4220, Status.Sekundær, Active.Aktiv);
            Artist ArtistCreated = artist1;

            // ACT 
            artistRepo.Remove(artist1.ArtistId);
            artist1 = artistRepo.Load(ArtistCreated.ArtistId);
            // ASSERT 
            Assert.IsNull(artist1);
        }

        [TestMethod]
        public void TestUpdateArtist()
        {
            // ARRANGE
            artist1 = artistRepo.Create("Jonas Grubbe", "Nørregade 1", "Odense", "jonasgrubbe@gmail.com", "Slemme Jonas", 12345678, 0101000101, 4220, Status.Sekundær, Active.Aktiv);

            // ACT 
            artistRepo.Update(artist1.ArtistId, "Jonathan Grubbe", "Nørregade 1", "Odense", "opdateret@gmail.com", "Slemme Jonathan", 12345678, 0101000101, Status.Sekundær, Active.Aktiv);

            // Assert 
            var updatedArtist = artistRepo.Load(artist1.ArtistId);

            Assert.IsNotNull(updatedArtist);
            Assert.AreEqual("Jonathan Grubbe", updatedArtist.Name);
            Assert.AreEqual("Nørregade 1", updatedArtist.Address);
            Assert.AreEqual("Odense", updatedArtist.City);
            Assert.AreEqual("opdateret@gmail.com", updatedArtist.Email);
            Assert.AreEqual("Slemme Jonathan", updatedArtist.Alias);
            Assert.AreEqual(12345678, updatedArtist.Phone);

            // Delete to avoid duplication in database
            artistRepo.Remove(artist1.ArtistId);
        }

        [TestMethod]
        public void TestAddJobfunction()
        {
            // Arrange
            artist1 = artistRepo.Create("Jonas Grubbe", "Nørregade 1", "Odense", "jonasgrubbe@gmail.com", "Slemme Jonas", 12345678, 0101000101, 4220, Status.Sekundær, Active.Aktiv);
            Jobfunction jfunc = jobfunctionRepo.Load(8);

            //Act
            artistRepo.Update(artist1, jfunc);

            // Assert 
            Assert.IsNotNull(artist1.Jobfunctions);
            Assert.AreEqual(artist1.Jobfunctions.Count, 1);
            Assert.AreEqual(artist1.Jobfunctions[0], jfunc); 
            

            // Delete to avoid duplication in database
            artistRepo.Remove(artist1.ArtistId);
        }

        [TestMethod]
        public void TestAddAttribbuteArtist()
        {
            // Arrange
            artist1 = artistRepo.Create("Jonas Grubbe", "Nørregade 1", "Odense", "jonasgrubbe@gmail.com", "Slemme Jonas", 12345678, 0101000101, 4220, Status.Sekundær, Active.Aktiv);
            Attribute att = attributeRepo.Load(8);

            //Act
            artistRepo.Update(artist1, att);

            // Assert 
            Assert.IsNotNull(artist1.Attributes);
            Assert.AreEqual(artist1.Attributes.Count, 1);
            Assert.AreEqual(artist1.Attributes[0], att);

            // Delete to avoid duplication in database
            artistRepo.Remove(artist1.ArtistId);
        }
    }
}