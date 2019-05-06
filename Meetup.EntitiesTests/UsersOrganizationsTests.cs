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
    public class UsersOrganizationsTests
    {
        [TestMethod()]
        public void UsersOrganizationsTest()
        {
            //Test hiring date
            UsersOrganizations usersOrganizations = new UsersOrganizations() { StartDate = new DateTime(2000, 10, 10) };
            Assert.ThrowsException<ArgumentException>(() => { usersOrganizations.EndDate = new DateTime(1999, 10, 10); }, "EndDate cannot be less than StartDate");
            usersOrganizations.EndDate = new DateTime(2001, 10, 10);
            Assert.ThrowsException<ArgumentException>(() => { usersOrganizations.StartDate = new DateTime(2002, 10, 10); }, "StartDate cannot be higher than EndDate");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            UsersOrganizations usersOrganizations = new UsersOrganizations() { Organization = new Organization() {Name = "Test" }, StartDate = new DateTime(2000, 10, 10) };
            Assert.AreEqual("Test: Ansættelsesdato 10-10-2000", usersOrganizations.ToString(), "ToString returned wrong string");
            usersOrganizations.EndDate = new DateTime(2001, 10, 10);
            Assert.AreEqual("Test: 10-10-2000 - 10-10-2001", usersOrganizations.ToString(), "ToString with 2 datetimes returned wrong string");
        }
    }
}