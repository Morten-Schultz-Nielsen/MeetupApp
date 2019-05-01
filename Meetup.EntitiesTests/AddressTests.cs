using Microsoft.VisualStudio.TestTools.UnitTesting;
using Meetup.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.Entities.Tests
{
    [TestClass()]
    public class AddressTests
    {
        [TestMethod()]
        public void AddressTest()
        {
            //test country name
            new Address() { Country = "Denmark" };
            Assert.ThrowsException<ArgumentException>(() => { new Address() { Country = "D3nm4rk" }; }, "Invalid country name");
            Assert.ThrowsException<ArgumentException>(() => { new Address() { Country = "" }; }, "Country cannot be empty");

            //Test city name
            new Address() { CityName = "Vejle" };
            Assert.ThrowsException<ArgumentException>(() => { new Address() { CityName = "Vejle The City" }; }, "Invalid city name");
            Assert.ThrowsException<ArgumentException>(() => { new Address() { CityName = "" }; }, "CityName cannot be empty");

            //Test Street name
            new Address() { StreetName = "My Street" };
            Assert.ThrowsException<ArgumentException>(() => { new Address() { StreetName = "My 2nd Street" }; }, "Invalid street name");
            Assert.ThrowsException<ArgumentException>(() => { new Address() { StreetName = "" }; }, "StreetName cannot be empty");

            //Test Street number
            new Address() { StreetNumber = "100ab32" };
            Assert.ThrowsException<ArgumentException>(() => { new Address() { StreetNumber = "One" }; }, "Invalid street number");
            Assert.ThrowsException<ArgumentException>(() => { new Address() { StreetNumber = "" }; }, "StreetNumber cannot be empty");

            //Test Street number
            new Address() { ZipCode = 1785 };
            Assert.ThrowsException<ArgumentException>(() => { new Address() { ZipCode = 23432 }; }, "Invalid zip code");

            //Test event list
            Assert.ThrowsException<ArgumentNullException>(() => { new Address() { Events = null }; }, "Events cannot be null");

            //Test user list
            Assert.ThrowsException<ArgumentNullException>(() => { new Address() { Users = null }; }, "Users cannot be null");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Address addres = new Address()
            {
                Country = "Denmark",
                CityName = "Vejle",
                StreetNumber = "1A",
                StreetName = "Street",
                ZipCode = 1000
            };
            Assert.AreEqual("Street 1A Vejle 1000 Denmark", addres.ToString(), "ToString gave wrong result");
        }
    }
}