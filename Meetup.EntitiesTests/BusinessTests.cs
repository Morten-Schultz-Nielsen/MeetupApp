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
    public class BusinessTests
    {
        /// <summary>
        /// Creates a simple <see cref="Business"/> object
        /// </summary>
        /// <param name="id">the id the business should have</param>
        /// <returns>A simple <see cref="Business"/></returns>
        public static Business GetSimpleBusiness(int id = 0)
        {
            return new Business("My Business") { Id = id };
        }

        [TestMethod()]
        public void GetSimpleTest()
        {
            GetSimpleBusiness();
        }

        [TestMethod()]
        public void BusinessTest()
        {
            GetSimpleBusiness();

            //Test name
            Assert.ThrowsException<ArgumentException>(() => { new Business("[Business]"); }, "Invalid business name");
            Assert.ThrowsException<ArgumentException>(() => { new Business(null); }, "Name cannot be empty");

            //Test if list can be null
            Assert.ThrowsException<ArgumentNullException>(() => { new Business("business") { UsersBusinesses = null }; }, "UsersBusinesses cannot be null");
        }
    }
}