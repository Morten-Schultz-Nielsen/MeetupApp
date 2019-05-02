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
    public class OrganizationTests
    {
        [TestMethod()]
        public void OrganizationTest()
        {
            //Test name
            new Organization() { Name = "My Organization" };
            Assert.ThrowsException<ArgumentException>(() => { new Organization() { Name = null }; }, "Name cannot be empty");

            //Test if list can be null
            Assert.ThrowsException<ArgumentNullException>(() => { new Organization() { UsersOrganizations = null }; }, "UsersOrganizations cannot be null");
        }

        [TestMethod()]
        public void NameExistsTest()
        {
            //Test API connection
            Organization.NameExists("LEGO Group");

            //Test API null parameter
            Assert.ThrowsException<ArgumentException>(() => { Organization.NameExists(null); });
        }
    }
}