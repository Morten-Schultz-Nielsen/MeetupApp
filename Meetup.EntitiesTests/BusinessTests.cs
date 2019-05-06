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
        [TestMethod()]
        public void BusinessTest()
        {
            //Test name
            new Business("My Business");
            Assert.ThrowsException<ArgumentException>(() => { new Business("[Business]"); }, "Invalid business name");
            Assert.ThrowsException<ArgumentException>(() => { new Business(null); }, "Name cannot be empty");

            //Test if list can be null
            Assert.ThrowsException<ArgumentNullException>(() => { new Business("business") { UsersBusinesses = null }; }, "UsersBusinesses cannot be null");
        }
    }
}