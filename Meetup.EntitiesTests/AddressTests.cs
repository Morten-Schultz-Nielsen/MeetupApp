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
        /// <summary>
        /// Creates an simple <see cref="Address"/> object
        /// </summary>
        /// <returns>An simple <see cref="Address"/></returns>
        public static Address GetSimpleAddress()
        {
            return new Address("Denmark", "Vejle", 1500, "My Street", "100ab32");
        }

        [TestMethod()]
        public void GetSimpleTest()
        {
            GetSimpleAddress();
        }

        [TestMethod()]
        public void AddressTest()
        {
            GetSimpleAddress();

            //test country name
            Assert.ThrowsException<ArgumentException>(() => { new Address("D3nm4rk", "Vejle", 1500, "My Street", "100ab32"); }, "Invalid country name");
            Assert.ThrowsException<ArgumentException>(() => { new Address("", "Vejle", 1500, "My Street", "100ab32"); }, "Country cannot be empty");

            //Test city name
            Assert.ThrowsException<ArgumentException>(() => { new Address("Denmark", "Vejle The City", 1500, "My Street", "100ab32"); }, "Invalid city name");
            Assert.ThrowsException<ArgumentException>(() => { new Address("Denmark", "", 1500, "My Street", "100ab32"); }, "CityName cannot be empty");

            //Test Street name
            Assert.ThrowsException<ArgumentException>(() => { new Address("Denmark", "Vejle", 1500, "My 2nd Street", "100ab32"); }, "Invalid street name");
            Assert.ThrowsException<ArgumentException>(() => { new Address("Denmark", "Vejle", 1500, "", "100ab32"); }, "StreetName cannot be empty");

            //Test Street number
            Assert.ThrowsException<ArgumentException>(() => { new Address("Denmark", "Vejle", 1500, "My Street", "One"); }, "Invalid street number");
            Assert.ThrowsException<ArgumentException>(() => { new Address("Denmark", "Vejle", 1500, "My Street", ""); }, "StreetNumber cannot be empty");

            //Test Street number
            Assert.ThrowsException<ArgumentException>(() => { new Address("Denmark", "Vejle", 234435, "My Street", "100ab32"); }, "Invalid zip code");
            Assert.ThrowsException<ArgumentException>(() => { new Address("Denmark", "Vejle", 235, "My Street", "100ab32"); }, "Invalid zip code");

            //Test event list
            Assert.ThrowsException<ArgumentNullException>(() => { new Address("Denmark", "Vejle", 1500, "My Street", "100ab32") { Events = null }; }, "Events cannot be null");

            //Test user list
            Assert.ThrowsException<ArgumentNullException>(() => { new Address("Denmark", "Vejle", 1500, "My Street", "100ab32") { Users = null }; }, "Users cannot be null");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Address address = GetSimpleAddress();
            Assert.AreEqual("My Street 100ab32 Vejle 1500 Denmark", address.ToString(), "ToString gave wrong result");
        }
    }
}